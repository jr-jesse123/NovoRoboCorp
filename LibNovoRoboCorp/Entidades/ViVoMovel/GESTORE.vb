Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial


Partial Public Class GESTOR


    Public Property EMPRESA As List(Of ClienteVivo)
    Public Property EMAIL As String
    Public Property TelefoneCelular As String
    Public Property TelefoneFixo As String
    <Key>
    <StringLength(11)>
    Public Property CPF As String
    Public Property Master As Boolean
    Public Property NOME As String
    Public Property Empresas As List(Of ClienteVivo)


    Sub New()

    End Sub

End Class
