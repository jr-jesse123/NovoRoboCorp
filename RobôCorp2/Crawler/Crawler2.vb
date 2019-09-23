Imports System.Data.Entity
Imports System.IO
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome
Imports OpenQA.Selenium.Firefox
Imports OpenQA.Selenium.Support.UI
Imports SeleniumExtras.WaitHelpers



Public Class Crawler2
    Property Drive As IWebDriver
    Public nrDeEnderecos As Integer
    Public NrDeContas As Integer
    Public Linhas As New List(Of LINHA2)
    Public Empresa As Empresa_Sem_linhas_Cadastrada2
    Public Gestores As New List(Of GESTORE2)
    Public CNPJ As New CNPJ2
    Public socios As New List(Of Socio2)
    Public Timoutemsegundos As Integer = 600
    Public TempoDeEspera As Integer
    Public ContaAtual As String
    Public NrDeTentativas As String
    Public PaginaDeLogin As PaginaDeLogin2
    Public PaginaDePesquisa As PaginaDePesquisa2

    Public PaginaDeConsultarPedidos As PaginaDeConsultarPedidos2
    Public Timeout As New TimeSpan(0, 0, 1)
    Public context As DbContext
    Public Wait As WebDriverWait
    Public options As New FirefoxOptions()

    Sub New()

        IniciarNavegador()
        Wait = New WebDriverWait(Drive, New TimeSpan(0, 0, 59))


        PaginaDeLogin = New PaginaDeLogin2(Me)

        Try
            PaginaDeLogin.Login()
        Catch ex As Threading.ThreadAbortException
            Stop
            Throw
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Stop
        End Try



    End Sub


    Friend Sub EnriquecerCNPJ(ProximaEmpresa As Empresa_Sem_linhas_Cadastrada2, _context As CrawlerContext)
        Me.context = _context
        Me.Empresa = ProximaEmpresa

        Try
            PaginaDePesquisa = New PaginaDePesquisa2(Me)
        Catch ex As Threading.ThreadAbortException
            Stop
            Throw
        Catch ex As Exception
            PaginaDePesquisa = New PaginaDePesquisa2(Me)
            
        End Try


        Console.WriteLine("Vivo Corp conectado, iniciando pesquisa de CNPJS às " + Now.TimeOfDay.ToString)
        If PaginaDePesquisa.VerificarRetornoDaconsultaCnpj Then


            PaginaDeConsultarPedidos = New PaginaDeConsultarPedidos2(Me)
            Try
enriquecerpedidos:
                PaginaDeConsultarPedidos.EnriquecerPedidos()

            Catch ex As Threading.ThreadAbortException
                Stop
                Throw
            Catch ex As StaleElementReferenceException
                GoTo enriquecerpedidos

            Catch ex As Exception
               Stop
                Throw
            End Try


        End If



        AdicionarInformacoesAoContexto(_context)


        Console.WriteLine("Informações adicionadas ao Banco de dados")

        Try
            'GerenciarApis()
        Catch ex As Threading.ThreadAbortException
            Stop
            Throw
        Catch ex As Exception
            Throw
        End Try




        LimparDados()
    End Sub

    Private Function GerenciarApis() As Boolean


        Dim Titulo As String = Empresa.Nome
        Dim stringDeObservacoes As String = Empresa.Observações + "<br>"
        Dim stringDeNrDeEnderecos As String = Empresa.Endereço + "<br>"
        Dim stringDeNrDelinhas As String = $"Nr de Linhas:  {Linhas.Count.ToString} <br>"
        Dim stringDeGestores As String = "Gestores: <br>"
        Dim stringDesocios As String = "Socios:  <br>"
        Dim apistring As String = ""
        Dim stringDeLinhas As String = "linhas: <br>"

        If Empresa.Endereço IsNot Nothing Then
            Dim stringDeEndereco As String = $"Emdereço: {Empresa.Endereço.ToString} <br>"
        Else
            Dim stringDeEndereco As String = "Sem endereços principais cadastrados"
        End If


        For Each gestor In Gestores
            stringDeGestores = stringDeGestores + gestor.Master + " " + gestor.NOME + " " + gestor.TelefoneCelular + " " +
 gestor.TelefoneFixo + " " + gestor.EMAIL + "<br>"
        Next

        For Each socio In socios
            stringDesocios = stringDesocios + socio.NOME + " " + socio.Telefone + " " + "<br>"
        Next

        If Linhas.Count >= 100 Or nrDeEnderecos > 100 Then
            apistring = $"https://4dconsultoria.bitrix24.com.br/rest/52/l3mea29nw1b1o21b/crm.lead.add/?fields[TITLE]='{Titulo}'&fields[COMMENTS]='{stringDeGestores + stringDesocios + stringDeNrDeEnderecos + stringDeNrDelinhas}'&fields[UF_CRM_1551207871]=159&fields[STATUS_ID]=New"

            Dim resultado As String = apiBitrix2.RequestDadosWeb(apistring)
            If resultado Like "*result*" Then
                Console.WriteLine("cliente enviao ao bitrix")
            End If
        End If

        For Each linha In Linhas
            stringDeLinhas = stringDeLinhas + linha.NrDaLinha + " " + linha.DataDeExpiracao + " " + " " + linha.TipoDeplano + "<br>"
        Next


    End Function

    Private Sub AdicionarInformacoesAoContexto(context As CrawlerContext)
        For Each linha In Linhas
            context.LINHAS.Add(linha)
        Next
        For Each gestor In Gestores
            context.GESTORES.Add(gestor)

        Next
        For Each socio In socios
            context.Socios.Add(socio)
        Next

        Dim teste = context.ChangeTracker.Entries.ToList

        FuncoesUteis.DetachEmpresaSemLinhas(context)

        teste = context.ChangeTracker.Entries.ToList

        CNPJ.ENRIQUECIDO = 1
        context.SaveChanges()
    End Sub

    Public Sub LimparDados()

        Linhas.Clear()
        sanitizarBD()



    End Sub


    Private Function ChecarPresenca(seletor As Integer, identificador As String)
        Dim parametroByXpath, parametroByClassName, parametroById, parametroByText, parametroByPartialText As String

        Select Case seletor
            Case 1
                parametroByXpath = identificador
            Case 2
                parametroByClassName = identificador
            Case 3
                parametroById = identificador
            Case 4
                parametroByText = identificador
            Case 5
                parametroByPartialText = identificador

        End Select

        Dim resultado As Boolean = FuncoesUteis.ChecarPresenca(Drive, parametroByXpath, parametroByClassName, parametroById,
                                     parametroByText, parametroByPartialText)
        Return resultado

    End Function


    Sub AdicionarLinhas(linha As LINHA2)
        Dim unico As Boolean = True

        For Each x In Linhas

            If x.NrDaLinha = linha.NrDaLinha Then
                unico = False
            End If
        Next
        If unico Then Linhas.Add(linha)

    End Sub

    Sub AdicionarEmpresas(empresa As Empresa_Sem_linhas_Cadastrada2)
        Dim unico As Boolean = True

        Me.Empresa = empresa


    End Sub

    Sub AdicionarGestores(gestor As GESTORE2)
        Dim unico As Boolean = True

        For Each x In Gestores

            If x.CPF = gestor.CPF Then
                unico = False
            End If
        Next
        If unico Then Gestores.Add(gestor)

    End Sub

    Sub AdicionarSocios(socio As Socio2)
        Dim unico As Boolean = True
        For Each x In socios

            If x.CPFOUCNPJ = socio.CPFOUCNPJ Then
                unico = False
            End If
        Next
        If unico Then socios.Add(socio)
    End Sub

    Sub sanitizarBD()
        Dim conexao As Mysqlcoon


        conexao = New Mysqlcoon
        Dim Sql As String
        Sql = "REPLACE INTO LINHAS_2  SELECT * FROM LINHAS;"
        conexao.SQLCommand(Sql)
        conexao.Close()

        conexao = New Mysqlcoon
        Sql = "TRUNCATE LINHAS;"
        conexao.SQLCommand(Sql)
        conexao.Close()

    End Sub

    Sub contador(tentativasMax As Integer, Optional ex As Exception = Nothing)
        NrDeTentativas = NrDeTentativas + 1
        Threading.Thread.Sleep(1000)
        If NrDeTentativas > tentativasMax Then
            Stop
            Throw New Exception("tentativas excedidas", ex)
        End If

    End Sub


    Sub IniciarNavegador()

        Dim ProcessosAnteriores As Process() = Process.GetProcessesByName("FireFox")

        Try
            Drive.Quit()
            Drive.Close()
        Catch ex As Threading.ThreadAbortException
            Stop
            Throw
        Catch ex As Exception

        End Try
        options.AddArgument("--headless")
        If System.Diagnostics.Debugger.IsAttached Then
            Drive = New FirefoxDriver()
        Else
            Drive = New FirefoxDriver(options)
        End If


        Drive.Manage.Timeouts.ImplicitWait() = Timeout


        Dim ProcessosPosteriores As Process() = Process.GetProcessesByName("FireFox")

        For Each processo As Process In ProcessosPosteriores

            Dim ProcessoTeste = ProcessosAnteriores.Where(Function(p) p.Id = processo.Id)

            If ProcessoTeste.Count = 0 Then
                ProcessosDoNavegador.Add(processo)
            End If

        Next

    End Sub
End Class
