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

        public CommandHandler(World world, List<Zone> zones, Player player) {
            this.World = world;
            this.Zones = zones;
            this.Player = player;
        }

        public void Quit() {
            Console.WriteLine("Exiting...");
            Environment.Exit(0);
        }

        public void Help() {

        }

        public void Go(Direction dir) {
            try {
                var res = Zones.Find((z) =>
                    Player.CurrentZone.DirectionTo(z) == dir);

                if (World.ZoneMap.ContainsEdge(Player.CurrentZone, res)) {
                    Player.MoveTo(res);
                }
            }
            catch (ArgumentNullException) {

            }
        }

        public void Look(Direction dir) {
            Player.Look(dir);
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
