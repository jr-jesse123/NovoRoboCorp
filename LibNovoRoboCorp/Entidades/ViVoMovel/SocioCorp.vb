Imports System.ComponentModel.DataAnnotations.Schema


Public Class SocioCorp
    Inherits SociosReceita

    Private _TelefoneCadastrado As String

    Property TelefoneCadastrado As String
        Get
            Return _TelefoneCadastrado
        End Get
        Set
            Dim sonumero = FuncoesUteis.RemoveSpecialCharacters(Value)
            _TelefoneCadastrado = sonumero
        End Set
    End Property
End Class

