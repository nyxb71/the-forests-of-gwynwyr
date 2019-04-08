using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace game
{
    public class Game1 : SadConsole.Game
    {
        public Game1() : base("", CONFIG.WIDTH, CONFIG.HEIGHT, null)
        {
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = false;
            base.Initialize();

            SadConsole.Game.Instance.Window.Title = CONFIG.TITLE;
            SadConsole.Settings.ResizeMode =
                SadConsole.Settings.WindowResizeOptions.Fit;
            SadConsole.Settings.AllowWindowResize = true;
            SadConsole.Settings.ToggleFullScreen();

            var mainConsole = new SadConsole.Console(CONFIG.WIDTH, CONFIG.HEIGHT);

            var promptResults = new SadConsole.ScrollingConsole(
                CONFIG.WIDTH - 2, CONFIG.HEIGHT - 1)
            {
                Position = new Point(1, 1),
                UseKeyboard = false
            };
            SadConsole.Global.CurrentScreen.Children.Add(promptResults);

            var statusBar = new StatusBar(CONFIG.FG, CONFIG.BG);
            SadConsole.Global.CurrentScreen.Children.Add(statusBar);
            statusBar.Render("The Clearing", "NSEW", 1, 2, 3);

            var promptBar = new PromptBar(CONFIG.FG, CONFIG.BG)
            {
                Position = new Point(0, CONFIG.HEIGHT - 1),
                UseKeyboard = true
            };
            SadConsole.Global.CurrentScreen.Children.Add(promptBar);

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
