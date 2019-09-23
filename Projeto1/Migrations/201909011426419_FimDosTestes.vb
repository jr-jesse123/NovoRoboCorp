Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class FimDosTestes
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropForeignKey("dbo.TESTE3TESTE", "TESTE3_id", "dbo.TESTE3")
            DropForeignKey("dbo.TESTE3TESTE", "TESTE_id", "dbo.TESTEs")
            DropForeignKey("dbo.TESTE2", "teste_id", "dbo.TESTEs")
            DropForeignKey("dbo.TESTE2", "teste4_id", "dbo.teste4")
            DropIndex("dbo.TESTE2", New String() { "teste_id" })
            DropIndex("dbo.TESTE2", New String() { "teste4_id" })
            DropIndex("dbo.TESTE3TESTE", New String() { "TESTE3_id" })
            DropIndex("dbo.TESTE3TESTE", New String() { "TESTE_id" })
            DropTable("dbo.teste4")
            DropTable("dbo.TESTE2")
            DropTable("dbo.TESTEs")
            DropTable("dbo.TESTE3")
            DropTable("dbo.teste5")
            DropTable("dbo.TESTE3TESTE")
        End Sub
        
        Public Overrides Sub Down()
            CreateTable(
                "dbo.TESTE3TESTE",
                Function(c) New With
                    {
                        .TESTE3_id = c.Int(nullable := False),
                        .TESTE_id = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) New With { t.TESTE3_id, t.TESTE_id })
            
            CreateTable(
                "dbo.teste5",
                Function(c) New With
                    {
                        .id = c.Int(nullable := False, identity := True)
                    }) _
                .PrimaryKey(Function(t) t.id)
            
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
            
            CreateTable(
                "dbo.TESTEs",
                Function(c) New With
                    {
                        .id = c.Int(nullable := False, identity := True),
                        .name = c.String(unicode := false),
                        .data = c.DateTime(nullable := False, precision := 0),
                        .NovoCampo = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.id)
            
            CreateTable(
                "dbo.TESTE2",
                Function(c) New With
                    {
                        .id = c.Int(nullable := False, identity := True),
                        .name = c.String(unicode := false),
                        .data = c.DateTime(nullable := False, precision := 0),
                        .NovoCampo = c.Boolean(nullable := False),
                        .DataPirvate = c.Int(nullable := False),
                        .teste_id = c.Int(),
                        .teste4_id = c.Int()
                    }) _
                .PrimaryKey(Function(t) t.id)
            
            CreateTable(
                "dbo.teste4",
                Function(c) New With
                    {
                        .id = c.Int(nullable := False, identity := True)
                    }) _
                .PrimaryKey(Function(t) t.id)
            
            CreateIndex("dbo.TESTE3TESTE", "TESTE_id")
            CreateIndex("dbo.TESTE3TESTE", "TESTE3_id")
            CreateIndex("dbo.TESTE2", "teste4_id")
            CreateIndex("dbo.TESTE2", "teste_id")
            AddForeignKey("dbo.TESTE2", "teste4_id", "dbo.teste4", "id")
            AddForeignKey("dbo.TESTE2", "teste_id", "dbo.TESTEs", "id")
            AddForeignKey("dbo.TESTE3TESTE", "TESTE_id", "dbo.TESTEs", "id", cascadeDelete := True)
            AddForeignKey("dbo.TESTE3TESTE", "TESTE3_id", "dbo.TESTE3", "id", cascadeDelete := True)
        End Sub
    End Class
End Namespace
