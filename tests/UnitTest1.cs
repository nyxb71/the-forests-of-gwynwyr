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
            Zones = ZoneFactory.GenerateZones(3);
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

        [Test]
        public void TestIsAdjacenct() {
            var center = new Location(1, 1);

            var should_be_adjacent = new Location[] {
                new Location(0, 1), // north
                new Location(1, 0), // west
                new Location(1, 2), // east
                new Location(2, 1), // south
            };

            foreach (Location loc in should_be_adjacent) {
                var res = Zone.IsAdjacent(center, loc);
                Console.WriteLine($"Loc {loc.x},{loc.y} is adjacent to {center.x},{center.y}? {res}");
                Assert.IsTrue(res);
            }

            var should_not_be_adjacent = new Location[] {
                new Location(0, 0), // northwest
                new Location(2, 0), // southwest
                new Location(2, 2), // southeast
                new Location(0, 2), // northeast
            };

            foreach (Location loc in should_not_be_adjacent) {
                var res = Zone.IsAdjacent(center, loc);
                Console.WriteLine($"Loc {loc.x},{loc.y} is NOT adjacent to {center.x},{center.y}? {!res}");
                Assert.IsFalse(res);
            }
        }
    }
}
