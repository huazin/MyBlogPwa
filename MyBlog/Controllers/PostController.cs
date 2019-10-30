using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Controllers
{
    public class PostController : Controller
    {
        private IBlogService _blogService {get;set;}
        public PostController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        // GET: Post/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogPost item)
        {
            if (ModelState.IsValid)
            {
                _blogService.Inserir(item);
                return RedirectToAction("Index", "Home");
            }

            return View(item);
        }
    }
}