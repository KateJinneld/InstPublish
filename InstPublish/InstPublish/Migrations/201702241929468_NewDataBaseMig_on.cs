namespace InstPublish.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewDataBaseMig_on : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Steps", "InstructionId", "dbo.Instructions");
            DropIndex("dbo.Steps", new[] { "InstructionId" });
            CreateTable(
                "dbo.Opinions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        InstructionId = c.Int(),
                        OpinionsValue = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Instructions", t => t.InstructionId)
                .Index(t => t.InstructionId);
            
            CreateTable(
                "dbo.TagInstructions",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Instruction_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Instruction_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Instructions", t => t.Instruction_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Instruction_Id);
            
            AddColumn("dbo.Comments", "Author", c => c.String());
            AddColumn("dbo.Comments", "InstructionId", c => c.Int());
            AddColumn("dbo.Comments", "Contetnt", c => c.String());
            AddColumn("dbo.Instructions", "Author", c => c.String());
            AddColumn("dbo.Instructions", "DateOfCreation", c => c.DateTime(nullable: false));
            AddColumn("dbo.Steps", "NumberOfStep", c => c.Int(nullable: false));
            AlterColumn("dbo.Steps", "InstructionId", c => c.Int());
            CreateIndex("dbo.Comments", "InstructionId");
            CreateIndex("dbo.Steps", "InstructionId");
            AddForeignKey("dbo.Comments", "InstructionId", "dbo.Instructions", "Id");
            AddForeignKey("dbo.Steps", "InstructionId", "dbo.Instructions", "Id");
            DropColumn("dbo.Comments", "Text");
            DropColumn("dbo.Comments", "Date");
            DropColumn("dbo.Instructions", "Description");
            DropColumn("dbo.Instructions", "Date");
            DropColumn("dbo.Steps", "Number");
            DropTable("dbo.Likes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        isLike = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Steps", "Number", c => c.Int(nullable: false));
            AddColumn("dbo.Instructions", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Instructions", "Description", c => c.String());
            AddColumn("dbo.Comments", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Comments", "Text", c => c.String());
            DropForeignKey("dbo.Steps", "InstructionId", "dbo.Instructions");
            DropForeignKey("dbo.TagInstructions", "Instruction_Id", "dbo.Instructions");
            DropForeignKey("dbo.TagInstructions", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.Opinions", "InstructionId", "dbo.Instructions");
            DropForeignKey("dbo.Comments", "InstructionId", "dbo.Instructions");
            DropIndex("dbo.TagInstructions", new[] { "Instruction_Id" });
            DropIndex("dbo.TagInstructions", new[] { "Tag_Id" });
            DropIndex("dbo.Steps", new[] { "InstructionId" });
            DropIndex("dbo.Opinions", new[] { "InstructionId" });
            DropIndex("dbo.Comments", new[] { "InstructionId" });
            AlterColumn("dbo.Steps", "InstructionId", c => c.Int(nullable: false));
            DropColumn("dbo.Steps", "NumberOfStep");
            DropColumn("dbo.Instructions", "DateOfCreation");
            DropColumn("dbo.Instructions", "Author");
            DropColumn("dbo.Comments", "Contetnt");
            DropColumn("dbo.Comments", "InstructionId");
            DropColumn("dbo.Comments", "Author");
            DropTable("dbo.TagInstructions");
            DropTable("dbo.Opinions");
            CreateIndex("dbo.Steps", "InstructionId");
            AddForeignKey("dbo.Steps", "InstructionId", "dbo.Instructions", "Id", cascadeDelete: true);
        }
    }
}
