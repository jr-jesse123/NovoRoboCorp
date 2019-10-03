Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial
Imports LibNovoRoboCorp

Partial Public Class GestorVivo
    Implements IEquatable(Of GestorVivo)
    Private _EMAIL As String
    Private _TelefoneCelular As String
    Private _TelefoneFixo As String
    Private _CPF As String
    Private _NOME As String

    Public Property EMPRESA As List(Of ClienteVivo)
    Public Property EMAIL As String
        Get
            Return _EMAIL
        End Get
        Set
            _EMAIL = Value
        End Set
    End Property

    Public Property TelefoneCelular As String
        Get
            Return _TelefoneCelular
        End Get
        Set
            Dim semletras = FuncoesUteis.RemoveSpecialCharacters(Value)
            _TelefoneCelular = semletras
        End Set
    End Property

    Public Property TelefoneFixo As String
        Get
            Return _TelefoneFixo
        End Get
        Set
            Dim semletras = FuncoesUteis.RemoveSpecialCharacters(Value)
            _TelefoneFixo = semletras
        End Set
    End Property

    <Key>
    <StringLength(11)>
    Public Property CPF As String
        Get
            Return _CPF
        End Get
        Set
            If Value.Length <= 11 Then
                _CPF = Value
            Else
                _CPF = "00000000000"
            End If


        End Set
    End Property

    Public Property Master As Boolean

    Public Property NOME As String
        Get
            Return _NOME
        End Get
        Set
            _NOME = Value
        End Set
    End Property

    Public Property Empresas As List(Of ClienteVivo)


    Sub New()

    End Sub

    Public Function Equals(other As GestorVivo) As Boolean Implements IEquatable(Of GestorVivo).Equals
        Return IIf(other.CPF = Me.CPF, True, False)
    End Function
End Class
