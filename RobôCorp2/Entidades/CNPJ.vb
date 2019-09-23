Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("corpscrapper.CNPJS")>
Partial Public Class CNPJ
    <Key>
    <Column(TypeName:="char")>
    <StringLength(14)>
    Public Property CNPJS As String

    <Column(TypeName:="enum")>
    <StringLength(65532)>
    Public Property ENRIQUECIDO As String

    <Column(TypeName:="char")>
    <StringLength(2)>
    Public Property UF As String
End Class
