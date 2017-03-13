using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InstPublish.Models
{
    public class InstructionAndStep
    {
        public Instruction Instruction { get; set; }
        public Step Step { get; set; }
        public Comment Comment { get; set; }
    }
}