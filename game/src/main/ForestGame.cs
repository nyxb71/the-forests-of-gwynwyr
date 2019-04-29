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
        private lib.DOSConsole Prompt;
        private StatusBar StatusBar;
        private float XPTimer;
        private float DamageTimer;

        public ForestsGame() : base("", CONFIG.WIDTH, CONFIG.HEIGHT, null)
        {
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = false;
            base.Initialize();

            Zones = ZoneFactory.GenerateZones(3);
            World = new World(Zones);
            Player = new Player(Zones[4], 50); // the center zone

            (Prompt, StatusBar) = UserInterface.Init();

            CommandHandler = new CommandHandler(World, Zones, Player, Prompt);

            Action<string> EnterPressedAction = (input) => {
                (var command, var arg) = Parser.ParseInput(input);
                CommandHandler.Dispatch(command, arg);
            };

            Prompt.SetHandler(EnterPressedAction);

            Prompt.Clear();
            Prompt.PrintText("Welcome to The Forests of Gwynwyr\n");
            CommandHandler.Help();
            Prompt.PrintText("Press F5 for fullscreen (recommended)");
            Prompt.PrintText("Press ESC to quit.");
            Prompt.PrintText("Press ENTER to start.");
        }

        protected override void LoadContent()
        {
        }

        private static void DoIfTimerElapsed(
            float seconds_elapsed,
            ref float timer,
            float interval,
            Action<int> action_to_take,
            int action_arg)
        {
            timer -= (float)seconds_elapsed;
            if (timer < 0) {
                action_to_take(action_arg);
                timer = interval;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.F5))
                SadConsole.Settings.ToggleFullScreen();

            DoIfTimerElapsed(
                (float)gameTime.ElapsedGameTime.TotalSeconds,
                ref XPTimer,
                10,
                i => Player.AddExperience((uint)i), 1);

            DoIfTimerElapsed(
                (float)gameTime.ElapsedGameTime.TotalSeconds,
                ref DamageTimer,
                20,
                i => Player.TakeDamage((uint)i), 1);

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
