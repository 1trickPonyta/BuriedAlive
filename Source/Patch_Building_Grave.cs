using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace BuriedAlive
{
    [HarmonyPatch(typeof(Building_Grave))]
    [HarmonyPatch("Accepts")]
    public static class Patch_Building_Grave_Accepts
	{
        public static void Postfix(Building_Grave __instance, ref Thing thing, ref bool __result) 
        {
            if (!__result)
            {
                if (thing is Pawn)
                {
                    __result = true;
                }
            }
		}
    }

    [HarmonyPatch(typeof(Building_Grave))]
    [HarmonyPatch("GetInspectString")]
    public static class Patch_Building_Grave_GetInspectString
    {
        public static bool Prefix(Building_Grave __instance, ref string __result)
        {
            Pawn pawn;
            if (__instance.HasCorpse && (pawn = __instance.ContainedThing as Pawn) != null) 
            {
                __result = $"{"CasketContains".Translate()}: {pawn.Label} (buried alive)";
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(Building_Grave))]
    [HarmonyPatch("get_Graphic")]
    public static class Patch_Building_Grave_get_Graphic
    {
        public static void Postfix(Building_Grave __instance, ref Graphic __result)
        {
            if (__instance.ContainedThing is Pawn && __result != Refs.r_Building_Grave_cachedGraphicFull(__instance) && __instance.def.building.fullGraveGraphicData != null)
            {
                if (Refs.r_Building_Grave_cachedGraphicFull(__instance) == null)
                {
                    Refs.r_Building_Grave_cachedGraphicFull(__instance) = __instance.def.building.fullGraveGraphicData.GraphicColoredFor(__instance);
                }
                __result = Refs.r_Building_Grave_cachedGraphicFull(__instance);
            }
        }
    }
}
