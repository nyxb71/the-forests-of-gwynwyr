using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace game
{
    public class ForestsGame : SadConsole.Game
    {
        private World World;
        private List<Zone> Zones;
        private Player Player;
        private GameEventQueue GameEvents;

        public ForestsGame() : base("", CONFIG.WIDTH, CONFIG.HEIGHT, null)
        {
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = false;
            base.Initialize();

            Zones = GameData.LoadZones("data/zones.json");
            World = new World(Zones);
            Player = new Player();
            GameEvents = new GameEventQueue();

            UserInterface.Init();
        }

        protected override void LoadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            GameEvents.ProcessNext();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
