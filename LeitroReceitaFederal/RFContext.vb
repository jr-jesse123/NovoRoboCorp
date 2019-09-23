Imports System.Data.Entity
Imports LibNovoRoboCorp
Imports MySql.Data.Entity

<DbConfigurationType(GetType(MySqlEFConfiguration))>
Public Class RFContext
    Inherits DbContext


    Public Property CNPJS As DbSet(Of CadastroCNPJ)
    Public Property Socios As DbSet(Of SociosReceita)
    Public Property CNAEsSecundarios As DbSet(Of CNAEsSecundarias)


    Sub New()
        MyBase.New("server=localhost;userid=root;password=758686;database=ReceitaFederal;persistsecurityinfo=True")
    End Sub


    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        MyBase.OnModelCreating(modelBuilder)

        modelBuilder.Conventions.Add(New NonPublicColumnAttributeConvention())

    End Sub



End Class
