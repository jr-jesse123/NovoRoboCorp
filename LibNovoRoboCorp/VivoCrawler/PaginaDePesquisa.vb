
Imports System.Data.Entity
Imports CrawlerCorp.CrawlerClass
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Support.UI
Imports SeleniumExtras.WaitHelpers



Public Class PaginaDePesquisa
    Dim Crawler As Crawler
    Dim drive As IWebDriver
    Public Wait As WebDriverWait
    Private Cnpjs As List(Of CadastroCNPJ)


    Sub New()
        Me.drive = WebdriverCt.Driver
        Me.Wait = WebdriverCt.Wait

    End Sub

    Private Sub ConsultarCnpj(CadastroCnpj As CadastroCNPJ)

        If FuncoesUteis.ChecarPresenca(drive, "//*[@id='s_1_1_16_0_Ctrl']") Then
            PesquisarCNPJ(CadastroCnpj)
        Else
            PosicionarNaConsulta()
            PesquisarCNPJ(CadastroCnpj)
        End If

    End Sub


    Private Sub PosicionarNaConsulta()

        Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div/div[1]/div/div[1]")))
        FuncoesUteis.AguardarClicarPorTexto("Contas", 60, drive)
        FuncoesUteis.AguardarClicarPorTexto("Hierarquia de contas", 60, drive)


    End Sub

    Private Sub PesquisarCNPJ(CadastroCnpj As CadastroCNPJ)

        Wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='s_1_1_16_0_Ctrl']")))

        If drive.FindElement(By.XPath("//*[@id='s_1_1_7_0_Ctrl']")).Displayed Then ' verifica se o campo de pesquisa já estaá aberto e cancela se estiver 
            drive.FindElement(By.XPath("//*[@id='s_1_1_7_0_Ctrl']")).Click()
        End If

        drive.FindElement(By.XPath("//*[@id='s_1_1_16_0_Ctrl']")).Click() ' Botão pesquisar

        Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='1_s_1_l_VIVO_Documento']"))).Click()


        drive.FindElement(By.XPath("//*[@id='1_VIVO_Documento']")).SendKeys(CadastroCnpj.CNPJ) ' FormCNPJAberto

        drive.FindElement(By.XPath("//*[@id='1_s_1_l_Account_Status']")).Click() ' FormStatusDaConta

        drive.FindElement(By.XPath("//*[@id='1_Account_Status']")).SendKeys("*Ativo*")

        drive.FindElement(By.XPath("//*[@id='s_1_1_8_0_Ctrl']")).Click()

    End Sub

    Public Function LocalizarProximoCnpj(empresas As DbSet(Of CadastroCNPJ)) As ClienteVivo

        Dim cnpjEnriquecer = empresas.Except(empresas.OfType(Of CadastroCNPJEnriquecido)).First


        Do
            ConsultarCnpj(cnpjEnriquecer)
            Dim BtnPesquisar As By = By.XPath("//*[@id='s_1_1_16_0_Ctrl']")

            Try
                Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(BtnPesquisar, "Pesquisar"))
            Catch ex As WebDriverTimeoutException
                If FuncoesUteis.ChecarErroPrivilegios(drive) Then
                    drive.Navigate.Back()
                    Dim cnpjNaoEncontrado = CType(cnpjEnriquecer, CadastroCNPJEnriquecido)
                    cnpjNaoEncontrado.EnriquecidoVivoMovel = Now
                    empresas.Add(cnpjNaoEncontrado)
                    Continue Do
                End If
            End Try

            'Crawler.Timeout = New TimeSpan(0, 0, 0)

            Try
                drive.FindElement(By.XPath("//*[@id='1']"))
                Console.WriteLine($"{cnpjEnriquecer.CNPJ} Retornou informações e será adicionada ao banco")
                WebdriverCt.Wait.Timeout = New TimeSpan(0, 0, 1)

                Dim ClienteVivo = CType(cnpjEnriquecer, ClienteVivo)
                ClienteVivo.EnriquecidoVivoMovel = Now

                AcessarClinte()
                Return ClienteVivo

            Catch ex As Threading.ThreadAbortException
                Throw

            Catch ex As Exception
                Console.WriteLine($"{cnpjEnriquecer.CNPJ} Foi consultado e Descartado às " + Now.ToString)
                Dim cnpjNaoEncontrado = CType(cnpjEnriquecer, CadastroCNPJEnriquecido)
                cnpjNaoEncontrado.EnriquecidoVivoMovel = Now
                empresas.Add(cnpjNaoEncontrado)
                Continue Do

            End Try

        Loop
    End Function



    Private Sub AcessarClinte()


        Try
            FuncoesUteis.AguardarClicarPorXpath("//*[@id='1_s_1_l_Parent_Account_Name']", 60, drive) ' acessar o cliente
        Catch
            FuncoesUteis.AguardarClicarPorXpath("/html/body/div[1]/div/div[5]/div/div[6]/div/div[1]/div/div[1]/div/div/form/span/div/div[2]/div/div/div[3]/div[3]/div/table/tbody/tr[2]/td[10]/a", 60, drive) ' acessar o cliente
        End Try


    End Sub


End Class
