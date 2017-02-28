using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InstPublish.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;

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
            ViewBag.UserId = User.Identity.GetUserId();
                return View(dataBase.Instructions);
            
        }
        [Authorize]
        public ActionResult CreateInstruction()
        {

           
           
            return View(dataBase.Instructions.ToList()[0]);
        }


        [HttpPost]
        public string SetOpinion(int instructionId, bool opinionValue)
        {
            string userId = User.Identity.GetUserId();
            Instruction instr = dataBase.Instructions.FirstOrDefault(x => x.Id == instructionId);
            Opinion opinion = dataBase.Opinions.Where(s => s.InstructionId == instructionId).
                FirstOrDefault(s => s.UserId == userId);
            if (opinion != null)
            {
                if (opinionValue == opinion.OpinionsValue) return (instr.Opinions.Where(x => x.OpinionsValue).Count() - instr.Opinions.Where(x => !x.OpinionsValue).Count()).ToString();
                else opinion.OpinionsValue = opinionValue;
            }
            else
            {
                opinion = new Opinion()
                {
                    Instruction = dataBase.Instructions.FirstOrDefault(x => x.Id == instructionId),
                    InstructionId = instructionId,
                    OpinionsValue = opinionValue,
                    UserId = User.Identity.GetUserId()
                };
                dataBase.Opinions.Add(opinion);
            } 
            dataBase.SaveChanges();
           
            return (instr.Opinions.Where(x => x.OpinionsValue).Count() - instr.Opinions.Where(x => !x.OpinionsValue).Count()).ToString();
        }
        
    

        [HttpPost]
        public void DeleteInstruction(int instructionId)
        {
            Instruction instructions = dataBase.Instructions
                .Include(x => x.Steps)
                .Include(x => x.Comments)
                .Include(x => x.Opinions)
                .Include(x => x.Tags)
                .FirstOrDefault(x => x.Id == instructionId);

            if (instructions != null)
            {
                dataBase.Instructions.Remove(instructions);
                dataBase.SaveChanges();
            }
        }




        [HttpPost]
        public ActionResult AddStep(int NumberOfStep, int InstructionId)
        {
            try
            {
                Step step = new Step();
                step.NumberOfStep = NumberOfStep;
                step.InstructionId = InstructionId;
                dataBase.Steps.Add(step);
                dataBase.SaveChanges();
                return Json(new { Message = "Successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { Message = "There is error in function \"AddStep\""});
            }
        }


       [HttpPost]
        public ActionResult StepsModule(int instructionId)
        {      
               List<Step> stepList = dataBase.Steps.Where(x => x.InstructionId == instructionId).ToList();
            return PartialView(stepList);
        }


        [Authorize]
        public ActionResult Contact()
        {

            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}