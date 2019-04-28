using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using LaYumba.Functional;
using static LaYumba.Functional.F;

using game;

namespace Tests
{
    public class Tests
    {
        private List<Zone> Zones;

        [SetUp]
        public void Setup()
        {
            Zones = GameData.LoadZones();
        }

        [Test]
        public void TestParserValidCommandNoArgs()
        {
            foreach (Command comm in System.Enum.GetValues(typeof(Command)))
            {
                Assert.IsTrue(Parser.ParseInput(comm.ToString()) == (Some(comm), None));
            }
        }

        [Test]
        public void TestParserWithCommandWithDirectionArg()
        {
            foreach (Command comm in new Command[] { Command.go, Command.look })
            {
                foreach (Direction dir in System.Enum.GetValues(typeof(Direction)))
                {
                    Assert.IsTrue(
                        Parser.ParseInput(comm.ToString() + " " + dir.ToString()) ==
                        (Some(comm), Some(dir)));
                }
            }
        }

        [Test]
        public void TestParseInvalidCommands()
        {
            foreach (var input in new HashSet<string> {
                "asdf", "", " ", " asdf", "foo asdf" })
            {
                Assert.IsTrue(Parser.ParseInput(input) == (None, None));
            }
        }

        [Test]
        public void TestZoneLoading()
        {
            foreach (Zone z in Zones) {
                Console.WriteLine($"Zone '{z.Name}' exists.");
                Assert.IsTrue(z.Name != null);
            }
        }
    }
}
