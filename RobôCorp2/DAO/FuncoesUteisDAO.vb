Imports System.Data.Entity
Imports System.Data.Entity.Validation

Public Class FuncoesUteisDAO


    Shared Sub ReabastecerCNPJS(context As CrawlerContext)

        If listaDeCNPJ.Where(Function(cnpj) cnpj.ENRIQUECIDO = Nothing).Count < 50 Then

            Dim CNPJSCONSULTADOS As Array = listaDeCNPJ.Where(Function(cnpj) cnpj.ENRIQUECIDO <> Nothing).ToArray

            Dim ListaReposicao As List(Of CNPJ) =
                        context.CNPJS.Where(Function(cnpj) cnpj.ENRIQUECIDO = Nothing And cnpj.UF = a + b) _
                .OrderBy(Function(cnpj) cnpj.CNPJS).Skip(200 * instancias).Take(CNPJSCONSULTADOS.Length).ToList


            For Each x As CNPJ In CNPJSCONSULTADOS
                context.CNPJS.Where(Function(y) y.CNPJS = x.CNPJS).First.ENRIQUECIDO = x.ENRIQUECIDO
                listaDeCNPJ.Remove(x)
            Next

            listaDeCNPJ.AddRange(ListaReposicao)
            Console.WriteLine("CNPJS REABASTECIDOS")


            Try
                context.SaveChanges()
            Catch ex As DbEntityValidationException
                TratarErroDBentity(ex, context)

            End Try

        End If
    End Sub

    Shared Sub sanitizarBD()

        Dim conexao As Mysqlcoon

        conexao = New Mysqlcoon
        Dim Sql As String
        Sql = "REPLACE INTO LINHAS_2  SELECT * FROM LINHAS;"
        conexao.SQLCommand(Sql)
        conexao.Close()

        conexao = New Mysqlcoon
        Sql = "TRUNCATE LINHAS"
        conexao.SQLCommand(Sql)
        conexao.Close()


        conexao = New Mysqlcoon
        Sql = "REPLACE INTO EMPRESAS_2  SELECT * FROM EMPRESAS;"
        conexao.SQLCommand(Sql)
        conexao.Close()

        conexao = New Mysqlcoon
        Sql = "TRUNCATE EMPRESAS;"
        conexao.SQLCommand(Sql)
        conexao.Close()

        conexao = New Mysqlcoon
        Sql = "REPLACE INTO GESTORES_2  SELECT * FROM GESTORES;"
        conexao.SQLCommand(Sql)
        conexao.Close()

        conexao = New Mysqlcoon
        Sql = "TRUNCATE GESTORES;"
        conexao.SQLCommand(Sql)
        conexao.Close()

        conexao = New Mysqlcoon
        Sql = "REPLACE INTO Socios_2  SELECT * FROM Socios;"
        conexao.SQLCommand(Sql)
        conexao.Close()

        conexao = New Mysqlcoon
        Sql = "TRUNCATE Socios;"
        conexao.SQLCommand(Sql)
        conexao.Close()

    End Sub

    Shared Sub TratarErroDBentity(ex As DbEntityValidationException, context As DbContext)

        Dim erro, campo, entidade As String
        Dim mensagemDeErro As String
        Dim y As Object


        erro = ex.EntityValidationErrors(0).ValidationErrors(0).ErrorMessage.ToString()
        campo = ex.EntityValidationErrors(0).ValidationErrors(0).PropertyName.ToString()
        mensagemDeErro = $"erro de formato de informação ao gravar o campo {campo} pois {erro}, {vbCrLf}
"
        entidade = ex.EntityValidationErrors(0).ValidationErrors(0).ToString

        Console.WriteLine(mensagemDeErro, entidade)


        For x = 0 To ex.EntityValidationErrors.Count - 1
            y = ex.EntityValidationErrors(x).Entry.Entity

            FuncoesUteis.Detach(context, y)
        Next


        
        context.SaveChanges()
        Console.WriteLine("Entidades salvas no banco de dados, com exclusão de erros")

    End Sub



End Class
