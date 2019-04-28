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
        private CommandHandler CommandHandler;

        public ForestsGame() : base("", CONFIG.WIDTH, CONFIG.HEIGHT, null)
        {
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = false;
            base.Initialize();

            Zones = GameData.LoadZones();
            World = new World(Zones);
            Player = new Player(Zones[4], 50); // the center zone
            CommandHandler = new CommandHandler(World, Zones, Player);

            Action<string> EnterPressedAction = (input) => {
                (var command, var arg) = Parser.ParseInput(input);
                Console.WriteLine("COMMAND PARSED AS: " + command + " " + arg);
                CommandHandler.Dispatch(command, arg);
            };

            UserInterface.Init(EnterPressedAction);
        }

        protected override void LoadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
