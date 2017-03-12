using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstPublish.Models
{
    public class Instruction
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public string LinkToYouTubeVideo { get; set; }
        public DateTime DateOfCreation { get; set; }
        public virtual List<Step> Steps { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Tag> Tags { get; set; }
        public virtual List<Opinion> Opinions { get; set; }

        public Instruction()
        {
            Steps = new List<Step>();
            Tags = new List<Tag>();
            Comments = new List<Comment>();
            Opinions = new List<Opinion>();
        }
    }
}