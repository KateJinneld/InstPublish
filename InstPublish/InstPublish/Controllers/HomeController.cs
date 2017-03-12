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
using System.IO;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Web.UI.WebControls;

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

        public ActionResult Step(int instructionId,int stepId)
        {
            return View(dataBase.Steps.FirstOrDefault(x => (x.Id == stepId) && (x.InstructionId == instructionId)));
        }

        [Authorize]
        public ActionResult CreateInstruction(int instructionId=0)
        {
            if (instructionId != 0)
            {
                Instruction instruction = dataBase.Instructions.FirstOrDefault(x => x.Id == instructionId);
                instruction.Steps = instruction.Steps.OrderBy(obj => obj.NumberOfStep).ToList();
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
                if (dataBase.Instructions.FirstOrDefault(x => x.Id == newInstruction.Id) != null)
                    DeleteInstruction(newInstruction.Id);
                dataBase.Instructions.Add(newInstruction);
                dataBase.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "Введены некорректные данные");
            }
            return RedirectToAction("Index");
        }

        public ActionResult ChangeStep(Step changedStep)
        {
            Step step = dataBase.Steps.FirstOrDefault(x => x.Id == changedStep.Id);
            step.Title = changedStep.Title;
            step.Description = changedStep.Description;
            dataBase.SaveChanges();
            return RedirectToAction("CreateInstruction", new { instructionId = changedStep.InstructionId});
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
        public void DeleteStep(int stepId)
        {
            Step step = dataBase.Steps.FirstOrDefault(x => x.Id == stepId);
            if (step != null)
            {
                dataBase.Steps.Remove(step);
                dataBase.SaveChanges();
            }
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

        public ActionResult SaveUploadedFile(int instructionId, int stepId)
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {

                        var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\WallImages", Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);

                        Account account = new Account(
                                        "mrgoldy",
                            "832584879924415",
                            "DynmE9VRkVfJHDPlIeFvw5BDH40");
                        Cloudinary cloudinary = new Cloudinary(account);
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(path)
                        };
                        var uploadResult = cloudinary.Upload(uploadParams);
                        dataBase.Steps.FirstOrDefault(x => x.Id == stepId).Image = "http://res.cloudinary.com"+uploadResult.Uri.AbsolutePath;
                        dataBase.SaveChanges();
                        
                    }

                }
            }
            catch 
            {
                isSavedSuccessfully = false;
            }

            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }














    }
}