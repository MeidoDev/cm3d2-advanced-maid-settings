@ECHO OFF

REM Path to .net compiler executable
SET COMPILER="C:\Windows\Microsoft.NET\Framework\v3.5\csc"

REM Path to CM3D2
SET GAME_DIR="D:\KISS\CM3D2"

CD /D %GAME_DIR%\UnityInjector
%COMPILER% /t:library /lib:%GAME_DIR%\CM3D2x64_Data\Managed /r:UnityEngine.dll /r:UnityInjector.dll /r:Assembly-CSharp.dll /r:Assembly-CSharp-firstpass.dll %1
pause;