using InstPublish.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InstPublish.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public InstructionContext dataBase = new InstructionContext();
        
        public ActionResult Index()
        {
            List<Instruction> list = new List<Instruction>();
               // list = dataBase.Instructions//.Where(x => x.Id>0).ToList();
                ViewBag.Count = list.Count;
                return View(dataBase.Instructions);
            
        }
        [Authorize]
        public ActionResult CreateInstruction()
        {

           
           
            return View(dataBase.Instructions.ToList()[0]);
        }

        public ActionResult Contact()
        {

            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}