Imports System.ComponentModel.DataAnnotations.Schema

<Table(NameOf(SocioCorp), Schema:="VivoCorp")>
Public Class SocioCorp
    Inherits SociosReceita

    Private _TelefoneCadastrado As String

    Property TelefoneCadastrado As String
        Get
            Return _TelefoneCadastrado
        End Get
        Set
            Dim sonumero = FuncoesUteis.RemoverLetras(Value)
            _TelefoneCadastrado = sonumero
        End Set
    End Property
End Class

