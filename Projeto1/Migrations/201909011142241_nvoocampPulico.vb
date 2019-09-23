Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class nvoocampPulico
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.TESTEs", "NovoCampo", Function(c) c.Boolean(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.TESTEs", "NovoCampo")
        End Sub
    End Class
End Namespace
