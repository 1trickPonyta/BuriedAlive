using RimWorld;
using System;
using Verse;

namespace BuriedAlive
{
    public class Hediff_BuriedAlive : Hediff
    {
        private float severityPerInterval = 0.01f;

        public override void PostMake()
        {
            base.PostMake();
            severityPerInterval = Rand.Range(0.008f, 0.012f);
            Building_Grave grave = this.pawn.ParentHolder as Building_Grave;
            if (grave != null)
            {
                this.CrushPawn(grave);
            }
        }

        public override void Tick()
        {
            base.Tick();

            Building_Grave grave = this.pawn.ParentHolder as Building_Grave;

            if (this.pawn.IsHashIntervalTick(50))
            {
                if (grave != null)
                {
                    this.Severity += severityPerInterval;
                } 
                else
                {
                    this.Severity -= 0.001f / severityPerInterval;
                }
            }

            if (this.pawn.IsHashIntervalTick(250))
            {
                if (grave != null)
                {
                    this.CrushPawn(grave);
                }
            }
        }

        private void CrushPawn(Building_Grave instigator)
        {
            BodyPartRecord part = this.pawn.health.hediffSet.GetNotMissingParts(BodyPartHeight.Undefined, BodyPartDepth.Undefined, null, null).RandomElement();
            this.pawn.TakeDamage(new DamageInfo(DamageDefOf.Crush, Rand.Range(1f, 3f), 99999f, -1f, instigator, part, null, DamageInfo.SourceCategory.ThingOrUnknown, null, false, false));
        }
    }
}
