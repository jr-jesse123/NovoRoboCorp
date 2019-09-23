
Imports CrawlerCorp.CrawlerClass
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Support.UI
Imports SeleniumExtras.WaitHelpers



Public Class NovaPaginaDePesquisa
    Dim empresa As EMPRESA
    Dim Crawler As Crawler
    Dim drive As IWebDriver
    Public Wait As WebDriverWait

    Sub New(_crawler As Crawler)
        Me.empresa = _crawler.Empresa
        Me.drive = _crawler.Drive
        Me.Wait = _crawler.Wait
        Me.Crawler = _crawler
        ConsultarCnpj()

    End Sub

    Public Sub ConsultarCnpj()

        If FuncoesUteis.ChecarPresenca(drive, "//*[@id='s_1_1_16_0_Ctrl']") Then
            PesquisarCNPJ(empresa)
        Else
            PosicionarNaConsulta()
            PesquisarCNPJ(empresa)
        End If




    End Sub


    Private Sub PosicionarNaConsulta()

        Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div/div[1]/div/div[1]")))
        FuncoesUteis.AguardarClicarPorTexto("Solicitação de serviço", 60, drive)
        FuncoesUteis.AguardarClicarPorTexto("Lista de Solicitações de serviço", 60, drive)


    End Sub

    Private Sub PesquisarCNPJ(eMPRESA As EMPRESA)


        Wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='s_vis_div']/select")))

        If drive.FindElement(By.XPath("//*[@id='s_2_1_16_0_Ctrl']")).Displayed Then ' verifica se o campo de pesquisa já estaá aberto e cancela se estiver 
            drive.FindElement(By.XPath("//*[@id='s_2_1_16_0_Ctrl']")).Click()
        End If

        drive.FindElement(By.XPath("//*[@id='s_2_1_10_0_Ctrl']")).Click() ' Botão pesquisar

        Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='1_s_2_l_VIVO_CPF_CNPJ']"))).Click()

        drive.FindElement(By.XPath("//*[@id='1_VIVO_CPF_CNPJ']")).SendKeys(eMPRESA.CNPJ) ' FormCNPJAberto


        drive.FindElement(By.XPath("//*[@id='s_2_1_7_0_Ctrl']")).Click()

    End Sub

    Public Function VerificarRetornoDaconsultaCnpj() As Boolean

        Dim BtnPesquisar As By = By.XPath("//*[@id='s_2_1_10_0_Ctrl']")

        Try
            Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(BtnPesquisar, "Pesquisar"))
        Catch ex As WebDriverTimeoutException
            If FuncoesUteis.ChecarErroPrivilegios(drive) Then
                drive.Navigate.Back()
                Return False
            End If
        End Try

        Crawler.Timeout = New TimeSpan(0, 0, 0)

        Try
            drive.FindElement(By.XPath("//*[@id='1_s_2_l_VIVO_Protocolo_SS']"))
            Console.WriteLine($"{empresa.CNPJ} Retornou informações e será adicionada ao banco")
            Crawler.Timeout = New TimeSpan(0, 0, 1)
            ThreadsActivity = Now
            Return True
        Catch ex As Threading.ThreadAbortException
            Throw

        Catch ex As Exception
            Console.WriteLine($"{empresa.CNPJ} Foi consultado e Descartado às " + Now.ToString)
            ThreadsActivity = Now
            Return False

        End Try

    End Function



End Class
