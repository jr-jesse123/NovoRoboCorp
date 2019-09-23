'olha só isso aqui.

Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
'vamo ver
Imports System.Data.Entity.Validation
Imports System.IO
Imports System.Threading
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome
Imports OpenQA.Selenium.Firefox
Imports OpenQA.Selenium.Support.UI
Imports RobôCorp
Imports SeleniumExtras.WaitHelpers

Public Class Crawler
    Public Contextolivre As Boolean = True
    Public threadRestuladosEBuscarCNPJS As Thread = New Thread(AddressOf SalvarResultados)
    Public WithEvents gerenciador As GerenciadorAtividade
    Property Drive As IWebDriver
    Public nrDeEnderecos As Integer
    Public NrDeContas As Integer
    Public Linhas, Linhas2 As New List(Of LINHA)
    Public Empresa, Empresa2 As EMPRESA
    Public empresas, empresas2 As New List(Of EMPRESA)
    Public Gestores, Gestores2 As New List(Of GESTORE)
    Public CNPJ As New CNPJ
    Public socios, socios2 As New List(Of Socio)
    Public Timoutemsegundos As Integer = 600
    Public TempoDeEspera As Integer
    Public ContaAtual As String
    Public NrDeTentativas As String
    Public PaginaDeConsultarPedidos As PaginaDeConsultarPedidos
    Public PaginaDeLogin As PaginaDeLogin
    Public PaginaDePesquisa As PaginaDePesquisa
    Public PaginaDoCliente As PaginaDoCliente
    Public PaginaDosGestores As PaginaDosGestores
    Public PaginaDeConsultarLinhas As PaginaDeConsultarLinhas
    Public PaginaDeConsultarSocios As PaginaDeConsultarSocios
    Public options As New FirefoxOptions()
    Public Timeout As New TimeSpan(0, 0, 1)
    Public Wait As WebDriverWait

    Sub New()

        IniciarNavegador()
        Wait = New WebDriverWait(Drive, New TimeSpan(0, 0, 59))
        PaginaDeLogin = New PaginaDeLogin(Me)

    End Sub


    Sub EnriquecerCNPJS()
        Do

            PaginaDePesquisa = New PaginaDePesquisa(Me)

            PaginaDePesquisa.LocalizarProximoCnpj()

            Try
                ObterInformacoes()
            Catch EX As Exception
                CNPJ.ENRIQUECIDO = 3
                FuncoesUteis.debug(EX)
            End Try


            PaginaDePesquisa.CNPJ.ENRIQUECIDO = 1
            FilaParaSalvarInformacoes()
        Loop
    End Sub

    Private Sub FilaParaSalvarInformacoes()

        If Contextolivre And Not threadRestuladosEBuscarCNPJS.IsAlive Then
            threadRestuladosEBuscarCNPJS = New Thread(AddressOf SalvarResultados)
            threadRestuladosEBuscarCNPJS.Start()
        End If

    End Sub

    Private Sub ObterInformacoes()

        PaginaDoCliente = New PaginaDoCliente(Me)
        PaginaDoCliente.EnriquecerCliente()

        PaginaDosGestores = New PaginaDosGestores(Me)
        PaginaDosGestores.EnriquecerGestores()

        PaginaDeConsultarSocios = New PaginaDeConsultarSocios(Me)
        PaginaDeConsultarSocios.EnriquecerSocios()



        PaginaDeConsultarLinhas = New PaginaDeConsultarLinhas(Me)
        PaginaDeConsultarLinhas.EnriquecerContasLinhas()

        If Linhas.Count > 0 Then
            PaginaDeConsultarPedidos = New PaginaDeConsultarPedidos(Me, Linhas)
            PaginaDeConsultarPedidos.VerificarFidelidades()
        End If



        If Linhas.Count >= 200 Then
            GerenciarApis()
        End If



        TransferirEmpresa()


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

        If Linhas.Count >= 200 Then
            apistring = $"https://4dconsultoria.bitrix24.com.br/rest/52/l3mea29nw1b1o21b/crm.lead.add/?fields[TITLE]='{Titulo}'&fields[COMMENTS]='{stringDeGestores + stringDesocios + stringDeNrDeEnderecos + stringDeNrDelinhas}'&fields[UF_CRM_1551207871]=159&fields[STATUS_ID]=New"

            Dim resultado As String = ApiBitrix.RequestDadosWeb(apistring)
            If resultado Like "*result*" Then
                Console.WriteLine("cliente enviao ao bitrix")
            End If
        End If

        For Each linha In Linhas
            stringDeLinhas = stringDeLinhas + linha.NrDaLinha + " " + linha.DataDeExpiracao + " " + " " + linha.TipoDeplano + "<br>"
        Next


    End Function

    Private Sub SalvarResultados()
        Contextolivre = False

        Console.WriteLine("salvando resultados")

        Using context As New CrawlerContext

            TransferirEntidades(context)

            FuncoesUteisDAO.ReabastecerCNPJS(context)

        End Using

        FuncoesUteisDAO.sanitizarBD()

        Contextolivre = True
    End Sub



    Private Sub TransferirEntidades(context As CrawlerContext)

        Linhas2.AddRange(Linhas)

        For Each linha In Linhas2
            context.LINHAS.Add(linha)
            Linhas.Remove(linha)
        Next

        Linhas2.Clear()

        Gestores2.AddRange(Gestores)
        For Each gestor In Gestores2
            context.GESTORES.Add(gestor)
            Gestores.Remove(gestor)
        Next
        Gestores2.Clear()

        socios2.AddRange(socios)
        For Each socio In socios2
            context.Socios.Add(socio)
            socios.Remove(socio)
        Next
        socios2.Clear()

        empresas2.AddRange(empresas)

        For Each x In empresas2
            context.EMPRESAS.Add(x)
            empresas.Remove(x)
        Next

        empresas2.Clear()

        Try
            context.SaveChanges()
            Console.WriteLine("Entidades enviadas para o Banco de Dados")
        Catch ex As Validation.DbEntityValidationException
            FuncoesUteisDAO.TratarErroDBentity(ex, context)
        End Try

    End Sub

    Public Sub TransferirEmpresa()


        If Empresa.Nome IsNot Nothing Then
            empresas.Add(FuncoesUteis.ObjectCopy(Empresa))
        End If

        Empresa = Nothing


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


    Sub AdicionarLinhas(linha As LINHA)
        Dim unico As Boolean = True

        For Each x In Linhas

            If x.NrDaLinha = linha.NrDaLinha Then
                unico = False
            End If
        Next
        If unico Then Linhas.Add(linha)

    End Sub

    Sub AdicionarEmpresas(empresa As EMPRESA)
        Dim unico As Boolean = True

        If empresa.Nome IsNot Nothing Then
            Me.Empresa = empresa
        End If


    End Sub

    Sub AdicionarGestores(gestor As GESTORE)
        Dim unico As Boolean = True

        For Each x In Gestores

            If x.CPF = gestor.CPF Then
                unico = False
            End If
        Next
        If unico Then Gestores.Add(gestor)

    End Sub

    Sub AdicionarSocios(socio As Socio)
        Dim unico As Boolean = True
        For Each x In socios

            If x.CPFOUCNPJ = socio.CPFOUCNPJ Then
                unico = False
            End If
        Next
        If unico Then socios.Add(socio)
    End Sub



    Sub contador(tentativasMax As Integer, Optional ex As Exception = Nothing)
        NrDeTentativas = NrDeTentativas + 1
        Threading.Thread.Sleep(1000)
        If NrDeTentativas > tentativasMax Then

            Throw New Exception("tentativas excedidas", ex)
        End If

    End Sub


    Sub IniciarNavegador()

        Dim ProcessosAnteriores As Process() = Process.GetProcessesByName("FireFox")

        Try
            Drive.Quit()
            Drive.Close()
        Catch ex As Threading.ThreadAbortException

            Throw
        Catch ex As Exception

        End Try
        options.AddArgument("--headless")
        Drive = New FirefoxDriver(options)
        Drive.Manage.Timeouts.ImplicitWait() = Timeout

        Dim ProcessosPosteriores As Process() = Process.GetProcessesByName("FireFox")

        For Each processo As Process In ProcessosPosteriores

            Dim ProcessoTeste = ProcessosAnteriores.Where(Function(p) p.Id = processo.Id)

            If ProcessoTeste.Count = 0 Then
                ProcessosDoNavegador.Add(processo)
            End If

        Next

    End Sub

    Protected Overrides Sub Finalize()

    End Sub
End Class
