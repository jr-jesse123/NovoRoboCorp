Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class teste2
        Inherits DbMigration
    
        Public Overrides Sub Up()
            MoveTable(name := "Receita.CadastrosCNPJs", newSchema := "'ReceitaFederal")
        End Sub
        
        Public Overrides Sub Down()
            MoveTable(name := "'ReceitaFederal.CadastrosCNPJs", newSchema := "Receita")
        End Sub
    End Class
End Namespace
