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
        If cnpj.ENRIQUECIDO = EnriquecidoEnum.NaoConsultado And cnpj.UF = UF Then
            cnpj.ENRIQUECIDO = 3
            Return True
        End If
        Return False
    End Function

    Public Shared Function ObterPrimeirosCNPJS() As List(Of CNPJ)

        Using context As New CrawlerContext
            'verificar se esse formato funciona

            'Dim listaDeCNPJ = context.CNPJS.Where(Function(cnpj) cnpj.ENRIQUECIDO = Nothing And cnpj.UF = UF) _
            '.OrderBy(Function(cnpj) cnpj.CNPJS).Skip(200).Take(100).ToList

            Console.WriteLine("primeiros CNPJS obtidos")

            Return listaDeCNPJ

        End Using



    End Function




End Class
