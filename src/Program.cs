using System;
using SadConsole;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;

namespace the_forests_of_gwynwyr
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }
    }
}
