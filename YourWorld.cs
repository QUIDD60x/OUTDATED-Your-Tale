﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.World.Generation;
using yourtale.Tiles.Furniture;
using static Terraria.ModLoader.ModContent;
using System.IO;
using Terraria.Localization;
using Terraria.ModLoader.IO;
using yourtale.Tiles.Ores;
using yourtale.NPCs.Evil.Boss;

namespace yourtale
{
    public class YourWorld : ModWorld
    {
        public static int StartPositionX = 0;
        public static int StartPositionY = 0;
        public static int Bed = 0;
        public static int Table = 0;
        public static int Wood = TileID.WoodBlock;
        public static int WoodWall = 4;
        public static int StoneWall = 5;
        public static int Brick = 38;
        public static int WoodTile = 106;
        public static int Door = 0;
        public static int Platform = 0;
        public static int Stone = TileID.Stone;
        public static int LivingWoodWall = 78;
        public static int PlankedWall = 27;
        public static int StoneSlab = 273;
        public static int StoneSlabWall = 147;
        public static int Fence = 106;
        public static int Grass = 2;
        public static int Chair = 0;
        private static bool GenerateHouse = false;
        public static bool downedCryolisis;

              //0=air, 1=dirt/snow/ice, 2=wood, 3=stone brick, 4=stone, 5=platform, 6=stone slab, 7=grass		
        static readonly byte[,] GuideHouse =
        {
			{1,1,4,4,4,1,3,3,3,3,3,3,3,3,3,1,1,1,1,1,1,1,1,1},
			{1,1,4,4,1,1,3,6,6,6,6,6,6,6,3,4,4,4,1,1,4,4,1,1},
            {1,1,1,1,1,1,3,6,0,0,0,0,0,6,3,1,4,4,1,4,4,1,1,4},
            {1,1,1,1,1,4,3,6,0,0,0,0,0,6,3,1,1,1,4,4,1,1,4,4},
            {7,7,7,3,3,3,3,6,0,0,6,6,6,6,3,3,3,3,3,3,3,1,1,4},
            {0,0,0,2,2,2,2,1,0,0,2,2,2,2,2,2,1,1,1,2,2,7,7,7},
            {8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8},
            {8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8},
            {8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8},
            {8,8,8,8,8,8,8,5,5,5,5,8,8,8,8,8,8,8,8,2,8,8,8,8},
            {8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,2,8,8,8,8},
            {8,8,8,8,2,2,8,8,8,8,8,8,8,8,8,8,8,8,2,2,8,8,8,8},
            {0,0,0,0,2,2,2,5,5,5,5,2,2,2,2,2,2,2,2,2,3,3,0,0},
			{0,0,0,0,3,2,2,0,0,0,0,0,0,0,0,0,0,2,2,3,3,3,0,0},
			{0,0,0,0,3,3,2,2,0,0,0,0,0,0,0,0,0,0,3,3,3,0,0,0},
			{0,0,0,0,0,3,3,2,2,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0},
			{0,0,0,0,0,3,3,3,2,2,0,0,0,0,2,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,3,3,3,2,2,0,0,2,2,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,3,3,3,2,2,2,2,3,3,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,3,3,3,2,2,3,3,3,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,0,3,3,3,3,0,0,0,0,0,0,0,0,0,0}
		};

        //0=air, 1=stone wall, 2=wooden wall, 3=living wood wall, 4=planked wall, 5=stone slab wall, 6=fence
        static readonly byte[,] GuideHouseWall =
        {
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,5,5,5,5,5,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,5,5,5,5,5,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,5,5,5,5,5,0,0,0,0,0,0,0,0,0,0,0},
			{6,6,6,6,6,0,0,2,5,5,2,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{6,0,6,6,6,2,3,2,2,2,2,0,0,0,0,0,2,3,2,6,6,6,6,6},
			{0,0,6,0,6,0,0,2,2,2,2,0,0,0,2,2,2,3,2,6,0,6,6,0},
			{0,0,6,0,0,0,0,0,2,2,2,2,0,0,2,2,2,3,2,0,0,0,6,0},
			{0,0,0,0,0,0,0,0,2,2,2,2,3,2,2,2,2,3,2,0,0,0,0,0},
			{0,0,0,0,0,0,3,2,2,2,2,2,3,2,2,2,2,3,2,0,0,0,0,0},
			{0,0,0,0,0,2,3,2,2,2,2,2,3,2,2,2,2,3,2,0,0,0,0,0},
			{0,0,0,0,0,0,4,3,4,4,3,4,4,3,4,4,3,4,0,0,0,0,0,0},
			{0,0,0,0,0,0,4,3,4,4,3,4,4,3,4,4,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,3,4,4,3,4,4,3,4,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,3,4,4,3,4,4,3,4,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,4,4,4,3,4,4,3,0,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,4,4,3,4,4,3,4,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,4,3,4,4,3,4,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
		};

        public override void Initialize()
        {
            GenerateHouse = false;
            downedCryolisis = false;
        }
       
        public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            downedCryolisis = downed.Contains("Cryolisis");
        }

        /*public override TagCompound Save()
        {
			var Downed = new List<string>();
            /*if (downedCryolisis) { downed.Add("Cryolisis"); }
            
            return new TagCompound
            {
                ["downed"] = downed,
            };
        }*/

        

        /*public override void LoadLegacy(BinaryReader reader)
        {
            int loadVersion = reader.ReadInt32();
            
            if (loadVersion == 0)
            {
                BitsByte Flags1 = reader.ReadByte();
                downedCryolisis = flags[0];
                //BitsByte flags2 = reader.ReadByte();
                //BitsByte flags3 = reader.ReadByte();
                //BitsByte flags4 = reader.ReadByte();
                //BitsByte flags5 = reader.ReadByte();
            }

            /*else
            {
                mod.Logger.WarnFormat("yourtale: Unknown loadVersion: {0}", loadVersion);
            }
        }*/

        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte();
            flags[0] = downedCryolisis;
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedCryolisis = flags[0];
        }

        /*//0=none, 1=bottom-left, 2=bottom-right, 3=top-left, 4=top-right, 5=half
        static readonly byte[,] GuideHouseSlopes =
        {
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,2,0,0,0,3,0,0,0,0,0,0,0,0,0,0,4,0,0,0,1,0,0},
			{0,0,0,2,0,0,0,3,0,0,0,0,0,0,0,0,4,0,0,0,1,0,0,0},
			{0,0,0,0,2,0,0,0,3,0,0,0,0,0,0,4,0,0,0,1,0,0,0,0},
			{0,0,0,0,0,2,0,0,0,3,0,0,0,0,4,0,0,0,1,0,0,0,0,0},
			{0,0,0,0,0,0,2,0,0,0,3,0,0,4,0,0,0,1,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,2,0,0,0,3,4,0,0,0,1,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,2,0,0,0,0,1,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,0,2,0,0,1,0,0,0,0,0,0,0,0,0,0}
		};*/

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight) //each task has a different name/stage,
        {
            int shiniesIndex = tasks.FindIndex(x => x.Name.Equals("Shinies"));
            if (shiniesIndex != -1)
            {
                tasks.Insert(shiniesIndex + 1, new PassLegacy("yourtale ore generation", OreGeneration));
            }
            int buriedChestIndex = tasks.FindIndex(x => x.Name.Equals("TestChest"));
            if (buriedChestIndex != -1)
            {
                tasks.Insert(buriedChestIndex + 1, new PassLegacy("yourtale chest generation", ChestGeneration));
            }
        }

        private void OreGeneration(GenerationProgress progress)
        {   //code running message
            progress.Message = "adding in Your Tale ores";
            // 6E-05 = 0.00006
            // So (Main.maxTilesX * Main.maxTilesY) * 0.00006
            // (4200 * 1200) * 0.00006 = 302.4
            for (int i = 0; i < (int)((Main.maxTilesX * Main.maxTilesY) * 9E-05); i++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next((int)WorldGen.worldSurfaceLow, Main.maxTilesY);

                //Higher numbers mean more, for some reason overlapping TileIDs will cause a lower chance ore to just not spawn, idk why atm. 
                Tile tile = Framing.GetTileSafely(x, y);
                if (tile.active() && (tile.type == TileID.Sand || tile.type == TileID.Dirt || tile.type == TileID.Mud))
                {
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(18, 35), WorldGen.genRand.Next(45, 80), TileType<Tiles.Ores.FlintDeposit>()); //worldgen numbers can and should be messed with untill you are happy
                }
                if (tile.active() && (tile.type == TileID.SnowBlock || tile.type == TileID.IceBlock))
                {
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(12, 24), WorldGen.genRand.Next(8, 45), TileType<Tiles.Ores.Cryolite>());
                }
                if (tile.active() && (tile.type == TileID.Stone || tile.type == TileID.ClayBlock || tile.type == TileID.Iron))
                {
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(2, 15), WorldGen.genRand.Next(15, 40), TileType<Tiles.Ores.Dolomite>());
                }
                if (tile.type == TileID.Emerald || tile.type == TileID.Amethyst || tile.type == TileID.Diamond || tile.type == TileID.Sapphire || tile.type == TileID.Topaz || tile.type == TileID.Ruby || tile.type == TileID.Gold || tile.type == TileID.Platinum || tile.type == TileID.Lead)
                {
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(80, 100), WorldGen.genRand.Next(80, 100), TileType<Tiles.Ores.Vigore>());
                }
                if (tile.type == TileID.Stone)
                {
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(5, 10), WorldGen.genRand.Next(6, 12), TileType<Tiles.Ores.Vigore>());
                }
            }
        }

        private void ChestGeneration(GenerationProgress progress)
        {
            progress.Message = "Adding YourTale loot into chests";
            for (int i = 0; i < 3; i++)
            {
                bool placeSuccessful = true;
                ushort tileToPlace = (ushort)TileType<Tiles.Furniture.TestChest>();
                int oldChestId = -1;
                int chestId = -1;
                while (!placeSuccessful)
                {
                    int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                    int y = WorldGen.genRand.Next(0, Main.maxTilesY);
                    oldChestId = chestId;
                    chestId = WorldGen.PlaceChest(x, y, tileToPlace, true, 1);
                    if (chestId != -1)
                    {
                        progress.Message = chestId.ToString();
                        Chest chest = Main.chest[chestId];
                        chest.item[1].SetDefaults(ItemType<Items.flint>());
                        chest.item[1].stack = WorldGen.genRand.Next(1, 1);
                        chest.item[1].SetDefaults(ItemType<Items.Weapons.Melee.THESWORD>());
                        chest.item[1].stack = WorldGen.genRand.Next(1, 1);
                        int index = 3;
                        switch (i)
                        {
                            case 0:
                                chest.item[2].SetDefaults(ItemID.BandofRegeneration);
                                break;
                            case 1:
                                chest.item[2].SetDefaults(ItemID.HermesBoots);
                                break;
                            default:
                                chest.item[2].SetDefaults(ItemID.MagicMirror);
                                break;
                        }

                        if (WorldGen.genRand.Next(3) == 0)
                        {
                            chest.item[index].SetDefaults(ItemID.Bomb);
                            chest.item[index].stack = WorldGen.genRand.Next(10, 20);
                            index++;
                        }
                        if (WorldGen.genRand.Next(2) == 0)
                        {
                            chest.item[index].SetDefaults(ItemID.Shuriken);
                            chest.item[index].stack = WorldGen.genRand.Next(30, 50);
                            index++;
                        }
                        yourtaleUtils.Log("Chest at {0}, {1}", x, y);
                        placeSuccessful = true;
                    }
                }
            }
        }
    }
}