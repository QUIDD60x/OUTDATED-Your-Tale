﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader.Config;

namespace yourtale
{
    [Label("Your Tale Gameconfig [CURRENTLY USELESS]")]
    class TestConfig : ModConfig
    {
        [JsonIgnore]
        public const string ConfigName = "Game";

        public override bool Autoload(ref string name)
        {
            name = ConfigName;
            return base.Autoload(ref name);
        }

        public override ConfigScope Mode => ConfigScope.ServerSide;

        public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message)
        {
            message = Language.GetTextValue("Mods.VanillaTweaks.Config.ServerBlocked");
            return false;
        }
        [Label("$ToolTweaks")]
        [Tooltip("$This nerfs some vanilla weapons, including the copper pick and stuff at the begining of the game.")]
        [DefaultValue(true)]
        public bool ToolTweaks;

        [Label("$Extractor Speed")]
        [Tooltip("$Manipulates extractor speed")]
        [Range(1f, 5f)]
        [DefaultValue(5f)]
        public float ExtractSpeedMultiplier;

        [Label("$Difficulty button")]
        [Tooltip("$Go ahead, turn it down.")]
        [ReloadRequired]
        [Range(0, 100)]
        [DefaultValue(69f)]
        public int MolotovBlueGelCraft;

        [Label("$Enable Monet")]
        [Tooltip("$This currently does not do anything yet, but will enable a custom currency once implemented.")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool CoinRecipesAtEndofList;

    }

}
