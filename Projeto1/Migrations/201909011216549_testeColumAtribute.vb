Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class testeColumAtribute
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.TESTEs", "DataPirvate", Function(c) c.Int(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.TESTEs", "DataPirvate")
        End Sub
    End Class
End Namespace
