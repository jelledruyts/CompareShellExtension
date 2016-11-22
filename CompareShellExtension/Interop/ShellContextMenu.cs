using CompareShellExtension.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

// Code based on Microsoft's All-In-One Code Framework sample at http://www.codeproject.com/Articles/174369/How-to-Write-Windows-Shell-Extension-with-NET-Lang
namespace CompareShellExtension.Interop
{
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("869D962A-EB9A-4227-B116-CB1E78454BBE"), ComVisible(true)]
    public class ShellContextMenu : IShellExtInit, IContextMenu
    {
        #region Fields

        private IList<string> selectedShellItems = new List<string>();
        private bool isExtendedContextMenu;
        private IList<ActiveCommand> activeCommands = new List<ActiveCommand>();

        #endregion

        #region Initialize

        /// <summary>
        /// Initialize the context menu handler.
        /// </summary>
        /// <param name="pidlFolder">A pointer to an ITEMIDLIST structure that uniquely identifies a folder.</param>
        /// <param name="pDataObj">A pointer to an IDataObject interface object that can be used to retrieve the objects being acted upon.</param>
        /// <param name="hKeyProgID">The registry key for the file object or folder type.</param>
        public void Initialize(IntPtr pidlFolder, IntPtr pDataObj, IntPtr hKeyProgID)
        {
            if (pDataObj == IntPtr.Zero)
            {
                throw new ArgumentException();
            }

            var fe = new FORMATETC();
            fe.cfFormat = (short)CLIPFORMAT.CF_HDROP;
            fe.ptd = IntPtr.Zero;
            fe.dwAspect = DVASPECT.DVASPECT_CONTENT;
            fe.lindex = -1;
            fe.tymed = TYMED.TYMED_HGLOBAL;
            var stm = new STGMEDIUM();

            // The pDataObj pointer contains the objects being acted upon. In this 
            // example, we get an HDROP handle for enumerating the selected files 
            // and folders.
            var dataObject = (IDataObject)Marshal.GetObjectForIUnknown(pDataObj);
            dataObject.GetData(ref fe, out stm);

            try
            {
                // Get an HDROP handle.
                IntPtr hDrop = stm.unionmember;
                if (hDrop == IntPtr.Zero)
                {
                    throw new ArgumentException();
                }

                // Determine how many files are involved in this operation.
                var numberOfItems = NativeMethods.DragQueryFile(hDrop, UInt32.MaxValue, null, 0);

                // Enumerate the selected files and folders.
                this.selectedShellItems.Clear();
                if (numberOfItems > 0)
                {
                    var fileName = new StringBuilder(260);
                    for (uint i = 0; i < numberOfItems; i++)
                    {
                        // Get the next file name.
                        if (0 != NativeMethods.DragQueryFile(hDrop, i, fileName, fileName.Capacity))
                        {
                            // Add the file name to the list.
                            this.selectedShellItems.Add(fileName.ToString());
                        }
                    }

                    // If we did not find any files we can work with, throw an exception.
                    if (this.selectedShellItems.Count == 0)
                    {
                        Marshal.ThrowExceptionForHR(WinError.E_FAIL);
                    }
                }
                else
                {
                    Marshal.ThrowExceptionForHR(WinError.E_FAIL);
                }
            }
            finally
            {
                NativeMethods.ReleaseStgMedium(ref stm);
            }
        }

        #endregion

        #region QueryContextMenu

        /// <summary>
        /// Add commands to a shortcut menu.
        /// </summary>
        /// <param name="hMenu">A handle to the shortcut menu.</param>
        /// <param name="iMenu">The zero-based position at which to insert the first new menu item.</param>
        /// <param name="idCmdFirst">The minimum value that the handler can specify for a menu item ID.</param>
        /// <param name="idCmdLast">The maximum value that the handler can specify for a menu item ID.</param>
        /// <param name="uFlags">Optional flags that specify how the shortcut menu can be changed.</param>
        /// <returns>
        /// If successful, returns an HRESULT value that has its severity value set 
        /// to SEVERITY_SUCCESS and its code value set to the offset of the largest 
        /// command identifier that was assigned, plus one.
        /// </returns>
        public int QueryContextMenu(IntPtr hMenu, uint iMenu, uint idCmdFirst, uint idCmdLast, uint uFlags)
        {
            // If uFlags include CMF_DEFAULTONLY then we should not do anything.
            if (((uint)CMF.CMF_DEFAULTONLY & uFlags) != 0)
            {
                return NativeHelpers.MakeHResult(WinError.SEVERITY_SUCCESS, 0, 0);
            }

            // Check if the user wants to see the extended context menu (i.e. pressing Shift while opening the context menu).
            this.isExtendedContextMenu = ((uint)CMF.CMF_EXTENDEDVERBS & uFlags) != 0;

            this.activeCommands.Clear();
            var maxItems = (idCmdLast - idCmdFirst) + 1 - 2 /* Subtract 2 for the separators */;
            if (maxItems > 0)
            {
                var menuIndex = iMenu;

                // Add a separator.
                var beginSeparator = new MENUITEMINFO();
                beginSeparator.cbSize = (uint)Marshal.SizeOf(beginSeparator);
                beginSeparator.fMask = MIIM.MIIM_TYPE;
                beginSeparator.fType = MFT.MFT_SEPARATOR;
                if (!NativeMethods.InsertMenuItem(hMenu, menuIndex++, true, ref beginSeparator))
                {
                    return Marshal.GetHRForLastWin32Error();
                }

                // Add the commands.
                var context = new CommandContext(this.selectedShellItems, this.isExtendedContextMenu);
                var commandOffset = default(uint);
                foreach (var command in CommandFactory.GetAvailableCommands())
                {
                    try
                    {
                        var commandState = command.GetState(context);
                        if (commandState != null && commandState.IsVisible)
                        {
                            var verb = Guid.NewGuid().ToString(); // Create a unique verb to be able to retrieve the right command later on based on that verb.
                            var activeCommand = new ActiveCommand(command, verb, commandOffset++);
                            this.activeCommands.Add(activeCommand);

                            var mii = new MENUITEMINFO();
                            mii.cbSize = (uint)Marshal.SizeOf(mii);
                            mii.fMask = MIIM.MIIM_BITMAP | MIIM.MIIM_STRING | MIIM.MIIM_FTYPE | MIIM.MIIM_ID | MIIM.MIIM_STATE;
                            mii.wID = idCmdFirst + activeCommand.Offset;
                            mii.fType = MFT.MFT_STRING;
                            mii.dwTypeData = commandState.MenuText;

                            if (!commandState.IsEnabled)
                            {
                                mii.fState = MFS.MFS_DISABLED;
                            }
                            else if (commandState.IsChecked)
                            {
                                mii.fState = MFS.MFS_CHECKED;
                            }
                            else
                            {
                                mii.fState = MFS.MFS_ENABLED;
                            }

                            if (!NativeMethods.InsertMenuItem(hMenu, menuIndex++, true, ref mii))
                            {
                                return Marshal.GetHRForLastWin32Error();
                            }

                            if (this.activeCommands.Count >= maxItems)
                            {
                                break;
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        Logger.LogError($"Error querying command \"{command.Name}\": {exc.ToString()}");
                    }
                }

                // Add a separator.
                var endSeparator = new MENUITEMINFO();
                endSeparator.cbSize = (uint)Marshal.SizeOf(endSeparator);
                endSeparator.fMask = MIIM.MIIM_TYPE;
                endSeparator.fType = MFT.MFT_SEPARATOR;
                if (!NativeMethods.InsertMenuItem(hMenu, menuIndex++, true, ref endSeparator))
                {
                    return Marshal.GetHRForLastWin32Error();
                }
            }

            // Return an HRESULT value with the severity set to SEVERITY_SUCCESS. 
            // Set the code value to the offset of the largest command identifier 
            // that was assigned, plus one (1).
            return NativeHelpers.MakeHResult(WinError.SEVERITY_SUCCESS, 0, (uint)this.activeCommands.Count);
        }

        #endregion

        #region GetCommandString

        /// <summary>
        /// Get information about a shortcut menu command, including the help string 
        /// and the language-independent, or canonical, name for the command.
        /// </summary>
        /// <param name="idCmd">Menu command identifier offset.</param>
        /// <param name="uFlags">
        /// Flags specifying the information to return. This parameter can have one 
        /// of the following values: GCS_HELPTEXTA, GCS_HELPTEXTW, GCS_VALIDATEA, 
        /// GCS_VALIDATEW, GCS_VERBA, GCS_VERBW.
        /// </param>
        /// <param name="pReserved">Reserved. Must be IntPtr.Zero</param>
        /// <param name="pszName">The address of the buffer to receive the null-terminated string being retrieved.</param>
        /// <param name="cchMax">Size of the buffer, in characters, to receive the null-terminated string.</param>
        public void GetCommandString(UIntPtr idCmd, uint uFlags, IntPtr pReserved, StringBuilder pszName, uint cchMax)
        {
            var offset = (int)idCmd.ToUInt32();
            var command = GetActiveCommand(offset);
            if (command != null)
            {
                var text = default(string);
                if ((GCS)uFlags == GCS.GCS_VERBW)
                {
                    text = command.Verb;
                }
                else if ((GCS)uFlags == GCS.GCS_HELPTEXTW)
                {
                    text = command.Command.HelpText;
                }

                if (text != null && text.Length > cchMax - 1)
                {
                    Marshal.ThrowExceptionForHR(WinError.STRSAFE_E_INSUFFICIENT_BUFFER);
                }
                else
                {
                    pszName.Clear();
                    if (text != null)
                    {
                        pszName.Append(text);
                    }
                }
            }
        }

        #endregion

        #region InvokeCommand

        /// <summary>
        /// Carry out the command associated with a shortcut menu item.
        /// </summary>
        /// <param name="pici">A pointer to a CMINVOKECOMMANDINFO or CMINVOKECOMMANDINFOEX structure containing information about the command.</param>
        public void InvokeCommand(IntPtr pici)
        {
            var isUnicode = false;

            // Determine which structure is being passed in, CMINVOKECOMMANDINFO or 
            // CMINVOKECOMMANDINFOEX based on the cbSize member of lpcmi. Although 
            // the lpcmi parameter is declared in Shlobj.h as a CMINVOKECOMMANDINFO 
            // structure, in practice it often points to a CMINVOKECOMMANDINFOEX 
            // structure. This struct is an extended version of CMINVOKECOMMANDINFO 
            // and has additional members that allow Unicode strings to be passed.
            var ici = (CMINVOKECOMMANDINFO)Marshal.PtrToStructure(pici, typeof(CMINVOKECOMMANDINFO));
            var iciex = new CMINVOKECOMMANDINFOEX();
            if (ici.cbSize == Marshal.SizeOf(typeof(CMINVOKECOMMANDINFOEX)))
            {
                if ((ici.fMask & CMIC.CMIC_MASK_UNICODE) != 0)
                {
                    isUnicode = true;
                    iciex = (CMINVOKECOMMANDINFOEX)Marshal.PtrToStructure(pici, typeof(CMINVOKECOMMANDINFOEX));
                }
            }

            // Determines whether the command is identified by its offset or verb.
            // There are two ways to identify commands:
            // 
            //   1) The command's verb string 
            //   2) The command's identifier offset
            // 
            // If the high-order word of lpcmi->lpVerb (for the ANSI case) or 
            // lpcmi->lpVerbW (for the Unicode case) is nonzero, lpVerb or lpVerbW 
            // holds a verb string. If the high-order word is zero, the command 
            // offset is in the low-order word of lpcmi->lpVerb.

            var activeCommand = default(ActiveCommand);
            if (!isUnicode && NativeHelpers.GetHighWord(ici.verb.ToInt32()) != 0)
            {
                // For the ANSI case, if the high-order word is not zero, the command's 
                // verb string is in lpcmi->lpVerb. 
                // Is the verb supported by this context menu extension?
                var verb = Marshal.PtrToStringAnsi(ici.verb);
                activeCommand = GetActiveCommand(verb);
            }
            else if (isUnicode && NativeHelpers.GetHighWord(iciex.verbW.ToInt32()) != 0)
            {
                // For the Unicode case, if the high-order word is not zero, the 
                // command's verb string is in lpcmi->lpVerbW. 
                // Is the verb supported by this context menu extension?
                var verb = Marshal.PtrToStringAnsi(iciex.verbW);
                activeCommand = GetActiveCommand(verb);
            }
            else
            {
                // If the command cannot be identified through the verb string, then 
                // check the identifier offset.
                // Is the command identifier offset supported by this context menu 
                // extension?
                var offset = NativeHelpers.GetLowWord(ici.verb.ToInt32());
                activeCommand = GetActiveCommand(offset);
            }

            if (activeCommand != null)
            {
                var context = new CommandContext(this.selectedShellItems, this.isExtendedContextMenu);
                Logger.LogInformation($"Invoking command \"{activeCommand.Command.Name}\" with {context.SelectedShellItems.Count} item(s)");
                try
                {
                    activeCommand.Command.Execute(context);
                }
                catch (Exception exc)
                {
                    Logger.LogError($"Error invoking command \"{activeCommand.Command.Name}\": {exc.ToString()}");
                }
            }
            else
            {
                // If the verb is not recognized by the context menu handler, it 
                // must return E_FAIL to allow it to be passed on to the other 
                // context menu handlers that might implement that verb.
                Marshal.ThrowExceptionForHR(WinError.E_FAIL);
            }
        }

        private ActiveCommand GetActiveCommand(string verb)
        {
            return this.activeCommands.FirstOrDefault(c => c.Verb == verb);
        }

        private ActiveCommand GetActiveCommand(int offset)
        {
            return this.activeCommands.FirstOrDefault(c => c.Offset == offset);
        }

        #endregion

        #region COM Registration

        [ComRegisterFunction]
        public static void Register(Type t)
        {
            try
            {
                Installer.Register(t);
            }
            catch (Exception exc)
            {
                Console.WriteLine($"{t.FullName} could not be registered: {exc.ToString()}");
                throw;
            }
        }

        [ComUnregisterFunction]
        public static void Unregister(Type t)
        {
            try
            {
                Installer.Unregister(t);
            }
            catch (Exception exc)
            {
                Console.WriteLine($"{t.FullName} could not be unregistered: {exc.ToString()}");
                throw;
            }
        }


        #endregion

        #region Helper Classes

        private class ActiveCommand
        {
            public ICommand Command { get; }
            public string Verb { get; }
            public uint Offset { get; }

            public ActiveCommand(ICommand command, string verb, uint offset)
            {
                this.Command = command;
                this.Verb = verb;
                this.Offset = offset;
            }
        }

        #endregion
    }
}