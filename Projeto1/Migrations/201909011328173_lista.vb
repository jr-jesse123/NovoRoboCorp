Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class lista
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.teste4",
                Function(c) New With
                    {
                        .id = c.Int(nullable := False, identity := True)
                    }) _
                .PrimaryKey(Function(t) t.id)
            
            AddColumn("dbo.TESTE2", "teste4_id", Function(c) c.Int())
            CreateIndex("dbo.TESTE2", "teste4_id")
            AddForeignKey("dbo.TESTE2", "teste4_id", "dbo.teste4", "id")
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.TESTE2", "teste4_id", "dbo.teste4")
            DropIndex("dbo.TESTE2", New String() { "teste4_id" })
            DropColumn("dbo.TESTE2", "teste4_id")
            DropTable("dbo.teste4")
        End Sub
    End Class
End Namespace
