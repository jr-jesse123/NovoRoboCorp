Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("corpscrapper.Socios")>
Partial Public Class Socio2
    <Key>
    <Column(TypeName:="char")>
    <StringLength(14)>
    Public Property CPFOUCNPJ As String

    <StringLength(200)>
    Public Property NOME As String

    <StringLength(20)>
    Public Property Telefone As String

    <StringLength(20)>
    Public Property CNPJ As String


    Sub New(socio As DataRow, _CNPJ As String)
        Me.CPFOUCNPJ = socio(1)
        Me.NOME = socio(2)
        Me.Telefone = socio(7)
        Me.CNPJ = _CNPJ
    End Sub
End Class
