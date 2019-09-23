Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Conventions
Imports System.Reflection
Imports MySql.Data.Entity




<DbConfigurationType(GetType(MySqlEFConfiguration))>
Public Class contexto
    Inherits DbContext



    Sub New()
        MyBase.New("server=localhost;userid=root;password=758686;database=teste;persistsecurityinfo=True")
    End Sub


    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        MyBase.OnModelCreating(modelBuilder)

        modelBuilder.Conventions.Add(New NonPublicColumnAttributeConvention())



    End Sub


End Class

''' <summary>
''' Convention to support binding private Or protected properties to EF columns.
''' </summary>
Public NotInheritable Class NonPublicColumnAttributeConvention
    Inherits Convention

    Public Sub New()
        Types().Having(AddressOf NonPublicProperties).Configure(Function(config, properties)

                                                                    For Each prop As PropertyInfo In properties
                                                                        config.[Property](prop)
                                                                    Next
                                                                End Function)
    End Sub

    Private Function NonPublicProperties(ByVal type As Type) As IEnumerable(Of PropertyInfo)
        Dim matchingProperties =
            type.GetProperties(BindingFlags.SetProperty Or BindingFlags.GetProperty Or
            BindingFlags.NonPublic Or BindingFlags.Instance).Where(Function(propInfo) _
          propInfo.GetCustomAttributes(GetType(ColumnAttribute), True).Length > 0).ToArray()


        Return If(matchingProperties.Length = 0, Nothing, matchingProperties)
    End Function
End Class



