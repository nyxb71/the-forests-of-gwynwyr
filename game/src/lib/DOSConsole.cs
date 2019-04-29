// Derived from
// https://github.com/SadConsole/SadConsole/blob/master/src/DemoProject/SharedCode/CustomConsoles/DOSConsole.cs

using Microsoft.Xna.Framework;

using System;
using LaYumba.Functional;
using static LaYumba.Functional.F;


namespace game.lib
{
    public class DOSConsole : SadConsole.ScrollingConsole
    {
        public string Prompt { get; set; }
        private int InitHeight = 32;

        private game.lib.ClassicConsoleKeyboardHandler _keyboardHandlerObject;

        // This console domonstrates a classic MS-DOS or Windows Command Prompt
        // style console.
        public DOSConsole(int width, int height) : base(width, height)
        {
            // this.IsVisible = false;

            // This is our cusotmer keyboard handler we'll be using to process
            // the cursor on this console.
            _keyboardHandlerObject = new game.lib.ClassicConsoleKeyboardHandler();

            // Assign our custom handler method from our handler object to this
            // consoles keyboard handler. We could have overridden the
            // ProcessKeyboard method, but I wanted to demonstrate how you can
            // use your own handler on any console type.
            Components.Add(_keyboardHandlerObject);

            // Our custom handler has a call back for processing the commands
            // the user types. We could handle this in any method object
            // anywhere, but we've implemented it on this console directly.

            // stub, will be replaced later
            _keyboardHandlerObject.EnterPressedAction = (s) => {};

            // Enable the keyboard and setup the prompt.
            UseKeyboard = true;
            // Cursor.PrintAppearance.Foreground = CONFIG.FG;
            Cursor.SetPrintAppearance(new SadConsole.Cell(CONFIG.FG, CONFIG.BG));
            Cursor.IsVisible = true;
            Prompt = "> ";


            // Startup description
            ClearText(InitHeight);
            Cursor.Position = new Point(0, InitHeight);
            Cursor.Print("").NewLine().NewLine();
            _keyboardHandlerObject.CursorLastY = InitHeight;
            TimesShiftedUp = 0;

            Cursor.DisableWordBreak = true;
            Cursor.Print(Prompt);
            Cursor.DisableWordBreak = false;
        }

        public void ClearText(int initHeight)
        {
            Clear();
            Cursor.Position = new Point(0, initHeight);
            _keyboardHandlerObject.CursorLastY = initHeight;
        }

        public void SetHandler(Action<string> handler) {
            _keyboardHandlerObject.EnterPressedAction = handler;
        }

        public void PrintText(string text) {
            Cursor.Print(text).NewLine();
        }

        public void PrintText(string[] text) {
            text.ForEach(s => PrintText(s));
        }
    }
}
