using System.IO;
using System.Collections.Generic;
using Codecool.DungeonCrawl.Logic.Actors;
using Vulkan;

namespace Codecool.DungeonCrawl.Logic
{
    /// <summary>
    /// Helper class to load the map from the disk
    /// </summary>
    public static class MapLoader
    {
        /// <summary>
        /// Load the map from the disk
        /// </summary>
        /// <returns>The loaded map</returns>
        /// <exception cref="InvalidDataException">The map has unrecognized character(s)</exception>
        public static GameMap LoadMap()
        {
            var lines = File.ReadAllLines("map.txt");
            var dimensions = lines[0].Split(" ");
            var width = int.Parse(dimensions[0]);
            var height = int.Parse(dimensions[1]);

            GameMap map = new GameMap(width, height, CellType.Empty);
            map.Skeletons = new List<Skeleton>();     // initialize new List of Sekelton(s) objects
            for (var y = 0; y < height; y++)
            {
                var line = lines[y + 1];
                for (int x = 0; x < width; x++)
                {
                    if (x < line.Length)
                    {
                        Cell cell = map.GetCell(x, y);
                        switch (line[x])
                        {
                            case ' ':
                                cell.Type = CellType.Empty;
                                break;
                            case '#':
                                cell.Type = CellType.Wall;
                                break;
                            case '.':
                                cell.Type = CellType.Floor;
                                break;
                            case 's':
                                cell.Type = CellType.Floor;
                                Skeleton skeleton = new Skeleton(cell);
                                map.Skeletons.Add(skeleton);
                                break;
                            case '@':
                                cell.Type = CellType.Floor;
                                map.Player = new Player(cell);
                                break;

                            case '$':
                                cell.Type = CellType.Portal;
                                break;
                            default:
                                throw new InvalidDataException($"Unrecognized character: '{line[x]}'");
                        }
                    }
                }
            }

            return map;
        }
    }
}