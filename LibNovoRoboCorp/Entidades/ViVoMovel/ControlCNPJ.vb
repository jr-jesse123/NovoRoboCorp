Imports System.ComponentModel.DataAnnotations.Schema

<Table("CadstRoCNPJEnriquecido")>
Public Class CadastroCNPJEnriquecido
    Inherits CadastroCNPJ

    Private _EnriquecidoVivoMovel As Date
    Private _EnriquecidoPhenix As Date
    Private _EnriquecidoClaro As Date

    Public Property EnriquecidoVivoMovel As Date
        Get
            Return _EnriquecidoVivoMovel
        End Get
        Protected Set
            _EnriquecidoVivoMovel = Value
        End Set
    End Property

    Public Property EnriquecidoPhenix As Date
        Get
            Return _EnriquecidoPhenix
        End Get
        Protected Set
            _EnriquecidoPhenix = Value
        End Set
    End Property

    Public Property EnriquecidoClaro As Date
        Get
            Return _EnriquecidoClaro
        End Get
        Protected Set
            _EnriquecidoClaro = Value
        End Set
    End Property
End Class
