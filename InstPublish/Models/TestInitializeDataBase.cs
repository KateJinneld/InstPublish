using InstPublish.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    //public class InstuctionDbInitializer : DropCreateDatabaseAlways<InstructionContext>
    //{
        //protected override void Seed(InstructionContext context)
        //{
        //    Instruction s1 = new Instruction
        //    {
        //        Id = 1,
        //        UserId = "d19-2d6a-4dc9-81b8-6c56086dcc64",
        //        Title = "Приготовление взрывной бомбы",
        //        Category = "Химия",
        //        Author = "MihA_aa@mail.ru",
        //        LinkToYouTubeVideo = "...",
        //        //Opinions = new List<Opinion>(),
        //        DateOfCreation = DateTime.Now
        //    };
        //    Instruction s2 = new Instruction
        //    {
        //        Id = 2,
        //        UserId = "d19-2d6a-4dc9-81b8-6c56086dcc64",
        //        Title = "Инструкция по взрыву банкомата",
        //        Category = "Преступление",
        //        Author = "MihA_aa@mail.ru",
        //        LinkToYouTubeVideo = "...",
              
        //        DateOfCreation = DateTime.Now
        //    };
        //    Instruction s3 = new Instruction
        //    {
        //        Id = 3,
        //        UserId = "d19-2d6a-4dc9-81b8-6c56086dcc64",
        //        Title = "Создание кошелька webmoney",
        //        Category = "Веб-инструкция",
        //        Author = "MihA_aa@mail.ru",
        //        LinkToYouTubeVideo = "...",
              
        //        DateOfCreation = DateTime.Now
        //    };
        //    Instruction s4 = new Instruction
        //    {
        //        Id = 4,
        //        UserId = "d19-2d6a-4dc9-81b8-6c56086dcc64",
        //        Title = "Приготовление борща",
        //        Category = "Кулинария",
        //        Author = "MihA_aa@mail.ru",
        //        LinkToYouTubeVideo = "...",
               
        //        DateOfCreation = DateTime.Now
        //    };

        //    context.Instructions.Add(s1);
        //    context.Instructions.Add(s2);
        //    context.Instructions.Add(s3);
        //    context.Instructions.Add(s4);

        //    Step step1 = new Step
        //    {
        //        Id = 1,
        //        Title = "Купить",
        //        NumberOfStep = 1,
        //        Image = "...",
        //        Description = "Делаем то",
        //        InstructionId = 1,
        //    };
        //    Step step2 = new Step
        //    {
        //        Id = 2,
        //        Title = "Приготовить",
        //        NumberOfStep = 2,
        //        Image = "...",
        //        Description = "Делаем сё",
        //        InstructionId = 1,

        //    };
        //    Step step3 = new Step
        //    {
        //        Id = 3,
        //        Title = "Поджечь",
        //        NumberOfStep = 3,
        //        Image = "...",
        //        Description = "Делаем то-то",
        //        InstructionId = 1,
        //    };
        //    Step step4 = new Step
        //    {
        //        Id = 1,
        //        Title = "Нати",
        //        NumberOfStep = 1,
        //        Image = "...",
        //        Description = "Делаем бла-бла",
        //        InstructionId = 2,
        //    };
        //    Step step5 = new Step
        //    {
        //        Id = 2,
        //        Title = "Зарегестрироваться",
        //        NumberOfStep = 2,
        //        Image = "...",
        //        Description = "Делаем сё",
        //        InstructionId = 3,

        //    };
        //    Step step6 = new Step
        //    {
        //        Id = 3,
        //        Title = "Сьесть",
        //        NumberOfStep = 3,
        //        Image = "...",
        //        Description = "Делаем то-то",
        //        InstructionId = 4,
        //    };

        //    context.Steps.Add(step1);
        //    context.Steps.Add(step2);
        //    context.Steps.Add(step3);
        //    context.Steps.Add(step4);
        //    context.Steps.Add(step5);
        //    context.Steps.Add(step6);

        //    Tag c1 = new Tag
        //    {
        //        Id = 1,
        //        Text = "Взрыв",
        //        Instructions = new List<Instruction>() { s1, s2 }
        //    };
        //    Tag c2 = new Tag
        //    {
        //        Id = 2,
        //        Text = "Еда",
        //        Instructions = new List<Instruction>() { s4 }
        //    };
        //    Tag c3 = new Tag
        //    {
        //        Id = 3,
        //        Text = "Веб",
        //        Instructions = new List<Instruction>() { s3 }
        //    };

        //    context.Tags.Add(c1);
        //    context.Tags.Add(c2);
        //    context.Tags.Add(c3);

        //    Comment comment1 = new Comment
        //    {
        //        Id = 1,
        //        UserId = "MihA_aa1@mail.ru",
        //        InstructionId = 1,
        //        Contetnt = "Збс взорвал банкомат, Спасибо"
        //    };
        //    Comment comment2 = new Comment
        //    {
        //        Id = 1,
        //        UserId = "MihA_aa@mail.ru",
        //        InstructionId = 1,
        //        Contetnt = "Рад стараться!"
        //    };
        //    context.Comments.Add(comment1);
        //    context.Comments.Add(comment2);
        //    base.Seed(context);
        //}
   // }
}