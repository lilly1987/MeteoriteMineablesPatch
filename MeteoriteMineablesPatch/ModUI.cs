using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;


namespace Lilly.MeteoriteMineablesPatch
{
    
    public class ModUI : Mod
    {
        public static ModUI self;
        public static Settings settings;

        public ModUI(ModContentPack content) : base(content)
        {
            self = this;
            MyLog.Message($"ST");
            settings = GetSettings<Settings>();// 주의. MainSettings의 patch가 먼저 실행됨      
            Settings.SetValue(Settings.min, Settings.max);
            MyLog.Message($"ED");            
        }

        Vector2 scrollPosition;
        string tmp;

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);

            var rect = new Rect(0, 0, inRect.width - 16, 1000);

            Widgets.BeginScrollView(inRect, ref scrollPosition, rect);

            Listing_Standard listing = new Listing_Standard();

            listing.Begin(rect);

            listing.GapLine();

            // ---------

            listing.CheckboxLabeled($"Debug", ref Settings.onDebug);

            listing.Label("자원 운석 최소 갯수".Translate(), tipSignal: ".".Translate());
            tmp = Settings.min.ToString();
            listing.TextFieldNumeric(ref Settings.min, ref tmp);

            listing.Label("자원 운석 최대 갯수".Translate(), tipSignal: ".".Translate());
            tmp = Settings.max.ToString();
            listing.TextFieldNumeric(ref Settings.max, ref tmp);

            // ---------

            listing.GapLine();

            listing.End();

            Widgets.EndScrollView();
        }

        public override string SettingsCategory()
        {
            return "자원 운석 모드".Translate();
        }

    }
}
