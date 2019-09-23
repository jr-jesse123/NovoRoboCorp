
Imports CrawlerCorp.CrawlerClass
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Support.UI
Imports SeleniumExtras.WaitHelpers



Public Class PaginaDePesquisa2
    Dim empresa As Empresa_Sem_linhas_Cadastrada2
    Dim Crawler As Crawler2
    Dim drive As IWebDriver
    Public Wait As WebDriverWait

    Sub New(_crawler As Crawler2)
        Me.empresa = _crawler.Empresa
        Me.drive = _crawler.Drive
        Me.Wait = _crawler.Wait
        Me.Crawler = _crawler
        ConsultarCnpj()

    End Sub

    Public Sub ConsultarCnpj()

        'If FuncoesUteis.ChecarPresenca(drive, "//*[@id='s_1_1_16_0_Ctrl']") Then
        'PesquisarCNPJ(empresa)
        'Else
        PosicionarNaConsulta()
            PesquisarCNPJ(empresa)
        ' End If




    End Sub


    Private Sub PosicionarNaConsulta()


        FuncoesUteis.AguardarClicarPorTexto("Contas", 60, drive)
        FuncoesUteis.AguardarClicarPorTexto("Hierarquia de contas", 60, drive)


    End Sub

    Private Sub PesquisarCNPJ(eMPRESA As Empresa_Sem_linhas_Cadastrada2)

        Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='s_1_1_16_0_Ctrl']")))

        drive.FindElement(By.XPath("//*[@id='s_1_1_16_0_Ctrl']")).Click() ' Botão pesquisar

        Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='1_s_1_l_VIVO_Documento']"))).Click()


        drive.FindElement(By.XPath("//*[@id='1_VIVO_Documento']")).SendKeys(eMPRESA.CNPJ) ' FormCNPJAberto

        drive.FindElement(By.XPath("//*[@id='1_s_1_l_Account_Status']")).Click() ' FormStatusDaConta

        drive.FindElement(By.XPath("//*[@id='1_Account_Status']")).SendKeys("*Ativo*")

        drive.FindElement(By.XPath("//*[@id='s_1_1_8_0_Ctrl']")).Click()

        Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div/div[1]/div/div[1]")))

    End Sub

    Public Function VerificarRetornoDaconsultaCnpj() As Boolean

        Dim BtnPesquisar As By = By.XPath("//*[@id='s_1_1_16_0_Ctrl']")
        Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(BtnPesquisar, "Pesquisar"))

        Try

            drive.FindElement(By.XPath("//*[@id='1']"))
            Console.WriteLine($"{empresa.CNPJ} Retornou informações e será adicionada ao banco")
            Return True
        Catch ex As NotFoundException

            Stop
            Dim linha As New LINHA2(Crawler.Empresa.CNPJ)
            Crawler.AdicionarLinhas(linha)

            Console.WriteLine($"{empresa.CNPJ} Foi consultado e Não Retornou registros, linha de controle adicionada.")

            Return False

        Catch ex As Exception
            Stop

        End Try

    End Function



End Class
