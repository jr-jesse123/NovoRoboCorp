Imports CrawlerCorp.CrawlerClass
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Support.UI
Imports SeleniumExtras.WaitHelpers

Public Class PaginaDosGestores
    Public crawler As Crawler
    Public driver As IWebDriver
    Public empresa As ClienteVivo
    Public wait As WebDriverWait

    Sub New()
        Me.driver = WebdriverCt.Driver
        Me.wait = WebdriverCt.Wait
    End Sub

    Public Function EnriquecerGestores() As List(Of GestorVivo)

        Dim TabelaGestores As IWebElement
        Dim GestoresDataTable As DataTable
        Dim ListaGestoresRaw As List(Of DataRow)
        Dim output As New List(Of GestorVivo)


        Try
            wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//*[@id='s_1_1_7_0_Ctrl']"), "Pesquisar"))

preparartabela:
            Try
                TabelaGestores = driver.FindElement(By.XPath("//*[@id='s_1_l']"))
                GestoresDataTable = FuncoesUteis.ConstruirDatatable(TabelaGestores)

                Dim colunas As Integer() = Enumerable.Range(0, 15).ToArray

                ListaGestoresRaw = FuncoesUteis.PrepararContasParaInserirNaTabela(GestoresDataTable, TabelaGestores, colunas) ' apesar do nome da função ela serve ao próposito de criar datarows para qualquer tabela

            Catch ex As StaleElementReferenceException
                GoTo preparartabela

            End Try



            For Each gestor In ListaGestoresRaw
                GestoresDataTable.Rows.Add(gestor)
            Next

            For i = 0 To GestoresDataTable.Rows.Count - 1
                Dim gestor As New GestorVivo
                If GestoresDataTable(i)(6).ToString.Length > 1 Then
                    gestor.CPF = GestoresDataTable(i)(6)
                    gestor.EMAIL = GestoresDataTable(i)(12)
                    gestor.EMPRESA.Add(empresa)
                    gestor.Master = GestoresDataTable(i)(1)
                    gestor.NOME = GestoresDataTable(i)(2) + " " + GestoresDataTable(i)(3) + " " + GestoresDataTable(i)(4)
                    gestor.TelefoneCelular = GestoresDataTable(i)(7)
                    gestor.TelefoneFixo = GestoresDataTable(i)(8)
                    output.Add(gestor)
                End If

            Next
            Console.WriteLine($"A empresa possui {(GestoresDataTable.Rows.Count - 1).ToString} Gestores Cadastrados")

        Catch ex As Exception
            FuncoesUteis.debug(ex)
            Stop
            Throw
        End Try

        Return output

    End Function


End Class
