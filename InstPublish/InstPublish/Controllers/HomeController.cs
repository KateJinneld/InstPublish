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

        public DateTime DataTime { get; private set; }

        public ActionResult Index()
        {
            List<Instruction> list = new List<Instruction>();
                ViewBag.Count = list.Count;
            ViewBag.UserId = User.Identity.GetUserId();
                return View(dataBase.Instructions);
            
        }


        [Authorize]
        public ActionResult CreateInstruction(int instructionId)
        {
            if (instructionId != 0)
            {
                Instruction instruction = dataBase.Instructions.FirstOrDefault(x => x.Id == instructionId);
                return View(instruction);
            }
            return View(new Instruction());
        }

        [Authorize]
        public ActionResult RedactInstruction(int instructionId)
        {
            Instruction instruction = dataBase.Instructions.FirstOrDefault(x => x.Id == instructionId);
            return View(instruction);
        }


        [HttpPost]
        public ActionResult AddInstruction(Instruction newInstruction, int[] selectedTags)
        {
            if (ModelState.IsValid)
            {
                newInstruction.Author = User.Identity.Name;
                newInstruction.DateOfCreation = DateTime.Now;
                newInstruction.UserId = User.Identity.GetUserId();
                dataBase.Instructions.Add(newInstruction);
                dataBase.SaveChanges();

            }
            else
            {
                ModelState.AddModelError("", "Введены некорректные данные");
            }
            return RedirectToAction("Index");
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
        public ActionResult DeleteInstruction(int instructionId)
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
            return RedirectToAction("Index");
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