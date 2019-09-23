Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("corpscrapper.GESTORES")>
Partial Public Class GESTORE
    Public Property Id As Integer

    <Column(TypeName:="char")>
    <StringLength(14)>
    Public Property EMPRESA As String

    <StringLength(200)>
    Public Property EMAIL As String

    <StringLength(15)>
    Public Property TelefoneCelular As String

    <StringLength(15)>
    Public Property TelefoneFixo As String

    <Column(TypeName:="char")>
    <Required>
    <StringLength(11)>
    Public Property CPF As String

    <StringLength(15)>
    Public Property Master As String

    <StringLength(200)>
    Public Property NOME As String

    <Column(TypeName:="timestamp")>
    Public Property timespan As Date?

    Sub New()
        timespan = Now
    End Sub

End Class
