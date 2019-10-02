Imports CrawlerCorp.CrawlerClass
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Support.UI
Imports SeleniumExtras.WaitHelpers

Public Class PaginaDeConsultarSocios
    Public drive As IWebDriver
    Public empresa As ClienteVivo
    Public crawler As Crawler
    Public Wait As WebDriverWait

    Sub New(crawler As Crawler)
        Me.drive = crawler.Drive
        Me.empresa = crawler.Empresa
        Me.crawler = crawler
        Me.Wait = WebdriverCt.Wait
    End Sub

    Public Sub EnriquecerSocios()
        Dim MenuInferior, BtnSocios, tabelaWebElement As IWebElement
        Dim Socios As DataTable
        Dim ListaDeSocios As List(Of DataRow)
        Dim socio As SocioCorp


        drive.FindElement(By.XPath("//*[@id='s_vctrl_div']")).FindElement(By.LinkText("Holding / Sócios")).Click() ' clica no botão holding/socios 

ExtrairSocios:
        Try
            tabelaWebElement = ExtrairSociosHolding()

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
            Console.WriteLine("nova tentativa por erro de stale")
            GoTo ExtrairSocios

        Catch ex As TimeoutException
            Console.WriteLine("nova tentativa por erro de timeout")
            GoTo ExtrairSocios
        End Try

        Console.WriteLine($"Encontrados {(ListaDeSocios.Count - 1).ToString} sócio(s)")

        For i = 0 To Socios.Rows.Count - 1
            If Socios.Rows(i)(2).Length > 1 Then
                socio = New SocioCorp
                crawler.AdicionarSocios(socio)
            End If
        Next

        Exit Sub

    End Sub



    Private Function ExtrairSociosHolding() As IWebElement
        Dim tabelaWebElement As IWebElement
        Dim tentativas As Integer

        On Error Resume Next
        Do While tabelaWebElement Is Nothing And tentativas < 5

            tabelaWebElement = drive.FindElement(By.XPath("//*[@id='s_5_l']"))
            If tabelaWebElement Is Nothing Then
                tabelaWebElement = drive.FindElement(By.XPath("//*[@id='s_3_l']"))
                tentativas = tentativas + 1
            End If

            If tabelaWebElement Is Nothing Then
                tabelaWebElement = drive.FindElement(By.XPath("//*[@id='s_4_l']"))
                tentativas = tentativas + 1
            End If

        Loop
        Return tabelaWebElement
        On Error GoTo 0

    End Function







End Class
