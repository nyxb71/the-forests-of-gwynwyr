using System;
using System.Collections.Generic;
using System.Linq;

using LaYumba.Functional;
using static LaYumba.Functional.F;

namespace game
{
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

        public static readonly string[] Commands = {
            "go", "look", "pickup", "drop", "use",
            "put", "open", "inventory", "save", "load", "restart" };

        public static readonly string[] Directions = {
            "north", "south", "east", "west"
        };

        public static (Option<string>, Option<string>) ParseInput(string input)
        {
            if (input.Length == 0 || input == "") return (None, None);

            // Command with no argument
            if (!input.Contains(' '))
            {
                return Commands.Contains(input) ? (Some(input), None) : (None, None);
            }

            // Command with argument
            var chunks = input.Split(" ");
            return (Commands.Contains(chunks[0]) &&
                    Directions.Contains(chunks[1])) ?
                    (Some(chunks[0]), Some(chunks[1])) :
                    (None, None);

        }
    }
}