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
        [SetUp]
        public void Setup()
        {
        }
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

        [Test]
        public void TestParserValidCommandNoArgs()
        {
            foreach (var e in Parser.Commands)
            {
                Assert.IsTrue(Parser.ParseInput(e) == (Some(e), None));
            }
        }

        [Test]
        public void TestParserWithCommandWithDirectionArg()
        {
            foreach (var comm in new string[] { "go", "look" })
            {
                foreach (var dir in Parser.Directions)
                {
                    Assert.IsTrue(
                        Parser.ParseInput(comm + " " + dir) ==
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
    }
}