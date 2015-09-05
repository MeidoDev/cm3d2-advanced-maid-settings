/*
────────────────────────
  開発版です。ひどいことが起こるかもしれない！
────────────────────────

プロフィールの「フリーコメント」欄にコマンドを入力することで、
声の音程が変わります。

    ６％アップ
    #PITCH=+0.06#

    １６％ダウン
    #PITCH=-0.16#

    １００％アップ
    #PITCH=+1.00#

    目の開度を50%に制限
    #MABATAKI=0.50#

    目の開度を100% (通常値) にする
    #MABATAKI=1.00#

    表情変化オフ (怒りなどの表情変化を行いません。まばたきや口パクはします)
    #HYOUJOU_OFF#

    リップシンクオフ (音声にあわせた口パク動作をしません)
    #LIPSYNC_OFF#

    目をカメラに向ける (顔は向けません。#HEADTOCAM#と併用可)
    #EYETOCAM#

    顔をカメラに向ける (目は向けません。#EYETOCAM#と併用可)
    #HEADTOCAM#

    スーパー無表情
    #MUHYOU#

    スライダー限界範囲拡大 (>>127 相当の処理)
    #WIDESLIDER#

    夜伽スクリプトでの@PropSet無視
    #PROPSET_OFF#

    瞳サイズ変更 (#EYEBALL=1.0,1.0#で通常値。0以上の値が有効)
    #EYEBALL=1.2,1.0#

    骨盤部コリジョン指定 (テスト中。負の値も有効。常時スカートめくりなどができますが、空中に浮く可能性があります)
    #TEST_PELVIS=0.5,1.0,2.0#

    表情変化速度指定 (テスト中。0以上の値が有効。小さいほど遅くなります。1.0で標準値)
    #TEST_FACE_ANIME_SPEED=0.5#

    リップシンク強度指定 (テスト中。0以上の値が有効。小さいほど音声の口への影響が小さくなります。1.0で標準値)
    #TEST_LIPSYNC=0.5#

    まばたき速度指定 (テスト中。0以上の値が有効。小さいほど遅くなります。1.0で標準値)
    #TEST_MABATAKI_SPEED=0.7#

    @EyeToCameraを無視する (テスト中)
    #TEST_EYETOCAMERA_OFF#

    目の縦横比指定 (テスト中。0.1～10が有効。小さいほど縦長、大きいほど横長になります。1.0で標準値)
    #TEST_EYE_RATIO=0.8#

    目の角度指定 (テスト中。後述)
    #TEST_EYE_ANG=10.0,0,0#

    骨盤スケール指定 (テスト中。0以上の値が有効。1.0で標準値。引数はそれぞれ、幅,奥行き,高さ)
    #TEST_PELSCL=1.0,0.8,0.9#

    脚部スケール指定 (テスト中。0以上の値が有効。1.0で標準値。引数はそれぞれ、幅,奥行き)
    #TEST_THISCL=0.9,0.8#

    股関節位置指定 (テスト中。負の値も有効。0.0で標準値。引数はそれぞれ、幅方向と前後方向)
    #TEST_THIPOS=-5.0,0#


▼ まばたき範囲指定

    「#MABATAKI=開度#」と指定します。0.0～1.0の値が有効です。
    「開度」は0.0で完全閉じ、0.5で半分開け、1.0で完全開けです。
    「#MABATAKI=1.0#」と指定すると、デフォルトの動作を同じになります。


▼ 目の角度指定

    「#TEST_EYE_ANG=角度,補正値1,補正値2#」と指定します。
    「角度」は0.0で標準、正の値でたれ目、負の値でつり目になります。

    「プリティフェイス」の場合は「#TEST_EYE_ANG=角度,0,0#」と書いて、回転を指定してください。
    「初代フェイス」の場合は「#TEST_EYE_ANG=角度,-4,-8#」と書いて、回転を指定してください。


▼ スライダー範囲指定

    「#TEST_SLIDER_TEMPLATE=XMLファイル名#」と指定します。
    XMLファイルの内容は改造スレッドに投稿してあるものを参照してください。
    また、F12キーを押すことで、メモリ上のキャッシュを消し、XMLファイルの再読み込みを行います。


▼ 前提

  ・Microsoft .NET Framework 3.5 がインストール済み
  ・UnityInjector が動作している
  ・C:\KISS\CM3D2_KAIZOU\ 下にゲーム一式がある


▼ コンパイル

(1) 今見ているこのファイルを、「CM3D2.MaidVoicePitch.Plugin.cs」という名前で保存してください

(2) 保存した「CM3D2.MaidVoicePitch.Plugin.cs」を
    「C:\KISS\CM3D2_KAIZOU\UnityInjector\」フォルダーへコピーして、
    「C:\KISS\CM3D2_KAIZOU\UnityInjector\CM3D2.MaidVoicePitch.Plugin.cs」というファイルが
    存在するようにしてください

(3) 保存した後、以下のコマンドを実行します

―――＜＜コマンドプロンプトでここから＞＞―――

cd /d C:\KISS\CM3D2_KAIZOU\UnityInjector
C:\Windows\Microsoft.NET\Framework\v3.5\csc /t:library /lib:..\CM3D2x64_Data\Managed /r:UnityEngine.dll /r:UnityInjector.dll /r:Assembly-CSharp.dll /r:Assembly-CSharp-firstpass.dll CM3D2.MaidVoicePitch.Plugin.cs

―――＜＜ここまで実行＞＞―――

コンパイルに成功すると、同じフォルダーに「CM3D2.MaidVoicePitch.Plugin.dll」というファイルが生成されます。


▼ 動作確認

  コンパイル後 C:\KISS\CM3D2_KAIZOU\CM3D2x64.exe を起動して、動作を確認してください


▼ 履歴

    0.1.11  "#EYETOCAM#" が指定されていない場合に、瞳が動かなくなるバグを修正 (その２>>216, >>217)

    0.1.10  "#PROPSET_OFF#" 等が正常に動作しないときがあるのを修正 (その２>>89, >>96, >>141)

    0.1.9   骨盤スケール指定 "#TEST_PELSCL=W.WW,D.DD,H.HH#" を追加 (テスト中) (その１>>961, >>970)
            脚部スケール指定 "#TEST_THISCL=W.WW,D.DD#" を追加 (テスト中) (その１>>961, >>970)
            股関節位置指定 "#TEST_THIPOS=X.XX,Z.ZZ#" を追加 (テスト中) (その１>>961, >>970)

    0.1.8   @EyeToCameraを無視する "#TEST_EYETOCAMERA_OFF#" を追加 (テスト中) (その1>>697)
            スライダー範囲指定 "#TEST_SLIDER_TEMPLATE=filename#" を追加 (テスト中) (その１>>804, >>807, その２>>11, >>12)
            目の縦横比変更 "#TEST_EYE_RATIO=N.NN#" を追加 (その1>>923, EyeScaleRotate)
            目の角度変更 "#TEST_EYE_ANG=N.NN,X.XX,Y.YY#" を追加 (その1>>923, EyeScaleRotate)

    0.1.7   VR版でも動作するように修正 (その1>>592)
            スライダー範囲拡大の名称を "#WIDESLIDER#" に変更
            @PropSet無視の名称を "#PROPSET_OFF#" に変更
            まばたき範囲指定を "#MABATAKI=N.NN#" に仕様変更
            瞳サイズ変更 "#EYEBALL=N.NN,M.MM#" を追加
            リップシンク強度指定 "#TEST_LIPSYNC=N.NN#" を追加 (テスト中) (その1>>624)
            まばたき速度指定 "#TEST_MABATAKI_SPEED=N.NN#" を追加 (テスト中)
            表情変化速度指定 "#TEST_FACE_ANIME_SPEED=N.NN#" を追加 (テスト中)
            骨盤部コリジョン指定 "#TEST_PELVIS=X.XX,Y.YY,Z.ZZ#" を追加 (テスト中)
            表情テンプレート指定 "#TEST_FACE_SCRIPT_TEMPLATE=filename# を追加 (テスト中)

    0.1.6   コンパイルバグ修正その２ (その1>>541)

    0.1.5   バグ修正

    0.1.4   夜伽スクリプトでの@PropSetの動作を制限する指定 "#TEST_PROPSET_OFF#" を追加 (その1>>466)

    0.1.3   UnityInjectorベースに変更 (その1>>436)
            まばたき範囲指定 "#MABATAKI=N.NN,M.MM#" を追加
            表情変化オフ指定 "#HYOUJOU_OFF#" を追加
            リップシンクオフ指定 "#LIPSYNC_OFF#" を追加
            目をカメラに向ける指定 "#EYETOCAM#" を追加
            顔をカメラに向ける指定 "#HEADTOCAM#" を追加

    0.1.2   説明を修正 (その1>>391)
            ピッチ指定を "#PITCH=N.NN#" に変更
            無表情指定 "#MUHYOU#" を追加

    0.1.1   説明を修正 (その1>>306)
            フリーコメント欄が空の場合に正常に動作していなかったのを修正 (その1>>308)

    0.1.0   最初の版
  */

using script;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using UnityEngine;
using UnityInjector;
using UnityInjector.Attributes;

namespace CM3D2.MaidVoicePitch.Plugin
{
    [PluginFilter("CM3D2x64"),
    PluginFilter("CM3D2x86"),
    PluginFilter("CM3D2VRx64"),
    PluginName("CM3D2 MaidVoicePitch"),
    PluginVersion("0.1.11.0")]
    public class MaidVoicePitch : PluginBase
    {
        public bool bEditScene = false;
        public bool bKagTagPropSetHooked = false;
        public Dictionary<WeakReference, Status> cache = null;
        public Dictionary<string, FaceScriptTemplate> faceScriptTemplates = null;
        public Dictionary<string, SliderTemplate> sliderTemplates = null;

        delegate bool TagCallbackDelegate(KagTagSupport tag_data);
        delegate bool TagProcDelegate(string kagManagerTypeName, Type kagManagerType, BaseKagManager baseKagManager, KagTagSupport tag_data);

        public class Status
        {
            public bool bPropSetOff = false;
            public bool bEyeToCameraOff = false;
            public string faceScriptTemplateFilename = null;
            public float faceAnimeSpeed = 1.0f;

            public Status() { }
            public Status(bool bPropSetOff, bool bEyeToCameraOff, string faceScriptTemplateFilename, float faceAnimeSpeed)
            {
                this.bPropSetOff = bPropSetOff;
                this.bEyeToCameraOff = bEyeToCameraOff;
                this.faceScriptTemplateFilename = faceScriptTemplateFilename;
                this.faceAnimeSpeed = faceAnimeSpeed;
            }
        }

        public void Awake()
        {
            UnityEngine.GameObject.DontDestroyOnLoad(this);
            FlushTemplatesCache();
            FlushCache();
            TestPropSet();
            LowerBodyScaleAwake();
        }

        public void OnLevelWasLoaded(int level)
        {
            bKagTagPropSetHooked = false;
            if (level == 5)
            {
                bEditScene = true;
            }
            TestPropSet();
        }

        public void Update()
        {
            // テンプレートキャッシュを消去して、再読み込みを促す
            if (Input.GetKey(KeyCode.F12))
            {
                FlushTemplatesCache();
            }

            if (CharacterMgr.EditModeLookHaveItem)
            {
                MaidsUpdate(true);  // エディット画面にいる場合のみ、アップデートを行う
            }
        }

        public void LateUpdate()
        {
            MaidsUpdate(false);
        }

        void MaidsUpdate(bool bEditUpdate)
        {
            if (GameMain.Instance == null || GameMain.Instance.CharacterMgr == null)
            {
                return;
            }

            FlushCache();
            CharacterMgr cm = GameMain.Instance.CharacterMgr;
            for (int i = 0, n = cm.GetStockMaidCount(); i < n; i++)
            {
                MaidUpdate(cm.GetStockMaid(i), bEditUpdate);
            }
        }

        void MaidUpdate(Maid maid, bool bEditUpdate)
        {
            if (maid == null)
            {
                return;
            }

            string freeComment = GetFreeComment(maid);
            if (freeComment == null)
            {
                return;
            }

            UpdateCache(maid, freeComment);

            // 全ての顔変化をやめる
            bool bMuhyou = freeComment.Contains("#MUHYOU#");

            // リップシンク(口パク)しない
            if (freeComment.Contains("#LIPSYNC_OFF#") || bMuhyou)
            {
                Helper.SetInstanceField(typeof(Maid), maid, "m_bFoceKuchipakuSelfUpdateTime", true);
            }
            else
            {
                if (bEditUpdate)
                {
                    Helper.SetInstanceField(typeof(Maid), maid, "m_bFoceKuchipakuSelfUpdateTime", false);
                }
            }

            if (bMuhyou)
            {
                Helper.SetInstanceField(typeof(Maid), maid, "MabatakiVal", 1f);
            }

            // 目と口の表情変化をやめる
            if (freeComment.Contains("#HYOUJOU_OFF#") || bMuhyou)
            {
                maid.FaceAnime("", 0f, 0);
            }

            // 目を常時カメラに向ける
            //	todo	状態は３つ。true=常に向ける、false=常に向けない、default=何も操作せず、スクリプト等にまかせる
            if (freeComment.Contains("#EYETOCAM#"))
            {
                if (maid.body0 != null)
                {
                    maid.body0.boEyeToCam = true;
                }
            }

            // 顔を常時カメラに向ける
            //	todo	状態は３つ。true=常に向ける、false=常に向けない、default=何も操作せず、スクリプト等にまかせる
            if (freeComment.Contains("#HEADTOCAM#"))
            {
                if (maid.body0 != null)
                {
                    maid.body0.boHeadToCam = true;
                }
            }

            Pitch(maid, freeComment);
            Mabataki(maid, freeComment);
            WideSlider(maid, freeComment);
            EyeBall(maid, freeComment);
            LowerBodyScale(maid, freeComment);
            EyeScaleRotate(maid, freeComment);
            TestMabatakiSpeed(maid, freeComment, bEditUpdate);
            TestPelvis(maid, freeComment);
            TestLipSync(maid, freeComment);
            TestSliderTemplate(maid, freeComment);
        }

        // キャッシュ状態更新
        void FlushCache()
        {
            cache = new Dictionary<WeakReference, Status>();
        }

        void FlushTemplatesCache()
        {
            faceScriptTemplates = new Dictionary<string, FaceScriptTemplate>();
            sliderTemplates = new Dictionary<string, SliderTemplate>();
        }

        Status UpdateCache(Maid maid, string freeComment)
        {
            if (maid == null || string.IsNullOrEmpty(freeComment))
            {
                return null;
            }

            // @PropSetを無視する
            bool bPropSetOff = freeComment.Contains("#PROPSET_OFF#") ||
                freeComment.Contains("#TEST_PROPSET_OFF#");

            // @EyeToCameraを無視する
            bool bEyeToCameraOff = freeComment.Contains("#TEST_EYETOCAMERA_OFF#");

            // テンプレート指定
            string templateName = null;
            {
                Match m = Regex.Match(freeComment, @"#TEST_FACE_SCRIPT_TEMPLATE=([^#]*)#");
                if (m.Groups.Count >= 2)
                {
                    templateName = m.Groups[1].Value;
                }
            }

            float faceAnimeSpeed = 1.0f;
            {
                Match m = Regex.Match(freeComment, @"#TEST_FACE_ANIME_SPEED=(.*)#");
                if (m.Groups.Count >= 2)
                {
                    faceAnimeSpeed = Mathf.Clamp(Helper.FloatTryParse(m.Groups[1].Value), 0.01f, 100f);
                }
            }

            Status newStatus = new Status(bPropSetOff, bEyeToCameraOff, templateName, faceAnimeSpeed);
            cache.Add(new WeakReference(maid, false), newStatus);
            return newStatus;
        }

        Status GetStatusByName(string maidName)
        {
            foreach (var kv in cache)
            {
                var o = (Maid)kv.Key.Target;
                if (o != null && o.name == maidName)
                {
                    return kv.Value;
                }
            }
            try
            {
                Maid maid = GetMaidByName(maidName);
                return UpdateCache(maid, GetFreeComment(maid));
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : {0}", e);
            }
            return null;
        }

        static Maid GetMaidByName(string maidName)
        {
            CharacterMgr cm = GameMain.Instance.CharacterMgr;
            for (int i = 0, n = cm.GetStockMaidCount(); i < n; i++)
            {
                Maid maid = cm.GetStockMaid(i);
                if (maid.name == maidName)
                {
                    return maid;
                }
            }
            return null;
        }

        static string GetFreeComment(Maid maid)
        {
            if (maid != null && maid.Param != null && maid.Param.status != null && maid.Param.status.free_comment != null)
            {
                return maid.Param.status.free_comment;
            }
            return null;
        }

        bool IsPropSetOff2(string maidName)
        {
            Status s = GetStatusByName(maidName);
            if (s == null)
            {
                return false;
            }
            return s.bPropSetOff;
        }

        bool IsPropSetOff(string maidName)
        {
            bool b = IsPropSetOff2(maidName);
            Console.WriteLine("MaidVoicePitch.IsPropSetOff({0}) -> {1}", maidName, b);
            return b;
        }

        bool IsEyeToCameraOff(string maidName)
        {
            Status s = GetStatusByName(maidName);
            if (s == null)
            {
                return false;
            }
            return s.bEyeToCameraOff;
        }

        FaceScriptTemplate GetFaceScriptTemplate(string maidName)
        {
            FaceScriptTemplate t = null;
            Status s = GetStatusByName(maidName);
            if (s != null)
            {
                string fname = s.faceScriptTemplateFilename;
                if (fname != null)
                {
                    if (!faceScriptTemplates.TryGetValue(fname, out t))
                    {
                        faceScriptTemplates[fname] = FaceScriptTemplate.Load(fname);
                        return faceScriptTemplates[fname];
                    }
                }
            }
            return t;
        }

        float GetFaceAnimeSpeed(string maidName)
        {
            Status s = GetStatusByName(maidName);
            if (s == null)
            {
                return 1f;
            }
            return s.faceAnimeSpeed;
        }

        // ピッチ変更
        void Pitch(Maid maid, string freeComment)
        {
            if (maid.AudioMan == null || maid.AudioMan.audiosource == null || !maid.AudioMan.audiosource.isPlaying)
            {
                return;
            }
            Match m = Regex.Match(freeComment, @"#PITCH=([-+]?[0-9]*\.?[0-9]+)");
            maid.AudioMan.audiosource.pitch = 1f + Helper.FloatTryParse(m.Groups[1].Value);
        }

        // まばたき制限
        void Mabataki(Maid maid, string freeComment)
        {
            float mabatakiVal = (float)Helper.GetInstanceField(typeof(Maid), maid, "MabatakiVal");

            Match m = Regex.Match(freeComment, @"#MABATAKI=([-+]?[0-9]*\.?[0-9]+)#");
            if (m.Groups.Count >= 2)
            {
                float f = Mathf.Clamp01(1f - Helper.FloatTryParse(m.Groups[1].Value));
                float mMin = Mathf.Asin(f);
                float mMax = (float)Math.PI - mMin;
                mMin = Mathf.Pow(mMin / (float)Math.PI, 0.5f);
                mMax = Mathf.Pow(mMax / (float)Math.PI, 0.5f);
                mabatakiVal = Mathf.Clamp(mabatakiVal, mMin, mMax);
            }

            // 旧まばたき (そのうち廃止)
            {
                Match mb = Regex.Match(freeComment, @"#MABATAKI=([-+]?[0-9]*\.?[0-9]+),([-+]?[0-9]*\.?[0-9]+)#");
                if (mb.Groups.Count >= 3)
                {
                    float mMin = Helper.FloatTryParse(mb.Groups[1].Value);
                    float mMax = Helper.FloatTryParse(mb.Groups[2].Value);
                    mabatakiVal = Mathf.Clamp(mabatakiVal, mMin, mMax);
                }
            }

            Helper.SetInstanceField(typeof(Maid), maid, "MabatakiVal", mabatakiVal);
        }

        // まばたき速度変更
        void TestMabatakiSpeed(Maid maid, string freeComment, bool bEditUpdate)
        {
            Match m = Regex.Match(freeComment, @"#TEST_MABATAKI_SPEED=([-+]?[0-9]*\.?[0-9]+)#");
            if (m.Groups.Count >= 2)
            {
                float mabatakiVal = (float)Helper.GetInstanceField(typeof(Maid), maid, "MabatakiVal");
                if (mabatakiVal != 1f && mabatakiVal > 0.1f && !bEditUpdate)
                {
                    float f = Mathf.Clamp(Helper.FloatTryParse(m.Groups[1].Value), 0f, 10f);
                    mabatakiVal += Time.deltaTime * 2f;
                    mabatakiVal -= Time.deltaTime * 2f * f;
                }
                Helper.SetInstanceField(typeof(Maid), maid, "MabatakiVal", mabatakiVal);
            }
        }

        // 瞳サイズ変更
        void EyeBall(Maid maid, string freeComment)
        {
            TBody tbody = maid.body0;
            Match mEyeBall = Regex.Match(freeComment, @"#EYEBALL=([-+]?[0-9]*\.?[0-9]+),([-+]?[0-9]*\.?[0-9]+)#");
            if (mEyeBall.Groups.Count >= 3 && tbody != null && tbody.trsEyeL != null && tbody.trsEyeR != null)
            {
                float w = Helper.FloatTryParse(mEyeBall.Groups[1].Value);
                float h = Helper.FloatTryParse(mEyeBall.Groups[2].Value);
                tbody.trsEyeL.localScale = new Vector3(1f, h, w);
                tbody.trsEyeR.localScale = new Vector3(1f, h, w);
            }
        }

        // 目のサイズ・角度変更
        // EyeScaleRotate : 目のサイズと角度変更する CM3D.MaidVoicePich.Plugin.cs の追加メソッド
        // http://pastebin.com/DBuN5Sws
        // その１>>923
        // http://jbbs.shitaraba.net/bbs/read.cgi/game/55179/1438196715/923
        void EyeScaleRotate(Maid maid, string freeComment)
        {
            Transform tL = null;
            Transform tR = null;

            for (int i = 0; i < maid.body0.bonemorph.bones.Count; i++)
            {
                if (maid.body0.bonemorph.bones[i].linkT == null) continue;
                if (maid.body0.bonemorph.bones[i].linkT.name == "Eyepos_L") tL = maid.body0.bonemorph.bones[i].linkT;
                if (maid.body0.bonemorph.bones[i].linkT.name == "Eyepos_R") tR = maid.body0.bonemorph.bones[i].linkT;
            }
            if (tL == null || tR == null) return;

            Match scl = Regex.Match(freeComment, @"#TEST_EYE_RATIO=([-+]?[0-9]*\.?[0-9]+)#");
            Match ang = Regex.Match(freeComment, @"#TEST_EYE_ANG=([-+]?[0-9]*\.?[0-9]+),([-+]?[0-9]*\.?[0-9]+),([-+]?[0-9]*\.?[0-9]+)#");

            if (scl.Groups.Count >= 2)
            {
                float aspectRatioMin = 0.1f;
                float aspectRatioMax = 10f;
                float aspectRatio = Mathf.Clamp(Helper.FloatTryParse(scl.Groups[1].Value), aspectRatioMin, aspectRatioMax);

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
                Vector3 sclrvs = new Vector3(1.00f, aspectH, aspectW);
                tL.localScale = Vector3.Scale(tL.localScale, sclrvs);
                tR.localScale = Vector3.Scale(tR.localScale, sclrvs);
            }

            if (ang.Groups.Count >= 4)
            {
                float ra = Helper.FloatTryParse(ang.Groups[1].Value);
                float rx = Helper.FloatTryParse(ang.Groups[2].Value) / 1000.0f;
                float ry = Helper.FloatTryParse(ang.Groups[3].Value) / 1000.0f;

                if (ra <= -999f)
                {
                    ra = Time.frameCount / 60.0f / 10.0f * Mathf.PI * 2.0f;
                    ra = Mathf.Sin(ra) * 30.0f;
                }

                rx += -9f / 1000f;
                ry += -17f / 1000f;

                {
                    //  プリティフェイス左目tL  rx =  -9.0, ry = -17.0
                    //  初代左目tL              rx = -13.0, ry = -25.0
                    Transform tLparent = tL.parent;
                    Vector3 localCenter = tL.localPosition + (new Vector3(0f, ry, rx)); // ローカル座標系での回転中心位置
                    Vector3 worldCenter = tLparent.TransformPoint(localCenter);         // ワールド座標系での回転中心位置
                    Vector3 localAxis = new Vector3(-1f, 0f, 0f);                       // ローカル座標系での回転軸
                    Vector3 worldAxis = tL.TransformDirection(localAxis);               // ワールド座標系での回転軸

                    tL.localRotation = new Quaternion(-0.00560432f, -0.001345155f, 0.06805823f, 0.9976647f);    // 初期の回転量
                    tL.RotateAround(worldCenter, worldAxis, ra);
                }

                {
                    Transform tRparent = tR.parent;
                    Vector3 localCenter = tR.localPosition + (new Vector3(0f, ry, -rx));    // ローカル座標系での回転中心位置
                    Vector3 worldCenter = tRparent.TransformPoint(localCenter);             // ワールド座標系での回転中心位置
                    Vector3 localAxis = new Vector3(-1f, 0f, 0f);                           // ローカル座標系での回転軸
                    Vector3 worldAxis = tR.TransformDirection(localAxis);                   // ワールド座標系での回転軸

                    tR.localRotation = new Quaternion(0.9976647f, 0.06805764f, -0.001350592f, -0.005603582f);   // 初期の回転量
                    tR.RotateAround(worldCenter, worldAxis, -ra);
                }
            }
            else
            {
                if (bEditScene)
                {
                    tL.localRotation = new Quaternion(-0.00560432f, -0.001345155f, 0.06805823f, 0.9976647f);
                    tR.localRotation = new Quaternion(0.9976647f, 0.06805764f, -0.001350592f, -0.005603582f);
                }
            }
        }

        // 下半身の大きさを調整
        // LowerBodyScale : 下半身の大きさを調整する CM3D.MaidVoicePitch.Plugin.cs の追加メソッド
        // http://pastebin.com/VDZVUQED
        // http://pastebin.com/D25zzGSz
        // http://pastebin.com/TaNfrY8d
        // その１>>961, >>970
        // http://jbbs.shitaraba.net/bbs/read.cgi/game/55179/1438196715/961
        // http://jbbs.shitaraba.net/bbs/read.cgi/game/55179/1438196715/970
        //
        // CM3D.MaidVoicePitch.Plugin を適用し #WIDESLIDER# を有効にした状態で、メイドのフリーコメント欄に、
        // #PELSCL=1.0,1.0,1.0# の記述で骨盤周りのサイズ調整。表記順は、幅,奥行き,高さ。
        // #THISCL=1.0,1.0# の記述で足全体のサイズ調整。表記順は、幅,奥行き。
        // #THIPOS=0.0,0.0# の記述で足全体の位置調整。表記順は、左右,前後。
        void LowerBodyScaleAwake()
        {
            string tag = "sintyou";
            string bname = "Bip01 ? Thigh";
            string key = string.Concat("min+" + tag, "*", bname.Replace('?', 'L'));

            if (BoneMorph.dic.ContainsKey(key))
            {
                return;
            }

            BoneMorphSetScale(tag, bname, 1f, 1f, 1f, 1f, 1f, 1f);
        }

        static void BoneMorphSetScale(string tag, string bname, float x, float y, float z, float x2, float y2, float z2)
        {
            // class BoneMorph { private static void SetScale(string, string, float, float, float, float, float, float); }
            MethodInfo methodInfo = typeof(BoneMorph).GetMethod(
                "SetScale",
                BindingFlags.NonPublic | BindingFlags.Static,
                null,
                new Type[] { typeof(string), typeof(string), typeof(float), typeof(float), typeof(float), typeof(float), typeof(float), typeof(float) },
                null
            );
            methodInfo.Invoke(null, new object[] { tag, bname, x, y, z, x2, y2, z2 });
        }

        void LowerBodyScale(Maid maid, string freeComment)
        {
            Match pscl = Regex.Match(freeComment, @"#TEST_PELSCL=([-+]?[0-9]*\.?[0-9]+),([-+]?[0-9]*\.?[0-9]+),([-+]?[0-9]*\.?[0-9]+)#");
            Match tscl = Regex.Match(freeComment, @"#TEST_THISCL=([-+]?[0-9]*\.?[0-9]+),([-+]?[0-9]*\.?[0-9]+)#");
            Match tpos = Regex.Match(freeComment, @"#TEST_THIPOS=([-+]?[0-9]*\.?[0-9]+),([-+]?[0-9]*\.?[0-9]+)#");

            if (pscl.Groups.Count < 4 && tscl.Groups.Count < 3 && tpos.Groups.Count < 3) return;

            BoneMorph_ bm_ = maid.body0.bonemorph;

            if (pscl.Groups.Count >= 4)
            {
                List<Transform> tListP = new List<Transform>();
                List<Transform> tListHL = new List<Transform>();
                List<Transform> tListHR = new List<Transform>();
                for (int i = 0; i < bm_.bones.Count; i++)
                {
                    if (bm_.bones[i].linkT == null) continue;
                    if (bm_.bones[i].linkT.name == "Bip01 Pelvis_SCL_") tListP.Add(bm_.bones[i].linkT);
                    if (bm_.bones[i].linkT.name == "Hip_L") tListHL.Add(bm_.bones[i].linkT);
                    if (bm_.bones[i].linkT.name == "Hip_R") tListHR.Add(bm_.bones[i].linkT);
                }
                if (tListP.Count < 1 || tListHL.Count < 1 || tListHR.Count < 1) return;

                float mw = Helper.FloatTryParse(pscl.Groups[1].Value);
                float md = Helper.FloatTryParse(pscl.Groups[2].Value);
                float mh = Helper.FloatTryParse(pscl.Groups[3].Value);

                Vector3 sclrvs = new Vector3(mh, md, mw);

                foreach (Transform t in tListP) t.localScale = Vector3.Scale(t.localScale, sclrvs);
                foreach (Transform t in tListHL) t.localScale = Vector3.Scale(t.localScale, sclrvs);
                foreach (Transform t in tListHR) t.localScale = Vector3.Scale(t.localScale, sclrvs);
            }
            if (tscl.Groups.Count >= 3)
            {
                List<Transform> tListTL = new List<Transform>();
                List<Transform> tListTR = new List<Transform>();
                for (int i = 0; i < bm_.bones.Count; i++)
                {
                    if (bm_.bones[i].linkT.name == "Bip01 L Thigh") tListTL.Add(bm_.bones[i].linkT);
                    if (bm_.bones[i].linkT.name == "Bip01 R Thigh") tListTR.Add(bm_.bones[i].linkT);
                }
                if (tListTL.Count < 1 || tListTR.Count < 1) return;

                float mw = Helper.FloatTryParse(tscl.Groups[1].Value);
                float md = Helper.FloatTryParse(tscl.Groups[2].Value);
                float mh = 1.0f;

                Vector3 sclrvs = new Vector3(mh, md, mw);

                foreach (Transform t in tListTL) t.localScale = Vector3.Scale(t.localScale, sclrvs);
                foreach (Transform t in tListTR) t.localScale = Vector3.Scale(t.localScale, sclrvs);
            }
            if (tpos.Groups.Count >= 3)
            {
                List<Transform> tListTL = new List<Transform>();
                List<Transform> tListTR = new List<Transform>();
                List<Transform> tListHL = new List<Transform>();
                List<Transform> tListHR = new List<Transform>();
                for (int i = 0; i < bm_.bones.Count; i++)
                {
                    if (bm_.bones[i].linkT == null) continue;
                    if (bm_.bones[i].linkT.name == "Hip_L") tListHL.Add(bm_.bones[i].linkT);
                    if (bm_.bones[i].linkT.name == "Hip_R") tListHR.Add(bm_.bones[i].linkT);
                    if (bm_.bones[i].linkT.name == "Bip01 L Thigh") tListTL.Add(bm_.bones[i].linkT);
                    if (bm_.bones[i].linkT.name == "Bip01 R Thigh") tListTR.Add(bm_.bones[i].linkT);
                }
                if (tListHL.Count < 1 || tListHR.Count < 1 || tListTL.Count < 1 || tListTR.Count < 1) return;

                float dx = Helper.FloatTryParse(tpos.Groups[1].Value);
                float dz = Helper.FloatTryParse(tpos.Groups[2].Value);
                float dy = 0.0f;

                Vector3 posrvsL = new Vector3(dy, dz / 1000f, -dx / 1000f);
                Vector3 posrvsR = new Vector3(dy, dz / 1000f, dx / 1000f);

                foreach (Transform t in tListTL) t.localPosition += posrvsL;
                foreach (Transform t in tListTR) t.localPosition += posrvsR;
                foreach (Transform t in tListHL) t.localPosition += posrvsL;
                foreach (Transform t in tListHR) t.localPosition += posrvsR;
            }
        }

        // スライダー範囲を拡大
        void WideSlider(Maid maid, string freeComment)
        {
            bool bSlider = freeComment.Contains("#WIDESLIDER#") ||
                freeComment.Contains("#TEST_WIDE_SLIDER#");

            if (!bSlider)
            {
                return;
            }

            float sliderScale = 20f;

            TBody tbody = maid.body0;
            if (tbody == null || tbody.bonemorph == null || tbody.bonemorph.bones == null)
            {
                return;
            }

            BoneMorph_ boneMorph_ = tbody.bonemorph;

            string[] PropNames = Helper.GetInstanceField(typeof(BoneMorph_), boneMorph_, "PropNames") as string[];
            if (PropNames == null)
            {
                return;
            }

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
                            s = boneMorph_.SCALE_Eye;
                            break;
                        case 3:
                            s = boneMorph_.Postion_EyeX * (0.5f + boneMorph_.Postion_EyeY * 0.5f);
                            break;
                        case 4:
                            s = boneMorph_.Postion_EyeY;
                            break;
                        case 5:
                            s = boneMorph_.SCALE_HeadX;
                            break;
                        case 6:
                            s = boneMorph_.SCALE_HeadY;
                            break;
                        case 7:
                            s = boneMorph_.SCALE_DouPer;
                            if (boneMorphLocal.Kahanshin == 0f)
                            {
                                s = 1f - s;
                            }
                            break;
                        case 8:
                            s = boneMorph_.SCALE_Sintyou;
                            break;
                        case 9:
                            s = boneMorph_.SCALE_Koshi;
                            break;
                        case 10:
                            s = boneMorph_.SCALE_Kata;
                            break;
                        case 11:
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
                        scl = Vector3.Scale(scl, Vector3.Lerp(n0, n1, f));
                        //  scl = Vector3.Scale(scl, Vector3.Lerp(v0, v1, s));
                    }

                    if ((boneMorphLocal.atr & 1 << (j + 16 & 31)) != 0)
                    {
                        Vector3 v0 = boneMorphLocal.vecs_min[j + 16];
                        Vector3 v1 = boneMorphLocal.vecs_max[j + 16];

                        Vector3 n0 = v0 * sliderScale - v1 * (sliderScale - 1f);
                        Vector3 n1 = v1 * sliderScale - v0 * (sliderScale - 1f);
                        float f = (s + sliderScale - 1f) * (1f / (sliderScale * 2.0f - 1f));
                        pos = Vector3.Scale(pos, Vector3.Lerp(n0, n1, f));

                        //  pos = Vector3.Scale(pos, Vector3.Lerp(v0, v1, s));
                    }
                }

                if (boneMorphLocal.linkT == null)
                {
                    continue;
                }

                if (boneMorphLocal.linkT.name != null && boneMorphLocal.linkT.name.Contains("Thigh_SCL_"))
                {
                    boneMorph_.SnityouOutScale = Mathf.Pow(scl.x, 0.9f);
                }
                boneMorphLocal.linkT.localPosition = pos;
                boneMorphLocal.linkT.localScale = scl;
            }
        }

        // スライダー範囲拡大を指定するテンプレートファイルの読み込み (テスト中)
        void TestSliderTemplate(Maid maid, string freeComment)
        {
            // エディット画面以外では無視
            if (Application.loadedLevel != 5)
            {
                return;
            }

            string fname = null;
            {
                Match m = Regex.Match(freeComment, @"#TEST_SLIDER_TEMPLATE=([^#]*)#");
                if (m.Groups.Count >= 2)
                {
                    fname = m.Groups[1].Value;
                }
            }
            if (fname == null)
            {
                return;
            }

            SliderTemplate sliderTemplate = null;
            if (!sliderTemplates.TryGetValue(fname, out sliderTemplate))
            {
                sliderTemplate = SliderTemplate.Load(fname);
                if (sliderTemplate == null)
                {
                    return;
                }
                sliderTemplates[fname] = sliderTemplate;
            }

            foreach (var kv in sliderTemplate.Sliders)
            {
                string name = kv.Key;
                SliderTemplate.Slider slider = kv.Value;
                MPN mpn = Helper.ToEnum<MPN>(name, MPN.null_mpn);
                if (mpn != MPN.null_mpn)
                {
                    MaidProp maidProp = maid.GetProp(mpn);
                    maidProp.min = (int)slider.min;
                    maidProp.max = (int)slider.max;
                }
            }
        }

        // リップシンク強度指定
        void TestLipSync(Maid maid, string freeComment)
        {
            Match m = Regex.Match(freeComment, @"#TEST_LIPSYNC=([-+]?[0-9]*\.?[0-9]+)#");
            if (m.Groups.Count >= 2)
            {
                float f1 = Mathf.Clamp01(Helper.FloatTryParse(m.Groups[1].Value));
                TBody tbody = maid.body0;
                if (tbody != null)
                {
                    maid.VoicePara_1 = f1 * 0.5f;
                    maid.VoicePara_2 = f1 * 0.074f;
                    maid.VoicePara_3 = f1 * 0.5f;
                    maid.VoicePara_4 = f1 * 0.05f;
                    if (f1 < 0.01f)
                    {
                        maid.voice_ao_f2 = 0;
                    }
                }
            }
        }

        // 骨盤部コリジョン指定 (空中に浮くので注意)
        void TestPelvis(Maid maid, string freeComment)
        {
            TBody tbody = maid.body0;
            Match m = Regex.Match(freeComment, @"#TEST_PELVIS=([-+]?[0-9]*\.?[0-9]+),([-+]?[0-9]*\.?[0-9]+),([-+]?[0-9]*\.?[0-9]+)#");
            if (m.Groups.Count >= 4 && tbody != null && tbody.Pelvis != null)
            {
                float x = Helper.FloatTryParse(m.Groups[1].Value);
                float y = Helper.FloatTryParse(m.Groups[2].Value);
                float z = Helper.FloatTryParse(m.Groups[3].Value);
                tbody.Pelvis.localScale = new Vector3(x, y, z);
            }
        }

        //
        void TestPropSet()
        {
            if (!bKagTagPropSetHooked)
            {
                OverWriteTagCallback("propset", delegate (string typeName, Type type, BaseKagManager baseKagManager, KagTagSupport tag_data)
                {
                    return TagPropSet(typeName, type, baseKagManager, tag_data);
                });

                OverWriteTagCallback("faceblend", delegate (string typeName, Type type, BaseKagManager baseKagManager, KagTagSupport tag_data)
                {
                    return TagFaceBlend(typeName, type, baseKagManager, tag_data);
                });

                OverWriteTagCallback("face", delegate (string typeName, Type type, BaseKagManager baseKagManager, KagTagSupport tag_data)
                {
                    return TagFace(typeName, type, baseKagManager, tag_data);
                });

                OverWriteTagCallback("eyetocamera", delegate (string typeName, Type type, BaseKagManager baseKagManager, KagTagSupport tag_data)
                {
                    return TagEyeToCamera(typeName, type, baseKagManager, tag_data);
                });

                bKagTagPropSetHooked = true;
            }
        }

        static void OverWriteTagCallback(string tagName, TagProcDelegate tagProcDelegate)
        {
            foreach (var kv in GameMain.Instance.ScriptMgr.kag_mot_dic)
            {
                BaseKagManager mgr = kv.Value;
                KagScript kag = mgr.kag;
                kag.RemoveTagCallBack(tagName);
                kag.AddTagCallBack(tagName, new KagScript.KagTagCallBack(delegate (KagTagSupport tag_data)
                {
                    return tagProcDelegate("MotionKagManager", typeof(MotionKagManager), mgr, tag_data);
                }));
            }

            {
                BaseKagManager mgr = GameMain.Instance.ScriptMgr.adv_kag;
                KagScript kag = mgr.kag;
                kag.RemoveTagCallBack(tagName);
                kag.AddTagCallBack(tagName, new KagScript.KagTagCallBack(delegate (KagTagSupport tag_data)
                {
                    return tagProcDelegate("ADVKagManager", typeof(ADVKagManager), mgr, tag_data);
                }));
            }

            {
                BaseKagManager mgr = GameMain.Instance.ScriptMgr.yotogi_kag;
                KagScript kag = mgr.kag;
                kag.RemoveTagCallBack(tagName);
                kag.AddTagCallBack(tagName, new KagScript.KagTagCallBack(delegate (KagTagSupport tag_data)
                {
                    return tagProcDelegate("YotogiKagManager", typeof(YotogiKagManager), mgr, tag_data);
                }));
            }
        }

        public bool TagPropSet(string dbg, Type type, BaseKagManager baseKagManager, KagTagSupport tag_data)
        {
            baseKagManager.CheckAbsolutelyNecessaryTag(tag_data, "propset", new string[] { "category", "val" });

            bool flag = tag_data.IsValid("temp");
            string str = tag_data.GetTagProperty("category").AsString();
            int num = tag_data.GetTagProperty("val").AsInteger();

            Maid maidAndMan = GetMaidAndMan(tag_data);
            if (maidAndMan == null)
            {
                return false;
            }

            if (IsPropSetOff(maidAndMan.name))
            {
                foreach (MPN mpn in Enum.GetValues(typeof(MPN)))
                {
                    if (mpn.ToString("G") == str)
                    {
                        return false;
                    }
                }
            }
            maidAndMan.SetProp(str, num, flag);
            return false;
        }

        public bool TagFaceBlend(string dbg, Type type, BaseKagManager baseKagManager, KagTagSupport tag_data)
        {
            baseKagManager.CheckAbsolutelyNecessaryTag(tag_data, "faceblend", new string[] { "name" });
            string str = tag_data.GetTagProperty("name").AsString();
            Maid maidAndMan = GetMaidAndMan(tag_data);
            if (maidAndMan == null)
            {
                return false;
            }
            if (str == "なし")
            {
                str = "無し";
            }
            string str1 = ProcFaceBlendName(maidAndMan, str);
            maidAndMan.FaceBlend(str1);
            return false;
        }

        public bool TagFace(string dbg, Type type, BaseKagManager baseKagManager, KagTagSupport tag_data)
        {
            baseKagManager.CheckAbsolutelyNecessaryTag(tag_data, "face", new string[] { "name" });
            string str0 = tag_data.GetTagProperty("name").AsString();
            Maid maidAndMan = GetMaidAndMan(tag_data);
            if (maidAndMan == null)
            {
                return false;
            }
            string str = ProcFaceName(maidAndMan, str0);
            int num = 0;
            if (tag_data.IsValid("wait"))
            {
                num = tag_data.GetTagProperty("wait").AsInteger();
            }
            WaitEventList waitEventList = GetWaitEventList(baseKagManager, "face");
            if (num > 0)
            {
                waitEventList.Add(() =>
                {
                    if (maidAndMan != null && maidAndMan.body0 != null && maidAndMan.body0.isLoadedBody)
                    {
                        maidAndMan.FaceAnime(str, 1f / GetFaceAnimeSpeed(maidAndMan.name), 0);
                    }
                }, num);
            }
            else
            {
                maidAndMan.FaceAnime(str, 1f / GetFaceAnimeSpeed(maidAndMan.name), 0);
                waitEventList.Clear();
            }
            return false;
        }

        public bool TagEyeToCamera(string dbg, Type type, BaseKagManager baseKagManager, KagTagSupport tag_data)
        {
            Maid maidAndMan = GetMaidAndMan(tag_data);
            if (maidAndMan == null)
            {
                return false;
            }
            if (IsEyeToCameraOff(maidAndMan.name))
            {
                return false;
            }
            baseKagManager.CheckAbsolutelyNecessaryTag(tag_data, "eyetocamera", new string[] { "move" });
            string str = tag_data.GetTagProperty("move").AsString();
            Maid.EyeMoveType eyeMoveType = Maid.EyeMoveType.無し;
            try
            {
                eyeMoveType = (Maid.EyeMoveType)((int)Enum.Parse(typeof(Maid.EyeMoveType), str));
            }
            catch (ArgumentException)
            {
                NDebug.Assert(string.Concat("Maid.EyeMoveType\nenum parse error.[", str, "]"));
            }
            int num = 500;
            if (tag_data.IsValid("blend"))
            {
                num = tag_data.GetTagProperty("blend").AsInteger();
            }
            maidAndMan.EyeToCamera(eyeMoveType, GameUty.MillisecondToSecond(num));
            return false;
        }

        public string ProcFaceName(Maid maid, string faceName)
        {
            FaceScriptTemplate t = GetFaceScriptTemplate(maid.name);
            if (t == null)
            {
                return faceName;
            }
            return t.ProcFaceName(faceName);
        }

        public string ProcFaceBlendName(Maid maid, string faceBlendName)
        {
            FaceScriptTemplate t = GetFaceScriptTemplate(maid.name);
            if (t == null)
            {
                return faceBlendName;
            }
            return t.ProcFaceBlendName(faceBlendName);
        }

        static Maid GetMaidAndMan(KagTagSupport tag_data)
        {
            // class BaseKagManager protected static Maid MaidAndMan(KagTagSupport);
            MethodInfo methodInfo = typeof(BaseKagManager).GetMethod(
                "GetMaidAndMan",
                BindingFlags.NonPublic | BindingFlags.Static,
                null,
                new Type[] { typeof(KagTagSupport) },
                null
            );
            object obj = methodInfo.Invoke(null, new object[] { tag_data });
            return obj as Maid;
        }

        static WaitEventList GetWaitEventList(BaseKagManager baseKagManager, string list_name)
        {
            // class BaseKagManager protected WaitEventList GetWaitEventList(string list_name)
            MethodInfo methodInfo = typeof(BaseKagManager).GetMethod(
                "GetWaitEventList",
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                new Type[] { typeof(string) },
                null
            );
            object obj = methodInfo.Invoke(baseKagManager, new object[] { list_name });
            return obj as WaitEventList;
        }
    }

    public class FaceScriptTemplate
    {
        public Dictionary<string, string> FaceBlends { get; set; }
        public Dictionary<string, string> Faces { get; set; }

        public FaceScriptTemplate()
        {
            FaceBlends = new Dictionary<string, string>();
            Faces = new Dictionary<string, string>();
        }

        public static FaceScriptTemplate Load(string fileName)
        {
            var result = new FaceScriptTemplate();
            var xd = new XmlDocument();
            try
            {
                xd.Load(fileName);
                foreach (XmlNode e in xd.SelectNodes("/facescripttemplate/faceblends/faceblend"))
                {
                    result.FaceBlends[e.Attributes["key"].Value] = e.Attributes["value"].Value;
                }
                foreach (XmlNode e in xd.SelectNodes("/facescripttemplate/faces/face"))
                {
                    result.Faces[e.Attributes["key"].Value] = e.Attributes["value"].Value;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("CM3D2.MaidVoicePitch.Plugin.FaceScriptTemplate.Load({0}) exception error -> " + e.Message);
            }
            return result;
        }

        public string ProcFaceName(string faceName)
        {
            string s;
            if (Faces.TryGetValue(faceName, out s))
            {
                return s;
            }
            return faceName;
        }

        public string ProcFaceBlendName(string faceBlendName)
        {
            string s;
            if (FaceBlends.TryGetValue(faceBlendName, out s))
            {
                return s;
            }
            return faceBlendName;
        }
    }

    public class SliderTemplate
    {
        public class Slider
        {
            public float min;
            public float max;
        }

        public Dictionary<string, Slider> Sliders { get; set; }

        public SliderTemplate()
        {
            Sliders = new Dictionary<string, Slider>();
        }

        public static SliderTemplate Load(string fileName)
        {
            var result = new SliderTemplate();
            var xd = new XmlDocument();
            try
            {
                xd.Load(fileName);
                foreach (XmlNode e in xd.SelectNodes("/slidertemplate/sliders/slider"))
                {
                    result.Sliders[e.Attributes["name"].Value] = new Slider
                    {
                        min = Helper.FloatTryParse(e.Attributes["min"].Value),
                        max = Helper.FloatTryParse(e.Attributes["max"].Value)
                    };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("CM3D2.MaidVoicePitch.Plugin.SliderTemplate.Load({0}) exception error -> " + e.Message);
            }
            return result;
        }
    }

    public static class Helper
    {
        static StreamWriter logStreamWriter = null;
        static public DateTime now = DateTime.Now;

        static public void Log(string s)
        {
            if (logStreamWriter == null)
            {
                string filename = (@".\MaidVoicePitch_" + now.ToString("yyyyMMdd_HHmmss") + ".log");
                logStreamWriter = new StreamWriter(filename, true);
            }
            logStreamWriter.Write(s);
            logStreamWriter.Flush();
        }

        static public float FloatTryParse(string s)
        {
            float f = 0f;
            float.TryParse(s, out f);
            return f;
        }

        // http://stackoverflow.com/a/1082587/2132223
        public static TEnum ToEnum<TEnum>(this string strEnumValue, TEnum defaultValue)
        {
            if (!Enum.IsDefined(typeof(TEnum), strEnumValue))
                return defaultValue;
            return (TEnum)Enum.Parse(typeof(TEnum), strEnumValue);
        }

        // http://stackoverflow.com/a/3303182/2132223
        static public FieldInfo GetFieldInfo(Type type, string fieldName)
        {
            return type.GetField(fieldName,
                                 BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        }

        static public object GetInstanceField(Type type, object instance, string fieldName)
        {
            FieldInfo field = GetFieldInfo(type, fieldName);
            return field == null ? null : field.GetValue(instance);
        }

        static public void SetInstanceField(Type type, object instance, string fieldName, object val)
        {
            FieldInfo field = GetFieldInfo(type, fieldName);
            if (field != null)
            {
                field.SetValue(instance, val);
            }
        }
    }
}