Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("ClienteVivo")>
Partial Public Class ClienteVivo
    Inherits CadastroCNPJEnriquecido

    Public Property GN As String
    Public Property CARTEIRA As String
    Public Property Observações As String
    Public Property Gestores As List(Of GESTOR)
    Public Property linhas As List(Of LINHA)


End Class
