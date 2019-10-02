Imports OpenQA.Selenium
Imports OpenQA.Selenium.Support.UI
Imports SeleniumExtras.WaitHelpers

Public Class PaginaDeConsultarLinhas
    Public drive As IWebDriver
    Public empresa As ClienteVivo
    Public ContaAtual As String
    Public crawler As Crawler
    Public Wait As WebDriverWait

    Sub New(_crawler As Crawler)
        Me.drive = _crawler.Drive
        Me.empresa = _crawler.Empresa
        Me.Wait = WebdriverCt.Wait
        Me.crawler = _crawler
    End Sub


    Public Sub EnriquecerContasLinhas()
        Dim LinhasDataTable As DataTable
        Dim nrdecontas As Integer = 1
        Try
            drive.FindElement(By.XPath("//*[@id='s_vctrl_div']")).FindElement(By.LinkText("Contas")).Click() ' clica no btn contas do menu inferior
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='s_2_rc']"))) ' aguardar o número de registros aparecer
            Console.WriteLine("Procurando por contas ativas")
            Me.ContaAtual = EncontrarPrimeiraContaAtiva()
            Dim xpath = "/html/body/div[1]/div/div[5]/div/div[6]/div/div[1]/div/div[3]/div[1]/table/tbody/tr/td[1]/div/div/div/form/span/div/div[1]/table/tbody/tr/td[1]"




            If ContaAtual <> "" Then

                Do

Espera:             Dim tentativas As Integer = 0
                    Try

                        Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath(xpath), "Produtos e serviços"))
                    Catch ex As Threading.ThreadAbortException
                        Throw

                    Catch ex As TimeoutException
                        If tentativas > 5 Then Throw
                        Console.WriteLine("aguardando por linhas")
                        tentativas = tentativas + 1
                        GoTo Espera
                    Catch ex As Exception
                        FuncoesUteis.debug(ex)
                    End Try

                    Console.WriteLine($"Pesquisando a conta {ContaAtual} às {Now.TimeOfDay.ToString}")
                    ThreadsActivity = Now


                    LinhasDataTable = EnriquecerLinhas(LinhasDataTable, empresa)

                    nrdecontas = nrdecontas + 1

                    Try
                        drive.Navigate.Back()
                        Wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText(ContaAtual))) ' espera a contaatual aparecer pra mostrar que voltou pra pagina de contas

                    Catch ex As Exception
                        drive.Navigate.Back()

                    End Try

                Loop While TemMaisContas2(Me.ContaAtual)

                crawler.Empresa.Observações = crawler.Empresa.Observações + " em " + nrdecontas.ToString + " Contas"

                If LinhasDataTable IsNot Nothing Then
                    Console.WriteLine($"Foram encontradas {nrdecontas.ToString} contas ativas no cnpj {empresa.CNPJ} totalizando {crawler.Linhas.Count.ToString} linhas")
                Else
                    Console.WriteLine($"Não foram encontradas linhas nas  {nrdecontas.ToString} contas do cnpj")
                End If



            Else
                Console.WriteLine("Nenhuma Conta ativa")

            End If
            nrdecontas = 0

            Exit Sub

        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Function EnriquecerLinhas(LinhasDataTable As DataTable, EMPRESA As ClienteVivo) As DataTable


        Dim tabelaWebElement As IWebElement
        Dim ListaDeLinhas As List(Of DataRow)
        Dim linhaEncontrada As Boolean
        Dim LinhaGravar As LINHA

        Do
            linhaEncontrada = False

            Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//*[@id='s_1_1_19_0_Ctrl']"), "Pesquisar"))
            If FuncoesUteis.ExpandirSeExpancivel("//*[@id='s_1_rc']", "//*[@id='s_1_1_3_0']/a/img", drive) Then
                Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='11_s_1_l_Serial_Number']")))
            End If


            tabelaWebElement = drive.FindElement(By.XPath("//*[@id='s_1_l']"))



            If LinhasDataTable Is Nothing Then
                LinhasDataTable = FuncoesUteis.ConstruirDatatable(drive.FindElement(By.XPath("//*[@id='s_1_l']"))) 'tablewebelement
            End If
            If drive.FindElement(By.XPath("//*[@id='s_1_l']")).Text.Length > 1 Then ' verifica se a tablea tem conteúdo
obterlinhas:

                Dim colunas As Integer() = {0, 1, 2, 3, 4, 6, 7, 8, 10, 12, 19}
                Try
                    ListaDeLinhas = FuncoesUteis.PrepararContasParaInserirNaTabela(LinhasDataTable, drive.FindElement(By.XPath("//*[@id='s_1_l']")), colunas) 'tablewebelement
                Catch ex As Threading.ThreadAbortException
                    Throw
                Catch ex As StaleElementReferenceException
                    Console.WriteLine("Nova Tentativa de stale às " + Now.TimeOfDay.ToString)
                    GoTo obterlinhas
                Catch ex As TimeoutException
                    Console.WriteLine("Nova Tentativa de timout às " + Now.TimeOfDay.ToString)
                    GoTo obterlinhas
                End Try
                linhaEncontrada = True
            End If

            If ListaDeLinhas IsNot Nothing Then
                If ListaDeLinhas.Count > 0 Then
                    For Each linha In ListaDeLinhas
                        If linha(4).Length < 5 Then
                            Continue For
                        Else
                            LinhasDataTable.Rows.Add(linha)

                        End If
                    Next
                End If

            End If

            ThreadsActivity = Now
            If linhaEncontrada Then Console.WriteLine("Conjunto de Linhas encontradas às " + Now.TimeOfDay.ToString)
        Loop While TemMaisContas("//*[@id='s_1_rc']", "//*[@id='last_pager_s_1_l']")

        For i = 0 To LinhasDataTable.Rows.Count - 1
            LinhaGravar = New LINHA(LinhasDataTable.Rows(i), EMPRESA.CNPJ)
            crawler.AdicionarLinhas(LinhaGravar)
        Next

        Console.WriteLine(LinhasDataTable.Rows.Count.ToString + " linha(s) Encontrada(s)")
        ThreadsActivity = Now
        Return LinhasDataTable

    End Function

    Private Function EncontrarPrimeiraContaAtiva() As String

        Dim maximocontasint As Integer
        Dim ProximaContaXPath, NumProximaconta As String

        Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//*[@id='s_2_1_8_0_Ctrl']"), "Conta Online"))

        Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='s_2_rc']")))
        If FuncoesUteis.ExpandirSeExpancivel("//*[@id='s_2_rc']", "//*[@id='s_2_1_2_0']/a/img", drive) Then
            Dim conta11 As IWebElement = Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='11_s_2_l_Name']"))) ' faz ele esperar pela expansão acontecer.

        End If

        Do

            maximocontasint = DefinirLimiteDolooping("//*[@id='s_2_rc']")

            For i = 1 To maximocontasint


                If drive.FindElement(By.XPath("//*[@id='" + i.ToString + "_s_2_l_Account_Status']")).Text Like "*Ativo*" Then ' verifica o texto da próxima conta
                    ProximaContaXPath = "//*[@id='" + i.ToString + "_s_2_l_Name']/a"
                    NumProximaconta = drive.FindElement(By.XPath(ProximaContaXPath)).Text
                    drive.FindElement(By.XPath(ProximaContaXPath)).Click() ' entrar na PrimeiraConta
                    Return NumProximaconta

                    Exit Function
                End If
            Next
            ThreadsActivity = Now
        Loop While TemMaisContas("//*[@id='s_2_rc']", "//*[@id='last_pager_s_2_l']/span")

        Return ""


    End Function

    Private Function TemMaisContas2(ContaAtual As String) As Boolean
        Dim maximoContasInt, maximoContasInt2 As Integer
        Dim ProximaConta2txt, ProximaContalink As String


        If FuncoesUteis.ExpandirSeExpancivel("//*[@id='s_2_rc']", "//*[@id='s_2_1_2_0']/a/img", drive) Then
            Dim conta11 As IWebElement = drive.FindElement(By.XPath("//*[@id='11_s_2_l_Name']")) ' faz ele esperar pela expansão acontecer.

        End If

        For w = 1 To 3

            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='s_2_rc']")))
            maximoContasInt = DefinirLimiteDolooping("//*[@id='s_2_rc']")

            For i = 1 To maximoContasInt


                If drive.FindElement(By.XPath("//*[@id='" + i.ToString + "_s_2_l_Name']/a")).Text = ContaAtual Then 'verifica se a proxima conta tem o número da conta atual pra começar o looping

                    Do
                        Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='s_2_rc']")))
                        maximoContasInt2 = DefinirLimiteDolooping("//*[@id='s_2_rc']")

                        For u = (i + 1) To maximoContasInt2



                            If drive.FindElement(By.XPath("//*[@id='" + u.ToString + "_s_2_l_Account_Status']")).Text Like "*Ativo*" Then ' checa se o texto da proxima conta é ativo

                                ProximaContalink = "//*[@id='" + u.ToString + "_s_2_l_Name']/a"
                                Me.ContaAtual = drive.FindElement(By.XPath(ProximaContalink)).Text ' armazena no texto da proxima conta na conta atual
                                drive.FindElement(By.XPath(ProximaContalink)).Click() ' clica na próxima conta

                                Return True
                            End If
                        Next
                    Loop While TemMaisContas("//*[@id='s_2_rc']", "//*[@id='last_pager_s_2_l']/span")
                End If
            Next

        Next

        Return False

    End Function

    Private Function TemMaisContas(PathDoSinalizador As String, Optional PathDoProximoBtn As String = Nothing) As Boolean
INICIO:
        Dim TemMais As Boolean


        TemMais = drive.FindElement(By.XPath(PathDoSinalizador)).Text Like "*+" 'verifica o texto do registros pra ver se tem + 

        For w = 1 To 3

            If PathDoProximoBtn Is Nothing Or TemMais = False Then

            Else

                Dim tentativas As Integer
                Try

                    drive.FindElement(By.XPath(PathDoProximoBtn)).Click() ' clica no botão de próximos registros
                Catch ex As ElementNotInteractableException
                    If tentativas > 5 Then Throw
                    Console.WriteLine("Erro ao avançar para próximo conjunto de linhas")
                    Console.WriteLine("Tentativa Número: " + tentativas.ToString)
                    Threading.Thread.Sleep(2000)
                    tentativas = tentativas + 1
                    GoTo INICIO
                Catch ex As Exception

                End Try



            End If
            Return TemMais
        Next

        Return TemMais
    End Function

    Private Function DescobrirNrDecontas(XpathDaTabela As String) As Integer

        Dim NrdeContas As IReadOnlyList(Of IWebElement)

        NrdeContas = drive.FindElement(By.XPath(XpathDaTabela)).FindElements(By.TagName("tr"))

        Return NrdeContas.Count
    End Function

    Private Function DefinirLimiteDolooping(xpath As String) As Integer

        Dim registros As IWebElement
        Dim registrosok As Boolean = True
        Dim registrotentativas As Integer = 0
        Dim registrostxt = drive.FindElement(By.XPath(xpath)).Text

        Do
            If registrostxt = "Registro 1 de 1+" Or registrostxt = "Sem registros" Then
                registrosok = False
            Else
                registrosok = True
            End If
            registros = drive.FindElement(By.XPath(xpath))

            If registrotentativas > 10 Then Return 0
            registrotentativas = registrotentativas + 1
        Loop Until registrosok


        Dim registrosarray() = Split(registrostxt, "-")
        registrosarray(0) = Replace(registrosarray(0), "Registros ", "")
        registrosarray(1) = Replace(registrosarray(1), " de", "")
        registrosarray(1) = Replace(registrosarray(1), "+", "")
        Dim registros2() = Split(registrosarray(1), " ")

        Dim registro1 = Integer.Parse(registrosarray(0))
        Dim regitro2 = Integer.Parse(registros2(1))
        Return regitro2 - registro1 + 1

    End Function

End Class


