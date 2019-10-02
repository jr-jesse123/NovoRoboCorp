Imports CrawlerCorp.CrawlerClass
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Support.UI
Imports SeleniumExtras.WaitHelpers

Public Class PaginaDosGestores
    Public crawler As Crawler
    Public drive As IWebDriver
    Public empresa As ClienteVivo
    Public wait As WebDriverWait

    Sub New(_crawler As Crawler)
        Me.crawler = _crawler
        Me.drive = crawler.Drive
        Me.empresa = crawler.Empresa
        Me.wait = WebdriverCt.Wait
    End Sub

    Public Sub EnriquecerGestores()

        Try

            Dim TabelaGestores As IWebElement
            Dim GestoresDataTable As DataTable
            Dim ListaDeGestores As List(Of DataRow)


            wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//*[@id='s_1_1_7_0_Ctrl']"), "Pesquisar"))

preparartabela:
            Try
                TabelaGestores = drive.FindElement(By.XPath("//*[@id='s_1_l']"))
                GestoresDataTable = FuncoesUteis.ConstruirDatatable(TabelaGestores)

                Dim colunas As Integer() = Enumerable.Range(0, 15).ToArray

                ListaDeGestores = FuncoesUteis.PrepararContasParaInserirNaTabela(GestoresDataTable, TabelaGestores, colunas) ' apesar do nome da função ela serve ao próposito de criar datarows para qualquer tabela

            Catch ex As StaleElementReferenceException
                GoTo preparartabela

            End Try



            For Each gestor In ListaDeGestores
                GestoresDataTable.Rows.Add(gestor)
            Next

            For i = 0 To GestoresDataTable.Rows.Count - 1
                Dim gestor As New GESTOR
                If GestoresDataTable(i)(6).ToString.Length > 1 Then
                    gestor.CPF = GestoresDataTable(i)(6)
                    gestor.EMAIL = GestoresDataTable(i)(12)
                    gestor.EMPRESA.Add(empresa)
                    gestor.Master = GestoresDataTable(i)(1)
                    gestor.NOME = GestoresDataTable(i)(2) + " " + GestoresDataTable(i)(3) + " " + GestoresDataTable(i)(4)
                    gestor.TelefoneCelular = GestoresDataTable(i)(7)
                    gestor.TelefoneFixo = GestoresDataTable(i)(8)
                    crawler.AdicionarGestores(gestor) ' testar salvamento direto

                End If

            Next
            Console.WriteLine($"A empresa possui {(GestoresDataTable.Rows.Count - 1).ToString} Gestores Cadastrados")

        Catch ex As Exception
            FuncoesUteis.debug(ex)
            Throw
        End Try


ExitSub:
    End Sub


End Class
