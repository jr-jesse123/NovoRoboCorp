Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("corpscrapper.Socios")>
Partial Public Class Socio
    <Key>
    Public Property CPFOUCNPJ As String

    Public Property NOME As String

    Public Property Telefone As String

    Public Property Empresas As List(Of EMPRESA)

    Sub New(socio As DataRow, _CNPJ As String)
        Me.CPFOUCNPJ = socio(1)
        Me.NOME = socio(2)
        Me.Telefone = socio(7)
        'Me.CNPJ = _CNPJ
    End Sub
End Class
