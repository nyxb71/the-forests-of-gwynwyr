using System;
using System.Collections.Generic;

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

        [Test]
        public void TestParserNoArgs()
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
            foreach (var e in System.Enum.GetValues(typeof(Command)))
            {
                Assert.IsTrue(Parser.ParseInput(e.ToString())
                    .Item1
                    .Match(
                        // Can't have None/Command as expression rvalue
                        None: () => "",
                        Some: (foo) => foo.ToString()) == e.ToString(),
                 $"Failed on: {e} != {Parser.ParseInput(e.ToString()).Item1}");
            }
        }
    }
}