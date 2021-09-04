using HarmonyLib;
using RimWorld;
using Verse;

namespace BuriedAlive
{
    public static class Refs
    {
        public static AccessTools.FieldRef<Building_Grave, Graphic> r_Building_Grave_cachedGraphicFull =
                AccessTools.FieldRefAccess<Building_Grave, Graphic>("cachedGraphicFull");
    }
}
