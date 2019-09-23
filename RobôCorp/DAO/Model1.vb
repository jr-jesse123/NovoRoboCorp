Imports System
Imports System.Data.Entity
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Linq

Partial Public Class CrawlerContext
    Inherits DbContext

    Public Sub New()
        MyBase.New("name=Model1")
    End Sub

    Public Overridable Property CNPJS As DbSet(Of CNPJ)
    Public Overridable Property EMPRESAS As DbSet(Of EMPRESA)
    Public Overridable Property GESTORES As DbSet(Of GESTORE)
    Public Overridable Property LINHAS As DbSet(Of LINHA)
    Public Overridable Property Socios As DbSet(Of Socio)
    Public Overridable Property Empresa_Sem_linhas_Cadastradas As DbSet(Of Empresa_Sem_linhas_Cadastrada)

    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
        modelBuilder.Entity(Of CNPJ)() _
            .Property(Function(e) e.CNPJS) _
            .IsUnicode(False)

        modelBuilder.Entity(Of CNPJ)() _
            .Property(Function(e) e.ENRIQUECIDO) _
            .IsUnicode(False)

        modelBuilder.Entity(Of CNPJ)() _
            .Property(Function(e) e.UF) _
            .IsUnicode(False)

        modelBuilder.Entity(Of EMPRESA)() _
            .Property(Function(e) e.CNAE) _
            .IsUnicode(False)

        modelBuilder.Entity(Of EMPRESA)() _
            .Property(Function(e) e.CNPJ) _
            .IsUnicode(False)

        modelBuilder.Entity(Of EMPRESA)() _
            .Property(Function(e) e.Nome) _
            .IsUnicode(False)

        modelBuilder.Entity(Of EMPRESA)() _
            .Property(Function(e) e.Endereço) _
            .IsUnicode(False)

        modelBuilder.Entity(Of EMPRESA)() _
            .Property(Function(e) e.CEP) _
            .IsUnicode(False)

        modelBuilder.Entity(Of EMPRESA)() _
            .Property(Function(e) e.UF) _
            .IsUnicode(False)

        modelBuilder.Entity(Of EMPRESA)() _
            .Property(Function(e) e.GN) _
            .IsUnicode(False)

        modelBuilder.Entity(Of EMPRESA)() _
            .Property(Function(e) e.CARTEIRA) _
            .IsUnicode(False)

        modelBuilder.Entity(Of EMPRESA)() _
            .Property(Function(e) e.Observações) _
            .IsUnicode(False)

        modelBuilder.Entity(Of GESTORE)() _
            .Property(Function(e) e.EMPRESA) _
            .IsUnicode(False)

        modelBuilder.Entity(Of GESTORE)() _
            .Property(Function(e) e.EMAIL) _
            .IsUnicode(False)

        modelBuilder.Entity(Of GESTORE)() _
            .Property(Function(e) e.TelefoneCelular) _
            .IsUnicode(False)

        modelBuilder.Entity(Of GESTORE)() _
            .Property(Function(e) e.TelefoneFixo) _
            .IsUnicode(False)

        modelBuilder.Entity(Of GESTORE)() _
            .Property(Function(e) e.CPF) _
            .IsUnicode(False)

        modelBuilder.Entity(Of GESTORE)() _
            .Property(Function(e) e.Master) _
            .IsUnicode(False)

        modelBuilder.Entity(Of GESTORE)() _
            .Property(Function(e) e.NOME) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.Produto) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.NrDoAtivo) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.Quantidade) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.NrDaLinha) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.Tipo) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.Subtipo) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.TipoDeplano) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.Status) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.Cliente) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.ContaDeServico) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.TipoDeNegociacao) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.UsuarioAutorizado) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.EnderecoDeInstalacao) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.PrazoSmp) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.DDD) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.NumeroDoATivoPrincipal) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.Organizacao) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.CriadoPor) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.CriadoEm) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.VlrNegociado) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.PrecoMedioDeVEnda) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.DiferencaPagar) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.DataDeAtivacao) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.DataDeExpiracao) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.Hierarquia) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.ContaPai) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.UsuarioAtualizacao) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.ResultadoAtualizado) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.DataAtualizacao) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.Observacoes) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.NomeContatoTecico) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.SobrenomeContatoTEcnico) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.IndicadorTitular) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.Portabilidade) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.ProjetoEspecial) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.TenoclogiaRamal) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.NumeroCentral) _
            .IsUnicode(False)

        modelBuilder.Entity(Of LINHA)() _
            .Property(Function(e) e.CNPJ) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Socio)() _
            .Property(Function(e) e.CPFOUCNPJ) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Socio)() _
            .Property(Function(e) e.NOME) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Socio)() _
            .Property(Function(e) e.Telefone) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Socio)() _
            .Property(Function(e) e.CNPJ) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Empresa_Sem_linhas_Cadastrada)() _
            .Property(Function(e) e.CNAE) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Empresa_Sem_linhas_Cadastrada)() _
            .Property(Function(e) e.CNPJ) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Empresa_Sem_linhas_Cadastrada)() _
            .Property(Function(e) e.Nome) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Empresa_Sem_linhas_Cadastrada)() _
            .Property(Function(e) e.Endereço) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Empresa_Sem_linhas_Cadastrada)() _
            .Property(Function(e) e.CEP) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Empresa_Sem_linhas_Cadastrada)() _
            .Property(Function(e) e.UF) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Empresa_Sem_linhas_Cadastrada)() _
            .Property(Function(e) e.GN) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Empresa_Sem_linhas_Cadastrada)() _
            .Property(Function(e) e.CARTEIRA) _
            .IsUnicode(False)

        modelBuilder.Entity(Of Empresa_Sem_linhas_Cadastrada)() _
            .Property(Function(e) e.Observações) _
            .IsUnicode(False)
    End Sub
End Class
