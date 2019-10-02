Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class initial
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "Receita.CadastrosCNPJs",
                Function(c) New With
                    {
                        .CNPJ = c.String(nullable := False, maxLength := 14, storeType := "nvarchar"),
                        .MatrizOuFilial = c.Int(nullable := False),
                        .RazaoSocial = c.String(maxLength := 150, storeType := "nvarchar"),
                        .NomeFantasia = c.String(maxLength := 55, storeType := "nvarchar"),
                        .SituacaoCadastral = c.Int(nullable := False),
                        .CodigoDaNaturezaJudica = c.String(maxLength := 4, storeType := "nvarchar"),
                        .DataDeInicioDaAtividade = c.DateTime(nullable := False, precision := 0),
                        .CNAE = c.String(maxLength := 7, storeType := "nvarchar"),
                        .Endereco = c.String(maxLength := 240, storeType := "nvarchar"),
                        .Bairro = c.String(maxLength := 55, storeType := "nvarchar"),
                        .CEP = c.String(maxLength := 8, storeType := "nvarchar"),
                        .UF = c.String(maxLength := 2, storeType := "nvarchar"),
                        .Cidade = c.String(maxLength := 60, storeType := "nvarchar"),
                        .Telefone = c.String(maxLength := 10, storeType := "nvarchar"),
                        .Telefone2 = c.String(maxLength := 10, storeType := "nvarchar"),
                        .Email = c.String(maxLength := 240, storeType := "nvarchar"),
                        .QualificacaoResponsavel = c.String(maxLength := 2, storeType := "nvarchar"),
                        .CapitalSocial = c.Double(nullable := False),
                        .PorteDaEmpresa = c.Int(nullable := False),
                        .OptanteSimples = c.Int(nullable := False),
                        .Mei = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.CNPJ)
            
            CreateTable(
                "dbo.CNAEsSecundarias",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .CNPJ = c.String(maxLength := 14, storeType := "nvarchar"),
                        .CnaesSecStr = c.String(unicode := false)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("Receita.CadastrosCNPJs", Function(t) t.CNPJ) _
                .Index(Function(t) t.CNPJ)
            
            CreateTable(
                "Receita.Socio",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False, identity := True),
                        .CNPJ = c.String(maxLength := 14, storeType := "nvarchar"),
                        .Categoria = c.Int(nullable := False),
                        .Nome = c.String(unicode := false),
                        .CnpjOuCpf = c.String(unicode := false),
                        .CodigoQualificacao = c.String(unicode := false),
                        .PercentualDoCapital = c.Double(nullable := False),
                        .DataDeEntradaNaSociedade = c.DateTime(nullable := False, precision := 0)
                    }) _
                .PrimaryKey(Function(t) t.ID) _
                .ForeignKey("Receita.CadastrosCNPJs", Function(t) t.CNPJ) _
                .Index(Function(t) t.CNPJ)
            
            CreateTable(
                "dbo.GESTORs",
                Function(c) New With
                    {
                        .CPF = c.String(nullable := False, maxLength := 11, storeType := "nvarchar"),
                        .EMAIL = c.String(unicode := false),
                        .TelefoneCelular = c.String(unicode := false),
                        .TelefoneFixo = c.String(unicode := false),
                        .Master = c.Boolean(nullable := False),
                        .NOME = c.String(unicode := false),
                        .ClienteVivo_CNPJ = c.String(maxLength := 14, storeType := "nvarchar")
                    }) _
                .PrimaryKey(Function(t) t.CPF) _
                .ForeignKey("dbo.ClienteVivo", Function(t) t.ClienteVivo_CNPJ) _
                .Index(Function(t) t.ClienteVivo_CNPJ)
            
            CreateTable(
                "dbo.LINHAs",
                Function(c) New With
                    {
                        .NrDaLinha = c.String(nullable := False, maxLength := 128, storeType := "nvarchar"),
                        .CriadoEm = c.String(unicode := false),
                        .PortadoEm = c.DateTime(nullable := False, precision := 0),
                        .FidelizadoAte = c.DateTime(nullable := False, precision := 0),
                        .Operadora = c.Int(nullable := False),
                        .Empresa_CNPJ = c.String(maxLength := 14, storeType := "nvarchar")
                    }) _
                .PrimaryKey(Function(t) t.NrDaLinha) _
                .ForeignKey("dbo.ClienteVivo", Function(t) t.Empresa_CNPJ) _
                .Index(Function(t) t.Empresa_CNPJ)
            
            CreateTable(
                "dbo.SovioVivoCorp",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False),
                        .TelefoneCadastrado = c.String(unicode := false)
                    }) _
                .PrimaryKey(Function(t) t.ID) _
                .ForeignKey("Receita.Socio", Function(t) t.ID) _
                .Index(Function(t) t.ID)
            
            CreateTable(
                "dbo.CadstRoCNPJEnriquecido",
                Function(c) New With
                    {
                        .CNPJ = c.String(nullable := False, maxLength := 14, storeType := "nvarchar"),
                        .EnriquecidoVivoMovel = c.DateTime(nullable := False, precision := 0),
                        .EnriquecidoPhenix = c.DateTime(nullable := False, precision := 0),
                        .EnriquecidoClaro = c.DateTime(nullable := False, precision := 0)
                    }) _
                .PrimaryKey(Function(t) t.CNPJ) _
                .ForeignKey("Receita.CadastrosCNPJs", Function(t) t.CNPJ) _
                .Index(Function(t) t.CNPJ)
            
            CreateTable(
                "dbo.ClienteVivo",
                Function(c) New With
                    {
                        .CNPJ = c.String(nullable := False, maxLength := 14, storeType := "nvarchar"),
                        .GESTOR_CPF = c.String(maxLength := 11, storeType := "nvarchar"),
                        .GESTOR_CPF1 = c.String(maxLength := 11, storeType := "nvarchar"),
                        .GN = c.String(unicode := false),
                        .CARTEIRA = c.String(unicode := false),
                        .Observações = c.String(unicode := false)
                    }) _
                .PrimaryKey(Function(t) t.CNPJ) _
                .ForeignKey("dbo.CadstRoCNPJEnriquecido", Function(t) t.CNPJ) _
                .ForeignKey("dbo.GESTORs", Function(t) t.GESTOR_CPF) _
                .ForeignKey("dbo.GESTORs", Function(t) t.GESTOR_CPF1) _
                .Index(Function(t) t.CNPJ) _
                .Index(Function(t) t.GESTOR_CPF) _
                .Index(Function(t) t.GESTOR_CPF1)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.ClienteVivo", "GESTOR_CPF1", "dbo.GESTORs")
            DropForeignKey("dbo.ClienteVivo", "GESTOR_CPF", "dbo.GESTORs")
            DropForeignKey("dbo.ClienteVivo", "CNPJ", "dbo.CadstRoCNPJEnriquecido")
            DropForeignKey("dbo.CadstRoCNPJEnriquecido", "CNPJ", "Receita.CadastrosCNPJs")
            DropForeignKey("dbo.SovioVivoCorp", "ID", "Receita.Socio")
            DropForeignKey("dbo.LINHAs", "Empresa_CNPJ", "dbo.ClienteVivo")
            DropForeignKey("dbo.GESTORs", "ClienteVivo_CNPJ", "dbo.ClienteVivo")
            DropForeignKey("Receita.Socio", "CNPJ", "Receita.CadastrosCNPJs")
            DropForeignKey("dbo.CNAEsSecundarias", "CNPJ", "Receita.CadastrosCNPJs")
            DropIndex("dbo.ClienteVivo", New String() { "GESTOR_CPF1" })
            DropIndex("dbo.ClienteVivo", New String() { "GESTOR_CPF" })
            DropIndex("dbo.ClienteVivo", New String() { "CNPJ" })
            DropIndex("dbo.CadstRoCNPJEnriquecido", New String() { "CNPJ" })
            DropIndex("dbo.SovioVivoCorp", New String() { "ID" })
            DropIndex("dbo.LINHAs", New String() { "Empresa_CNPJ" })
            DropIndex("dbo.GESTORs", New String() { "ClienteVivo_CNPJ" })
            DropIndex("Receita.Socio", New String() { "CNPJ" })
            DropIndex("dbo.CNAEsSecundarias", New String() { "CNPJ" })
            DropTable("dbo.ClienteVivo")
            DropTable("dbo.CadstRoCNPJEnriquecido")
            DropTable("dbo.SovioVivoCorp")
            DropTable("dbo.LINHAs")
            DropTable("dbo.GESTORs")
            DropTable("Receita.Socio")
            DropTable("dbo.CNAEsSecundarias")
            DropTable("Receita.CadastrosCNPJs")
        End Sub
    End Class
End Namespace
