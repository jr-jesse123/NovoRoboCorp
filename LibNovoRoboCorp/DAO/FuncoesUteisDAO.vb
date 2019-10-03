Imports System.Data.Entity
Imports System.Data.Entity.Validation
Imports System.Data.Entity.Infrastructure


Public Class FuncoesUteisDAO

    Shared Sub TratarErroDBentity(ex As DbEntityValidationException, context As DbContext)

        Dim erro, campo, entidade As String
        Dim mensagemDeErro As String
        Dim y As Object

        erro = ex.EntityValidationErrors(0).ValidationErrors(0).ErrorMessage.ToString()
        campo = ex.EntityValidationErrors(0).ValidationErrors(0).PropertyName.ToString()
        mensagemDeErro = $"erro de formato de informação ao gravar o campo {campo} pois {erro}, {vbCrLf}
"
        entidade = ex.EntityValidationErrors(0).ValidationErrors(0).ToString

        Crawler.EnviarLog(String.Format(mensagemDeErro, entidade))

        For x = 0 To ex.EntityValidationErrors.Count - 1
            y = ex.EntityValidationErrors(x).Entry.Entity

            FuncoesUteis.Detach(context, y)
        Next


        
        context.SaveChanges()
        Crawler.EnviarLog("Entidades salvas no banco de dados, com exclusão de erros")

    End Sub



End Class
