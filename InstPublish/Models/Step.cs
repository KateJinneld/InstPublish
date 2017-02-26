using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InstPublish.Models
{
    public class Step
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int NumberOfStep { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int? InstructionId { get; set; }
        public virtual Instruction Instruction { get; set; }
    }
}