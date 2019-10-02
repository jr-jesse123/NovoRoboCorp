Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Configuration
Imports System.Data.Entity.ModelConfiguration.Conventions
Imports System.Reflection
Imports MySql.Data.Entity




<DbConfigurationType(GetType(MySqlEFConfiguration))>
Public Class CrawlerContext
    Inherits DbContext


    Public Property LINHAS As DbSet(Of LINHA)
    Public Property GESTORES As DbSet(Of GESTOR)
    Public Property Empresas As DbSet(Of CadastroCNPJ)





    'Port=3309
    Sub New()
        MyBase.New("server=192.168.244.112;userid=root;password=758686;database=CRMIntegrado;persistsecurityinfo=True")
    End Sub


    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        MyBase.OnModelCreating(modelBuilder)

        modelBuilder.Conventions.Add(New NonPublicColumnAttributeConvention())

        'modelBuilder.Entity(Of ClienteVivo)() _
        '.Map(Sub(cv)
        '         cv.MapInheritedProperties()
        '         cv.ToTable("ClienteVivo")
        '     End Sub)

    End Sub


End Class



Public Class EntityMappingConfiguration(Of TEntityType) ' where TEntityType : Class

    Public Function Requires(discriminator As String) As ValueConditionConfiguration

    End Function



    Public Sub ToTable(tableName As String)
    End Sub

    Public Sub MapInheritedProperties()
    End Sub


End Class