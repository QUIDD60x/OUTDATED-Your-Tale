﻿using Terraria;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using yourtale.Tiles.Ores;

namespace yourtale.Items.Placeables
{
    public class Cryolite : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cryolite sample");
            Tooltip.SetDefault("irratates my skin! feels like ice..."); // \n = new line
        }
        public override void SetDefaults()
        {
            item.Size = new Vector2(12);
            item.rare = ItemRarityID.Blue;
            item.value = Item.sellPrice(copper: 2);

            item.autoReuse = true;
            item.useTurn = true;
            item.useTime = 10;
            item.useAnimation = 12;
            item.useStyle = ItemUseStyleID.SwingThrow;

            item.consumable = true;
            item.maxStack = 999;

            item.createTile = TileType<Tiles.Ores.Cryolite>();
        }
    }
}