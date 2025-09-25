using RimWorld;
using System;
using System.Reflection;
using Verse;


namespace Lilly.MeteoriteMineablesPatch
{
    public class Settings : ModSettings
    {
        public static bool onDebug = true;

        public static int min = 25;
        public static int max = 100;

        public override void ExposeData()
        {
            MyLog.Message($"ST {Scribe.mode}");
            base.ExposeData();
            if (Scribe.mode != LoadSaveMode.Saving && Scribe.mode != LoadSaveMode.LoadingVars)
            {
                MyLog.Message($"ED {Scribe.mode}");
                return;
            }
            Scribe_Values.Look(ref onDebug, "onDebug", false);
            Scribe_Values.Look(ref min, "RangeMin",25);
            Scribe_Values.Look(ref max, "RangeMax",100);
            SetValue(min, max);
            MyLog.Message($"ED {Scribe.mode}");
        }

        public static void SetValue(int min = 25, int max = 100)
        {

            try
            {
                // 대상 필드 찾기
                FieldInfo field = typeof(ThingSetMaker_Meteorite).GetField("MineablesCountRange", BindingFlags.Public | BindingFlags.Static);

                if (field != null)
                {
                    var range = new IntRange(min, max); // 원하는 범위로 설정
                    // readonly 필드 강제 수정
                    field.SetValue(null, range); // 원하는 범위로 변경
                    MyLog.Message($"MineablesCountRange has been overridden to {range.min}~{range.max}");
                }
                else
                {
                    MyLog.Warning($"Failed to find MineablesCountRange field");
                }

            }
            catch (Exception e)
            {
                MyLog.Error($" Fail");
                MyLog.Error(e.ToString());
                MyLog.Error($" Fail");
                //throw;
            }
        }
    }
}
