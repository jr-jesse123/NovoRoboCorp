Imports CrawlerCorp.CrawlerClass
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Support.UI
Imports SeleniumExtras.WaitHelpers

Public Class PaginaDoCliente
    Public crawler As Crawler
    Public drive As IWebDriver
    Public empresa As EMPRESA
    Public Wait As WebDriverWait

    Sub New(_crawler)
        Me.crawler = _crawler
        Me.drive = _crawler.Drive
        Me.empresa = _crawler.Empresa
        Me.Wait = _crawler.Wait
    End Sub



    Private Sub EnriquecerEmpresa()

        Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("s_3_1_4_0_Label")))

        empresa.Nome = drive.FindElement(By.Id("s_3_1_4_0_Label")).Text

        Console.WriteLine($"O nome da empresa é {empresa.Nome}") ' verificar se a empresa do crawler é atualizado
        crawler.Empresa = empresa

    End Sub

    Friend Sub EnriquecerCliente()

        EnriquecerEmpresa()

    End Sub
    Private Function TemMaisenderecos(PathDoSinalizador As String, Optional PathDoProximoBtn As String = Nothing) As Boolean
        Dim TemMais As Boolean

        TemMais = drive.FindElement(By.XPath(PathDoSinalizador)).Text Like "*+"

        For w = 1 To 3

            If PathDoProximoBtn Is Nothing Or TemMais = False Then

            Else

                Try

                    Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PathDoProximoBtn)))
                    Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(PathDoProximoBtn)))
                    drive.FindElement(By.XPath(PathDoProximoBtn)).Click()

                Catch ex As Threading.ThreadAbortException
                    Throw

                Catch ex As Exception
                    FuncoesUteis.debug(ex)

                    If ex.Message Like "ui-icon ui-icon-seek-end" Then

                    Else
                    End If

                End Try


            End If
            Return TemMais
        Next

        Return TemMais
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

    Sub EnriquecerClienteEnderecos()

        Dim NrDeEnderecos, maximoEnderecos As Integer


            drive.FindElement(By.XPath("//*[@id='s_vctrl_div']")).FindElement(By.LinkText("Endereço do cliente")).Click() 'clica no botão de endereço no menu inferior

            Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//*[@id='s_5_1_9_0_Ctrl']"), "Pesquisar"))

            If FuncoesUteis.ExpandirSeExpancivel("//*[@id='s_5_rc']", "/html/body/div[1]/div/div[5]/div/div[6]/div/div[1]/div/div[3]/div[1]/div/div/form/span/div/div[1]/table/tbody/tr/td[14]/span/a/img", drive) Then
                Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='11']"))) '//*[@id="11"]
            End If


            Try
                Do
                    Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='1']")))
                    Console.WriteLine("Procurando Por Endereços " + NrDeEnderecos.ToString + " Encontrados às " + Now.TimeOfDay.ToString)
                    Try
                        maximoEnderecos = DefinirLimiteDolooping("//*[@id='s_5_rc']")
                    Catch ex As Threading.ThreadAbortException
                        Throw
                    Catch ex As Exception
                        FuncoesUteis.debug(ex)
                    End Try


                    For i = 1 To maximoEnderecos
                        Try
                            NrDeEnderecos = NrDeEnderecos + 1
                            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='" + i.ToString + "_s_5_l_SSA_Primary_Field']")))
                            If drive.FindElement(By.XPath("//*[@id='" + i.ToString + "_s_5_l_SSA_Primary_Field']")).GetAttribute("title") = "Marcada" Then
                                Dim endereço As String = drive.FindElement(By.XPath("//*[@id='" + i.ToString + "']")).Text
                                Console.WriteLine("Endereço principal encontrado")
                                Console.WriteLine(endereço)
                                Dim cep As String = drive.FindElement(By.XPath("//*[@id='" + i.ToString + "_s_5_l_Postal_Code']")).Text
                                empresa.Endereço = empresa.Endereço + "|" + endereço
                                empresa.CEP = cep
                                empresa.Observações = "número de endereços registrado =" + NrDeEnderecos.ToString
                            End If
                        Catch ex As Threading.ThreadAbortException
                            Throw

                        Catch ex As Exception
                            If ex.Message Like "*_s_5_l_SSA_Primary_Field*" Then

                            Else
                                FuncoesUteis.debug(ex)
                            End If

                        End Try

                    Next

                    ThreadsActivity = Now
                Loop While TemMaisenderecos("//*[@id='s_5_rc']", "/html/body/div[1]/div/div[5]/div/div[6]/div/div[1]/div/div[3]/div[1]/div/div/form/span/div/div[2]/div/div/div[5]/div/table/tbody/tr/td[2]/table/tbody/tr/td[7]/span")

            Catch ex As Threading.ThreadAbortException
                Throw
            Catch ex As Exception
                FuncoesUteis.debug(ex)
            End Try
            crawler.nrDeEnderecos = NrDeEnderecos


    End Sub
End Class
