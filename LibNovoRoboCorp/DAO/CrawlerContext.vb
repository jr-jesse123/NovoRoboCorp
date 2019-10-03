Imports System.Data.Entity
Imports MySql.Data.Entity




<DbConfigurationType(GetType(MySqlEFConfiguration))>
Public Class CrawlerContext
    Inherits DbContext


    Public Property LINHAS As DbSet(Of LINHA)
    'Public Property GESTORES As DbSet(Of GestorVivo)
    'Public Property Empresas As DbSet(Of CadastroCNPJ)






    Sub New()
        MyBase.New("server=192.168.244.112;userid=root;password=758686;persistsecurityinfo=True;database=VivoCorp")
    End Sub


    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        MyBase.OnModelCreating(modelBuilder)

        modelBuilder.Conventions.Add(New NonPublicColumnAttributeConvention())

        'modelBuilder.Entity(Of ClienteVivo)() _
        '.Map(Sub(cv)
        '         cv.MapInheritedProperties()
        '         cv.ToTable("ClienteVivo")
        '     End Sub)

        'modelBuilder.HasDefaultSchema("VivoCorp")


        'modelBuilder.Entity(Of ClienteVivo)() _
        '.ToTable("ClientesVivo", "Receita")

        'modelBuilder.Entity(Of CadastroCNPJ)() _
        '.ToTable("CadastrosCNPJs", "Receita")

        'modelBuilder.Entity(Of SociosReceita)() _
        '.ToTable("Socios", "Receita")

        'modelBuilder.Entity(Of CadastroCNPJEnriquecido)() _
        '.ToTable("CadastrosCNPJEnriquecidos", "VivoCorp") ' verificar este teste

        'modelBuilder.Entity(Of SocioCorp)() _
        '.ToTable("SociosVivoCorp") ' verificar este teste



    End Sub


End Class



'Public Class EntityMappingConfiguration(Of TEntityType) ' where TEntityType : Class

'    Public Function Requires(discriminator As String) As ValueConditionConfiguration

'    End Function



'    Public Sub ToTable(tableName As String)
'    End Sub

'    Public Sub MapInheritedProperties()
'    End Sub


'End Class