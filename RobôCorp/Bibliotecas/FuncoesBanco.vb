Imports System.Data.Entity

Public Class FuncoesBanco

    Shared Function VerificarSeJaFoiConsultado(CNPJ As String)


        Dim Sql As String
        Sql = $"SELECT  `CNPJ` FROM `corpscrapper`.`EMPRESAS_2` WHERE  CNPJ = {CNPJ}"

        Dim retorno As New DataTable

        Dim conexao As Mysqlcoon = New Mysqlcoon
        conexao.SQLQuery(Sql, retorno)
        conexao.Close()

        If retorno.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If


    End Function

    Public Shared Function SepararCNPJS(cnpj As CNPJ) As Boolean
        If cnpj.ENRIQUECIDO Is Nothing And cnpj.UF = a + b Then
            cnpj.ENRIQUECIDO = 3
            Return True
        End If
        Return False
    End Function



End Class
