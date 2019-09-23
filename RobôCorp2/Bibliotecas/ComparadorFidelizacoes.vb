Public Class ComparadorFidelizacoes
    Implements IEqualityComparer(Of DataRow)

    Public Function Equals(x As DataRow, y As DataRow) As Boolean Implements IEqualityComparer(Of DataRow).Equals
        Return IIf(x(26) = y(26), True, False)
    End Function

    Public Function GetHashCode(obj As DataRow) As Integer Implements IEqualityComparer(Of DataRow).GetHashCode
        Return Nothing
    End Function

End Class
