Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Conventions
Imports System.Reflection
Imports MySql.Data.Entity




<DbConfigurationType(GetType(MySqlEFConfiguration))>
Public Class CrawlerContext
    Inherits DbContext


    Public Property LINHAS As DbSet(Of LINHA)
    Public Property GESTORES As DbSet(Of GESTOR)
    Public Property SOCIOS As DbSet(Of Socio)
    Public Property Empresas As DbSet(Of EMPRESA)
    Public Property CNPJS As DbSet(Of CNPJ)



    'Port=3309
    Sub New()
        MyBase.New("server=localhost;userid=root;password=758686;database=RoboCorp;persistsecurityinfo=True")
    End Sub


    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        MyBase.OnModelCreating(modelBuilder)

        modelBuilder.Conventions.Add(New NonPublicColumnAttributeConvention())



    End Sub


End Class

