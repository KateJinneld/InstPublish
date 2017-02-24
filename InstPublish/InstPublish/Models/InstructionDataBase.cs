using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InstPublish.Models
{
    public class InstructionDataBase
    {
        public InstructionContext DataBase { get; set; }
        public List<Instruction> Instructions { get { return DataBase.Instructions.ToList(); }
        set { Instructions = value; } }
        public InstructionDataBase()
        {
            DataBase = new InstructionContext();
        }
    }
}