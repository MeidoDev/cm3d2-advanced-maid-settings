Advanced Maid Settings (v2.1)
=============

This package is a combination of multiple plugins and patches that enable an advanced customization of your maids beyond of what the game originally offers. **In order to be able to install it, you will need a correctly set up ReiPatcher and UnityInjector environment.**

The following mods are included:

---
### ExternalSaveData ###

**Version:** 0.1.3.0

**Source:** [Github](https://github.com/neguse11/cm3d2_plugins_okiba/tree/master/ExternalSaveData)

**Author:** [neguse11](https://github.com/neguse11)

A patch that allows plugins to save additional data alongside game savefiles.

---
### MaidVoicePitch ###

**Version:** 0.2.9.0

**Source:** [Github](https://github.com/neguse11/cm3d2_plugins_okiba/tree/master/MaidVoicePitch)

**Author:** [neguse11](https://github.com/neguse11)

This plugin adds many additional configuration options for maids like voice pitch, body part scaling and head tracking adjustments. It can also unlock all the regular sliders to allow further body customization.


### AddModsSliders ###

**Version:** 0.1.1.15

**Source:** [Github](https://github.com/CM3D2-01/CM3D2.AddModsSlider.Plugin)

**Author:** [CM3D2-01](https://github.com/CM3D2-01)

This plugin provides a customizable GUI element which is used to set values for the MaidVoicePitch plugin.

---


### Installation

* Download the source archive and extract it into a directory of your liking (but not the ReiPatcher or CM3D2 folder), while keeping the directory structure intact.


* If you had already installed a previous version of Advanced Maid Settings, make sure you move away or delete the files `ModsParam.xml` and `MaidVoicePitchSlider.xml`, since the script will not overwrite them with their updated versions otherwise. This is meant so you don't lose any customization in case you had edited those files.


* Edit the "Setup" section of `build.bat` so that the paths match your installation. Note: The script assumes that your ReiPatcher patches are stored in a subfolder called `Patches` inside your ReiPatcher folder. If you changed that for some reason, make sure to edit the rest of the file accordingly.


* Run `build.bat`. If everything worked, all the necessary file have been created / copied into the correct places so you just have to run ReiPatcher now and you're done.


### Usage


* While in the maid edit screen, press `F5` to open the customization panel.


* If your maid has MaidVoicePitch configuration data stored in her free comment, you can load it by pressing `F4`.


* Please note that extended slider ranges (i.e. for body size) won't work until you activate the "Unlock sliders" / `WIDESLIDER` option.


* Customization data is no longer stored in the free comment, but in a special XML file when you save the game. For example, if you customized your maid and saved the game into slot 2 (`SaveData002.save`), the information will be saved into `SaveData002.save.exsave.xml`. It is also possible to manually edit these files and then load to game to adjust maid settings.


* If you do not want regular sliders having extended value ranges, delete the file `MaidVoicePitchSlider.xml` in the `UnityInjector\Config` folder, or change the values specified in that file accordingly.

### Settings Reference


| ID | Description |
|:--:|-------------|
| WIDESLIDER | Increases the value range of sliders and disables checks so negative values for settings like body height are allowed possible
| PROPSET_OFF | Disables `@PropSet` in game scripts. Enabling this supposedly fixes the problem of negative shoulder and neck values getting reset during story scripts|
| LIPSYNC_OFF | Disables lip-sync when the maid is talking |
| HYOUJOU_OFF | Disables the automatic change of facial expressions (non script invoked) |
| EYETOCAMERA_OFF | Disables the `@EyeToCamera` script command (changing the EyeToCam setting inside scripts) |
| MUHYOU | Disables all face animations (expressions, lip sync, blinking, etc) |
| FARMFIX | Fixes the distortion and wrong positioning of the forearm if your maid is smaller than normally allowed |
| FACE_OFF | Disables the `@Face` script command (changing facial expression inside scripts) |
| FACEBLEND_OFF | Disables the `@FaceBlend` script command (changing blush / tear settings inside scripts) |
| EYETOCAM | Changes the eye to camera behavior during scripts. `0` => as defined in the script, `1 => always look at camera, `-1` => never look at camera |
| HEADTOCAM | Similar to `EYETOCAM`, but for head movement instead |
| PITCH | Changes the voice pitch | 
| MABATAKI | Adjusts the relative angle of fully opened eyes (0% ~ 100%) |
| LIPSYNC_INTENISTY | Reduces the range of mouth/lip movement animations during lip-sync |
| EYEBALL | Iris / eyeball scaling |
| EYE_RATIO | Adjusts the width/height aspect ratio of the eye
| EYE_ANG | Rotates and distorts the eyes |
| PELSCL | Scales the pelvis region of the body |
| THISCL | Scales the legs |
| THIPOS | Adjusts the position of the legs |
| SKTSCL | Scales the collision region of skirts |
| SPISCL | Scales the lower abdomen region of the body |
| S0ASCL | Scales the abdomen region of the body |
| S1_SCL | Scales the stomach region of the body |
| S1ASCL | Scales the chest region of the body |
| HEAD_TRACK | Advanced settings for the EyeToCam / HeadToCam feature, like changing offets, angles and other things |












