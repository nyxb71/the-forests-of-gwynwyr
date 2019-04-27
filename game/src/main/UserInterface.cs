using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using SadConsole.Components;

namespace game
{
    public class UserInterface
    {
        public static void Init(Action<string> EnterPressedAction)
        {
            SadConsole.Game.Instance.Window.Title = CONFIG.TITLE;
            SadConsole.Settings.ResizeMode =
                SadConsole.Settings.WindowResizeOptions.Fit;
            SadConsole.Settings.AllowWindowResize = true;

            var prompt = new lib.CustomConsoles.DOSConsole(
                EnterPressedAction,
                CONFIG.WIDTH,
                CONFIG.HEIGHT - 1)
            {
                Position = new Point(0, 1),
                UseKeyboard = true
            };
            SadConsole.Global.CurrentScreen.Children.Add(prompt);

            var statusBar = new StatusBar(CONFIG.FG, CONFIG.BG)
            {
                IsVisible = true
            };
            SadConsole.Global.CurrentScreen.Children.Add(statusBar);

            statusBar.Render("The Clearing", "NSEW", 1, 2, 3);

            SadConsole.Global.FocusedConsoles.Set(prompt);

        }
    }

    internal class InvertedBar : SadConsole.Console
    {
        public InvertedBar(Color fg, Color bg) : base(CONFIG.WIDTH, 1)
        {
            Cursor.IsVisible = false;
            this.DefaultBackground = fg;
            this.DefaultForeground = bg;
            this.Cursor.PrintAppearance.Foreground = fg;
            this.Cursor.PrintAppearance.Background = bg;
        }

        public void PrintInverted(int x, int y, string text)
        {
            this.Print(x, y, text, this.DefaultForeground);
        }

    }

    internal class StatusBar : InvertedBar
    {
        private readonly string _fmtString = "LOC: {0,-30} | EXITS: {1,-4} " +
            "| HP: {2,-3} | LVL: {3,-2} | XP: {4,-4} |";

        public StatusBar(Color fg, Color bg) : base(fg, bg)
        {
        }

        public void Render(string loc, string exits, uint hp, uint lvl, uint xp) =>
            this.PrintInverted(1, 0,
                string.Format(_fmtString, loc, exits, hp, lvl, xp));
    }
}
