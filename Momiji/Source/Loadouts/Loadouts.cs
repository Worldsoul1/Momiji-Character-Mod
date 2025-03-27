using System.Collections.Generic;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.EntityLib.Cards.Neutral.NoColor;
using Momiji.Source.Cards;
using Momiji.Source.Exhibits;
using Momiji.Source.Ultimate;
namespace Momiji.Source
{
    public class SampleCharacterLoadouts
    {
        public static string UltimateSkillA = nameof(RabiesBite);
        public static string UltimateSkillB = nameof(ExpelleesCanan);

        public static string ExhibitA = nameof(HeavyBlade);
        public static string ExhibitB = nameof(BatteredShield);
        public static List<string> DeckA = new List<string>{
            nameof(Shoot),
            nameof(Shoot),
            nameof(Boundary),
            nameof(Boundary),
            nameof(MomijiAttackR),
            nameof(MomijiAttackR), 
            nameof(MomijiBlockW), 
            nameof(MomijiBlockW), 
            nameof(MomijiBlockW),
            nameof(GuardBreak)
        };

        public static List<string> DeckB = new List<string>{
            nameof(Shoot),
            nameof(Shoot),
            nameof(Boundary),
            nameof(Boundary),
            nameof(MomijiAttackW),
            nameof(MomijiAttackW), 
            nameof(MomijiBlockR), 
            nameof(MomijiBlockR), 
            nameof(EyeforanEye),
            nameof(FarSight),
        };

        public static PlayerUnitConfig playerUnitConfig = new PlayerUnitConfig(
            Id: BepinexPlugin.modUniqueID,
            HasHomeName: true,
            ShowOrder: 8, 
            Order: 0,
            UnlockLevel: 0,
            ModleName: "",
            NarrativeColor: "#e58c27",
            IsSelectable: true,
            BasicRingOrder: 6,
            LeftColor: ManaColor.Red,
            RightColor: ManaColor.White,
            MaxHp: 80,
            InitialMana: new ManaGroup() { White = 2, Blue = 0, Black = 0, Red = 2, Green = 0, Colorless = 0, Philosophy = 0 },
            InitialMoney: 60,
            InitialPower: 0,
            UltimateSkillA: SampleCharacterLoadouts.UltimateSkillA,
            UltimateSkillB: SampleCharacterLoadouts.UltimateSkillB,
            ExhibitA: SampleCharacterLoadouts.ExhibitA,
            ExhibitB: SampleCharacterLoadouts.ExhibitB,
            DeckA: SampleCharacterLoadouts.DeckA,
            DeckB: SampleCharacterLoadouts.DeckB,
            DifficultyA: 1,
            DifficultyB: 2
        );
    }
}
