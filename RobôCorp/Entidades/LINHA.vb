Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("corpscrapper.LINHAS")>
Partial Public Class LINHA
    <StringLength(200)>
    Public Property Produto As String

    <StringLength(200)>
    Public Property NrDoAtivo As String

    <StringLength(200)>
    Public Property Quantidade As String

    <Key>
    <StringLength(200)>
    Public Property NrDaLinha As String

    <StringLength(200)>
    Public Property Tipo As String

    <StringLength(200)>
    Public Property Subtipo As String

    <StringLength(200)>
    Public Property TipoDeplano As String

    <StringLength(200)>
    Public Property Status As String

    <StringLength(200)>
    Public Property Cliente As String

    <StringLength(200)>
    Public Property ContaDeServico As String

    <StringLength(200)>
    Public Property TipoDeNegociacao As String

    <StringLength(200)>
    Public Property UsuarioAutorizado As String

    <StringLength(200)>
    Public Property EnderecoDeInstalacao As String

    <StringLength(200)>
    Public Property PrazoSmp As String

    <StringLength(200)>
    Public Property DDD As String

    <StringLength(200)>
    Public Property NumeroDoATivoPrincipal As String

    <StringLength(200)>
    Public Property Organizacao As String

    <StringLength(200)>
    Public Property CriadoPor As String

    <StringLength(200)>
    Public Property CriadoEm As String

    <StringLength(200)>
    Public Property VlrNegociado As String

    <StringLength(200)>
    Public Property PrecoMedioDeVEnda As String

    <StringLength(200)>
    Public Property DiferencaPagar As String

    <StringLength(200)>
    Public Property DataDeAtivacao As String

    <StringLength(200)>
    Public Property DataDeExpiracao As String

    <StringLength(200)>
    Public Property Hierarquia As String

    <StringLength(200)>
    Public Property ContaPai As String

    <StringLength(200)>
    Public Property UsuarioAtualizacao As String

    <StringLength(200)>
    Public Property ResultadoAtualizado As String

    <StringLength(200)>
    Public Property DataAtualizacao As String

    <StringLength(200)>
    Public Property Observacoes As String

    <StringLength(200)>
    Public Property NomeContatoTecico As String

    <StringLength(200)>
    Public Property SobrenomeContatoTEcnico As String

    <StringLength(200)>
    Public Property IndicadorTitular As String

    <StringLength(200)>
    Public Property Portabilidade As String

    <StringLength(200)>
    Public Property ProjetoEspecial As String

    <StringLength(200)>
    Public Property TenoclogiaRamal As String

    <StringLength(200)>
    Public Property NumeroCentral As String

    <StringLength(200)>
    Public Property CNPJ As String

    <Column(TypeName:="timestamp")>
    Public Property timespan As Date?


    Sub New(linha As DataRow, _CNPJ As String)
        Me.Produto = linha(1)
        Me.NrDoAtivo = "VIVO"
        Me.Quantidade = linha(3)
        Me.NrDaLinha = linha(4)
        'Me.Tipo = linha(5)
        'Me.Subtipo = linha(6)
        Me.TipoDeplano = linha(7)
        Me.Status = linha(8)
        'Me.Cliente = linha(9)
        Me.ContaDeServico = linha(10)
        'Me.TipoDeNegociacao = linha(11)
        'Me.UsuarioAutorizado = linha(12)
        'Me.EnderecoDeInstalacao = linha(13)
        'Me.PrazoSmp = linha(14)
        'Me.DDD = linha(15)
        'Me.NumeroDoATivoPrincipal = linha(16)
        'Me.Organizacao = linha(17)
        'Me.CriadoPor = linha(18)
        Me.CriadoEm = linha(19)
        'Me.VlrNegociado = linha(20)
        'Me.PrecoMedioDeVEnda = linha(21)
        'Me.DiferencaPagar = linha(22)
        'Me.DataDeAtivacao = linha(23)
        'Me.DataDeExpiracao = linha(24)
        'Me.Hierarquia = linha(25)
        'Me.ContaPai = linha(26)
        'Me.UsuarioAtualizacao = linha(27)
        'Me.ResultadoAtualizado = linha(28)
        'Me.DataAtualizacao = linha(29)
        'Me.Observacoes = linha(30)
        'Me.NomeContatoTecico = linha(31)
        'Me.SobrenomeContatoTEcnico = linha(32)
        'Me.IndicadorTitular = linha(33)
        'Me.Portabilidade = linha(34)
        'Me.ProjetoEspecial = linha(35)
        Me.TenoclogiaRamal = "01/01/1900"
        'Me.NumeroCentral = linha(37)
        Me.CNPJ = _CNPJ
        Me.timespan = Now

    End Sub
End Class
