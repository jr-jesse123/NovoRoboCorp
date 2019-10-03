Imports CrawlerCorp.CrawlerClass
Imports LibNovoRoboCorp
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Support.UI
Imports SeleniumExtras.WaitHelpers

Public Class PaginaDeConsultarSocios
    Public Property ListaSocios As List(Of SociosReceita)
    Public driver As IWebDriver
    Public empresa As ClienteVivo
    Public crawler As Crawler
    Public Wait As WebDriverWait

    Sub New(socios As List(Of SociosReceita))
        Me.ListaSocios = socios
        Me.driver = WebdriverCt.Driver
        Me.Wait = WebdriverCt.Wait
    End Sub

    Public Sub EnriquecerSocios()
        Dim tabelaWebElement As IWebElement
        Dim Socios As DataTable
        Dim ListaDeSocios As List(Of DataRow)


        'socio = CType(socio, SocioCorp)


        driver.FindElement(By.XPath("//*[@id='s_vctrl_div']")).FindElement(By.LinkText("Holding / Sócios")).Click() ' clica no botão holding/socios 

ExtrairSocios:
        Try
            tabelaWebElement = ObterTabelaElement()

            If tabelaWebElement Is Nothing Then Exit Sub

            Socios = FuncoesUteis.ConstruirDatatable(tabelaWebElement)

            Dim colunas As Integer() = {1, 2, 7}

            ListaDeSocios = FuncoesUteis.PrepararContasParaInserirNaTabela(Socios, tabelaWebElement, colunas)

            For Each socio2 As DataRow In ListaDeSocios
                If socio2(2).ToString.Length > 2 Then
                    Socios.Rows.Add(socio2)
                End If
            Next

        Catch ex As StaleElementReferenceException
            Crawler.EnviarLog("nova tentativa por erro de stale")
            GoTo ExtrairSocios

        Catch ex As TimeoutException
            Crawler.EnviarLog("nova tentativa por erro de timeout")
            GoTo ExtrairSocios
        End Try

        Crawler.EnviarLog($"Encontrados {(ListaDeSocios.Count - 1).ToString} sócio(s)")

        For i = 0 To Socios.Rows.Count - 1
            If Socios.Rows(i)(2).Length > 1 Then

                Try
                    Stop
                    Dim novoSocio = CType(ListaSocios.Where(Function(s) s.CnpjOuCpf = Socios.Rows(i)(2)).First, SocioCorp)
                    novoSocio.TelefoneCadastrado = Socios.Rows(i)(7)


                Catch ex As Exception

                End Try


            End If
        Next

        Exit Sub

    End Sub



    Private Function ObterTabelaElement() As IWebElement
        Dim tabelaWebElement As IWebElement
        Dim tentativas As Integer

        On Error Resume Next
        Do While tabelaWebElement Is Nothing And tentativas < 5

            tabelaWebElement = driver.FindElement(By.XPath("//*[@id='s_5_l']"))
            If tabelaWebElement Is Nothing Then
                tabelaWebElement = driver.FindElement(By.XPath("//*[@id='s_3_l']"))
                tentativas = tentativas + 1
            End If

            If tabelaWebElement Is Nothing Then
                tabelaWebElement = driver.FindElement(By.XPath("//*[@id='s_4_l']"))
                tentativas = tentativas + 1
            End If

        Loop
        Return tabelaWebElement
        On Error GoTo 0

    End Function

End Class
