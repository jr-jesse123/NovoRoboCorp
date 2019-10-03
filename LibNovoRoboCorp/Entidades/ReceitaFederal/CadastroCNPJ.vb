﻿Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

<Table("CadastrosCNPJs", Schema:="ReceitaFederal")>
Public Class CadastroCNPJ ' tipo 1
    Protected _CNPJ As String
    Protected _MatrizOuFilial As MatrizOuFilialEnum
    Protected _RazaoSocial As String
    Protected _NomeFantasia As String
    Protected _SituacaoCadastral As Integer
    Protected _CodigoDaNaturezaJudica As String
    Protected _DataDeInicioDaAtividade As Date
    Protected _CNAE As String
    Protected _Endereco As String
    Protected _Mei As Boolean
    Protected _OptanteSimples As OpcaoSimplesEnum
    Protected _PorteDaEmpresa As PorteEmpresaEnum
    Protected _CapitalSocial As Double
    Protected _QualificacaoResponsavel As String
    Protected _Email As String
    Protected _Telefone2 As String
    Protected _Telefone As String
    Protected _Cidade As String
    Protected _UF As String
    Protected _CEP As String
    Protected _Bairro As String
    Public Overridable Property Socios As List(Of SociosReceita)
    Public Overridable Property CnasesSecundarios As List(Of CNAEsSecundarias)

    <Key>
    <Required>
    <StringLength(14, MinimumLength:=14)>
    <RegularExpression("\d{14}")>
    Public Overridable Property CNPJ As String ' 3 ao 17
        Get
            Return _CNPJ
        End Get
        Protected Set
            _CNPJ = Value
        End Set
    End Property
    <RegularExpression("\d")>
    Overridable Property MatrizOuFilial As MatrizOuFilialEnum ' 18 1 - true 2 - false
        Get
            Return _MatrizOuFilial
        End Get
        Protected Set
            _MatrizOuFilial = Value
        End Set
    End Property

    <StringLength(150)>
    Overridable Property RazaoSocial As String ' 19 ao 168
        Get
            Return _RazaoSocial
        End Get
        Protected Set
            _RazaoSocial = Value
        End Set
    End Property

    <StringLength(55)>
    Overridable Property NomeFantasia As String ' '169 a 223
        Get
            Return _NomeFantasia
        End Get
        Protected Set
            _NomeFantasia = Value
        End Set
    End Property

    Overridable Property SituacaoCadastral As Integer '224 e 225
        Get
            Return _SituacaoCadastral
        End Get
        Protected Set
            _SituacaoCadastral = Value
        End Set
    End Property

    <StringLength(4)>
    Overridable Property CodigoDaNaturezaJudica As String ' 393 a 397
        Get
            Return _CodigoDaNaturezaJudica
        End Get
        Protected Set
            _CodigoDaNaturezaJudica = Value
        End Set
    End Property

    Overridable Property DataDeInicioDaAtividade As Date ' 398 a 495
        Get
            Return _DataDeInicioDaAtividade
        End Get
        Protected Set
            _DataDeInicioDaAtividade = Value
        End Set
    End Property

    <StringLength(7, MinimumLength:=7)>
    Overridable Property CNAE As String ' 496 a 502
        Get
            Return _CNAE
        End Get
        Protected Set
            _CNAE = Value
        End Set
    End Property

    <StringLength(240)>
    Overridable Property Endereco As String ' 403 a 643
        Get
            Return _Endereco
        End Get
        Protected Set
            _Endereco = Value
        End Set
    End Property

    <StringLength(55)>
    Overridable Property Bairro As String
        Get
            Return _Bairro
        End Get
        Protected Set
            _Bairro = Value
        End Set
    End Property

    <StringLength(8)>
    <RegularExpression("\d{8}")>
    Overridable Property CEP As String ' 674 a 683
        Get
            Return _CEP
        End Get
        Protected Set
            _CEP = Value
        End Set
    End Property

    <RegularExpression("\D{2}")>
    <StringLength(2)>
    Overridable Property UF As String ' 684 e 685
        Get
            Return _UF
        End Get
        Protected Set
            _UF = Value
        End Set
    End Property

    <StringLength(60)>
    Overridable Property Cidade As String ' 690 a 651
        Get
            Return _Cidade
        End Get
        Protected Set
            _Cidade = Value
        End Set
    End Property

    <StringLength(10)>
    Overridable Property Telefone As String
        Get
            Return _Telefone
        End Get
        Protected Set
            _Telefone = Value
        End Set
    End Property

    <StringLength(10)>
    Overridable Property Telefone2 As String ' 662 a 671
        Get
            Return _Telefone2
        End Get
        Protected Set
            _Telefone2 = Value
        End Set
    End Property

    <StringLength(240)>
    Overridable Property Email As String ' 672 a 821
        Get
            Return _Email
        End Get
        Protected Set
            _Email = Value
        End Set
    End Property

    <StringLength(2)>
    Overridable Property QualificacaoResponsavel As String ' 822 e 823
        Get
            Return _QualificacaoResponsavel
        End Get
        Protected Set
            _QualificacaoResponsavel = Value
        End Set
    End Property

    Overridable Property CapitalSocial As Double ' 824 a 827
        Get
            Return _CapitalSocial
        End Get
        Protected Set
            _CapitalSocial = Value
        End Set
    End Property

    Overridable Property PorteDaEmpresa As PorteEmpresaEnum
        Get
            Return _PorteDaEmpresa
        End Get
        Protected Set
            _PorteDaEmpresa = Value
        End Set
    End Property

    Overridable Property OptanteSimples As OpcaoSimplesEnum
        Get
            Return _OptanteSimples
        End Get
        Protected Set
            _OptanteSimples = Value
        End Set
    End Property

    Overridable Property Mei As Boolean ' s para sim n para não
        Get
            Return _Mei
        End Get
        Protected Set
            _Mei = Value
        End Set
    End Property



    Public Sub New()
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
            Crawler.EnviarLog(ex.Message + Environment.NewLine + ex.StackTrace)
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

