using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InstPublish.Models
{
    public class Opinion
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int? InstructionId { get; set; }
        public virtual Instruction Instruction { get; set; }
        public bool OpinionsValue { get; set; }
    }
}