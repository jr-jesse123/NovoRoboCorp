
Imports MySql.Data.MySqlClient




Public Class Mysqlcoon

        Public Property StringConn As String
        Public Property ConnDB As MySqlConnection

        Public Sub New(Optional Servidor As String = "186.202.152.128",
                       Optional vUser As String = "corpscrapper",
                       Optional vPassword As String = "d4consultoria",
                       Optional vDatabase As String = "corpscrapper")

            StringConn = "server=" + Servidor + ";User Id=" + vUser + ";password=" + vPassword + ";" + ";database=" + vDatabase + ";"
            ConnDB = New MySqlConnection(StringConn)
            ConnDB.Open()

        End Sub



        Public Function SQLQuery(SQL As String, ByRef DT As DataTable) As String

            Try
                Dim myCommand As IDbCommand = New MySqlCommand(SQL, ConnDB) With {
                    .CommandTimeout = 1000
                }
                Dim myReader As IDataReader = myCommand.ExecuteReader
                DT.Load(myReader)
                Return ""

            Catch ex As Exception

            End Try
            Return ""
        End Function

        Public Function SQLCommand(SQL As String) As String

            Try
                Dim myCommand As IDbCommand = New MySqlCommand(SQL, ConnDB)
                With myCommand
                    .CommandTimeout = 1000
                    .ExecuteReader()
                End With
                Return ""

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

        Public Sub Close()
            ConnDB.Close()
        End Sub





    End Class



