Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class NparaN
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.TESTE3TESTE",
                Function(c) New With
                    {
                        .TESTE3_id = c.Int(nullable := False),
                        .TESTE_id = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) New With { t.TESTE3_id, t.TESTE_id }) _
                .ForeignKey("dbo.TESTE3", Function(t) t.TESTE3_id, cascadeDelete := True) _
                .ForeignKey("dbo.TESTEs", Function(t) t.TESTE_id, cascadeDelete := True) _
                .Index(Function(t) t.TESTE3_id) _
                .Index(Function(t) t.TESTE_id)
            
            DropColumn("dbo.TESTEs", "DataPirvate")
        End Sub
        
        Public Overrides Sub Down()
            AddColumn("dbo.TESTEs", "DataPirvate", Function(c) c.Int(nullable := False))
            DropForeignKey("dbo.TESTE3TESTE", "TESTE_id", "dbo.TESTEs")
            DropForeignKey("dbo.TESTE3TESTE", "TESTE3_id", "dbo.TESTE3")
            DropIndex("dbo.TESTE3TESTE", New String() { "TESTE_id" })
            DropIndex("dbo.TESTE3TESTE", New String() { "TESTE3_id" })
            DropTable("dbo.TESTE3TESTE")
        End Sub
    End Class
End Namespace
