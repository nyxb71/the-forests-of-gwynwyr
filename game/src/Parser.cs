using System;
using System.Collections.Generic;
using System.Linq;

using LaYumba.Functional;
using static LaYumba.Functional.F;

namespace game
{
    public enum Command
    {
        go, look, pickup, drop, use, put, open, inventory, save, load, restart
    }

    public enum Direction
    {
        north, south, east, west
    }

    public static class Parser
    {
        /*
            Commands:
            go     <direction>
            look   <direction|item|entity>
            pickup <item|entity>
            drop   <item|entity>
            use    <item|entity>
            put    <item|entity>
            open   <item>
            inventory
            save 
            load
            restart
         */

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
            if (input.Length == 0) return (None, None);

            // Command with no argument
            if (!input.Contains(' '))
            {
                return ToEnum<Command>(input).Match(
                    None: () => (None, None),
                    Some: (comm) => (Some(comm), None));
            }

            // Command with argument
            var chunks = input.Split(" ");
            Option<Command> commWithArg = ToEnum<Command>(chunks[0]);
            Option<Direction> arg = ToEnum<Direction>(chunks[1]);
            if (commWithArg != None && arg != None)
            {
                return (commWithArg, arg);
            }

            return (None, None);
        }
    }
}