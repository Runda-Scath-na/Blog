﻿using System;
using System.Data.Entity;
using System.Threading;
using System.Web.Mvc;
using Blog.Controllers;
using Blog.Data;
using Blog.Domain;
using Blog.Models;
using Moq;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Blog.Tests.Controllers {

    [TestFixture]
    public class HomeControllerTest
    {
        private Mock<DbSet<Blogs>> _mockBlogs;
        private BlogViewModel _blogViewModel = new BlogViewModel();

        [SetUp]
        public void SetUp() {

            _mockBlogs = new Mock<DbSet<Blogs>>();
        }

        [Test]
        public void Index() {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void IndexPageDisplayPosts() {


            var controller = GetMockedController(_mockBlogs);
            var result = controller.Index();

            Assert.AreEqual("",result);
        }

        [Test]
        public void NewPost() {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.NewPost() as ViewResult;

            // Assert
            Assert.AreEqual("What do you want to blog about?", result.ViewBag.Message);
        }

        [Test]
        public void NewPostFailed(){

            var controller = GetMockedController(_mockBlogs);
            HomeControllerTest.PostCreate(new BlogViewModel());
            controller.ViewData.ModelState.AddModelError("Key", "Value");
            var result = controller.NewPost(_blogViewModel) as ViewResult;

            Assert.AreEqual("NewPost", result.ViewName);
        }

        [Test]
        public void NewPostSuccessWithTease() { 

            _blogViewModel = new BlogViewModel();
            _blogViewModel.PostTitle = "New Post";
            _blogViewModel.PostAuthor = "Person";
            _blogViewModel.PostTease = "Wahh wahh wahh";
            _blogViewModel.PostBody = "Blah Blah Blah Charlie Brown";

            var controller = new HomeController();
            var result = controller.NewPost(_blogViewModel) as RedirectResult;

            Assert.AreEqual("Index", result.Url);
        }

        [Test]
        public void NewPostSuccessWithOutTease() {

            _blogViewModel = new BlogViewModel();
            _blogViewModel.PostTitle = "New Post";
            _blogViewModel.PostAuthor = "Person";
            _blogViewModel.PostBody = "Blah Blah Blah Charlie Brown";

            var controller = new HomeController();
            var result = controller.NewPost(_blogViewModel) as RedirectResult;

            Assert.AreEqual("Index", result.Url);
        }

        [Test]
        public void Contact() {
            // Arrange
            var controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        
    }
}
