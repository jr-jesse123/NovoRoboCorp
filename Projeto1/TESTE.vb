Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Public Class TESTE
    Public Property id As Integer
    Public Property name As String
    Public Property data As Date
    Public Property NovoCampo As Boolean

    Public Property testes3 As List(Of TESTE3)
End Class


Public Class TESTE2
    Public Property id As Integer
    Public Property name As String
    Public Property data As Date
    Public Property NovoCampo As Boolean
    <Column>
    Private Property DataPirvate As Integer

    Public Property teste As TESTE
End Class


Public Class TESTE3
    Public Property id As Integer
    Public Property name As String
    Public Property data As Date
    Public Property NovoCampo As Boolean
    <Column>
    Private Property DataPirvate As Integer

    Public Property testes1 As List(Of TESTE)





End Class

Public Class teste4
    Property id As Integer
    Property testes As List(Of TESTE2)
End Class


Public Class teste5
    Property id As Integer
End Class
