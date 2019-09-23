Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class correcaoSocio
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.CNAEsSecundarias",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .CNPJ = c.String(unicode := false),
                        .CnaesSecStr = c.String(unicode := false)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.CadastroCNPJs",
                Function(c) New With
                    {
                        .CNPJ = c.String(nullable := False, maxLength := 14, storeType := "nvarchar"),
                        .MatrizOuFilial = c.Int(nullable := False),
                        .RazaoSocial = c.String(maxLength := 150, storeType := "nvarchar"),
                        .NomeFantasia = c.String(maxLength := 55, storeType := "nvarchar"),
                        .SituacaoCadastral = c.Int(nullable := False),
                        .CodigoDaNaturezaJudica = c.String(maxLength := 5, storeType := "nvarchar"),
                        .DataDeInicioDaAtividade = c.DateTime(nullable := False, precision := 0),
                        .CNAE = c.String(maxLength := 7, storeType := "nvarchar"),
                        .Endereco = c.String(maxLength := 240, storeType := "nvarchar"),
                        .Bairro = c.String(maxLength := 50, storeType := "nvarchar"),
                        .CEP = c.String(maxLength := 9, storeType := "nvarchar"),
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
                "dbo.SociosReceitas",
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
                .PrimaryKey(Function(t) t.ID)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.SociosReceitas")
            DropTable("dbo.CadastroCNPJs")
            DropTable("dbo.CNAEsSecundarias")
        End Sub
    End Class
End Namespace
