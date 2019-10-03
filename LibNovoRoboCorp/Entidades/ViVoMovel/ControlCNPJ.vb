Imports System.ComponentModel.DataAnnotations.Schema


Public Class CadastroCNPJEnriquecido
    Inherits CadastroCNPJ



    Private _EnriquecidoVivoMovel As Date
    Private _EnriquecidoPhenix As Date
    Private _EnriquecidoClaro As Date

    Public Property EnriquecidoVivoMovel As Date
        Get
            Return _EnriquecidoVivoMovel
        End Get
        Set
            _EnriquecidoVivoMovel = Value
        End Set
    End Property

    Public Property EnriquecidoPhenix As Date
        Get
            Return _EnriquecidoPhenix
        End Get
        Set
            _EnriquecidoPhenix = Value
        End Set
    End Property

    Public Property EnriquecidoClaro As Date
        Get
            Return _EnriquecidoClaro
        End Get
        Set
            _EnriquecidoClaro = Value
        End Set
    End Property


    Sub New()

        'stop feito para saber se o new é chamado quando há um cast
        Stop
    End Sub
End Class
