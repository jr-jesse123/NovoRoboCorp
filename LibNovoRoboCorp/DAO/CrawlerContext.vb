Imports System.Data.Entity
Imports MySql.Data.Entity




'<DbConfigurationType(GetType(MySqlEFConfiguration))>
Public Class CrawlerContext
    Inherits DbContext


    Public Property LINHAS As DbSet(Of LINHA)
    Public Property GESTORES As DbSet(Of GestorVivo)
    Public Property Empresas As DbSet(Of CadastroCNPJ)

    Sub New()
        MyBase.New("Data Source=192.168.244.112,1433;User ID=sa;Password=Pwd758686;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;initial catalog=BDComercial4d")


        Dim teste = System.Configuration.ConfigurationManager.AppSettings("")

    End Sub


    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        MyBase.OnModelCreating(modelBuilder)

        modelBuilder.Conventions.Add(New NonPublicColumnAttributeConvention())


    End Sub


End Class


