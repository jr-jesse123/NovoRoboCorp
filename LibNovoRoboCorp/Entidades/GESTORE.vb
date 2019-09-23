Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("corpscrapper.GESTORES")>
Partial Public Class GESTOR
    Public Property Id As Integer
    <Column(TypeName:="char")>
    <StringLength(14)>
    Public Property EMPRESA As String
    Public Property EMAIL As String
    Public Property TelefoneCelular As String
    Public Property TelefoneFixo As String
    <Key>
    <StringLength(11)>
    Public Property CPF As String
    Public Property Master As Boolean
    Public Property NOME As String
    Public Property timespan As Date
    Public Property Empresas As List(Of EMPRESA)

    Sub New()
        timespan = Now
    End Sub

End Class
