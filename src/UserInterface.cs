using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using SadConsole.Components;
using SadConsole.Input;

namespace game
{
    internal class InvertedBar : SadConsole.Console
    {
        public InvertedBar(Color fg, Color bg) : base(CONFIG.WIDTH, 1)
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
        public string Prompt { get; set; }
        private lib.InputHandling.ClassicConsoleKeyboardHandler _keyboardHandler;

        public PromptBar(Color fg, Color bg) : base(fg, bg)
        {
            Prompt = " > ";
            // _keyboardHandler = new lib.InputHandling.ClassicConsoleKeyboardHandler();
            // Components.Add(_keyboardHandler);
            _keyboardHandler.EnterPressedAction = EnterPressedActionHandler;


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


    class PromptKeyboardHandler : KeyboardConsoleComponent
    {

        public Action<string> EnterPressedAction = (s) => { int i = s.Length; };

        public override void ProcessKeyboard(SadConsole.Console console,
            SadConsole.Input.Keyboard info, out bool handled)
        {
            foreach (var key in info.KeysPressed)
            {
                if (key.Character != '\0')
                    console.Cursor.Print(key.Character.ToString());

                // else if (key.Key == Keys.Back)
                // {
                //     string prompt = ((game.PromptBar)console).Prompt;

                // }

                if (key.Key == Keys.Enter)
                {
                    string prompt = ((game.PromptBar)console).Prompt;
                    int start = prompt.Length;
                    string data = console.GetString(start, CONFIG.WIDTH);
                    EnterPressedAction(data);
                }

            }
            handled = true;
        }
    }
}