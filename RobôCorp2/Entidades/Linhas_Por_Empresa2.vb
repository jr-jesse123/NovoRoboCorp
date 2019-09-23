Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("corpscrapper.Linhas Por Empresa")>
Partial Public Class Linhas_Por_Empresa2
    <Column(TypeName:="char")>
    <StringLength(14)>
    Public Property CNPJ As String

    <Key>
    <Column("count(CNPJ)")>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property count_CNPJ_ As Long
End Class
