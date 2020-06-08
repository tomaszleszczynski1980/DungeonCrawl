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
        public static Program Singleton { get; private set; }
        
        /// <summary>
        /// _map
        /// </summary>
        private GameMap _map;
        private TextField _healthTextField;

        public Sprite MapContainer { get; private set; }

        // private Sprite _playerGfx;
        // private List<Sprite> _skeletonsGfx;
        private Key[] _listenedKeys = { Key.Up, Key.Down, Key.Left, Key.Right };
        private int _fpsCounter = 0;

        /// <summary>
        /// Event listening for key Pressed
        /// </summary>
        public static event Action<Key> OnKeyPressed;

        /// <summary>
        /// Event listening for key Pressed
        /// </summary>
        public static event Action<Actor> Collision;

        /// <summary>
        /// Entry point
        /// </summary>
        public static void Main()
        {
            Singleton = new Program();
            Singleton.Start();
        }

        private void Start()
        {
            PerlinApp.Start(_map.Width * Tiles.TileWidth,
                _map.Height * Tiles.TileWidth,
                "Dungeon Crawl",
                OnStart); 
        }

        private Program()
        {
            _map = MapLoader.LoadMap();
        }

        private void OnStart()
        {
            var stage = PerlinApp.Stage;

            // health textField
            // _healthTextField = new TextField(
            //     PerlinApp.FontRobotoMono.CreateFont(14),
            //     _map.Player.Health.ToString(),
            //     false);
            // _healthTextField.HorizontalAlign = HorizontalAlignment.Center;
            // _healthTextField.Width = 100;
            // _healthTextField.Height = 20;
            // _healthTextField.X = _map.Width * Tiles.TileWidth / 2 - 50;
            // stage.AddChild(_healthTextField);

            stage.EnterFrameEvent += StageOnEnterFrameEvent;

            MapContainer = new Sprite();
            stage.AddChild(MapContainer);
            DrawMap();
            MapLoader.CreateActors(_map);
        }

        private void DrawMap()
        {
            MapContainer.RemoveAllChildren();
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
                    MapContainer.AddChild(sp);
                }
            }
        }

        // this gets called every frame
        private void StageOnEnterFrameEvent(DisplayObject target, float elapsedtimesecs)
        {
            foreach (Key k in _listenedKeys)
            {
                if (KeyboardInput.IsKeyPressedThisFrame(k))
                {
                    OnKeyPressed?.Invoke(k);
                }
            }

            const int enemiesDelay = 25; // enemies will move once in 25 frames.

            // if (_fpsCounter % enemiesDelay == 0)
            // {
            //     _fpsCounter = 0;
            //
            //     Random rnd = new Random();
            //     for (int index = 0; index < _map.Actors.Count; index++)
            //     {
            //         _map.Actors[index].Move(rnd.Next(-1, 2), rnd.Next(-1, 2));
            //         _actorsGfx[index].X = _map.Actors[index].X * Tiles.TileWidth;
            //         _actorsGfx[index].Y = _map.Actors[index].Y * Tiles.TileWidth;
            //     }
            // }

            _fpsCounter++;

            // render changes
            // _playerGfx.X = _map.Player.X * Tiles.TileWidth;
            // _playerGfx.Y = _map.Player.Y * Tiles.TileWidth;
        }
    }
}