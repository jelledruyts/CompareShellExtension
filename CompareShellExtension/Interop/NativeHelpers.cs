namespace CompareShellExtension.Interop
{
    internal static class NativeHelpers
    {
        public static int GetHighWord(int number)
        {
            return ((number & 0x80000000) == 0x80000000) ?
                (number >> 16) : ((number >> 16) & 0xffff);
        }

        public static int GetLowWord(int number)
        {
            return number & 0xffff;
        }

        /// <summary>
        /// Create an HRESULT value from component pieces.
        /// </summary>
        /// <param name="sev">The severity to be used</param>
        /// <param name="fac">The facility to be used</param>
        /// <param name="code">The error number</param>
        /// <returns>A HRESULT constructed from the above 3 values</returns>
        public static int MakeHResult(uint sev, uint fac, uint code)
        {
            return (int)((sev << 31) | (fac << 16) | code);
        }
    }
}