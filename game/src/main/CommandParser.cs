using System;
using System.Collections.Generic;
using System.Linq;

using LaYumba.Functional;
using static LaYumba.Functional.F;

namespace game
{
    public enum Command {
        go, look, restart, quit, help
    }
    public enum Direction {
        north, south, east, west
    }

    public static class Parser
    {
        public static T ParseEnum<T>(string value) =>
            (T)System.Enum.Parse(typeof(T), value, true);

        public static Option<T> ToEnum<T>(string input)
        {
            try
            {
                return System.Enum.IsDefined(typeof(T), input) ?
                    Some(ParseEnum<T>(input)) : None;
            }
            catch (ArgumentException)
            {
                return None;
            }
        }

        public static (Option<Command>, Option<Direction>) ParseInput(string input)
        {
            var trimmed = input.ToLower().Trim();

            if (trimmed.Length == 0) return (None, None);

            // Command with no argument
            if (!trimmed.Contains(" "))
            {
                return ToEnum<Command>(trimmed).Match(
                    None: () => (None, None),
                    Some: (comm) => (Some(comm), None));
            }

            // Command with argument
            var chunks = trimmed.Split(" ");
            return ToEnum<Command>(chunks[0]).Match(
                () => (None, None),
                (command) => ToEnum<Direction>(chunks[1]).Match(
                    () => (None, None),
                    (commandArg) => (Some(command), Some(commandArg))
                )
            );
        }
    }
}
