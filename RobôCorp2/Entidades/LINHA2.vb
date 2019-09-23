Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("corpscrapper.LINHAS")>
Partial Public Class LINHA2
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


    Sub New(_CNPJ As String)
        Me.Produto = "linha Fictícia para descartar a empresa da lista de empresas sem linhas."
        Me.NrDaLinha = Today + " " +  Now.TimeOfDay.ToString
        Me.CNPJ = _CNPJ
        Console.WriteLine("criando linha fictícia para controle")
    End Sub


    Sub New(linha As DataRow, _CNPJ As String)
        Me.Produto = "Pedido Concluído"
        Me.NrDoAtivo = linha(4)
        Me.Quantidade = linha(5)
        Me.NrDaLinha = linha(26)
        Me.Tipo = linha(6)
        Me.Subtipo = linha(7)
        Me.TipoDeplano = linha(8)
        Me.Status = linha(8)
        Me.Cliente = linha(9)
        Me.ContaDeServico = linha(10)
        Me.TipoDeNegociacao = linha(11)
        Me.UsuarioAutorizado = linha(12)
        Me.EnderecoDeInstalacao = linha(13)
        Me.PrazoSmp = linha(24)
        Me.DDD = Left(linha(26), 2)
        Me.NumeroDoATivoPrincipal = linha(20)
        Me.Organizacao = linha(22)
        Me.CriadoPor = linha(23)
        Me.CriadoEm = linha(30)
        Me.VlrNegociado = linha(31)
        Me.PrecoMedioDeVEnda = linha(32)
        Me.DiferencaPagar = linha(54)
        Me.DataDeAtivacao = linha(56)
        Me.DataDeExpiracao = linha(57)
        Me.Hierarquia = linha(59)
        Me.ContaPai = linha(60)
        Me.CNPJ = _CNPJ

    End Sub
End Class
