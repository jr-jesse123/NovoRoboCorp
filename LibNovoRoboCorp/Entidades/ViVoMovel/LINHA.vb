Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial


Partial Public Class LINHA
    Public Property Empresa As ClienteVivo
    <Key>
    Public Property NrDaLinha As String
    Public Property CriadoEm As String
    Public Property PortadoEm As Date
    Public Property FidelizadoAte As Date
    Public Property Operadora As OperadoraEnum

    Sub New(linha As DataRow, _CNPJ As String)

    End Sub
End Class
