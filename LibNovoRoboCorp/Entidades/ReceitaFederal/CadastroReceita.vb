Imports System.ComponentModel.DataAnnotations

Public Class CadastroCNPJ ' tipo 1
    Private _CNPJ As String
    Private _MatrizOuFilial As MatrizOuFilialEnum
    Private _RazaoSocial As String
    Private _NomeFantasia As String
    Private _SituacaoCadastral As Integer
    Private _CodigoDaNaturezaJudica As String
    Private _DataDeInicioDaAtividade As Date
    Private _CNAE As String
    Private _Endereco As String
    Private _Mei As Boolean
    Private _OptanteSimples As OpcaoSimplesEnum
    Private _PorteDaEmpresa As PorteEmpresaEnum
    Private _CapitalSocial As Double
    Private _QualificacaoResponsavel As String
    Private _Email As String
    Private _Telefone2 As String
    Private _Telefone As String
    Private _Cidade As String
    Private _UF As String
    Private _CEP As String
    Private _Bairro As String



    <Key>
    <StringLength(14, MinimumLength:=14)>
    <RegularExpression("\d{14}")>
    Property CNPJ As String ' 3 ao 17
        Get
            Return _CNPJ
        End Get
        Private Set
            _CNPJ = Value
        End Set
    End Property
    <RegularExpression("\d")>
    Property MatrizOuFilial As MatrizOuFilialEnum ' 18 1 - true 2 - false
        Get
            Return _MatrizOuFilial
        End Get
        Private Set
            _MatrizOuFilial = Value
        End Set
    End Property

    <StringLength(150)>
    Property RazaoSocial As String ' 19 ao 168
        Get
            Return _RazaoSocial
        End Get
        Private Set
            _RazaoSocial = Value
        End Set
    End Property

    <StringLength(55)>
    Property NomeFantasia As String ' '169 a 223
        Get
            Return _NomeFantasia
        End Get
        Private Set
            _NomeFantasia = Value
        End Set
    End Property

    Property SituacaoCadastral As Integer '224 e 225
        Get
            Return _SituacaoCadastral
        End Get
        Private Set
            _SituacaoCadastral = Value
        End Set
    End Property

    <StringLength(4)>
    Property CodigoDaNaturezaJudica As String ' 393 a 397
        Get
            Return _CodigoDaNaturezaJudica
        End Get
        Private Set
            _CodigoDaNaturezaJudica = Value
        End Set
    End Property

    Property DataDeInicioDaAtividade As Date ' 398 a 495
        Get
            Return _DataDeInicioDaAtividade
        End Get
        Private Set
            _DataDeInicioDaAtividade = Value
        End Set
    End Property

    <StringLength(7, MinimumLength:=7)>
    Property CNAE As String ' 496 a 502
        Get
            Return _CNAE
        End Get
        Private Set
            _CNAE = Value
        End Set
    End Property

    <StringLength(240)>
    Property Endereco As String ' 403 a 643
        Get
            Return _Endereco
        End Get
        Private Set
            _Endereco = Value
        End Set
    End Property

    <StringLength(55)>
    Property Bairro As String
        Get
            Return _Bairro
        End Get
        Private Set
            _Bairro = Value
        End Set
    End Property

    <StringLength(8)>
    <RegularExpression("\d{8}")>
    Property CEP As String ' 674 a 683
        Get
            Return _CEP
        End Get
        Private Set
            _CEP = Value
        End Set
    End Property

    <RegularExpression("\D{2}")>
    <StringLength(2)>
    Property UF As String ' 684 e 685
        Get
            Return _UF
        End Get
        Private Set
            _UF = Value
        End Set
    End Property

    <StringLength(60)>
    Property Cidade As String ' 690 a 651
        Get
            Return _Cidade
        End Get
        Private Set
            _Cidade = Value
        End Set
    End Property

    <StringLength(10)>
    Property Telefone As String
        Get
            Return _Telefone
        End Get
        Private Set
            _Telefone = Value
        End Set
    End Property

    <StringLength(10)>
    Property Telefone2 As String ' 662 a 671
        Get
            Return _Telefone2
        End Get
        Private Set
            _Telefone2 = Value
        End Set
    End Property

    <StringLength(240)>
    Property Email As String ' 672 a 821
        Get
            Return _Email
        End Get
        Private Set
            _Email = Value
        End Set
    End Property

    <StringLength(2)>
    Property QualificacaoResponsavel As String ' 822 e 823
        Get
            Return _QualificacaoResponsavel
        End Get
        Private Set
            _QualificacaoResponsavel = Value
        End Set
    End Property

    Property CapitalSocial As Double ' 824 a 827
        Get
            Return _CapitalSocial
        End Get
        Private Set
            _CapitalSocial = Value
        End Set
    End Property

    Property PorteDaEmpresa As PorteEmpresaEnum
        Get
            Return _PorteDaEmpresa
        End Get
        Private Set
            _PorteDaEmpresa = Value
        End Set
    End Property

    Property OptanteSimples As OpcaoSimplesEnum
        Get
            Return _OptanteSimples
        End Get
        Private Set
            _OptanteSimples = Value
        End Set
    End Property

    Property Mei As Boolean ' s para sim n para não
        Get
            Return _Mei
        End Get
        Private Set
            _Mei = Value
        End Set
    End Property



    Private Sub New()
    End Sub



    Public Sub New(vlinha As String)



        Me.CNPJ = vlinha.Substring(3, 14)
        Me.MatrizOuFilial = vlinha.Substring(17, 1)
        Dim RazaoSocial = vlinha.Substring(18, 150)
        Try
            RazaoSocial = RazaoSocial.Substring(0, RazaoSocial.IndexOf("    "))
        Catch

        Finally
            Me.RazaoSocial = RazaoSocial
        End Try

        Dim NomeFantasia = vlinha.Substring(168, 55)
        Try
            NomeFantasia = NomeFantasia.Substring(0, NomeFantasia.IndexOf("   "))
        Catch
        Finally
            Me.NomeFantasia = NomeFantasia
        End Try


        Me.SituacaoCadastral = vlinha.Substring(223, 2)
        Me.CodigoDaNaturezaJudica = vlinha.Substring(363, 4)

        Dim datastr = vlinha.Substring(367, 8)
        Dim ano = datastr.Substring(0, 4)
        Dim mes = datastr.Substring(4, 2)
        Dim dia = datastr.Substring(6, 2)
        Dim data As Date

        Try
            data = New Date(ano, mes, dia)
        Catch ex As Exception
            Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace)
            data = Today
        End Try


        Me.DataDeInicioDaAtividade = data
        Me.CNAE = vlinha.Substring(375, 7)
        Dim Endereco = vlinha.Substring(382, 240)

        Try
            Endereco = Replace(Endereco, "  ", "")
        Catch
        Finally
            Me.Endereco = Endereco
        End Try


        Dim Bairro = vlinha.Substring(624, 50)

        Try
            Bairro = Bairro.Substring(0, Bairro.IndexOf("    "))
        Catch
        Finally
            Me.Bairro = Bairro
        End Try

        Me.CEP = vlinha.Substring(674, 8)
        Me.UF = vlinha.Substring(682, 2)
        Dim Cidade = vlinha.Substring(688, 55)
        Try
            Cidade = Cidade.Substring(0, Cidade.IndexOf("    "))
        Catch
        Finally
            Me.Cidade = Cidade
        End Try


        Dim Telefone = vlinha.Substring(738, 12)
        Dim Telefone2 = vlinha.Substring(762, 12)
        Dim Email = vlinha.Substring(774, 115)

        Try
            Email = Email.Substring(0, Email.IndexOf("    "))
        Catch
        Finally
            Me.Email = Email
        End Try

        Me.QualificacaoResponsavel = vlinha.Substring(889, 2)
        Me.CapitalSocial = vlinha.Substring(891, 14)
        Try
            Me.PorteDaEmpresa = vlinha.Substring(905, 2)
        Catch ex As InvalidCastException
            '    Stop
            Me.PorteDaEmpresa = 0
        End Try

        Try
            Me.OptanteSimples = vlinha.Substring(907, 1)
        Catch ex As InvalidCastException
            Me.OptanteSimples = 0
        End Try


        Dim Ismei As Boolean = vlinha.Substring(924, 1) = "S"

        Me.Mei = Ismei

    End Sub

    Public Overrides Function ToString() As String
        Dim output As String

        Dim meicsv As String

        meicsv = IIf(Mei, 1, 0)

        Dim intpotantesimples As Integer = OptanteSimples
        Dim intportdaempresa As Integer = PorteDaEmpresa
        Dim intMatis As Integer = MatrizOuFilial

        output = $"{CNPJ};{intMatis};{RazaoSocial};{NomeFantasia};{SituacaoCadastral};{CodigoDaNaturezaJudica};"
        output += $"{DataDeInicioDaAtividade.ToString("yyyy-MM-dd hh:mm:ss")};{CNAE};{Endereco};{Bairro};{CEP};{UF};"
        output += $"{Cidade};{Telefone};{Telefone2};{Email};{QualificacaoResponsavel};{CapitalSocial};{intportdaempresa};"
        output += $"{intpotantesimples};{meicsv};"

        Return output
    End Function

End Class

Public Enum MatrizOuFilialEnum
    Matriz = 1
    Filial = 2
End Enum
