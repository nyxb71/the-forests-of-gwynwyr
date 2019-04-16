using System;
using System.Collections.Generic;
using System.Linq;

using LaYumba.Functional;
using static LaYumba.Functional.F;

namespace game
{
    public class CommandDispatcher
    {
        public static string look(Option<string> direction)
        {
            return "Called look()";
        }

        public static string go(Option<string> direction)
        {
            return "Called go()";
        }

        public static void Dispatch(Option<string> command, Option<string> arg)
        {
            var commands = new Dictionary<string, Func<Option<string>, string>> {
                { "look", lookarg => look(lookarg) },
                { "go", goarg => go(goarg) }
            };

            var foo = command.Match(
                None: () => { },
                Some: (c) => arg.Match(
                    None: () => commands[c](None),
                    Some: (a) => commands[c](a)
                )
            );
        }
    }
}