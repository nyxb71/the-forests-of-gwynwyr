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
                Assert.IsTrue(Parser.ParseInput(comm.ToString()) ==
                    (Some(comm), None));
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
        public void TestIsAdjacenctShouldBeAdjacent() {
            var center = new Location(1, 1);

            var should_be_adjacent = new Location[] {
                new Location(0, 1),
                new Location(1, 0),
                new Location(1, 2),
                new Location(2, 1),
            };

            foreach (Location loc in should_be_adjacent) {
                var res = Location.IsAdjacent(center, loc);
                Console.WriteLine($"Loc {loc.x},{loc.y} is adjacent to {center.x},{center.y}? {res}");
                Assert.IsTrue(res);
            }
        }

        [Test]
        public void TestIsAdjacenctShouldNotBeAdjacent() {
            var center = new Location(1, 1);

            var should_not_be_adjacent = new Location[] {
                new Location(0, 0),
                new Location(2, 0),
                new Location(2, 2),
                new Location(0, 2),
            };

            foreach (Location loc in should_not_be_adjacent) {
                var res = Location.IsAdjacent(center, loc);
                Console.WriteLine($"Loc {loc.x},{loc.y} is NOT adjacent to {center.x},{center.y}? {!res}");
                Assert.IsFalse(res);
            }
        }

        [Test]
        public void TestDirectionOfAdjacents() {
            var center = new Location(1, 1);

            var correct_directions = new Dictionary<Location, Direction> {
                {new Location(1, 2), Direction.north},
                {new Location(1, 0), Direction.south},
                {new Location(2, 1), Direction.east},
                {new Location(0, 1), Direction.west},
            };

            foreach ((var key, var val) in correct_directions) {
                var dir = Location.DirectionTo(center, key);
                Console.WriteLine(
                    $"{key.x},{key.y} is {dir} of {center.x},{center.y}");
                Assert.IsTrue(dir == Some(val));
            }
        }

        [Test]
        public void TestDirectionOfNonAdjacents() {
            var center = new Location(1, 1);

            var should_have_no_direction = new Location[] {
                new Location(0, 0), // southwest
                new Location(2, 0), // southeast
                new Location(2, 2), // northeast
                new Location(0, 2), // northwest
            };

            foreach (Location loc in should_have_no_direction) {
                Assert.IsTrue(Location.DirectionTo(center, loc) == None);
            }
        }
    }
}
