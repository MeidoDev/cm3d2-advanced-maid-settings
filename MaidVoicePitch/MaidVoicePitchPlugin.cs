﻿using CM3D2.ExternalSaveData.Managed;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityInjector;
using UnityInjector.Attributes;

namespace CM3D2.MaidVoicePitch.Plugin
{
    [PluginFilter("CM3D2x64"),
    PluginFilter("CM3D2x86"),
    PluginFilter("CM3D2VRx64"),
    PluginName("CM3D2 MaidVoicePitch"),
    PluginVersion("0.2.9.0")]
    public class MaidVoicePitch : PluginBase
    {
        public static string PluginName { get { return "CM3D2.MaidVoicePitch"; } }
        static bool bDeserialized = false;

        static string[] boneMorph_PropNames;

        static string[] BoneMorph_PropNames
        {
            get
            {
                if (boneMorph_PropNames == null)
                {
                    boneMorph_PropNames = Helper.GetInstanceField(typeof(BoneMorph_), null, "PropNames") as string[];
                }
                return boneMorph_PropNames;
            }
        }

        TBodyMoveHeadAndEye tbodyMoveHeadAndEye = new TBodyMoveHeadAndEye();

        public void Awake()
        {
            UnityEngine.GameObject.DontDestroyOnLoad(this);

            string[][] tagAndBones = new string[][] {
                new string[] { "sintyou", "Bip01 ? Thigh" },    // 下半身の大きさを調整
                new string[] { "sintyou", "Bip01 ? Forearm" },  // 前腕の歪みを修正
            };
            foreach (string[] tagAndBone in tagAndBones)
            {
                string tag = tagAndBone[0];
                string bname = tagAndBone[1];
                string key = string.Concat("min+" + tag, "*", bname.Replace('?', 'L'));
                if (!BoneMorph.dic.ContainsKey(key))
                {
                    PluginHelper.BoneMorphSetScale(tag, bname, 1f, 1f, 1f, 1f, 1f, 1f);
                }
            }
        }

        public void OnLevelWasLoaded(int level)
        {
            KagHooks.SetHook(PluginName, true);

            // TBody.MoveHeadAndEye 処理終了後のコールバック
            CM3D2.MaidVoicePitch.Managed.Callbacks.TBody.MoveHeadAndEye.Callbacks[PluginName] = tbodyMoveHeadAndEyeCallback;

            // BoneMorph_.Blend 処理終了後のコールバック
            CM3D2.MaidVoicePitch.Managed.Callbacks.BoneMorph_.Blend.Callbacks[PluginName] = boneMorph_BlendCallback;

            // AudioSourceMgr.Play処理終了後のコールバック
            CM3D2.MaidVoicePitch.Managed.Callbacks.AudioSourceMgr.Play.Callbacks[PluginName] =
                (audioSourceMgr, f_fFadeTime, loop) =>
                {
                    SetAudioPitch(audioSourceMgr);
                };

            // AudioSourceMgr.PlayOneShot処理終了後のコールバック
            CM3D2.MaidVoicePitch.Managed.Callbacks.AudioSourceMgr.PlayOneShot.Callbacks[PluginName] =
                (audioSourceMgr) =>
                {
                    SetAudioPitch(audioSourceMgr);
                };

            // GameMain.Deserialize処理終了後のコールバック
            //  ロードが行われたときに呼び出される
            CM3D2.ExternalSaveData.Managed.GameMainCallbacks.Deserialize.Callbacks[PluginName] =
                (gameMain, f_nSaveNo) =>
                {
                    bDeserialized = true;
                };

            // ロード直後のシーン読み込みなら、初回セットアップを行う
            if (bDeserialized)
            {
                bDeserialized = false;
                ExSaveData.CleanupMaids();
                FreeComment.FreeCommentToSetting(PluginName, false);
                CleanupExSave();
            }
        }

        public void OnGUI()
        {
            PluginHelper.DebugGui();
        }

        public void Update()
        {
            //PluginHelper.LineClear();
            PluginHelper.DebugClear();
            // テンプレートキャッシュを消去して、再読み込みを促す
            if (Input.GetKey(KeyCode.F12))
            {
                FaceScriptTemplates.Clear();
                SliderTemplates.Clear();
            }
            SliderTemplates.Update(PluginName);

            // フリーコメントから設定を読み込む
            if (Input.GetKey(KeyCode.F4))
            {
                FreeComment.FreeCommentToSetting(PluginName, true);
                CleanupExSave();
            }

            // エディット画面にいる場合は特別処理として毎フレームアップデートを行う
            if (Application.loadedLevel == 5)
            {
                if (GameMain.Instance != null && GameMain.Instance.CharacterMgr != null)
                {
                    CharacterMgr cm = GameMain.Instance.CharacterMgr;
                    for (int i = 0, n = cm.GetStockMaidCount(); i < n; i++)
                    {
                        EditSceneMaidUpdate(cm.GetStockMaid(i));
                    }
                }

                // todo 以下を直すこと：
                //      FARMFIX等のスライダーではないトグル操作等を行った場合にコールバックが
                //      呼ばれていない。これを回避するため、とりあえず毎フレーム呼びだすことにする
                //
                MaidVoicePitch_UpdateSliders();
            }
        }

        /// <summary>
        /// BoneMorph_.Blend の処理終了後に呼ばれるコールバック。
        /// 初期化、設定変更時のみ呼び出される。
        /// ボーンのブレンド処理が行われる際、拡張スライダーに関連する補正は基本的にここで行う。
        /// 毎フレーム呼び出されるわけではないことに注意
        /// </summary>
        void boneMorph_BlendCallback(BoneMorph_ boneMorph_)
        {
            Maid maid = PluginHelper.GetMaid(boneMorph_);
            if (maid == null)
            {
                return;
            }
            WideSlider(maid);
            EyeBall(maid);
        }

        /// <summary>
        /// TBody.MoveHeadAndEye の処理終了後に呼ばれるコールバック
        ///  表示されている間は毎フレーム呼び出される
        /// </summary>
        void tbodyMoveHeadAndEyeCallback(TBody tbody)
        {
            tbodyMoveHeadAndEye.Callback(tbody);

            if (tbody.boMAN || tbody.trsEyeL == null || tbody.trsEyeR == null)
            {
                return;
            }

            Maid maid = PluginHelper.GetMaid(tbody);
            if (maid == null)
            {
                return;
            }
            if (maid.Visible)
            {
                DisableLipSync(maid);
                DisableFaceAnime(maid);
                Mabataki(maid);
                EyeToCam(maid, tbody);
                HeadToCam(maid, tbody);
                RotatePupil(maid, tbody);
                SetLipSyncIntensity(maid, tbody);
                ForeArmFix(maid);
            }
        }

        /// <summary>
        /// AudioSourceMgr.Play および AudioSourceMgr.PlayOneShot の処理終了後に呼ばれるコールバック。
        /// ピッチ変更を行う
        /// </summary>
        static void SetAudioPitch(AudioSourceMgr audioSourceMgr)
        {
            Maid maid = PluginHelper.GetMaid(audioSourceMgr);
            if (maid == null || audioSourceMgr.audiosource == null || !audioSourceMgr.audiosource.isPlaying)
            {
                return;
            }
            float f = ExSaveData.GetFloat(maid, PluginName, "PITCH", 0f);
            audioSourceMgr.audiosource.pitch = 1f + f;
        }

        /// <summary>
        /// AddModsSlider等から呼び出されるコールバック
        /// 呼び出し方法は this.gameObject.SendMessage("MaidVoicePitch.TestUpdateSliders");
        /// </summary>
        public void MaidVoicePitch_UpdateSliders()
        {
            if (GameMain.Instance != null && GameMain.Instance.CharacterMgr != null)
            {
                CharacterMgr cm = GameMain.Instance.CharacterMgr;
                for (int i = 0, n = cm.GetStockMaidCount(); i < n; i++)
                {
                    Maid maid = cm.GetStockMaid(i);
                    if (maid != null && maid.body0 != null && maid.body0.bonemorph != null)
                    {
                        //
                        //  todo    本当にこの方法しかないのか調べること
                        //
                        //  １人目のメイドをエディットし、管理画面に戻り、
                        //  続けて２人目をエディットしようとすると、１人目のメイドの
                        //  boneMorphLocal.linkT が null になっていて例外がおきるので
                        //  あらかじめ linkT を調べる
                        //
                        bool safe = true;
                        foreach (BoneMorphLocal boneMorphLocal in maid.body0.bonemorph.bones)
                        {
                            if (boneMorphLocal.linkT == null)
                            {
                                safe = false;
                            }
                        }
                        if (safe)
                        {
                            try
                            {
                                // 同じ "sintyou" の値を入れて、強制的にモーフ再計算を行う
                                float SCALE_Sintyou = maid.body0.bonemorph.SCALE_Sintyou;
                                maid.body0.BoneMorph_FromProcItem("sintyou", SCALE_Sintyou);
                            }
                            catch (Exception ex)
                            {
#if !DEBUG
                                ; // 最低だ…
#else
                                Helper.ShowException(ex);
#endif
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// エディットシーン用の状態更新
        /// </summary>
        void EditSceneMaidUpdate(Maid maid)
        {
            if (maid == null || !maid.Visible)
            {
                return;
            }

            bool bMuhyou = ExSaveData.GetBool(maid, PluginName, "MUHYOU", false);
            bool bLipSyncOff = ExSaveData.GetBool(maid, PluginName, "LIPSYNC_OFF", false);
            if (bLipSyncOff || bMuhyou)
            {
                // 何もしない
            }
            else
            {
                // エディットシーンではリップシンクを強制的に復活させる
                Helper.SetInstanceField(typeof(Maid), maid, "m_bFoceKuchipakuSelfUpdateTime", false);
            }
        }

        // 目を常時カメラに向ける
        void EyeToCam(Maid maid, TBody tbody)
        {
            float fEyeToCam = ExSaveData.GetFloat(maid, PluginName, "EYETOCAM", 0f);
            if (fEyeToCam < -0.5f)
            {
                tbody.boEyeToCam = false;
            }
            else if (fEyeToCam > 0.5f)
            {
                tbody.boEyeToCam = true;
            }
        }

        // 顔を常時カメラに向ける
        void HeadToCam(Maid maid, TBody tbody)
        {
            float fHeadToCam = ExSaveData.GetFloat(maid, PluginName, "HEADTOCAM", 0f);
            if (fHeadToCam < -0.5f)
            {
                tbody.boHeadToCam = false;
            }
            else if (fHeadToCam > 0.5f)
            {
                tbody.boHeadToCam = true;
            }
        }

        // まばたき制限
        void Mabataki(Maid maid)
        {
            float mabatakiVal = (float)Helper.GetInstanceField(typeof(Maid), maid, "MabatakiVal");
            float f = Mathf.Clamp01(1f - ExSaveData.GetFloat(maid, PluginName, "MABATAKI", 1f));
            float mMin = Mathf.Asin(f);
            float mMax = (float)Math.PI - mMin;
            mMin = Mathf.Pow(mMin / (float)Math.PI, 0.5f);
            mMax = Mathf.Pow(mMax / (float)Math.PI, 0.5f);
            mabatakiVal = Mathf.Clamp(mabatakiVal, mMin, mMax);
            if (ExSaveData.GetBool(maid, PluginName, "MUHYOU", false))
            {
                // 無表情の場合、常に目を固定
                mabatakiVal = mMin;
            }
            Helper.SetInstanceField(typeof(Maid), maid, "MabatakiVal", mabatakiVal);
        }

        // 瞳サイズ変更
        void EyeBall(Maid maid)
        {
            TBody tbody = maid.body0;
            if (tbody != null && tbody.trsEyeL != null && tbody.trsEyeR != null)
            {
                float w = ExSaveData.GetFloat(maid, PluginName, "EYEBALL.width", 1f);
                float h = ExSaveData.GetFloat(maid, PluginName, "EYEBALL.height", 1f);
                tbody.trsEyeL.localScale = new Vector3(1f, h, w);
                tbody.trsEyeR.localScale = new Vector3(1f, h, w);
            }
        }

        // 瞳の角度を目の角度に合わせて補正
        void RotatePupil(Maid maid, TBody tbody)
        {
            /*
                        //  注意：TBody.MoveHeadAndEye内で trsEye[L,R].localRotation が上書きされているため、
                        //  この値は TBody.MoveHeadAndEyeが呼ばれるたびに書き換える必要がある
                        float eyeAng = ExSaveData.GetFloat(maid, PluginName, "EYE_ANG.angle", 0f);
                        Vector3 eea = (Vector3)Helper.GetInstanceField(typeof(TBody), tbody, "EyeEulerAngle");
                        tbody.trsEyeL.localRotation = tbody.quaDefEyeL * Quaternion.Euler(eyeAng, eea.x * -0.2f, eea.z * -0.1f);
                        tbody.trsEyeR.localRotation = tbody.quaDefEyeR * Quaternion.Euler(-eyeAng, eea.x * 0.2f, eea.z * 0.1f);
            */
        }

        // リップシンク強度指定
        void SetLipSyncIntensity(Maid maid, TBody tbody)
        {
            if (!ExSaveData.GetBool(maid, PluginName, "LIPSYNC_INTENISTY", false))
            {
                return;
            }
            float f1 = Mathf.Clamp01(ExSaveData.GetFloat(maid, PluginName, "LIPSYNC_INTENISTY.value", 1f));
            maid.VoicePara_1 = f1 * 0.5f;
            maid.VoicePara_2 = f1 * 0.074f;
            maid.VoicePara_3 = f1 * 0.5f;
            maid.VoicePara_4 = f1 * 0.05f;
            if (f1 < 0.01f)
            {
                maid.voice_ao_f2 = 0;
            }
        }

        // リップシンク(口パク)抑制
        void DisableLipSync(Maid maid)
        {
            bool bMuhyou = ExSaveData.GetBool(maid, PluginName, "MUHYOU", false);
            bool bLipSyncOff = ExSaveData.GetBool(maid, PluginName, "LIPSYNC_OFF", false);
            if (bLipSyncOff || bMuhyou)
            {
                Helper.SetInstanceField(typeof(Maid), maid, "m_bFoceKuchipakuSelfUpdateTime", true);
            }
        }

        // 目と口の表情変化をやめる
        void DisableFaceAnime(Maid maid)
        {
            bool bMuhyou = ExSaveData.GetBool(maid, PluginName, "MUHYOU", false);
            bool bHyoujouOff = ExSaveData.GetBool(maid, PluginName, "HYOUJOU_OFF", false);
            if (bHyoujouOff || bMuhyou)
            {
                maid.FaceAnime("", 0f, 0);
            }
        }

        // ForeArmFix : 前腕の歪みを修正する CM3D.MaidVoicePitch.Plugin.cs の追加メソッド
        // CM3D.MaidVoicePitch.Plugin を適用しメイドのフリーコメント欄に #FARMFIX# の記述で前腕の歪みを修正する。
        // 前腕歪みバグを修正
        void ForeArmFix(Maid maid)
        {
            if (!ExSaveData.GetBool(maid, PluginName, "FARMFIX", false))
            {
                return;
            }

            BoneMorph_ bm_ = maid.body0.bonemorph;
            List<Transform> tListFAL = new List<Transform>();
            List<Transform> tListFAR = new List<Transform>();
            float sclUAx = -1f;

            for (int i = 0; i < bm_.bones.Count; i++)
            {
                if (bm_.bones[i].linkT == null) continue;
                if (bm_.bones[i].linkT.name == "Bip01 L Forearm") tListFAL.Add(bm_.bones[i].linkT);
                if (bm_.bones[i].linkT.name == "Bip01 R Forearm") tListFAR.Add(bm_.bones[i].linkT);
                if (sclUAx < 0f && bm_.bones[i].linkT.name == "Bip01 L UpperArm") sclUAx = bm_.bones[i].linkT.localScale.x;
            }
            if (sclUAx < 0f || tListFAL.Count < 1 || tListFAR.Count < 1) return;

            Vector3 sclUA = new Vector3(sclUAx, 1f, 1f);

            Vector3 antisclUA_d = new Vector3(1f / sclUAx - 1f, 0f, 0f);

            Vector3 eaFAL = tListFAL[0].localRotation.eulerAngles;
            Vector3 eaFAR = tListFAR[0].localRotation.eulerAngles;

            Quaternion antirotFAL = Quaternion.Euler(eaFAL - new Vector3(180f, 180f, 180f));
            Quaternion antirotFAR = Quaternion.Euler(eaFAR - new Vector3(180f, 180f, 180f));
            Vector3 sclFAL_d = antirotFAL * antisclUA_d;
            Vector3 sclFAR_d = antirotFAR * antisclUA_d;

            Vector3 antisclFAL = new Vector3(1f, 1f, 1f) + new Vector3(Mathf.Abs(sclFAL_d.x), Mathf.Abs(sclFAL_d.y), Mathf.Abs(sclFAL_d.z));
            Vector3 antisclFAR = new Vector3(1f, 1f, 1f) + new Vector3(Mathf.Abs(sclFAR_d.x), Mathf.Abs(sclFAR_d.y), Mathf.Abs(sclFAR_d.z));

            foreach (Transform t in tListFAL) t.localScale = Vector3.Scale(antisclFAL, sclUA);
            foreach (Transform t in tListFAR) t.localScale = Vector3.Scale(antisclFAR, sclUA);
        }

        // スライダー範囲を拡大
        void WideSlider(Maid maid)
        {
            if (!ExSaveData.GetBool(maid, PluginName, "WIDESLIDER", false))
            {
                return;
            }

            TBody tbody = maid.body0;
            string[] PropNames = BoneMorph_PropNames;
            if (tbody == null || tbody.bonemorph == null || tbody.bonemorph.bones == null || PropNames == null)
            {
                return;
            }
            BoneMorph_ boneMorph_ = tbody.bonemorph;

            Vector3 eyeScl;
            {
                float aspectRatioMin = 0.1f;
                float aspectRatioMax = 10f;
                float aspectRatio = Mathf.Clamp(ExSaveData.GetFloat(maid, PluginName, "EYE_RATIO", 1f), aspectRatioMin, aspectRatioMax);

                float aspectW = 1f;
                float aspectH = 1f;
                if (aspectRatio >= 1f)
                {
                    // 1以上の場合、横幅は固定で、高さを小さくする
                    aspectW = 1f;
                    aspectH = 1f / aspectRatio;
                }
                else
                {
                    // 1未満の場合、高さは固定で、横幅を小さくする
                    aspectW = 1f * aspectRatio;
                    aspectH = 1f;
                }

                eyeScl = new Vector3(1.00f, aspectH, aspectW);
            }

            float eyeAngAngle;
            float eyeAngX;
            float eyeAngY;
            {
                float ra = ExSaveData.GetFloat(maid, PluginName, "EYE_ANG.angle", 0f);
                float rx = ExSaveData.GetFloat(maid, PluginName, "EYE_ANG.x", 0f);
                float ry = ExSaveData.GetFloat(maid, PluginName, "EYE_ANG.y", 0f);

                rx += -9f;
                ry += -17f;

                rx /= 1000f;
                ry /= 1000f;

                eyeAngAngle = ra;
                eyeAngX = rx;
                eyeAngY = ry;
            }

            Vector3 thiScl = new Vector3(
                1.0f,
                ExSaveData.GetFloat(maid, PluginName, "THISCL.depth", 1f),
                ExSaveData.GetFloat(maid, PluginName, "THISCL.width", 1f));

            Vector3 thiPosL;
            Vector3 thiPosR;
            {
                float dx = ExSaveData.GetFloat(maid, PluginName, "THIPOS.x", 0f);
                float dz = ExSaveData.GetFloat(maid, PluginName, "THIPOS.z", 0f);
                float dy = 0.0f;
                thiPosL = new Vector3(dy, dz / 1000f, -dx / 1000f);
                thiPosR = new Vector3(dy, dz / 1000f, dx / 1000f);
            }

            // 骨盤
            Vector3 pelvisScl = new Vector3(
                ExSaveData.GetFloat(maid, PluginName, "PELSCL.height", 1f),
                ExSaveData.GetFloat(maid, PluginName, "PELSCL.depth", 1f),
                ExSaveData.GetFloat(maid, PluginName, "PELSCL.width", 1f));

            // スカート
            Vector3 sktScl = new Vector3(
                ExSaveData.GetFloat(maid, PluginName, "SKTSCL.height", 1f),
                ExSaveData.GetFloat(maid, PluginName, "SKTSCL.depth", 1f),
                ExSaveData.GetFloat(maid, PluginName, "SKTSCL.width", 1f));

            // 胴(下腹部周辺)
            Vector3 spineScl = new Vector3(
                ExSaveData.GetFloat(maid, PluginName, "SPISCL.height", 1f),
                ExSaveData.GetFloat(maid, PluginName, "SPISCL.depth", 1f),
                ExSaveData.GetFloat(maid, PluginName, "SPISCL.width", 1f));

            // 胴0a(腹部周辺)
            Vector3 spine0aScl = new Vector3(
                ExSaveData.GetFloat(maid, PluginName, "S0ASCL.height", 1f),
                ExSaveData.GetFloat(maid, PluginName, "S0ASCL.depth", 1f),
                ExSaveData.GetFloat(maid, PluginName, "S0ASCL.width", 1f));

            // 胴1_(みぞおち周辺)
            Vector3 spine1Scl = new Vector3(
                ExSaveData.GetFloat(maid, PluginName, "S1_SCL.height", 1f),
                ExSaveData.GetFloat(maid, PluginName, "S1_SCL.depth", 1f),
                ExSaveData.GetFloat(maid, PluginName, "S1_SCL.width", 1f));

            // 胴1a(首・肋骨周辺)
            Vector3 spine1aScl = new Vector3(
                ExSaveData.GetFloat(maid, PluginName, "S1ASCL.height", 1f),
                ExSaveData.GetFloat(maid, PluginName, "S1ASCL.depth", 1f),
                ExSaveData.GetFloat(maid, PluginName, "S1ASCL.width", 1f));

            Transform tEyePosL = null;
            Transform tEyePosR = null;

            float sliderScale = 20f;
            for (int i = boneMorph_.bones.Count - 1; i >= 0; i--)
            {
                BoneMorphLocal boneMorphLocal = boneMorph_.bones[i];
                Vector3 scl = new Vector3(1f, 1f, 1f);
                Vector3 pos = boneMorphLocal.pos;
                for (int j = 0; j < (int)PropNames.Length; j++)
                {
                    float s = 1f;
                    switch (j)
                    {
                        case 0:
                            s = boneMorph_.SCALE_Kubi;
                            break;
                        case 1:
                            s = boneMorph_.SCALE_Ude;
                            break;
                        case 2:
                            s = boneMorph_.SCALE_EyeX;
                            break;
                        case 3:
                            s = boneMorph_.SCALE_EyeY;
                            break;
                        case 4:
                            s = boneMorph_.Postion_EyeX * (0.5f + boneMorph_.Postion_EyeY * 0.5f);
                            break;
                        case 5:
                            s = boneMorph_.Postion_EyeY;
                            break;
                        case 6:
                            s = boneMorph_.SCALE_HeadX;
                            break;
                        case 7:
                            s = boneMorph_.SCALE_HeadY;
                            break;
                        case 8:
                            s = boneMorph_.SCALE_DouPer;
                            if (boneMorphLocal.Kahanshin == 0f)
                            {
                                s = 1f - s;
                            }
                            break;
                        case 9:
                            s = boneMorph_.SCALE_Sintyou;
                            break;
                        case 10:
                            s = boneMorph_.SCALE_Koshi;
                            break;
                        case 11:
                            s = boneMorph_.SCALE_Kata;
                            break;
                        case 12:
                            s = boneMorph_.SCALE_West;
                            break;
                        default:
                            s = 1f;
                            break;
                    }

                    if ((boneMorphLocal.atr & 1 << (j & 31)) != 0)
                    {
                        Vector3 v0 = boneMorphLocal.vecs_min[j];
                        Vector3 v1 = boneMorphLocal.vecs_max[j];

                        Vector3 n0 = v0 * sliderScale - v1 * (sliderScale - 1f);
                        Vector3 n1 = v1 * sliderScale - v0 * (sliderScale - 1f);
                        float f = (s + sliderScale - 1f) * (1f / (sliderScale * 2.0f - 1f));
                        scl = Vector3.Scale(scl, MaidVoicePitch.UncLerp(n0, n1, f));
                    }

                    if ((boneMorphLocal.atr & 1 << (j + 16 & 31)) != 0)
                    {
                        Vector3 v0 = boneMorphLocal.vecs_min[j + 16];
                        Vector3 v1 = boneMorphLocal.vecs_max[j + 16];

                        Vector3 n0 = v0 * sliderScale - v1 * (sliderScale - 1f);
                        Vector3 n1 = v1 * sliderScale - v0 * (sliderScale - 1f);
                        float f = (s + sliderScale - 1f) * (1f / (sliderScale * 2.0f - 1f));
                        pos = Vector3.Scale(pos, MaidVoicePitch.UncLerp(n0, n1, f));
                    }
                }

                Transform linkT = boneMorphLocal.linkT;
                if (linkT == null)
                {
                    continue;
                }

                string name = linkT.name;

                if (name != null && name.Contains("Thigh_SCL_"))
                {
                    boneMorph_.SnityouOutScale = Mathf.Pow(scl.x, 0.9f);
                }

                if (name == null)
                {
                    // 何もしない
                }
                else if (name == "Eyepos_L")
                {
                    scl = Vector3.Scale(scl, eyeScl);
                }
                else if (name == "Eyepos_R")
                {
                    scl = Vector3.Scale(scl, eyeScl);
                }
                else if (name == "Hip_L")
                {
                    scl = Vector3.Scale(scl, pelvisScl);
                    pos += thiPosL;
                }
                else if (name == "Hip_R")
                {
                    scl = Vector3.Scale(scl, pelvisScl);
                    pos += thiPosR;
                }
                else if (name == "Skirt")
                {
                    scl = Vector3.Scale(scl, sktScl);
                }
                else if (name == "Bip01 L Thigh")
                {
                    scl = Vector3.Scale(scl, thiScl);
                    pos += thiPosL;
                }
                else if (name == "Bip01 R Thigh")
                {
                    scl = Vector3.Scale(scl, thiScl);
                    pos += thiPosR;
                }
                else if (name == "Bip01 Pelvis_SCL_")
                {
                    scl = Vector3.Scale(scl, pelvisScl);
                }
                else if (name == "Bip01 Spine_SCL_")
                {
                    scl = Vector3.Scale(scl, spineScl);
                }
                else if (name == "Bip01 Spine0a_SCL_")
                {
                    scl = Vector3.Scale(scl, spine0aScl);
                }
                else if (name == "Bip01 Spine1_SCL_")
                {
                    scl = Vector3.Scale(scl, spine1Scl);
                }
                else if (name == "Bip01 Spine1a_SCL_")
                {
                    scl = Vector3.Scale(scl, spine1aScl);
                }

                linkT.localPosition = pos;
                linkT.localScale = scl;

                if (name != null)
                {
                    if (name == "Eyepos_L")
                    {
                        tEyePosL = linkT;
                    }
                    else if (name == "Eyepos_R")
                    {
                        tEyePosR = linkT;
                    }
                }
            }

            // 目のサイズ・角度変更
            // EyeScaleRotate : 目のサイズと角度変更する CM3D.MaidVoicePich.Plugin.cs の追加メソッド
            // http://pastebin.com/DBuN5Sws
            // その１>>923
            // http://jbbs.shitaraba.net/bbs/read.cgi/game/55179/1438196715/923
            if (tEyePosL != null)
            {
                Transform linkT = tEyePosL;
                Vector3 localCenter = linkT.localPosition + (new Vector3(0f, eyeAngY, eyeAngX)); // ローカル座標系での回転中心位置
                Vector3 worldCenter = linkT.parent.TransformPoint(localCenter);         // ワールド座標系での回転中心位置
                Vector3 localAxis = new Vector3(-1f, 0f, 0f);                       // ローカル座標系での回転軸
                Vector3 worldAxis = linkT.TransformDirection(localAxis);               // ワールド座標系での回転軸

                linkT.localRotation = new Quaternion(-0.00560432f, -0.001345155f, 0.06805823f, 0.9976647f);    // 初期の回転量
                linkT.RotateAround(worldCenter, worldAxis, eyeAngAngle);
            }
            if (tEyePosR != null)
            {
                Transform linkT = tEyePosR;
                Vector3 localCenter = linkT.localPosition + (new Vector3(0f, eyeAngY, -eyeAngX));    // ローカル座標系での回転中心位置
                Vector3 worldCenter = linkT.parent.TransformPoint(localCenter);             // ワールド座標系での回転中心位置
                Vector3 localAxis = new Vector3(-1f, 0f, 0f);                           // ローカル座標系での回転軸
                Vector3 worldAxis = linkT.TransformDirection(localAxis);                   // ワールド座標系での回転軸

                linkT.localRotation = new Quaternion(0.9976647f, 0.06805764f, -0.001350592f, -0.005603582f);   // 初期の回転量
                linkT.RotateAround(worldCenter, worldAxis, -eyeAngAngle);
            }
        }
        
        public static Vector3 UncLerp(Vector3 from, Vector3 to, float t)
		{
			return new Vector3(from.x + (to.x - from.x) * t, from.y + (to.y - from.y) * t, from.z + (to.z - from.z) * t);
		}

        // 動作していない古い設定を削除する
        static void CleanupExSave()
        {
            string[] obsoleteSettings = {
                "WIDESLIDER.enable", "PROPSET_OFF.enable", "LIPSYNC_OFF.enable",
                "HYOUJOU_OFF.enable", "EYETOCAMERA_OFF.enable", "MUHYOU.enable",
                "FARMFIX.enable", "EYEBALL.enable", "EYE_ANG.enable",
                "PELSCL.enable", "SKTSCL.enable", "THISCL.enable", "THIPOS.enable",
                "PELVIS.enable", "FARMFIX.enable", "SPISCL.enable",
                "S0ASCL.enable", "S1_SCL.enable", "S1ASCL.enable",
                "FACE_OFF.enable", "FACEBLEND_OFF.enable",

                // 以下0.2.4で廃止
                "FACE_ANIME_SPEED",
                "MABATAKI_SPEED",
                "PELVIS", "PELVIS.x", "PELVIS.y", "PELVIS.z",
            };
            CharacterMgr cm = GameMain.Instance.CharacterMgr;
            for (int i = 0, n = cm.GetStockMaidCount(); i < n; i++)
            {
                Maid maid = cm.GetStockMaid(i);
                foreach (string s in obsoleteSettings)
                {
                    ExSaveData.Remove(maid, PluginName, s);
                }

                {
                    string fname = ExSaveData.Get(maid, PluginName, "SLIDER_TEMPLATE", null);
                    if (string.IsNullOrEmpty(fname))
                    {
                        ExSaveData.Set(maid, PluginName, "SLIDER_TEMPLATE", "UnityInjector/Config/MaidVoicePitchSlider.xml", true);
                    }
                }
            }

            string[] obsoleteGlobalSettings = {
                "TEST_GLOBAL_KEY"
            };
            foreach (string s in obsoleteGlobalSettings)
            {
                ExSaveData.GlobalRemove(PluginName, s);
            }
        }
    }
}
