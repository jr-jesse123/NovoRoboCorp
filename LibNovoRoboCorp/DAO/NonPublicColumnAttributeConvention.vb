Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.ModelConfiguration.Conventions
Imports System.Reflection
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



