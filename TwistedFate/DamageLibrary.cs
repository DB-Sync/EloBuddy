﻿namespace TwistedBuddy
{
    using EloBuddy;
    using EloBuddy.SDK;

    class DamageLibrary
    {
        public static float CalculateDamage(Obj_AI_Base target, bool Q, bool W, bool E, bool R)
        {
            var totaldamage = 0f;

            if (target != null)
            {
                if (Q && Program.Q.IsReady())
                {
                    totaldamage = totaldamage + QDamage(target);
                }

                if (W && Program.W.IsReady())
                {
                    totaldamage = totaldamage + WDamage(target, Cards.None);
                }

                if (E && Program.E.IsReady())
                {
                    totaldamage = totaldamage + EDamage(target);
                }

                if (R && Program.R.IsReady())
                {
                    totaldamage = totaldamage + RDamage();
                }
            }

            return totaldamage;
        }
        public static float PredictWDamage(Obj_AI_Base target, Cards card)
        {
            if (target != null)
            {
                return WDamage(target, card);
            }
            return 0f;
        }

        private static float QDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, new[] { 0, 60, 105, 150, 195, 240 }[Program.Q.Level]) + (Player.Instance.TotalMagicalDamage * 0.65f);
        }

        private static float WDamage(Obj_AI_Base target, Cards card)
        {
            if (Player.Instance.HasBuff("bluecardpreattack") || Cards.Blue.Equals(card))
            {
                return Player.Instance.CalculateDamageOnUnit(
                    target,
                    DamageType.Mixed,
                    new[] {0, 40, 60, 80, 100, 120}[Program.W.Level]) + (Player.Instance.TotalMagicalDamage*0.5f)
                       + (Player.Instance.TotalAttackDamage);
            }
            if (Player.Instance.HasBuff("redcardpreattack") || Cards.Red.Equals(card))
            {
                return Player.Instance.CalculateDamageOnUnit(
                    target,
                    DamageType.Mixed,
                    new[] { 0, 30, 45, 60, 75, 90 }[Program.W.Level] + (Player.Instance.TotalMagicalDamage * 0.5f)
                    + Player.Instance.TotalAttackDamage);
            }
            if (Player.Instance.HasBuff("goldcardpreattack") || Cards.Yellow.Equals(card))
            {
                return Player.Instance.CalculateDamageOnUnit(
                    target,
                    DamageType.Mixed,
                    new[] { 0, 15, 22.5f, 30, 37.5f, 45 }[Program.W.Level] + (Player.Instance.TotalMagicalDamage * 0.5f)
                    + Player.Instance.TotalAttackDamage);
            }
            return 0;
        }

        private static float EDamage(Obj_AI_Base target)
        {
            if (Player.Instance.HasBuff("cardmasterstackparticle"))
            {
                return Player.Instance.CalculateDamageOnUnit(
                    target,
                    DamageType.Magical,
                    new[] { 0, 55, 80, 105, 130, 155 }[Program.E.Level]) + (Player.Instance.TotalMagicalDamage * 0.5f);
            }
            return 0;
        }

        private static float RDamage()
        {
            return 0;
        }
    }
}
