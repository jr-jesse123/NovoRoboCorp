Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class testerelacionamento3
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.TESTE2",
                Function(c) New With
                    {
                        .id = c.Int(nullable:=False, identity:=True),
                        .name = c.String(unicode:=False),
                        .data = c.DateTime(nullable:=False, precision:=0),
                        .NovoCampo = c.Boolean(nullable:=False),
                        .DataPirvate = c.Int(nullable:=False),
                        .teste_id = c.Int()
                    }) _
                .PrimaryKey(Function(t) t.id) _
                .ForeignKey("dbo.TESTEs", Function(t) t.teste_id) _
                .Index(Function(t) t.teste_id)

            CreateTable(
                "dbo.TESTE3",
                Function(c) New With
                    {
                        .id = c.Int(nullable := False, identity := True),
                        .name = c.String(unicode := false),
                        .data = c.DateTime(nullable := False, precision := 0),
                        .NovoCampo = c.Boolean(nullable := False),
                        .DataPirvate = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.TESTE2", "teste_id", "dbo.TESTEs")
            DropIndex("dbo.TESTE2", New String() { "teste_id" })
            DropTable("dbo.TESTE3")
            DropTable("dbo.TESTE2")
        End Sub
    End Class
End Namespace
