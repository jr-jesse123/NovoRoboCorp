Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

<Table(NameOf(SociosReceita), Schema:="ReceitaFederal")>
Public Class SociosReceita ' tipo 2

    Private _CNPJ As String
    Private _DataDeEntradaNaSociedade As Date
    Private _PercentualDoCapital As Double
    Private _CodigoQualificacao As String
    Private _CnpjOuCpf As String
    Private _Nome As String
    Private _Categoria As CategoriaEnum
    Private _ID As Integer
    Private Shared count As Long

    Public Property ID As Integer
        Get
            Return _ID
        End Get
        Private Set
            _ID = Value
        End Set
    End Property
    <StringLength(14, MinimumLength:=14)>
    Property CNPJ As String ' 3 a 13
        Get
            Return _CNPJ
        End Get
        Private Set
            _CNPJ = Value
        End Set
    End Property

    Property Categoria As CategoriaEnum '17
        Get
            Return _Categoria
        End Get
        Private Set
            _Categoria = Value
        End Set
    End Property

    Property Nome As String ' 18 a 168
        Get
            Return _Nome
        End Get
        Private Set
            _Nome = Value
        End Set
    End Property

    Property CnpjOuCpf As String '168 a 182
        Get
            Return _CnpjOuCpf
        End Get
        Private Set
            _CnpjOuCpf = Value
        End Set
    End Property

    Property CodigoQualificacao As String '182 a 184
        Get
            Return _CodigoQualificacao
        End Get
        Private Set
            _CodigoQualificacao = Value
        End Set
    End Property

    Property PercentualDoCapital As Double '184 a 189
        Get
            Return _PercentualDoCapital
        End Get

        Private Set
            _PercentualDoCapital = Value
        End Set
    End Property

    Property DataDeEntradaNaSociedade As Date ' 189 a 197
        Get
            Return _DataDeEntradaNaSociedade
        End Get
        Private Set
            _DataDeEntradaNaSociedade = Value
        End Set
    End Property

    Property TipoDeSocio
    Protected Sub New()

    End Sub

    Public Sub New(vlinha As String)


        Me.CNPJ = vlinha.Substring(3, 14)
        Me.Categoria = vlinha.Substring(17, 1)

        Dim nome As String = vlinha.Substring(18, 150)

        Try
            nome = nome.Substring(0, nome.IndexOf("   "))
        Catch
        Finally
            Me.Nome = nome
        End Try


        Me.CnpjOuCpf = vlinha.Substring(168, 14)
        Me.CodigoQualificacao = vlinha.Substring(182, 2)
        Me.PercentualDoCapital = vlinha.Substring(184, 5)

        Dim datastr = vlinha.Substring(189, 8)

        Dim ano = datastr.Substring(0, 4)
        Dim mes = datastr.Substring(4, 2)
        Dim dia = datastr.Substring(6, 2)

        Dim data

        Try
            data = New Date(ano, mes, dia)
        Catch ex As Exception
            Crawler.EnviarLog(ex.Message + Environment.NewLine + ex.StackTrace)
            data = Today
        End Try


        Me.DataDeEntradaNaSociedade = data

    End Sub

    Public Overrides Function ToString() As String
        Dim output As String

        Dim intcategoria As Integer = Categoria

        output = $"{count};{CNPJ};{intcategoria};{Nome};{CnpjOuCpf};{CodigoQualificacao};{PercentualDoCapital};"

        output += $"{DataDeEntradaNaSociedade.ToString("yyyy-MM-dd hh:mm:ss")};"

        count += 1


        Return output
    End Function


End Class
