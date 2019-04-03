using Microsoft.Xna.Framework;
using System;
using SadConsole.Components;

namespace the_forests_of_gwynwyr
{
    internal class InvertedBar : SadConsole.Console
    {
        public InvertedBar(Color fg, Color bg) : base(Config.GAMEWIDTH, 1)
        {
            Cursor.IsVisible = false;
            this.IsVisible = true;
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

    class PromptBar : InvertedBar
    {
        private const string _prompt = "> ";
        // private lib.InputHandling.ClassicConsoleKeyboardHandler _keyboardHandler;

        public PromptBar(Color fg, Color bg) : base(fg, bg)
        {
            // _keyboardHandler = new lib.InputHandling.ClassicConsoleKeyboardHandler();
            // Components.Add(_keyboardHandler);
            // _keyboardHandler.EnterPressedAction = EnterPressedActionHandler;

            UseKeyboard = true;
            Cursor.IsVisible = true;

            ClearText();
        }

        public void ClearText()
        {
            Clear();
            Cursor.Position = new Point(3, 0);
            // _keyboardHandler.CursorLastY = 0;
        }

        private void EnterPressedActionHandler(string value)
        {
            Cursor.Print(value.ToLower());
        }

    }
}