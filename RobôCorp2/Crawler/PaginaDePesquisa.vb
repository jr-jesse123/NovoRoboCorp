
Imports CrawlerCorp.CrawlerClass
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Support.UI
Imports SeleniumExtras.WaitHelpers



Public Class PaginaDePesquisa
    Dim empresa As EMPRESA
    Dim Crawler As Crawler
    Dim drive As IWebDriver
    Public Wait As WebDriverWait
    Public CNPJ As CNPJ
    Public Contextolivre As Boolean = True

    Sub New(_crawler As Crawler)
        Me.drive = _crawler.Drive
        Me.Wait = _crawler.Wait
        Me.Crawler = _crawler


    End Sub

    Public Sub ConsultarCnpj()

        CNPJ = listaDeCNPJ.Where(Function(X) X.ENRIQUECIDO = Nothing).First

        If FuncoesUteis.ChecarPresenca(drive, "//*[@id='s_1_1_16_0_Ctrl']") Then
            PesquisarCNPJ(CNPJ)
        Else
            PosicionarNaConsulta()
            PesquisarCNPJ(CNPJ)
        End If



        If listaDeCNPJ.Where(Function(cnpj) cnpj.ENRIQUECIDO = Nothing).Count < 50 And Contextolivre = True Then
            Dim THREAD As New Threading.Thread(AddressOf FilaParaAbastecerCNPJ)
            THREAD.Start()
        End If

    End Sub

    Private Sub FilaParaAbastecerCNPJ()
        Contextolivre = False
        Using context As New CrawlerContext
            FuncoesUteisDAO.ReabastecerCNPJS(context)
        End Using
        Contextolivre = True
    End Sub

    Private Sub PosicionarNaConsulta()

        Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div/div[1]/div/div[1]")))
        FuncoesUteis.AguardarClicarPorTexto("Contas", 60, drive)
        FuncoesUteis.AguardarClicarPorTexto("Hierarquia de contas", 60, drive)


    End Sub

    Private Sub PesquisarCNPJ(eMPRESA As CNPJ)

        Wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='s_1_1_16_0_Ctrl']")))

        If drive.FindElement(By.XPath("//*[@id='s_1_1_7_0_Ctrl']")).Displayed Then ' verifica se o campo de pesquisa já estaá aberto e cancela se estiver 
            drive.FindElement(By.XPath("//*[@id='s_1_1_7_0_Ctrl']")).Click()
        End If

        drive.FindElement(By.XPath("//*[@id='s_1_1_16_0_Ctrl']")).Click() ' Botão pesquisar

        Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='1_s_1_l_VIVO_Documento']"))).Click()


        drive.FindElement(By.XPath("//*[@id='1_VIVO_Documento']")).SendKeys(eMPRESA.CNPJS) ' FormCNPJAberto

        drive.FindElement(By.XPath("//*[@id='1_s_1_l_Account_Status']")).Click() ' FormStatusDaConta

        drive.FindElement(By.XPath("//*[@id='1_Account_Status']")).SendKeys("*Ativo*")

        drive.FindElement(By.XPath("//*[@id='s_1_1_8_0_Ctrl']")).Click()

    End Sub

    Public Sub LocalizarProximoCnpj()
        Do


            ConsultarCnpj()


            Dim BtnPesquisar As By = By.XPath("//*[@id='s_1_1_16_0_Ctrl']")

            Try
                Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(BtnPesquisar, "Pesquisar"))
            Catch ex As WebDriverTimeoutException
                If FuncoesUteis.ChecarErroPrivilegios(drive) Then
                    drive.Navigate.Back()
                    CNPJ.ENRIQUECIDO = 2
                    Continue Do
                End If
            End Try

            Crawler.Timeout = New TimeSpan(0, 0, 0)

            Try
                drive.FindElement(By.XPath("//*[@id='1']"))
                Console.WriteLine($"{CNPJ.CNPJS} Retornou informações e será adicionada ao banco")
                Crawler.Timeout = New TimeSpan(0, 0, 1)
                ThreadsActivity = Now
                Crawler.Empresa = New EMPRESA(CNPJ.CNPJS)
                AcessarClinte()
                Exit Sub

            Catch ex As Threading.ThreadAbortException
                Throw

            Catch ex As Exception
                Console.WriteLine($"{CNPJ.CNPJS} Foi consultado e Descartado às " + Now.ToString)
                ThreadsActivity = Now
                CNPJ.ENRIQUECIDO = 2
                Continue Do

            End Try

        Loop
    End Sub



    Private Sub AcessarClinte()


        Try
            FuncoesUteis.AguardarClicarPorXpath("//*[@id='1_s_1_l_Parent_Account_Name']", 60, drive) ' acessar o cliente
        Catch
            FuncoesUteis.AguardarClicarPorXpath("/html/body/div[1]/div/div[5]/div/div[6]/div/div[1]/div/div[1]/div/div/form/span/div/div[2]/div/div/div[3]/div[3]/div/table/tbody/tr[2]/td[10]/a", 60, drive) ' acessar o cliente
        End Try


    End Sub


End Class
