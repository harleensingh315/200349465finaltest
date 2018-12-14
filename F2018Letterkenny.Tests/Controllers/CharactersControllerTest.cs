using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using F2018Letterkenny.Controllers;
using F2018Letterkenny.Models;
using System.Collections.Generic;
using System.Linq;

namespace F2018Letterkenny.Tests.Controllers
{
    [TestClass]
    public class CharactersControllerTest
    {
        CharactersController controller;
        Mock<IMockCharacter> mock;
        List<Character> characters;        

        [TestInitialize]
        public void TestInitialize()
        {
            mock = new Mock<IMockCharacter>();

            characters = new List<Character>
            {
                new Character
                {
                    CharacterId = 43,
                    Name = "Reilly",
                    Description = "Hockey Player with Flow",
                    Quote = "Wheel, snipe, celly boys",
                    Photo = "reilly.png"
                },
                new Character
                {
                    CharacterId = 43,
                    Name = "Jonesy",
                    Description = "Even Dumber Hockey Player",
                    Quote = "Backcheck, Forecheck, Paycheque",
                    Photo = "jonesey.png"
                }
            };

            mock.Setup(m => m.Characters).Returns(characters.AsQueryable());
            controller = new CharactersController(mock.Object);
        }
        [TestMethod]
        public void IndexViewLoads()
        {
            ViewResult result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }
        [TestMethod]
        public void IndexValidLoadsCharacter()
        {
            var actual = (List<Character>)((ViewResult)controller.Index()).Model;
            CollectionAssert.AreEqual(characters, actual);
        }

        public void Create_ViewLoads()
        {
            var result = (ViewResult)controller.Create();
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void Create_Validcharacter()
        {
            Character newCharacter = new Character
            {
                CharacterId = 4,
                Name = "Four",
                City = "London",
                Url = "http://three.ca",
                Logo = "three.gif"
            };
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Create(newCharacter);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Create_InValidCharacter()
        {
            Character newCharacter = new Character();
            controller.ModelState.AddModelError("Unable To Create Grade", "Grade Creation Exception");
            ViewResult result = (ViewResult)controller.Create(newCharacter);
            Assert.AreEqual("Create", result.ViewName);
        }

        
    }
}
    

