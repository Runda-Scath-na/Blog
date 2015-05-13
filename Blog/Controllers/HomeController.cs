using System;
using System.Linq;
using System.Web.Mvc;
using Blog.Data;
using Blog.Domain;
using Blog.Models;

namespace Blog.Controllers {

    public class HomeController : Controller
    {
        private BlogContext _db;
        private BlogViewModel _blogViewModel;

        public HomeController()
        {
            _db = new BlogContext();
        }

        public HomeController(BlogContext blogContext)
        {
            _db = blogContext;
        }

        public ActionResult Index() {

            return View();
        }

        public ActionResult NewPost() {
            ViewBag.Message = "What do you want to blog about?";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewPost(BlogViewModel blogViewModel) {
        
            if (ModelState.IsValid) {

                var blog = new Blogs
                {
                    PostId = Guid.NewGuid(),
                    PostTitle = blogViewModel.PostTitle,
                    PostAuthor = blogViewModel.PostAuthor,
                    PostDate = DateTime.Now,
                    PostTease = blogViewModel.PostTease,
                    PostBody = blogViewModel.PostBody
                };

                _db = new BlogContext();
                
                _db.Blogs.Add(blog);
                _db.SaveChanges();

                return Redirect("Index");
            }
            return View("NewPost");
        }

        public ActionResult Contact() {

            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}