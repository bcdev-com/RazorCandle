﻿using System;
using System.IO;
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

        [Test]
        public void FileExploration()
        {
            Generator.Generate(new Arguments
            {
                Source = "views\\ConventionBased.cshtml",
                Model = "{FirstName: 'Jose', LastName: 'Juan'}"
            });

            File.ReadAllText("views\\ConventionBased.html")
                .Should().Contain("<script type=\"text/javascript\" src=\"others/Hello.js\" />");
        }

        [Test]
        public void CanGenerateTemplateWithEmptyNestedTemplate()
        {
            Assert.DoesNotThrow(() => Generator.Generate(new Arguments { Source = "views\\PageWithEmptyNested.cshtml" }));
        }

        [Test]
        public void CanGenerateWithNestedWithoutExtension()
        {
            Generator.Generate(new Arguments
            {
                Source = "views\\PageWithNestedNoExtension.cshtml"
            });

            File.ReadAllText("views\\PageWithNestedNoExtension.html")
                .Should().Contain("1 + 1 is 2");
        }

        [Test]
        public void CanGenerateWithAspNewViewFolderStructure()
        {
            Generator.Generate(new Arguments
            {
                Source = "views\\PageWithAspNetNestedTemplate.cshtml",
                AspNetProjectFolder = "views\\ExampleASPNetFolder\\"
            });

            File.ReadAllText("views\\PageWithAspNetNestedTemplate.html")
                .Should().Contain("1 + 1 is 2");
        }

        [Test]
        public void CanRenderTemplateWithWildcard()
        {
            Generator.Generate(new Arguments
            {
                Source = "views\\PageWithWildcard.cshtml"
            });

            File.ReadAllText("views\\PageWithWildcard.html")
                .Should().Contain("Hello")
                .And.Contain("ByeBye");
        }

        [Test]
        public void CanRenderTemplateWithForEachFile()
        {
            Generator.Generate(new Arguments
            {
                Source = "views\\PageWithForEachFile.cshtml"
            });

            File.ReadAllText("views\\PageWithForEachFile.html")
                .Should().Be.EqualTo("others\\ByeBye.jsothers\\Hello.js");
        }

        [Test]
        public void CanRenderTemplateWithForEachFileAndToUrl()
        {
            Generator.Generate(new Arguments
            {
                Source = "views\\PageWithForEachFileAndToUrl.cshtml"
            });

            File.ReadAllText("views\\PageWithForEachFileAndToUrl.html")
                .Should().Be.EqualTo("others/ByeBye.jsothers/Hello.js");
        }

        [Test]
        public void CanGenerateWithBrokenPathArgument()
        {
            //This is because there is an extrange feature of c# that remove double quotes from command line arguments..

            Generator.Generate(new Arguments
            {
                Source = "views\\Page1.cshtml\""
            });

            File.ReadAllText("views\\Page1.html")
                .Should().Contain("1 + 1 is 2");
        }

        [Test]
        public void CanGenerateWithBrokenPathAspNetFolderArgument()
        {
            //This is because there is an extrange feature of c# that remove double quotes from command line arguments..


            Generator.Generate(new Arguments
            {
                Source = "views\\PageWithAspNetNestedTemplate.cshtml",
                AspNetProjectFolder = "views\\ExampleASPNetFolder\\\""
            });

            File.ReadAllText("views\\PageWithAspNetNestedTemplate.html")
                .Should().Contain("1 + 1 is 2");
        }
    }
}