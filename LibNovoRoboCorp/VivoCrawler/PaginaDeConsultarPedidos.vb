Imports CrawlerCorp.CrawlerClass
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Support.UI
Imports RobôCorp.CrawlerClass
Imports SeleniumExtras.WaitHelpers

Public Class PaginaDeConsultarPedidos
    Public drive As IWebDriver
    Public empresa As ClienteVivo
    Public PedidoAtual As String
    Public crawler As Crawler
    Public Wait As WebDriverWait
    Public linhas As List(Of LINHA)
    Public DataDoPedido As Date
    Public fidelidades As IList
    Public fidelizacoes

    Sub New(_crawler As Crawler, _linhas As List(Of LINHA))
        Me.drive = _crawler.Drive
        Me.empresa = _crawler.Empresa
        Me.Wait = WebdriverCt.Wait
        Me.crawler = _crawler
        Me.linhas = _linhas

        'EnriquecerCarteira()
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

    End Sub

    Public Function VerificarFidelidades()
        Dim LinhasDataTable As DataTable
        Dim nrdecontas As Integer = 1
        


            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='s_1_rc']"))) ' aguardar o número de registros aparecer
            Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//*[@id='s_1_1_18_0_Ctrl']"), "Pesquisar")) ' aguardar o número de registros aparecer

            Console.WriteLine("Procurando por Pedidos concluídos Nos últimos 24 Meses")

            If drive.FindElement(By.XPath("//*[@id='s_1_rc']")).Text = "Sem registros" Then
                Exit Function
            End If

            ' ordenar por data

            drive.FindElement(By.XPath("//*[@id='jqgh_s_1_l_Order_Date']")).Click()
            Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='jqgh_s_1_l_Order_Date']")))
            drive.FindElement(By.XPath("//*[@id='jqgh_s_1_l_Order_Date']")).Click()

            '************


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
                        'drive.FindElement(By.XPath(xpath))

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

                    Dim tentativas3 As Integer = 0
                    Try
Enriquecer:
                        LinhasDataTable = EnriquecerFidelidades(LinhasDataTable)
                    Catch ex As StaleElementReferenceException
                        GoTo Enriquecer
                    Catch ex As WebDriverTimeoutException
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
                    Console.WriteLine($"Foram encontradas {LinhasDataTable.Rows.Count} linhas fidelizadas no cnpj {empresa.CNPJ} totalizando {crawler.Linhas.Count.ToString} linhas")
                Else
                    Console.WriteLine($"Não foram encontrados pedidos concluídos nas  {nrdecontas.ToString} contas do cnpj")
                End If

                LinhasDataTable = InverterOdemTabela(LinhasDataTable)
                Try
                    fidelizacoes = LinhasDataTable.AsEnumerable.Distinct(New ComparadorFidelizacoes)
                Catch ex As Exception

                End Try


                ArquivarDataDeFidelidadeDasLinhas()

            Else
                Console.WriteLine("Nenhuma Fidelização Econtrada")

            End If
            nrdecontas = 0


            Exit Function
            Return ""

    End Function

    Private Function InverterOdemTabela(linhasDataTable As DataTable) As DataTable

        Dim novaTabela As DataTable = linhasDataTable.Clone


        Dim x = linhasDataTable.Rows.Count - 1

        For x = (linhasDataTable.Rows.Count - 1) To 0 Step -1



            Try
                Dim copialinha As DataRow = novaTabela.NewRow()
                copialinha.ItemArray = linhasDataTable.Rows(x).ItemArray.Clone

                novaTabela.Rows.Add(COPIALINHA)

            Catch ex As Exception

            End Try

        Next

        Return novaTabela

    End Function

    Public Sub ArquivarDataDeFidelidadeDasLinhas()

        For Each linha In fidelizacoes

            Try
                For Each linharow In crawler.Linhas
                    If linharow.NrDaLinha = linha(26) Then

                        linharow.FidelizadoAte = linha(46)
                    End If
                Next

            Catch ex As Exception
                Stop
            End Try


        Next



    End Sub

    Private Sub AcessarPedidos()



        'Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("/html/body/div[1]/div/div[5]/div/div[6]/div/div[1]/div/div[3]/table/tbody/tr/td/div/table/tbody/tr[1]/td/div[1]/div/div/form/span/div/div[1]/table/tbody/tr/td[1]"), "Holding / Sócios"))


AbrirPedidos:
        Try
            Dim pedidos As IReadOnlyList(Of IWebElement) = drive.FindElements(By.LinkText("Pedidos")) ' PEDISOS SE ELE EXISTIR
            pedidos(1).Click()
            'drive.FindElement(By.Id("//*[@id='s_vctrl_div']")).FindElement(By.LinkText("Pedidos")).Click() ' PEDISOS SE ELE EXISTIR

        Catch ex As StaleElementReferenceException
            GoTo AbrirPedidos
        Catch ex As ArgumentOutOfRangeException
            drive.FindElement(By.XPath("//*[@id='j_s_vctrl_div_tabScreen']")).Click()
            drive.FindElement(By.XPath("/html/body/div[1]/div/div[5]/div/div[6]/div/div[1]/div/div[2]/div[2]/ul/li[11]/select/option[3]")).Click()

        End Try
    End Sub


    Private Function EnriquecerFidelidades(LinhasDataTable As DataTable) As DataTable

        Dim tabelaWebElement As IWebElement
        Dim ListaDeLinhas As List(Of DataRow)
        Dim linhaEncontrada As Boolean
        Dim LinhaGravar As LINHA
        Dim linhaReferencia As IWebElement
        Dim DataPesquisa As Date

        Try
            Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//*[@id='s_2_1_5_0_Ctrl']"), "Replicar Tudo"))
        Catch ex As Exception
            FuncoesUteis.debug(ex)
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

            tabelaWebElement = drive.FindElement(By.XPath("//*[@id='s_2_l']"))

            If LinhasDataTable Is Nothing Then
                LinhasDataTable = FuncoesUteis.ConstruirDatatable(drive.FindElement(By.XPath("//*[@id='s_2_l']"))) 'tablewebelement
            End If
            If drive.FindElement(By.XPath("/html/body/div[1]/div/div[5]/div/div[6]/div/div[1]/div/div[3]/div[1]/div/div/form/span/div/div[2]/div/div/div[3]/div[3]/div/table/tbody/tr[2]/td[1]/span")).Text.Length > 1 Then ' verifica se a tablea tem conteúdo

obterlinhas:
                Try

                    ListaDeLinhas = FuncoesUteis.PrepararContasParaInserirNaTabela(LinhasDataTable, drive.FindElement(By.XPath("//*[@id='s_2_l']")), 26, 46) ' tablewebelement
                Catch ex As StaleElementReferenceException
                    Console.WriteLine(ex.Message)
                    Console.WriteLine("Nova Tentativa de stale às " + Now.TimeOfDay.ToString)
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
                        'inserir data do pedido na coluna correta da linha se estiver em branco
                        If linha(26) Is Nothing Or linha(26).Length < 5 Then Continue For

                        Dim data As Date
                        Try
                            data = linha(46)
                            If data < (Today - New TimeSpan(800, 0, 0, 0)) Then
                                data = DataDoPedido.ToShortDateString

                            Else
                                linha(46) = DataDoPedido.ToShortDateString
                            End If
                        Catch
                            data = DataDoPedido.ToShortDateString
                            linha(46) = DataDoPedido.ToShortDateString
                        End Try

                        LinhasDataTable.Rows.Add(linha)

                    Next
                End If

            End If

            ThreadsActivity = Now
            linhaReferencia = drive.FindElement(By.XPath("//*[@id='1_s_2_l_Service_Id']"))

            If linhaEncontrada Then Console.WriteLine("Pesquisando Pedidos de " + DataDoPedido.ToShortDateString + " às " + Now.TimeOfDay.ToString)

        Loop While TemMaisContas("//*[@id='s_2_rc']", "//*[@id='last_pager_s_2_l']", linhaReferencia)


        Console.WriteLine((ListaDeLinhas.Count - 1).ToString + " fidelização(ões) Encontrada(s)")

        Return LinhasDataTable

    End Function

    Private Function EncontrarPrimeiroPedidoConcluído() As String
        
        Dim maximoPedidos As Integer
        Dim ProximaContaXPath, NumProximaconta As String

        Try
            Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//*[@id='s_1_1_18_0_Ctrl']"), "Pesquisar"))
        Catch ex As Exception
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

                        DataDoPedido = drive.FindElement(By.XPath("//*[@id='" + i.ToString + "_s_1_l_Order_Date']")).Text
                        If DataDoPedido + TimeSpan.FromDays(735) < Today Then
                            Return ""
                        End If


                        ProximaContaXPath = "//*[@id='" + i.ToString + "_s_1_l_Order_Number']/a"
                        NumProximaconta = drive.FindElement(By.XPath(ProximaContaXPath)).Text
                        drive.FindElement(By.XPath(ProximaContaXPath)).Click() ' entrar na PrimeiraConta
                        Return NumProximaconta

                        Exit Function

                    End If

                Catch ex As ElementNotInteractableException
                    GoTo Checarpedidos

                Catch ex As StaleElementReferenceException
                    GoTo Checarpedidos

                Catch ex As Exception
                    FuncoesUteis.debug(ex)
                End Try


            Next
            ThreadsActivity = Now
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

                                DataDoPedido = drive.FindElement(By.XPath("//*[@id='" + u.ToString + "_s_1_l_Order_Date']")).Text
                                If DataDoPedido + TimeSpan.FromDays(702) < Today Then
                                    Exit Function
                                End If


                                ProximoPedidolink = "//*[@id='" + u.ToString + "_s_1_l_Order_Number']/a"
                                Me.PedidoAtual = drive.FindElement(By.XPath(ProximoPedidolink)).Text ' armazena no texto da proxima conta na conta atual
                                drive.FindElement(By.XPath(ProximoPedidolink)).Click() ' clica na próxima conta



                                Return True
                            End If
                        Next
                        linhaReferencia = drive.FindElement(By.XPath("//*[@id='1_s_1_l_Order_Number']"))
                        ThreadsActivity = Now
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

        Try
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
        Catch ex As Exception
            Return False
        End Try




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


