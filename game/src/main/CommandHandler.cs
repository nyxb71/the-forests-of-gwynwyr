using System;
using System.Collections.Generic;
using System.Linq;

using LaYumba.Functional;
using static LaYumba.Functional.F;

namespace game
{
    public class CommandHandler
    {
        private readonly World World;
        private readonly List<Zone> Zones;
        private readonly Dictionary<Command, Action> DispatchTableNoArgs;
        private readonly Dictionary<Command, Action<Direction>> DispatchTableWithDir;
        private Player Player;
        private lib.DOSConsole Prompt;

        public CommandHandler(
                World world,
                List<Zone> zones,
                Player player,
                lib.DOSConsole prompt
                )
        {
            this.World = world;
            this.Zones = zones;
            this.Player = player;
            this.Prompt = prompt;

            this.DispatchTableNoArgs = new Dictionary<Command, Action>() {
                { Command.help, () => Help() },
                { Command.quit, () => Quit() }
            };

            this.DispatchTableWithDir = new Dictionary<Command, Action<Direction>>() {
                { Command.go, (dir) => Go(dir) },
                { Command.look, (dir) => Look(dir) },
            };
        }

        private void Quit() => Environment.Exit(0);

        private void Help()
        {
            var helptexts = new string[] {
                "-- Commands --",
                "go   <north|south|east|west> : go to zone in specified direction",
                "look <north|south|east|west> : look in a direction",
                "quit                         : quit the game",
                "help                         : show this text",
                "",
            };
            Prompt.PrintText(helptexts);
        }

        private void Go(Direction dir)
        {
            try
            {
                var res = Zones.Where((z) =>
                    Player.CurrentZone.DirectionTo(z) == dir);

                if (res.Count() > 0)
                {
                    if (World.ZoneMap.ContainsEdge(Player.CurrentZone, res.First()))
                    {
                        Player.Go(res.First());
                        Prompt.PrintText($"You entered zone: {Player.CurrentZone.Name}");
                    }
                }
                else
                {
                    Prompt.PrintText("You have reached the edge of the map.");
                }
            }
            catch (ArgumentNullException)
            {
            }
        }

        private void Look(Direction dir) =>
            Prompt.PrintText(Player.Look(dir));

        private void InvalidCommand() =>
            Prompt.PrintText("Invalid command. Type 'help' for commands.");

        private void InvalidCommand(Direction dir) => InvalidCommand();

        public void Dispatch(
            Option<Command> comm,
            Option<Direction> arg)
        {
            comm.Match(
                None: () => { InvalidCommand(); },
                Some: (command) =>
                {
                    arg.Match(
                        None: () =>
                        {
                            (DispatchTableNoArgs.Keys.Contains(command) ?
                                DispatchTableNoArgs[command] : InvalidCommand)
                                ();
                        },
                        Some: (commandArg) =>
                        {
                            (DispatchTableWithDir.Keys.Contains(command) ?
                                DispatchTableWithDir[command] : InvalidCommand)
                                (commandArg);
                        }
                    );
                }
            );
        }
    }
}
