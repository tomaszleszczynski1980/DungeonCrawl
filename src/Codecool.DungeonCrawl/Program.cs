using System;
using System.Collections.Generic;
using Codecool.DungeonCrawl.Logic;
using Codecool.DungeonCrawl.Logic.Actors;
using Perlin;
using Perlin.Display;
using SixLabors.Fonts;
using Veldrid;

namespace Codecool.DungeonCrawl
{
    /// <summary>
    /// The main class and entry point.
    /// </summary>
    public class Program
    {
        private GameMap _map;
        private TextField _healthTextField;
        private Sprite _mapContainer;
        private Sprite _playerGfx;
        private List<Sprite> _skeletonsGfx;

        /// <summary>
        /// Entry point
        /// </summary>
        public static void Main()
        {
            new Program();
        }

        private Program()
        {
            _map = MapLoader.LoadMap();
            PerlinApp.Start(_map.Width * Tiles.TileWidth,
                _map.Height * Tiles.TileWidth,
                "Dungeon Crawl",
                OnStart);
        }

        private void OnStart()
        {
            var stage = PerlinApp.Stage;

            // health textField
            _healthTextField = new TextField(
                PerlinApp.FontRobotoMono.CreateFont(14),
                _map.Player.Health.ToString(),
                false);
            _healthTextField.HorizontalAlign = HorizontalAlignment.Center;
            _healthTextField.Width = 100;
            _healthTextField.Height = 20;
            _healthTextField.X = _map.Width * Tiles.TileWidth / 2 - 50;
            stage.AddChild(_healthTextField);

            stage.EnterFrameEvent += StageOnEnterFrameEvent;

            _mapContainer = new Sprite();
            stage.AddChild(_mapContainer);
            DrawMap();

            _skeletonsGfx = new List<Sprite>();    // initialize skeletonsGfx list

            for (int index = 0; index < _map.Skeletons.Count; index++)
            {
                _skeletonsGfx.Add(new Sprite("tiles.png", false, Tiles.SkeletonTile));
                _skeletonsGfx[index].X = _map.Skeletons[index].X * Tiles.TileWidth;
                _skeletonsGfx[index].Y = _map.Skeletons[index].Y * Tiles.TileWidth;
                stage.AddChild(_skeletonsGfx[index]);
            }

            _playerGfx = new Sprite("tiles.png", false, Tiles.PlayerTile);
            stage.AddChild(_playerGfx);
        }

        private void DrawMap()
        {
            _mapContainer.RemoveAllChildren();
            for (int x = 0; x < _map.Width; x++)
            {
                for (int y = 0; y < _map.Height; y++)
                {
                    var cell = _map.GetCell(x, y);
                    var tile = Tiles.GetMapTile(cell);

                    // tiles are 16x16 pixels
                    var sp = new Sprite("tiles.png", false, tile);
                    sp.X = x * Tiles.TileWidth;
                    sp.Y = y * Tiles.TileWidth;
                    _mapContainer.AddChild(sp);
                }
            }
        }

        // this gets called every frame
        private void StageOnEnterFrameEvent(DisplayObject target, float elapsedtimesecs)
        {
            // process inputs
            if (KeyboardInput.IsKeyPressedThisFrame(Key.Up))
            {
                _map.Player.Move(0, -1);
                Random rnd = new Random();

                for (int index = 0; index < _map.Skeletons.Count; index++)
                {
                    _map.Skeletons[index].Move(rnd.Next(-1, 2), rnd.Next(-1, 2));
                    _skeletonsGfx[index].X = _map.Skeletons[index].X * Tiles.TileWidth;
                    _skeletonsGfx[index].Y = _map.Skeletons[index].Y * Tiles.TileWidth;
                }
            }

            if (KeyboardInput.IsKeyPressedThisFrame(Key.Down))
            {
                _map.Player.Move(0, 1);
                Random rnd = new Random();

                for (int index = 0; index < _map.Skeletons.Count; index++)
                {
                    _map.Skeletons[index].Move(rnd.Next(-1, 2), rnd.Next(-1, 2));
                    _skeletonsGfx[index].X = _map.Skeletons[index].X * Tiles.TileWidth;
                    _skeletonsGfx[index].Y = _map.Skeletons[index].Y * Tiles.TileWidth;
                }
            }

            if (KeyboardInput.IsKeyPressedThisFrame(Key.Left))
            {
                _map.Player.Move(-1, 0);
                Random rnd = new Random();

                for (int index = 0; index < _map.Skeletons.Count; index++)
                {
                    _map.Skeletons[index].Move(rnd.Next(-1, 2), rnd.Next(-1, 2));
                    _skeletonsGfx[index].X = _map.Skeletons[index].X * Tiles.TileWidth;
                    _skeletonsGfx[index].Y = _map.Skeletons[index].Y * Tiles.TileWidth;
                }
            }

            if (KeyboardInput.IsKeyPressedThisFrame(Key.Right))
            {
                _map.Player.Move(1, 0);
                Random rnd = new Random();

                for (int index = 0; index < _map.Skeletons.Count; index++)
                {
                    _map.Skeletons[index].Move(rnd.Next(-1, 2), rnd.Next(-1, 2));
                    _skeletonsGfx[index].X = _map.Skeletons[index].X * Tiles.TileWidth;
                    _skeletonsGfx[index].Y = _map.Skeletons[index].Y * Tiles.TileWidth;
                }
            }

            // render changes
            _playerGfx.X = _map.Player.X * Tiles.TileWidth;
            _playerGfx.Y = _map.Player.Y * Tiles.TileWidth;
        }
    }
}