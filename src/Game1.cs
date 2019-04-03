using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace the_forests_of_gwynwyr
{
    public class Game1 : SadConsole.Game
    {
        public Game1() : base("", Config.GAMEWIDTH, Config.GAMEHEIGHT, null)
        {
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = false;
            base.Initialize();

            SadConsole.Game.Instance.Window.Title = "The Forests of Gwynwyr";
            SadConsole.Settings.ResizeMode = SadConsole.Settings.WindowResizeOptions.Fit;
            SadConsole.Settings.AllowWindowResize = true;
            SadConsole.Settings.ToggleFullScreen();


            var mainConsole = new SadConsole.Console(
                Config.GAMEWIDTH, Config.GAMEHEIGHT);

            var prompt = new lib.CustomConsoles.DOSConsole(
                Config.GAMEWIDTH - 2, Config.GAMEHEIGHT - 1)
            {
                Position = new Point(1, 1)
            };
            SadConsole.Global.CurrentScreen.Children.Add(prompt);
            prompt.UseKeyboard = true;


            var statusBar = new StatusBar(Config.GAMEFG, Config.GAMEBG);
            SadConsole.Global.CurrentScreen.Children.Add(statusBar);
            statusBar.Render("The Clearing", "NSEW", 1, 2, 3);

            var promptBar = new PromptBar(Config.GAMEFG, Config.GAMEBG)
            {
                Position = new Point(0, Config.GAMEHEIGHT - 1)
            };
            SadConsole.Global.CurrentScreen.Children.Add(promptBar);
            promptBar.UseKeyboard = true;
            SadConsole.Global.FocusedConsoles.Set(promptBar);

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
