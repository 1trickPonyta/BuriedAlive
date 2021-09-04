using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace BuriedAlive
{
    [HarmonyPatch(typeof(Map))]
    [HarmonyPatch("MapPostTick")]
    public static class Patch_Map_MapPostTick
    {
        public static void Postfix(Map __instance)
        {
            List<Building> allGraves = new List<Building>();
            allGraves.AddRange(__instance.listerBuildings.AllBuildingsColonistOfDef(ThingDefOf.Grave));
            allGraves.AddRange(__instance.listerBuildings.AllBuildingsNonColonistOfDef(ThingDefOf.Grave));
            foreach (Building building in allGraves)
            {
                Pawn pawn;
                if ((pawn = ((Building_Grave)building).ContainedThing as Pawn) != null)
                {
                    pawn.health.HealthTick();
                }
            }
        }
    }
}
