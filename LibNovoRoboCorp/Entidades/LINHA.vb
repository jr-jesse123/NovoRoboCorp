Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("corpscrapper.LINHAS")>
Partial Public Class LINHA
    Public Property Empresa As EMPRESA
    Public Property Produto As String
    Public Property NrDoAtivo As String
    Public Property Quantidade As String
    <Key>
    Public Property NrDaLinha As String
    Public Property TipoDeplano As String
    Public Property Status As String
    Public Property ContaDeServico As String
    Public Property CriadoEm As String
    Public Property Portado As Boolean
    Public Property PortadoEm As Date
    Public Property TenoclogiaRamal As String
    Public Property NumeroCentral As String
    Public Property timespan As Date
    Public Property DataDeExpiracao As Date


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
        'Me.CNPJ = _CNPJ
        Me.timespan = Now

    End Sub
End Class
