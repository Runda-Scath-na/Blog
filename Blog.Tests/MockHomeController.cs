using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Controllers;
using Blog.Data;
using Blog.Domain;
using Blog.Models;
using Moq;

namespace Blog.Tests {
    public class MockHomeController
    {

        private BlogViewModel _blogViewModel;
        private bool _withModelError = false;

        public MockHomeController WithModelError() {
            this._withModelError = true;
            return this;
        }

        public MockHomeController PostCreate(BlogViewModel blogViewModel) {
            this._blogViewModel = blogViewModel;
            return this;
        }

        public void VerifyAdd(Func<Times> times) {

            var mockBlogs = new Mock<DbSet<Blogs>>();
            var controller = GetMockedController(mockBlogs);

            if (_withModelError) {

                controller.ViewData.ModelState.AddModelError("Key", "Value");   // any error will do
            }
            controller.NewPost(_blogViewModel);

            mockBlogs.Verify(x => x.Add(It.Is<Blogs>(s => s.PostTitle == _blogViewModel.PostTitle)), times);
        }

        private HomeController GetMockedController(Mock<DbSet<Blogs>> mockBlogs) {

            var mockContext = new Mock<BlogContext>();
            mockContext.Setup(x => x.Blogs).Returns(mockBlogs.Object);

            return new HomeController(mockContext.Object);
        }
    }
}
