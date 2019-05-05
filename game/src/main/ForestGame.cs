using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using LaYumba.Functional;
using static LaYumba.Functional.F;

namespace game
{
    public class ForestsGame : SadConsole.Game
    {
        private World World;
        private List<Zone> Zones;
        private Player Player;
        private UserInterface UI;
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

            Zones = ZoneFactory.GenerateZones(CONFIG.ZONES_DIMENSION);
            World = new World(Zones);
            Player = new Player(Zones[CONFIG.ZONES_START], 50); // the center zone

            UI = new UserInterface();

            var CommandHandler = new CommandHandler(World, Zones, Player, UI.Prompt);

            UI.Prompt.SetHandler(input => {
                CommandHandler.Dispatch(Parser.ParseInput(input));});

            UI.Prompt.Clear();
            UI.Prompt.PrintText("Welcome to The Forests of Gwynwyr\n");
            CommandHandler.Dispatch((Some(Command.help), None));
            UI.Prompt.PrintText("Press F5 for fullscreen (recommended)");
            UI.Prompt.PrintText("Press ESC to quit.");
            UI.Prompt.PrintText("Press ENTER to start.");
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

            UI.StatusBar.Render(
                Player.CurrentZone.Name,
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
