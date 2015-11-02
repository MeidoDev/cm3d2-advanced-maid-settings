@ECHO OFF

REM ===================
REM ====== Setup ======
REM ===================

REM Path to .net compiler executable
SET COMPILER="C:\Windows\Microsoft.NET\Framework\v3.5\csc"

REM Used CM3D2 Architecture (x86 | x64)
SET ARCH="x64"

REM Path to CM3D2
SET GAME_DIR="D:\KISS\CM3D2"

REM Path to ReiPatcher
SET RP_DIR="D:\KISS\_workdir\ReiPatcher"

REM ==============================
REM ====== ExternalSaveData ======
REM ==============================

REM Compile patch
SET P_LIB=/lib:%RP_DIR% /r:ReiPatcher.exe /r:mono.cecil.dll
SET P_OUT=/out:%RP_DIR%\Patches\ExternalSaveData.Patcher.dll
SET P_SRC=ExternalSaveData\ExternalSaveDataPatcher.cs Lib\PatcherHelper.cs
%COMPILER% /t:library %P_LIB% %P_OUT% %P_SRC%

REM Compile hook
SET P_LIB=/lib:%GAME_DIR%\CM3D2%ARCH%_Data\Managed /r:Assembly-CSharp.dll /r:UnityEngine.dll
SET P_OUT=/out:%GAME_DIR%\CM3D2%ARCH%_Data\Managed\CM3D2.ExternalSaveData.Managed.dll
SET P_SRC=ExternalSaveData\ExSaveData.cs ExternalSaveData\GameMainCallbacks.cs Lib\Helper.cs
%COMPILER% /t:library %P_LIB% %P_OUT% %P_SRC%

REM ============================
REM ====== MaidVoicePitch ======
REM ============================

REM Compile patch
SET P_LIB=/lib:%RP_DIR% /r:ReiPatcher.exe /r:mono.cecil.dll /r:reipatcherplus.dll
SET P_OUT=/out:%RP_DIR%\Patches\CM3D2.MaidVoicePitch.Patcher.dll
SET P_SRC=MaidVoicePitch\MaidVoicePitchPatcher.cs Lib\PatcherHelper.cs Lib\Helper.cs
%COMPILER% /t:library %P_LIB% %P_OUT% %P_SRC%

REM Compile hook
SET P_LIB=/lib:%GAME_DIR%\CM3D2%ARCH%_Data\Managed /r:Assembly-CSharp.dll /r:UnityEngine.dll
SET P_OUT=/out:%GAME_DIR%\CM3D2%ARCH%_Data\Managed\CM3D2.MaidVoicePitch.Managed.dll
SET P_SRC=MaidVoicePitch\MaidVoicePitchManaged.cs Lib\Helper.cs
%COMPILER% /t:library %P_LIB% %P_OUT% %P_SRC%

REM Compile plugin
SET P_LIB=/lib:%GAME_DIR%\CM3D2%ARCH%_Data\Managed /r:Assembly-CSharp.dll /r:Assembly-CSharp-firstpass.dll /r:CM3D2.ExternalSaveData.Managed.dll /r:CM3D2.MaidVoicePitch.Managed.dll /r:UnityEngine.dll /r:UnityInjector.dll
SET P_OUT=/out:%GAME_DIR%\UnityInjector\CM3D2.MaidVoicePitch.Plugin.dll
SET P_SRC=MaidVoicePitch\MaidVoicePitchPlugin.cs MaidVoicePitch\DebugLineRender.cs MaidVoicePitch\FaceScripteTemplates.cs MaidVoicePitch\FreeComment.cs MaidVoicePitch\KagHooks.cs MaidVoicePitch\SliderTemplates.cs MaidVoicePitch\TBodyMoveHeadAndEyeReplcacement.cs MaidVoicePitch\TemplateFiles.cs Lib\Helper.cs Lib\PluginHelper.cs
%COMPILER% /t:library %P_LIB% %P_OUT% %P_SRC%

REM Copy config XML
copy MaidVoicePitch\MaidVoicePitchSlider.xml %GAME_DIR%\UnityInjector\Config\

REM ==========================
REM ====== AddModsSlider ======
REM ==========================

REM Compile plugin
SET P_LIB=/lib:%GAME_DIR%\CM3D2%ARCH%_Data\Managed /r:Assembly-CSharp.dll /r:CM3D2.ExternalSaveData.Managed.dll /r:UnityEngine.dll /r:UnityInjector.dll
SET P_OUT=/out:%GAME_DIR%\UnityInjector\CM3D2.AddModsSlider.Plugin.dll
SET P_SRC=AddModsSlider\CM3D2.AddModsSlider.Plugin.cs
%COMPILER% /t:library %P_LIB% %P_OUT% %P_SRC%

REM Copy config XML
copy AddModsSlider\ModsParam.xml %GAME_DIR%\UnityInjector\Config\

pause;
