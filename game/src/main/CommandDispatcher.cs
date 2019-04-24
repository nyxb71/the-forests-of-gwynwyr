using System;
using System.Collections.Generic;
using System.Linq;

using LaYumba.Functional;
using static LaYumba.Functional.F;

namespace game
{
    public static class CommandDispatcher
    {
        public static void Quit() {
            Environment.Exit(0);
        }

        public static void Help() {

        }

        public static void Dispatch(Character character, Option<Command> comm, Option<Direction> arg)
        {
            comm.Match(
                () => {},
                (command) => {
                    arg.Match(
                        () => {
                            switch (command) {
                                case Command.go:
                                    break;
                                case Command.look:
                                    break;
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
                                    character.Go(commandArg);
                                    break;
                                case Command.look:
                                    character.Look(commandArg);
                                    break;
                                case Command.quit:
                                    Quit();
                                    break;

                            }
                        }
                    );
                }
            );

        }
    }
}
