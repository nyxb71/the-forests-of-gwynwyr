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
        private Player Player;
        private lib.DOSConsole Prompt;

        public CommandHandler(
                World world,
                List<Zone> zones,
                Player player,
                lib.DOSConsole prompt
                ) {
            this.World = world;
            this.Zones = zones;
            this.Player = player;
            this.Prompt = prompt;
        }

        public void Quit() {
            Console.WriteLine("Exiting...");
            Environment.Exit(0);
        }

        public void Help() {

        }

        public void Go(Direction dir) {
            try {
                var res = Zones.Where((z) =>
                    Player.CurrentZone.DirectionTo(z) == dir);

                if (res.Count() > 0) {
                    Console.WriteLine("Moving to Zone: " + res.First().Name);

                    if (World.ZoneMap.ContainsEdge(Player.CurrentZone, res.First())) {
                        Player.Go(res.First());
                        Prompt.PrintText($"You entered zone: {Player.CurrentZone.Name}");
                    }
                }
                else {
                    Console.WriteLine("Couldn't find zone to move to.");
                }
            }
            catch (ArgumentNullException) {
                Console.WriteLine("Couldn't find zone to move to.");

            }
        }

        public void Look(Direction dir) {
            Prompt.PrintText(Player.Look(dir));

        }

        public void Dispatch(
            Option<Command> comm,
            Option<Direction> arg)
        {
            comm.Match(
                () => {},
                (command) => {
                    arg.Match(
                        () => {
                            switch (command) {
                                case Command.quit:
                                    Quit();
                                    break;
                                case Command.help:
                                    Help();
                                    break;
                            }
                        },
                        (commandArg) => {
                            switch (command) {
                                case Command.go:
                                    Go(commandArg);
                                    break;
                                case Command.look:
                                    Look(commandArg);
                                    break;
                                case Command.quit:
                                    Quit();
                                    break;
                                case Command.help:
                                    Help();
                                    break;
                            }
                        }
                    );
                }
            );
        }
    }
}
