using System.Collections.Generic;
using TiledCS;
using System.Linq;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System;
using System.Windows.Shapes;
using System.Windows.Controls;
using Trophy_Redeem.src.graphics;

namespace Trophy_Redeem.src.maps
{

    /// <summary> Class converts TiledMap (from Tiled Level Editor) into its single Tiles (Rectangles) </summary>
    public class CustomTiledMap : Component
    {

        const string RELATIVE_MAP_DIR_PATH = "src/assets/maps/";
        const string RELATIVE_TILESET_DIR_PATH = "src/assets/tilesets/";
        const string RELATIVE_IMAGE_DIR_PATH = "src/assets/pngs/";

        TiledMap map;
        public int width;
        public int height;
        private string name;


        public CustomTiledMap(string name)
        {
            this.name = name;
            map = new TiledMap(RELATIVE_MAP_DIR_PATH + name + ".tmx");
            BuildElements();
        }

        void BuildElements()
        {
            var tilesets = map.GetTiledTilesets(RELATIVE_TILESET_DIR_PATH);
            var tileLayers = getAllLayers(map);

            height = tileLayers.First().height * map.TileHeight;
            width = tileLayers.First().width * map.TileWidth;

            foreach (var layer in tileLayers)
            {
                for (int row = 0; row < layer.height; row++)
                {
                    for (int column = 0; column < layer.width; column++)
                    {
                        // Select tile from left to right / top to bottom
                        int tileNumber = row * layer.width + column;
                        int tileIndexOfTileset = layer.data[tileNumber];

                        if (tileIndexOfTileset == 0)
                        {
                            continue;
                        }

                        TiledMapTileset mapTileset = map.GetTiledMapTileset(tileIndexOfTileset);
                        var tileset = tilesets[mapTileset.firstgid];
                        TiledSourceRect tileRect = map.GetSourceRect(mapTileset, tileset, tileIndexOfTileset);

                        if (tileRect == null)
                        {
                            continue;
                        }

                        ImageBrush brush = new ImageBrush();
                        int rectXOffset = tileRect.x + tileRect.x / tileRect.width * tileset.Margin + tileset.Spacing;
                        int rectYOffset = tileRect.y + tileRect.y / tileRect.height * tileset.Margin + tileset.Spacing;
                        brush.ImageSource = new CroppedBitmap(
                            new BitmapImage(new Uri(RELATIVE_IMAGE_DIR_PATH + name + "/" + System.IO.Path.GetFileName(tileset.Image.source), UriKind.Relative)),
                            new Int32Rect(rectXOffset, rectYOffset, tileRect.width, tileRect.height)
                        );

                        Rectangle renderRect = new Rectangle() { Height = tileRect.height, Width = tileRect.width };
                        renderRect.Fill = brush;

                        int tileRenderPosX = column * map.TileWidth;
                        int tileRenderPosY = row * map.TileHeight;
                        Canvas.SetLeft(renderRect, tileRenderPosX);
                        Canvas.SetTop(renderRect, tileRenderPosY);
                        elementList.Add(renderRect);
                    }
                }
            }
        }

        private IEnumerable<TiledLayer> getAllLayers(TiledMap map)
        {
            List<TiledLayer> layers = new List<TiledLayer>();

            layers.AddRange(map.Layers);

            foreach (var group in map.Groups)
            {
                layers.AddRange(getAllLayersFromGroup(group));
            }

            return layers;
        }
        private IEnumerable<TiledLayer> getAllLayersFromGroup(TiledGroup group)
        {
            List<TiledLayer> layers = new List<TiledLayer>();

            layers.AddRange(group.layers);

            foreach (var subGroup in group.groups)
            {
                layers.AddRange(getAllLayersFromGroup(subGroup));
            }

            return layers;
        }
    }
}
