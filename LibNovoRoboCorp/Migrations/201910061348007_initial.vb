Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class initial
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "ReceitaFederal.CNAEsSecundarias",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .CNPJ = c.String(maxLength := 14),
                        .CnaesSecStr = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("ReceitaFederal.CadastroCNPJ", Function(t) t.CNPJ) _
                .Index(Function(t) t.CNPJ)
            
            CreateTable(
                "ReceitaFederal.CadastroCNPJ",
                Function(c) New With
                    {
                        .CNPJ = c.String(nullable := False, maxLength := 14),
                        .MatrizOuFilial = c.Int(nullable := False),
                        .RazaoSocial = c.String(maxLength := 150),
                        .NomeFantasia = c.String(maxLength := 55),
                        .CodigoDaNaturezaJudica = c.String(maxLength := 4),
                        .DataDeInicioDaAtividade = c.DateTime(nullable := False),
                        .CNAE = c.String(maxLength := 7),
                        .Endereco = c.String(maxLength := 240),
                        .Bairro = c.String(maxLength := 55),
                        .CEP = c.String(maxLength := 8),
                        .UF = c.Int(nullable := False),
                        .Cidade = c.String(maxLength := 60),
                        .Telefone1 = c.String(maxLength := 12),
                        .Telefone2 = c.String(maxLength := 12),
                        .Email = c.String(maxLength := 240),
                        .QualificacaoResponsavel = c.String(maxLength := 2),
                        .CapitalSocial = c.Double(nullable := False),
                        .PorteDaEmpresa = c.Int(nullable := False),
                        .OptanteSimples = c.Int(nullable := False),
                        .Mei = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.CNPJ)
            
            CreateTable(
                "ReceitaFederal.SociosReceita",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False, identity := True),
                        .CNPJ = c.String(maxLength := 14),
                        .Categoria = c.Int(nullable := False),
                        .Nome = c.String(),
                        .CnpjOuCpf = c.String(),
                        .CodigoQualificacao = c.String(),
                        .PercentualDoCapital = c.Double(nullable := False),
                        .DataDeEntradaNaSociedade = c.DateTime(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.ID) _
                .ForeignKey("ReceitaFederal.CadastroCNPJ", Function(t) t.CNPJ) _
                .Index(Function(t) t.CNPJ)
            
            CreateTable(
                "VivoCorp.GestorVivo",
                Function(c) New With
                    {
                        .CPF = c.String(nullable := False, maxLength := 11),
                        .EMAIL = c.String(),
                        .TelefoneCelular = c.String(),
                        .TelefoneFixo = c.String(),
                        .Master = c.Boolean(nullable := False),
                        .NOME = c.String(),
                        .ClienteVivo_CNPJ = c.String(maxLength := 14)
                    }) _
                .PrimaryKey(Function(t) t.CPF) _
                .ForeignKey("VivoCorp.ClienteVivo", Function(t) t.ClienteVivo_CNPJ) _
                .Index(Function(t) t.ClienteVivo_CNPJ)
            
            CreateTable(
                "VivoCorp.LINHA",
                Function(c) New With
                    {
                        .NrDaLinha = c.String(nullable := False, maxLength := 128),
                        .CriadoEm = c.String(),
                        .PortadoEm = c.DateTime(nullable := False),
                        .FidelizadoAte = c.DateTime(nullable := False),
                        .Operadora = c.Int(nullable := False),
                        .Empresa_CNPJ = c.String(maxLength := 14)
                    }) _
                .PrimaryKey(Function(t) t.NrDaLinha) _
                .ForeignKey("VivoCorp.ClienteVivo", Function(t) t.Empresa_CNPJ) _
                .Index(Function(t) t.Empresa_CNPJ)
            
            CreateTable(
                "VivoCorp.SocioCorp",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False),
                        .TelefoneCadastrado = c.String()
                    }) _
                .PrimaryKey(Function(t) t.ID) _
                .ForeignKey("ReceitaFederal.SociosReceita", Function(t) t.ID) _
                .Index(Function(t) t.ID)
            
            CreateTable(
                "Controle.CadastroCNPJEnriquecido",
                Function(c) New With
                    {
                        .CNPJ = c.String(nullable := False, maxLength := 14),
                        .EnriquecidoVivoMovel = c.DateTime(nullable := False),
                        .EnriquecidoPhenix = c.DateTime(nullable := False),
                        .EnriquecidoClaro = c.DateTime(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.CNPJ) _
                .ForeignKey("ReceitaFederal.CadastroCNPJ", Function(t) t.CNPJ) _
                .Index(Function(t) t.CNPJ)
            
            CreateTable(
                "VivoCorp.ClienteVivo",
                Function(c) New With
                    {
                        .CNPJ = c.String(nullable := False, maxLength := 14),
                        .GestorVivo_CPF = c.String(maxLength := 11),
                        .GestorVivo_CPF1 = c.String(maxLength := 11),
                        .GN = c.String(),
                        .CARTEIRA = c.String(),
                        .Observações = c.String()
                    }) _
                .PrimaryKey(Function(t) t.CNPJ) _
                .ForeignKey("Controle.CadastroCNPJEnriquecido", Function(t) t.CNPJ) _
                .ForeignKey("VivoCorp.GestorVivo", Function(t) t.GestorVivo_CPF) _
                .ForeignKey("VivoCorp.GestorVivo", Function(t) t.GestorVivo_CPF1) _
                .Index(Function(t) t.CNPJ) _
                .Index(Function(t) t.GestorVivo_CPF) _
                .Index(Function(t) t.GestorVivo_CPF1)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("VivoCorp.ClienteVivo", "GestorVivo_CPF1", "VivoCorp.GestorVivo")
            DropForeignKey("VivoCorp.ClienteVivo", "GestorVivo_CPF", "VivoCorp.GestorVivo")
            DropForeignKey("VivoCorp.ClienteVivo", "CNPJ", "Controle.CadastroCNPJEnriquecido")
            DropForeignKey("Controle.CadastroCNPJEnriquecido", "CNPJ", "ReceitaFederal.CadastroCNPJ")
            DropForeignKey("VivoCorp.SocioCorp", "ID", "ReceitaFederal.SociosReceita")
            DropForeignKey("VivoCorp.LINHA", "Empresa_CNPJ", "VivoCorp.ClienteVivo")
            DropForeignKey("VivoCorp.GestorVivo", "ClienteVivo_CNPJ", "VivoCorp.ClienteVivo")
            DropForeignKey("ReceitaFederal.SociosReceita", "CNPJ", "ReceitaFederal.CadastroCNPJ")
            DropForeignKey("ReceitaFederal.CNAEsSecundarias", "CNPJ", "ReceitaFederal.CadastroCNPJ")
            DropIndex("VivoCorp.ClienteVivo", New String() { "GestorVivo_CPF1" })
            DropIndex("VivoCorp.ClienteVivo", New String() { "GestorVivo_CPF" })
            DropIndex("VivoCorp.ClienteVivo", New String() { "CNPJ" })
            DropIndex("Controle.CadastroCNPJEnriquecido", New String() { "CNPJ" })
            DropIndex("VivoCorp.SocioCorp", New String() { "ID" })
            DropIndex("VivoCorp.LINHA", New String() { "Empresa_CNPJ" })
            DropIndex("VivoCorp.GestorVivo", New String() { "ClienteVivo_CNPJ" })
            DropIndex("ReceitaFederal.SociosReceita", New String() { "CNPJ" })
            DropIndex("ReceitaFederal.CNAEsSecundarias", New String() { "CNPJ" })
            DropTable("VivoCorp.ClienteVivo")
            DropTable("Controle.CadastroCNPJEnriquecido")
            DropTable("VivoCorp.SocioCorp")
            DropTable("VivoCorp.LINHA")
            DropTable("VivoCorp.GestorVivo")
            DropTable("ReceitaFederal.SociosReceita")
            DropTable("ReceitaFederal.CadastroCNPJ")
            DropTable("ReceitaFederal.CNAEsSecundarias")
        End Sub
    End Class
End Namespace
