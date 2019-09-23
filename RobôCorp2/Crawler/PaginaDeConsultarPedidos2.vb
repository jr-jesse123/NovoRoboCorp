Imports CrawlerCorp.CrawlerClass
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Support.UI
Imports RobôCorp.CrawlerClass
Imports SeleniumExtras.WaitHelpers

Public Class PaginaDeConsultarPedidos2
    Public drive As IWebDriver
    Public empresa As Empresa_Sem_linhas_Cadastrada2
    Public PedidoAtual As String
    Public crawler As Crawler2
    Public Wait As WebDriverWait

    Sub New(_crawler As Crawler2)
        Me.drive = _crawler.Drive
        Me.empresa = _crawler.Empresa
        Me.Wait = _crawler.Wait
        Me.crawler = _crawler

        AcessarClinte()
        EnriquecerCarteira()
        AcessarPedidos()

    End Sub

    Private Sub EnriquecerCarteira()
        Dim AtendimentoDataTable As DataTable
        Dim ListaDeAtendimento As List(Of DataRow)
        Dim Referencia As IWebElement

        Wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Atendimento Comercial e Carteira")))
        drive.FindElement(By.LinkText("Atendimento Comercial e Carteira")).Click()
        Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div/div[5]/div/div[6]/div/div[1]/div/div[3]/div[1]/div/div/form/span/div/div[2]/div/div/div[3]/div[3]/div/table/tbody")))

        If FuncoesUteis.ExpandirSeExpancivel("//*[@id='s_2_rc']", "/html/body/div[1]/div/div[5]/div/div[6]/div/div[1]/div/div[3]/div[1]/div/div/form/span/div/div[1]/table/tbody/tr/td[9]/span/a/img", drive) Then
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='11_s_2_l_VIVO_Organization']")))
        End If

        Do

            Dim tabelaWebElement = drive.FindElement(By.XPath("/html/body/div[1]/div/div[5]/div/div[6]/div/div[1]/div/div[3]/div[1]/div/div/form/span/div/div[2]/div/div/div[3]/div[3]/div/table/tbody"))


            If AtendimentoDataTable Is Nothing Then
                AtendimentoDataTable = FuncoesUteis.ConstruirDatatable(drive.FindElement(By.XPath("/html/body/div[1]/div/div[5]/div/div[6]/div/div[1]/div/div[3]/div[1]/div/div/form/span/div/div[2]/div/div/div[3]/div[3]/div/table/tbody"))) 'tablewebelement
            End If
            If drive.FindElement(By.XPath("/html/body/div[1]/div/div[5]/div/div[6]/div/div[1]/div/div[3]/div[1]/div/div/form/span/div/div[2]/div/div/div[3]/div[3]/div/table/tbody")).Text.Length > 1 Then ' verifica se a tablea tem conteúdo

obterlinhas:
                Try
                    ListaDeAtendimento = FuncoesUteis.PrepararContasParaInserirNaTabela(AtendimentoDataTable, drive.FindElement(By.XPath("//*[@id='s_2_l']"))) 'tablewebelement
                Catch ex As StaleElementReferenceException
                    Console.WriteLine(ex.Message)
                    Console.WriteLine("Nova Tentativa de stale às " + Now.TimeOfDay.ToString)
                    Stop
                    GoTo obterlinhas


                Catch ex As WebDriverTimeoutException
                    Console.WriteLine("Nova Tentativa de timout às " + Now.TimeOfDay.ToString)
                    GoTo obterlinhas
                End Try

            End If

            If ListaDeAtendimento IsNot Nothing Then
                If ListaDeAtendimento.Count > 0 Then
                    For Each linha In ListaDeAtendimento
                        If Join(linha.ItemArray).Length < 100 Then
                            Continue For
                        Else
                            AtendimentoDataTable.Rows.Add(linha)

                        End If
                    Next
                End If

            End If

            ThreadsActivity = Now
            Referencia = drive.FindElement(By.XPath("//*[@id='1_s_2_l_VIVO_Organization']"))

        Loop While TemMaisContas("//*[@id='s_2_rc']", "//*[@id='last_pager_s_2_l']", Referencia)


        Stop

    End Sub

    Public Sub EnriquecerPedidos()
        Dim LinhasDataTable As DataTable
        Dim nrdecontas As Integer = 1



        Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='s_1_rc']"))) ' aguardar o número de registros aparecer
        Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//*[@id='s_1_1_18_0_Ctrl']"), "Pesquisar")) ' aguardar o número de registros aparecer



        Console.WriteLine("Procurando por Pedidos concluídos")

        If drive.FindElement(By.XPath("//*[@id='s_1_rc']")).Text = "Sem registros" Then
            Dim linha As New LINHA2(crawler.Empresa.CNPJ)
            crawler.AdicionarLinhas(linha)

            Exit Sub
        End If


        Me.PedidoAtual = EncontrarPrimeiroPedidoConcluído()



        Dim xpath = "//*[@id='s_2_1_5_0_Ctrl']"

        Dim tentativas As Integer = 0

        If PedidoAtual <> "" Then

            Do

Espera:
                Try

                    Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath(xpath), "Replicar Tudo"))
                Catch ex As WebDriverTimeoutException

                    If tentativas > 5 Then Throw


                    Try
                        If drive.FindElement(By.XPath("/html/body/div[1]/div/div[5]/div/div[6]/div/div[1]/div/table/tbody/tr/td/table[3]/tbody/tr/td/table/tbody/tr/td/table/tbody/tr[2]/td")).Text _
                            = "Você não tem os privilégios necessários para visualizar informações detalhadas para este registro ou essas informações foram recentemente excluídas.(SBL-DAT-00309)" & vbCrLf & " " Then
                            GoTo ProximoPedido
                        End If
                    Catch ex2 As Exception

                    End Try


                    Console.WriteLine("aguardando por Pedidos")
                    tentativas = tentativas + 1
                    Console.WriteLine("Tentativa " + tentativas.ToString)
                    GoTo Espera
                End Try

                Console.WriteLine($"Pesquisando o Pedido {PedidoAtual} às {Now.TimeOfDay.ToString}")

                If drive.FindElement(By.XPath("//*[@id='1_s_2_l_VIVO_Tipo_Linha']")).Text Like "Troca*" Then
                    Console.WriteLine("Pedido identificado como troca, Procurar próximo")

                    Try
                        drive.Navigate.Back()
                        Wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText(PedidoAtual))) ' espera a contaatual aparecer pra mostrar que voltou pra pagina de contas
                    Catch ex As WebDriverTimeoutException
                        Console.WriteLine("tentando novo retorno")
                        drive.Navigate.Back()
                    End Try

                    Continue Do
                End If

                Dim tentativas3 As Integer = 0
                Try
Enriquecer:
                    LinhasDataTable = EnriquecerLinhas(LinhasDataTable, empresa)
                Catch ex As StaleElementReferenceException
                    GoTo Enriquecer
                Catch ex As Exception
                    If tentativas3 > 5 Then Throw
                    tentativas3 = tentativas3 + 1
                    Console.WriteLine("Tentativa " + tentativas3.ToString)
                    GoTo Enriquecer
                End Try
                tentativas3 = 0


ProximoPedido:
                Try
                    drive.Navigate.Back()
                    Wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText(PedidoAtual))) ' espera a contaatual aparecer pra mostrar que voltou pra pagina de contas
                Catch ex As WebDriverTimeoutException
                    Console.WriteLine("tentando novo retorno")
                    drive.Navigate.Back()
                End Try


            Loop While TemMaisContas2(Me.PedidoAtual)



            If LinhasDataTable IsNot Nothing Then
                Console.WriteLine($"Foram encontradas {nrdecontas.ToString} pedidos concluidos no cnpj {empresa.CNPJ} totalizando {crawler.Linhas.Count.ToString} linhas")
            Else
                Console.WriteLine($"Não foram encontradas linhas nas  {nrdecontas.ToString} contas do cnpj")
            End If

            crawler.NrDeContas = nrdecontas

        End If
        nrdecontas = 0

        If LinhasDataTable Is Nothing Then
            Dim linha As New LINHA2(crawler.Empresa.CNPJ)
            crawler.AdicionarLinhas(linha)
            Exit Sub

        End If



        Exit Sub

    End Sub

    Private Sub AcessarPedidos()

        Wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Endereço do cliente")))

        Try
            drive.FindElement(By.Id("//*[@id='s_vctrl_div']")).FindElement(By.LinkText("Pedidos")).Click() ' PEDISOS SE ELE EXISTIR
        Catch ex As Exception
            drive.FindElement(By.XPath("//*[@id='j_s_vctrl_div_tabScreen']")).Click()
            drive.FindElement(By.XPath("/html/body/div[1]/div/div[5]/div/div[6]/div/div[1]/div/div[2]/div[2]/ul/li[11]/select/option[3]")).Click()


        End Try
    End Sub

    Private Sub AcessarClinte()
        Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='1_s_1_l_Parent_Account_Name']")))
        FuncoesUteis.AguardarClicarPorXpath("//*[@id='1_s_1_l_Parent_Account_Name']", 60, drive) ' acessar o cliente
    End Sub

    Private Function EnriquecerLinhas(LinhasDataTable As DataTable, EMPRESA As Empresa_Sem_linhas_Cadastrada2) As DataTable

        Dim tabelaWebElement As IWebElement
        Dim ListaDeLinhas As List(Of DataRow)
        Dim linhaEncontrada As Boolean
        Dim LinhaGravar As LINHA2
        Dim linhaReferencia As IWebElement

        Try
            Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//*[@id='s_2_1_5_0_Ctrl']"), "Replicar Tudo"))
        Catch ex As Exception
            Stop
            If drive.FindElement(By.XPath("/html/body/div[1]/div/div[5]/div/div[6]/div/div[1]/" _
+ "div/div[1]/div/form/div/div/table/tbody/tr/td/div[3]/div/div/table/tbody/tr[3]/td[3]/div/input")).Text Then
                Console.WriteLine("pedido sem informações")
                drive.Navigate.Back()
                Exit Function
            End If




        End Try





        If FuncoesUteis.ExpandirSeExpancivel("//*[@id='s_2_rc']", "/html/body/div[1]/div/div[5]/div/div[6]/div/div[1]/div/div[3]/div[1]/div/div/form/span/div/div[1]/table/tbody/tr/td[21]/span/a/img", drive) Then
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='11_s_2_l_Outline_Number']")))
        End If


        Do
            linhaEncontrada = False


            tabelaWebElement = drive.FindElement(By.XPath("//*[@id='s_2_l']"))


            If LinhasDataTable Is Nothing Then
                LinhasDataTable = FuncoesUteis.ConstruirDatatable(drive.FindElement(By.XPath("//*[@id='s_2_l']"))) 'tablewebelement
            End If
            If drive.FindElement(By.XPath("/html/body/div[1]/div/div[5]/div/div[6]/div/div[1]/div/div[3]/div[1]/div/div/form/span/div/div[2]/div/div/div[3]/div[3]/div/table/tbody/tr[2]/td[1]/span")).Text.Length > 1 Then ' verifica se a tablea tem conteúdo

obterlinhas:
                Try
                    ListaDeLinhas = FuncoesUteis.PrepararContasParaInserirNaTabela(LinhasDataTable, drive.FindElement(By.XPath("//*[@id='s_2_l']"))) 'tablewebelement
                Catch ex As StaleElementReferenceException
                    Console.WriteLine(ex.Message)
                    Console.WriteLine("Nova Tentativa de stale às " + Now.TimeOfDay.ToString)
                    Stop
                    GoTo obterlinhas


                Catch ex As WebDriverTimeoutException
                    Console.WriteLine("Nova Tentativa de timout às " + Now.TimeOfDay.ToString)
                        GoTo obterlinhas
                    End Try
                    linhaEncontrada = True
                End If

                If ListaDeLinhas IsNot Nothing Then
                If ListaDeLinhas.Count > 0 Then
                    For Each linha In ListaDeLinhas
                        If Join(linha.ItemArray).Length < 100 Then
                            Continue For
                        Else
                            LinhasDataTable.Rows.Add(linha)

                        End If
                    Next
                End If

            End If

            ThreadsActivity = Now
            linhaReferencia = drive.FindElement(By.XPath("//*[@id='1_s_2_l_Service_Id']"))

            If linhaEncontrada Then Console.WriteLine("Conjunto de Linhas encontradas às " + Now.TimeOfDay.ToString)

        Loop While TemMaisContas("//*[@id='s_2_rc']", "//*[@id='last_pager_s_2_l']", linhaReferencia)

        For i = 0 To LinhasDataTable.Rows.Count - 1
            LinhaGravar = New LINHA2(LinhasDataTable.Rows(i), EMPRESA.CNPJ)

            crawler.AdicionarLinhas(LinhaGravar)
        Next

        Console.WriteLine(LinhasDataTable.Rows.Count.ToString + " linha(s) Encontrada(s)")

        Return LinhasDataTable

    End Function

    Private Function EncontrarPrimeiroPedidoConcluído() As String

        Dim maximoPedidos As Integer
        Dim ProximaContaXPath, NumProximaconta As String

        Try
            Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//*[@id='s_1_1_18_0_Ctrl']"), "Pesquisar"))
        Catch ex As Exception
            Stop
        End Try



        If FuncoesUteis.ExpandirSeExpancivel("//*[@id='s_1_rc']", "/html/body/div[1]/div/div[5]/div/div[6]/div/div[1]/div/div[3]/div[1]/div/div/form/span/div/div[1]/table/tbody/tr/td[13]/span/a/img", drive) Then
            Try
                Dim conta11 As IWebElement = Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='11_s_1_l_Order_Number']"))) ' faz ele esperar pela expansão acontecer.
            Catch ex As WebDriverTimeoutException
                Console.WriteLine("falha ao expandri lista de pedidos")
            End Try


        End If

        Do

            maximoPedidos = DefinirLimiteDolooping("//*[@id='s_1_rc']")

            For i = 1 To maximoPedidos

                Try
Checarpedidos:
                    Dim pedido As String = Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='" + i.ToString + "_s_1_l_Status']"))).Text
                    If UCase(pedido) Like "*CONCLU*" Then ' verifica o texto da próxima conta
                        ProximaContaXPath = "//*[@id='" + i.ToString + "_s_1_l_Order_Number']/a"
                        NumProximaconta = drive.FindElement(By.XPath(ProximaContaXPath)).Text
                        drive.FindElement(By.XPath(ProximaContaXPath)).Click() ' entrar na PrimeiraConta
                        Return NumProximaconta

                        Exit Function

                    End If

                Catch ex As ElementNotInteractableException
                    GoTo Checarpedidos
                Catch ex As Exception

                    Stop
                End Try

            Next
            FuncoesUteis.InformarFuncionamento()
        Loop While TemMaisContas("//*[@id='s_1_rc']", "//*[@id='last_pager_s_2_l']/span")

        Return ""


    End Function

    Private Function TemMaisContas2(ContaAtual As String) As Boolean
        Dim maximoPedidosInt, maximoContasInt2 As Integer
        Dim ProximaConta2txt, ProximoPedidolink As String
        Dim linhaReferencia As IWebElement

        For w = 1 To 3

            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='s_1_rc']")))
            maximoPedidosInt = DefinirLimiteDolooping("//*[@id='s_1_rc']")

            For i = 1 To maximoPedidosInt

                Dim pedido As String = Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='" + i.ToString + "_s_1_l_Order_Number']/a"))).Text

                If pedido = ContaAtual _
                    And UCase(drive.FindElement(By.XPath("//*[@id='" + i.ToString + "_s_1_l_Status']")).Text) Like "*CONCLU*" Then 'verifica se a proxima conta tem o número da conta atual pra começar o looping

                    Do
                        Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='s_1_rc']")))
                        maximoContasInt2 = DefinirLimiteDolooping("//*[@id='s_1_rc']")

                        For u = (i + 1) To maximoContasInt2

                            If drive.FindElement(By.XPath("//*[@id='" + u.ToString + "_s_1_l_Order_Number']/a")).Text = ContaAtual Then
                                Continue For
                            End If

                            If UCase(drive.FindElement(By.XPath("//*[@id='" + u.ToString + "_s_1_l_Status']")).Text) Like "*CONCLU*" Then ' checa se o texto da proxima conta é ativo

                                ProximoPedidolink = "//*[@id='" + u.ToString + "_s_1_l_Order_Number']/a"
                                Me.PedidoAtual = drive.FindElement(By.XPath(ProximoPedidolink)).Text ' armazena no texto da proxima conta na conta atual
                                drive.FindElement(By.XPath(ProximoPedidolink)).Click() ' clica na próxima conta



                                Return True
                            End If
                        Next
                        linhaReferencia = drive.FindElement(By.XPath("//*[@id='1_s_1_l_Order_Number']"))
                        FuncoesUteis.InformarFuncionamento()
                    Loop While TemMaisContas("//*[@id='s_1_rc']", "/html/body/div[1]/div/div[5]/div/div[6]/div/div[1]/div/div[3]/div[1]/div/div/form/span/div/div[2]/div/div/div[5]/div/table/tbody/tr/td[2]/table/tbody/tr/td[7]/span", linhaReferencia)
                End If
            Next

        Next

        Return False

    End Function

    Private Function TemMaisContas(PathDoSinalizador As String, Optional PathDoProximoBtn As String = Nothing, Optional ByRef linhaReferencia As IWebElement = Nothing) As Boolean

        Dim temMais As Boolean


        Dim registrostxt As String = drive.FindElement(By.XPath(PathDoSinalizador)).Text
        Dim InicialFinalTotal() = FuncoesUteis.TratarInformacaoDeRegistros(registrostxt)


        If (InicialFinalTotal(1) < InicialFinalTotal(2)) Then ' garante que não está expandido ainda.
            drive.FindElement(By.XPath(PathDoProximoBtn)).Click() ' Clicar no Btn Mais
            If linhaReferencia IsNot Nothing Then
                Wait.Until(ExpectedConditions.StalenessOf(linhaReferencia))
                linhaReferencia = Nothing
            End If
            Return True

        Else
            Return False
        End If

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


