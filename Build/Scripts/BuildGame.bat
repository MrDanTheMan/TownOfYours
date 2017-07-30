@ECHO off
SETLOCAL enabledelayedexpansion
GOTO :Main

:Main
	SET configuration=%1
	SET platform=%2

	IF NOT DEFINED configuration (
		GOTO :ShowHelp
	)

	IF NOT DEFINED platform (
		GOTO :ShowHelp
	)

	CALL :RunBuild
	pause
	GOTO :EOF

:ShowHelp
	ECHO Invalid usage !
	ECHO Example usage: BuildGame.bat debug x86
	ECHO Example usage: BuildGame.bat debug x64
	ECHO Example usage: BuildGame.bat release x86
	ECHO Example usage: BuildGame.bat release x64
	GOTO :EOF

:RunBuild
	SET msbuildProcess="..\MSBuild\BuildGame.msbuild"
	SET msbuildExe="C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\msbuild"

	ECHO MSBuild.exe: %msbuildExe%
	ECHO Build Configuration: %configuration%
	ECHO Build Platform: %platform%

	REM Builds debug binaries
	IF "%configuration%"=="debug" (
		IF "%platform%"=="x86" (
			CALL %msbuildExe% %msbuildProcess% /target:BuildDebug86
			GOTO :EOF
		)
		IF "%platform%"=="x64" (
			CALL %msbuildExe% %msbuildProcess% /target:BuildDebug64
			GOTO :EOF
		)
		IF "%platform%"=="all" (
			CALL %msbuildExe% %msbuildProcess% /target:BuildDebug86
			CALL %msbuildExe% %msbuildProcess% /target:BuildDebug64
			GOTO :EOF
		)
	
		GOTO :ShowHelp
	)
	
	REM Builds release binaries
	IF "%configuration%"=="release" (
		IF "%platform%"=="x86" (
			CALL %msbuildExe% %msbuildProcess% /target:BuildRelease86
			GOTO :EOF
		)
		IF "%platform%"=="x64" (
			CALL %msbuildExe% %msbuildProcess% /target:BuildRelease64
			GOTO :EOF
		)
		IF "%platform%"=="all" (
			CALL %msbuildExe% %msbuildProcess% /target:BuildRelease86
			CALL %msbuildExe% %msbuildProcess% /target:BuildRelease64
			GOTO :EOF
		)
	
		GOTO :ShowHelp
	)
	
	REM Builds both debug and release binaries
	IF "%configuration%"=="all" (
		IF "%platform%"=="x86" (
			CALL %msbuildExe% %msbuildProcess% /target:BuildDebug86
			CALL %msbuildExe% %msbuildProcess% /target:Buildrelease86
			GOTO :EOF
		)
		IF "%platform%"=="x64" (
			CALL %msbuildExe% %msbuildProcess% /target:BuildDebug64
			CALL %msbuildExe% %msbuildProcess% /target:Buildrelease64
			GOTO :EOF
		)
		IF "%platform%"=="all" (
			CALL %msbuildExe% %msbuildProcess% /target:BuildDebug86
			CALL %msbuildExe% %msbuildProcess% /target:Buildrelease86
			CALL %msbuildExe% %msbuildProcess% /target:BuildDebug64
			CALL %msbuildExe% %msbuildProcess% /target:Buildrelease64
			GOTO :EOF
		)
	
		GOTO :ShowHelp
	)

	GOTO :ShowHelp	