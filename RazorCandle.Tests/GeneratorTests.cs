﻿using System.IO;
using NUnit.Framework;
using SharpTestsEx;

namespace RazorCandle.Tests
{
    public class GeneratorTests
    {
        [Test]
        public void CanGenerateASimpleFile()
        {
            Generator.Generate(new Arguments
                                   {
                                       Source = "views\\Page1.cshtml"
                                   });

            File.ReadAllText("views\\Page1.html")
                .Should().Contain("1 + 1 is 2");
        }

        [Test]
        public void CanGenerateWithNested()
        {
            Generator.Generate(new Arguments
            {
                Source = "views\\PageWithNested.cshtml"
            });

            File.ReadAllText("views\\PageWithNested.html")
                .Should().Contain("1 + 1 is 2");
        }

        [Test]
        public void CanGenerateWithNestedInRelativePath()
        {
            Generator.Generate(new Arguments
            {
                Source = "views\\nestedpath\\PageWithNestedWithRelativePath.cshtml"
            });

            File.ReadAllText("views\\nestedpath\\PageWithNestedWithRelativePath.html")
                .Should().Contain("1 + 1 is 2");
        }

        [Test]
        public void CanGenerateWithSimpleJsonModel()
        {
            Generator.Generate(new Arguments
            {
                Source = "views\\PageWithModel.cshtml",
                Model = "{Name: 'Jose'}"
            });

            File.ReadAllText("views\\PageWithModel.html")
                .Should().Contain("hello Jose");
        }
    }
}