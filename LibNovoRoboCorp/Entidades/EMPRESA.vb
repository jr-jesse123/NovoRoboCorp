Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

Partial Public Class EMPRESA
    Public Property Id As Integer

    Public Property ReceitaAnual As Long

    Public Property CNAE As String

    Public Property CapitalSocial As Long

    <Key>
    <Column(TypeName:="char")>
    <StringLength(14)>
    Public Property CNPJ As String

    Public Property Nome As String

    Public Property Endereço As String
    Public Property Cidade As String

    Public Property CEP As String

    Public Property UF As UFEnum

    Public Property GN As String

    Public Property CARTEIRA As String

    Public Property NrDeLinhas As Integer

    Public Property Observações As String

    Public Property FolhaPagamento As Long

    Public Property Gestores As List(Of GESTOR)
    Public Property Socios As List(Of Socio)
    Public Property linhas As List(Of LINHA)

    Sub New(_CNPJ)
        Me.CNPJ = _CNPJ
    End Sub
End Class
