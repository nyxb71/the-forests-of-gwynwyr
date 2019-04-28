using System;
using System.Collections.Generic;
using System.Linq;
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
        private DOSConsole Prompt;
        private StatusBar StatusBar;

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
                Console.WriteLine("INPUT: " + input);
                (var command, var arg) = Parser.ParseInput(input);
                Console.WriteLine("COMMAND PARSED AS: " + command + " " + arg);
                CommandHandler.Dispatch(command, arg);
                Console.WriteLine("CURRENT ZONE: " + Player.CurrentZone.Name);
                Console.WriteLine(new string('-', 60));
            };

            (Prompt, StatusBar) = UserInterface.Init(EnterPressedAction);
        }

        protected override void LoadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            StatusBar.Render(Player.CurrentZone.Name,
                string.Join(",", World.ZoneExits(Player.CurrentZone)
                    .OrderBy(s => s)
                    .Select(s => s.ToString().ToUpper().First())),
                Player.Health,
                Player.Level,
                Player.Experience
            );

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
