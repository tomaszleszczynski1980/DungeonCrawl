using System.IO;
using System.Collections.Generic;
using Codecool.DungeonCrawl.Logic.Actors;

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
            map.Actors = new List<Actor>();     // initialize new List of Actors objects
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
                                map.Actors.Add(skeleton);
                                break;
                            case '@':
                                cell.Type = CellType.Floor;
                                Player player = new Player(cell);
                                map.Actors.Add(player);
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