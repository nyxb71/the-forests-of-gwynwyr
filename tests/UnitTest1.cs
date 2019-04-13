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
        public void TestParserNoArgs()
        {
            foreach (var e in Parser.Commands)
            {
                Assert.IsTrue(Parser.ParseInput(e) == (Some(e), None));
            }
        }

        [Test]
        public void TestParserWithDirectionArg()
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
            Assert.IsTrue(Parser.ParseInput("asdf") == (None, None));
            Assert.IsTrue(Parser.ParseInput("") == (None, None));
            Assert.IsTrue(Parser.ParseInput(" ") == (None, None));
            Assert.IsTrue(Parser.ParseInput(" asdf") == (None, None));
            Assert.IsTrue(Parser.ParseInput("foo asdf") == (None, None));
        }
    }
}