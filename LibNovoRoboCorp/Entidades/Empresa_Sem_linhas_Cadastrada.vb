Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("corpscrapper.Empresa Sem linhas Cadastradas")>
Partial Public Class Empresa_Sem_linhas_Cadastrada
    Public Property Id As Integer?

    Public Property ReceitaAnual As Long?

    <Column(TypeName:="text")>
    <StringLength(65535)>
    Public Property CNAE As String

    Public Property CapitalSocial As Long?

    <Key>
    <Column(TypeName:="char")>
    <StringLength(14)>
    Public Property CNPJ As String

    <Column(TypeName:="text")>
    <StringLength(65535)>
    Public Property Nome As String

    <Column(TypeName:="text")>
    <StringLength(65535)>
    Public Property Endereço As String

    <Column(TypeName:="char")>
    <StringLength(8)>
    Public Property CEP As String

    <Column(TypeName:="char")>
    <StringLength(2)>
    Public Property UF As String

    <StringLength(200)>
    Public Property GN As String

    <StringLength(50)>
    Public Property CARTEIRA As String

    Public Property NrDeLinhas As Integer?

    <Column(TypeName:="text")>
    <StringLength(65535)>
    Public Property Observações As String

    Public Property FolhaPagamento As Long?
End Class
