using yourtale.Tiles;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace yourtale.NPCs.Banners
{
    public class PrinceSlimeBanner : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ZombKing Banner");
        }
        // The tooltip for this item is automatically assigned from .lang files
        public override void SetDefaults()
        {
            item.width = 10;
            item.height = 24;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.rare = ItemRarityID.Blue;
            item.value = Item.buyPrice(0, 0, 10, 0);
            item.createTile = ModContent.TileType<MonsterBanner>();
            item.placeStyle = 1;        //Place style means which frame(Horizontally, starting from 0) of the tile should be placed
        }
    }
}