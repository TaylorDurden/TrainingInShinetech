using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoveDemo.DomainModels;
using LoveDemo.Models;

namespace LoveDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoveRepository _repository;

        public HomeController(ILoveRepository repository)
        {
            _repository = repository;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Send(Person person)
        {
            //var loveRepository=new LoveRepository();
            //var allUsers = loveRepository.GetLoversByName(person.Name);
            var allLover = _repository.GetLoversByName(person.Name);
            return View(allLover);
        }
    }
}