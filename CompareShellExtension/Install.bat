@ECHO OFF

REM Check for admin permissions.
>NUL 2>&1 "%SYSTEMROOT%\system32\cacls.exe" "%SYSTEMROOT%\system32\config\system"
IF "%ERRORLEVEL%" NEQ "0" (
    ECHO This script must be run as an administrator.
	GOTO End
)

REM Determine the location of regasm.exe.
SET REGASM="%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\regasm.exe"
IF NOT EXIST %REGASM% (
	SET REGASM="%WINDIR%\Microsoft.NET\Framework\v4.0.30319\regasm.exe"
)
IF NOT EXIST %REGASM% (
    ECHO The "regasm.exe" tool was not found, please make sure .NET Framework 4 or higher is installed on this machine.
	GOTO End
)

REM Set variables.
SET DLLNAME=CompareShellExtension.dll
SET PRODUCTPATH=%PROGRAMFILES%\CompareShellExtension
SET DLLPATH=%PRODUCTPATH%\%DLLNAME%

REM Prompt to install or uninstall.
CHOICE /C IU /M "Press I to Install or U to Uninstall"
IF "%ERRORLEVEL%" == "1" GOTO Install
IF "%ERRORLEVEL%" == "2" GOTO Uninstall
ECHO Cancelled.
GOTO :End

REM Install.
:Install
ECHO Installing...
TASKKILL /F /IM explorer.exe
XCOPY /Y /F "%~dp0%DLLNAME%" "%PRODUCTPATH%\"
XCOPY /Y /F "%0" "%PRODUCTPATH%\"
%REGASM% "%DLLPATH%" /codebase
explorer.exe
GOTO End

REM Uninstall.
:Uninstall
ECHO Uninstalling...
TASKKILL /F /IM explorer.exe
%REGASM% "%DLLPATH%" /unregister
explorer.exe
RD /S /Q "%PRODUCTPATH%"
GOTO End

:End
PAUSE