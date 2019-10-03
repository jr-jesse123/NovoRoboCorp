Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports LibNovoRoboCorp

Partial Public Class LINHA
    Implements IEquatable(Of LINHA)
    Public Property Empresa As ClienteVivo
    <Key>
    Public Property NrDaLinha As String
    Public Property CriadoEm As String
    Public Property PortadoEm As Date
    Public Property FidelizadoAte As Date
    Public Property Operadora As OperadoraEnum

    Sub New(linha As DataRow, _CNPJ As String)
        Stop
    End Sub

    Public Function Equals(other As LINHA) As Boolean Implements IEquatable(Of LINHA).Equals
        If other.NrDaLinha = Me.NrDaLinha Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
