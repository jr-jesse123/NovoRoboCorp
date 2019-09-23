Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class initial
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.TESTEs",
                Function(c) New With
                    {
                        .id = c.Int(nullable := False, identity := True),
                        .name = c.String(unicode := false),
                        .data = c.DateTime(nullable := False, precision := 0)
                    }) _
                .PrimaryKey(Function(t) t.id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.TESTEs")
        End Sub
    End Class
End Namespace
