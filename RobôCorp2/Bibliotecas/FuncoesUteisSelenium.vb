Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Support.UI
Imports RobôCorp
Imports SeleniumExtras.WaitHelpers


Public Class FuncoesUteis

    Shared Function ChecarPresenca(ByRef drive As IWebDriver, Optional xpath As String = "", Optional ClassName As String = "",
                               Optional id As String = "", Optional texto As String = "",
                               Optional PartialText As String = "") As Boolean
        Dim by As By

        If xpath.Length > 0 Then by = By.XPath(xpath)
        If ClassName.Length > 0 Then by = By.ClassName(ClassName)
        If id.Length > 0 Then by = By.Id(id)
        If texto.Length > 0 Then by = By.LinkText(texto)
        If PartialText.Length > 0 Then by = By.PartialLinkText(PartialText)

        Try
            drive.FindElement(by)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function


    Shared Function AguardarPor1(ByRef drive As IWebDriver, timeoutEmSegundos As Integer, Optional xpath As String = "", Optional ClassName As String = "",
                                   Optional id As String = "", Optional texto As String = "",
                                   Optional PartialText As String = "") As Boolean
        Dim by As By

        If Not xpath Is Nothing Then by = By.XPath(xpath)
        If Not ClassName Is Nothing Then by = By.ClassName(ClassName)
        If Not id Is Nothing Then by = By.Id(id)
        If Not texto Is Nothing Then by = By.LinkText(texto)
        If Not PartialText Is Nothing Then by = By.PartialLinkText(PartialText)

        Dim encontrado As Boolean
        Dim tempoEsperado As Integer
        Dim tempoDeEspera As Integer = 250


        Do
            Try
                encontrado = drive.FindElement(by).Displayed
            Catch ex As Exception
            End Try
            Threading.Thread.Sleep(tempoDeEspera)
            tempoEsperado = +tempoDeEspera

        Loop While Not (encontrado Or tempoEsperado < timeoutEmSegundos)

        Return encontrado
    End Function

    Friend Shared Function ConstruirDatatable(tabelaWEbElement As IWebElement) As DataTable ' adicionar: ParamArray colunas As Integer()
        Dim datatable As New DataTable
        Dim linha As IWebElement
        Dim Colunas As IReadOnlyList(Of IWebElement)

        Try
Constuir:
            linha = tabelaWEbElement.FindElement(By.TagName("tr"))
            Colunas = linha.FindElements(By.TagName("td"))

            For Each coluna In Colunas

                datatable.Columns.Add(coluna.Text)

            Next

        Catch ex As StaleElementReferenceException
            GoTo Constuir
        End Try

        Return datatable
    End Function

    Friend Shared Function ChecarErroPrivilegios(drive As IWebDriver)

        Try
            If drive.FindElement(By.XPath("/html/body/div[1]/div/div[5]/div/div[6]/div/div[1]/div/table/tbody/tr/td/table[3]/tbody/tr/td/table/tbody/tr/td/table/tbody/tr[2]/td")).Text _
                    = "Você não tem os privilégios necessários para visualizar informações detalhadas para este registro ou essas informações foram recentemente excluídas.(SBL-DAT-00309)" & vbCrLf & " " Then
                Return True
            Else
                Return False
            End If

        Catch ex2 As Exception

        End Try


    End Function

    Shared Function EsperarClicavel(ElementXpath As String, drive As IWebDriver, Optional timeoutEmSegundos As Integer = 60) As Boolean


        Dim clicado As Boolean
        Dim tempoEsperado As TimeSpan
        Dim tempoDeEspera As TimeSpan
        tempoDeEspera.Add(TimeSpan.FromMilliseconds(250))

        Do
            Try
                drive.FindElement(By.XPath(ElementXpath)).Click()
                clicado = True
            Catch ex As Exception
                Err.Clear()
            Finally
                Threading.Thread.Sleep(tempoDeEspera)
                tempoEsperado.Add(tempoDeEspera)
            End Try
        Loop While Not (clicado Or tempoEsperado.Seconds > timeoutEmSegundos)

        Return clicado
    End Function

    '    Function checarPresenca(drive As IWebDriver, xPath As String, Optional metodo As Integer = 1)


    '        'se tiver erro ao encontrar o objeto já retorna falso
    '        On Error GoTo objeto_nao_encontrado
    '        Dim ele As IWebElement = drive.FindElement(By.XPath(xPath))

    '        Select Case metodo

    '            Case Is = 1
    '                If ele.Displayed Then
    '                    Return True
    '                Else
    '                    Return False
    '                End If

    '                Exit Function

    '            Case Is = 2

    '                If ele Is Nothing Then
    '                    Return False
    '                Else
    '                    Return True
    '                End If
    '                Exit Function

    '            Case Is = 3
    '                If ele.Enabled Then
    '                    Return True
    '                Else
    '                    Return False
    '                End If
    '                Exit Function
    '        End Select

    'objeto_nao_encontrado:
    '        Return False

    '        Exit Function
    '        '*******************

    '    End Function


    Shared Sub AguardarClicarPorTexto(BtnTexto As String, timeOutEmSegundos As Integer, drive As IWebDriver) ' função duplicada 
        Dim Clicado As Boolean
        Dim TempoDeEspera As Integer
        Do Until Clicado


            Try

                drive.FindElement(By.LinkText(BtnTexto)).Click()
                Clicado = True
                TempoDeEspera = 0
            Catch EX As Exception
                If TempoDeEspera < timeOutEmSegundos Then
                    Clicado = False
                    Threading.Thread.Sleep(1000)
                    TempoDeEspera = TempoDeEspera + 1
                Else
                    Throw
                End If

            End Try
        Loop
    End Sub

    Shared Sub AguardarClicarPorXpath(BtnTexto As String, Timeout As Integer, drive As IWebDriver)
        Dim Clicado As Boolean
        Dim TempoDeEspera As Integer
        Do Until Clicado
            Try
                drive.FindElement(By.XPath(BtnTexto)).Click()
                Clicado = True
                TempoDeEspera = 0
            Catch ex As Exception
                If TempoDeEspera < Timeout Then
                    Clicado = False
                    Threading.Thread.Sleep(1000)
                    TempoDeEspera = TempoDeEspera + 1

                Else
                    Throw New Exception("Não conseguiu clicar")
                End If

            End Try
        Loop
    End Sub
    '    Shared Function AguardarPor(seletor As Integer, identificador As String, drive As IWebDriver, Optional timeoutemsegundos As Integer = 60)

    '        Dim parametroByXpath, parametroByClassName, parametroById, parametroByText, parametroByPartialText As String

    '        Select Case seletor
    '            Case 1
    '                parametroByXpath = identificador
    '            Case 2
    '                parametroByClassName = identificador
    '            Case 3
    '                parametroById = identificador
    '            Case 4
    '                parametroByText = identificador
    '            Case 5
    '                parametroByPartialText = identificador
    '        End Select

    '#Disable Warning BC42104 ' A variável é usada antes de receber um valor
    '        Dim resultado As Boolean =
    '        FuncoesUteis.AguardarPor1(drive, timeoutemsegundos, parametroByXpath, parametroByClassName, parametroById,
    '                                 parametroByText, parametroByPartialText)
    '#Enable Warning BC42104 ' A variável é usada antes de receber um valor
    '        Return resultado

    '    End Function

    Shared Function PrepararContasParaInserirNaTabela(Contas As DataTable, tabelaWEbElement As IWebElement, ParamArray colunas As Integer()) _
        As List(Of DataRow)


        Dim ListaContas As New List(Of DataRow)

        Dim webrows As IReadOnlyList(Of IWebElement) = tabelaWEbElement.FindElements(By.TagName("tr"))

        For Each webrow In webrows

            Dim conta As DataRow = Contas.NewRow
            Dim td As IReadOnlyList(Of IWebElement) = webrow.FindElements(By.TagName("td"))

            For i = 0 To (td.Count - 1)
                'Console.WriteLine(i.ToString + " " + td(i).Text)
                If colunas.Contains(i) Then
                    Try
                        If i = 1 Then
                            If td(i).GetAttribute("title") = "Y" Or td(i).GetAttribute("title") = "Marcada" Then
                                conta(i) = "Principal"
                            Else
                                conta(i) = td(i).Text
                            End If
                        Else
                            conta(i) = td(i).Text
                        End If

                    Catch ex As StaleElementReferenceException
                        conta(i) = "Erro na importação desta infomação"
                        Console.WriteLine("Erro de stale no elemento: " + td(i).GetAttribute("name"))
                    End Try
                End If
            Next

            ListaContas.Add(conta)
        Next

        Return ListaContas
    End Function

    Shared Function ExpandirSeExpancivel(PathDoSinalizador As String, PathDoBotao As String, drive As IWebDriver) As Boolean

        Dim registros As IWebElement

        Dim TemMais As Boolean = drive.FindElement(By.XPath(PathDoSinalizador)).Text Like "*+"

        Dim registrosok As Boolean = True
        If TemMais Then

            Dim registrostxt = drive.FindElement(By.XPath(PathDoSinalizador)).Text ' mesma fonte de Temmais

            Dim registrotentativas As Integer = 0
            Do
                If registrostxt = "Registro 1 de 1+" Or registrostxt = "Sem registros" Then
                    registrosok = False
                Else
                    registrosok = True
                End If
                registros = drive.FindElement(By.XPath(PathDoSinalizador))

                If registrotentativas > 10 Then Return False
                registrotentativas = registrotentativas + 1
            Loop Until registrosok

            Dim InicialFinal() = TratarInformacaoDeRegistros(registrostxt)


            If (InicialFinal(1) - InicialFinal(0) < 10) Then
                drive.FindElement(By.XPath(PathDoBotao)).Click() ' Clicar no Btn Mais


                ' VERIFICA SE AUMENTOU O NÚMERO DE REGISTROS
                Dim novoregistrotxt = drive.FindElement(By.XPath(PathDoSinalizador)).Text

                InicialFinal = TratarInformacaoDeRegistros(novoregistrotxt)

                If (InicialFinal(1) - InicialFinal(0) <= 10) Then
                    Return False
                Else
                    Return True
                End If

            Else
                Return False
            End If
        End If

        Return False
    End Function

    Public Shared Function TratarInformacaoDeRegistros(registrostxt As String) As Integer()

        Dim registrosarray() = Split(registrostxt, "-")
        registrosarray(0) = Replace(registrosarray(0), "Registros ", "")
        registrosarray(1) = Replace(registrosarray(1), " de", "")
        registrosarray(1) = Replace(registrosarray(1), "+", "")
        Dim registros2() = Split(registrosarray(1), " ")

        Dim registro1 = Integer.Parse(registrosarray(0))
        Dim registro2 = Integer.Parse(registros2(1))

        Dim InicialFinal() = {registro1, registro2}


        Return InicialFinal

    End Function

    Public Shared Sub Detach(context As CrawlerContext, y As Object)

        For Each dbEntityEntry As DbEntityEntry In context.ChangeTracker.Entries
            If dbEntityEntry.Entity IsNot Nothing And dbEntityEntry.Entity.Equals(y) Then
                dbEntityEntry.State = EntityState.Detached
            End If

        Next
    End Sub

    Public Shared Sub DetachAll(context As CrawlerContext)

        For Each dbEntityEntry As DbEntityEntry In context.ChangeTracker.Entries
            If dbEntityEntry.Entity.GetType.ToString = "RobôCorp.CNPJ" Then Continue For
            If dbEntityEntry.Entity IsNot Nothing Then
                dbEntityEntry.State = EntityState.Detached
            End If


        Next
    End Sub

    Public Shared Sub debug(ex As Exception)
        Console.WriteLine(ex.Message)
        Console.WriteLine(ex.StackTrace)
        Console.Beep(600, 4000)
        Stop
        Dim log As String = listaDeCNPJ.First.CNPJS.ToString + vbCrLf + ex.Message + vbCrLf + ex.StackTrace
        GravarLog(log)




    End Sub

    Public Shared Function ContarTentativas(tentativas As Integer, Optional ByRef maximo As Integer = 5)
        If tentativas > maximo Then
            Return True
        Else
            maximo = maximo + 1
            Return False
        End If

    End Function



    Shared Function VerificarNumeroDeInstancias()

        Dim instances As Process()
        instances = Process.GetProcessesByName("RobôCorp")
        Console.WriteLine(instances.Count.ToString + " Instancias encontradas")
        Dim indice As Integer = instances.Count
        If indice < 1 Then indice = 1
        Console.WriteLine("aplicando indice = " + indice.ToString)

        Return indice
    End Function
    Shared Function ObjectCopy(ByVal obj As Object) As Object
        'copies original object to stream then 
        'deserializes that stream and returns the output
        'to create clone (copy) of object

        Dim objMemStream As New MemoryStream(5000)
        Dim objBinaryFormatter As New BinaryFormatter(Nothing,
            New StreamingContext(StreamingContextStates.Clone))

        objBinaryFormatter.Serialize(objMemStream, obj)

        objMemStream.Seek(0, SeekOrigin.Begin)

        ObjectCopy = objBinaryFormatter.Deserialize(objMemStream)

        objMemStream.Close()
    End Function


    Shared Sub GravarLog(lOG As String)

        Dim caminhoDoArquivo As String = Directory.GetCurrentDirectory + "\" + "log.txt"

        Using FluxoDoArquvio As New FileStream(caminhoDoArquivo, FileMode.Append)
            Using escritor = New StreamWriter(FluxoDoArquvio)
                escritor.Write(lOG)
            End Using
        End Using

    End Sub





End Class



