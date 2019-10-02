
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Threading
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome

Public Class Crawler
    Public Contextolivre As Boolean = True
    Public threadRestuladosEBuscarCNPJS As Thread = New Thread(AddressOf SalvarResultados)
    Property Drive As ChromeDriver = WebdriverCt.Driver


    Public Linhas, Linhas2 As New List(Of LINHA)
    Public Empresa, Empresa2 As ClienteVivo
    Public empresas, empresas2 As New List(Of ClienteVivo)
    Public Gestores, Gestores2 As New List(Of GESTOR)
    Public CNPJ As New CNPJ
    Public socios, socios2 As New List(Of SociosReceita)

    Sub EnriquecerCNPJS()
        Do

            Dim PaginaDepesquisa As New PaginaDePesquisa()
            PaginaDepesquisa.LocalizarProximoCnpj()

            Try
                ObterInformacoes()
            Catch EX As Exception
                CNPJ.ENRIQUECIDO = 3
                FuncoesUteis.debug(EX)
            End Try


            PaginaDepesquisa.CNPJ.ENRIQUECIDO = 1
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

        Dim PaginaDoCliente = New PaginaDoCliente(Me)
        PaginaDoCliente.EnriquecerCliente()

        Dim PaginaDosGestores = New PaginaDosGestores(Me)
        PaginaDosGestores.EnriquecerGestores()

        Dim PaginaDeConsultarSocios = New PaginaDeConsultarSocios(Me)
        PaginaDeConsultarSocios.EnriquecerSocios()

        Dim PaginaDeConsultarLinhas = New PaginaDeConsultarLinhas(Me)
        PaginaDeConsultarLinhas.EnriquecerContasLinhas()

        If Linhas.Count > 0 Then
            Dim PaginaDeConsultarPedidos = New PaginaDeConsultarPedidos(Me, Linhas)
            PaginaDeConsultarPedidos.VerificarFidelidades()
        End If

        If Linhas.Count >= 200 Then
            Dim GerenciardorDeApis = New GerenciadorDeApis(Linhas, Empresa, Gestores, socios)
            GerenciardorDeApis.EnviarInformacoes()

        End If

        TransferirEmpresa()

    End Sub


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
            'context.LINHAS.Add(linha)
            Linhas.Remove(linha)
        Next

        Linhas2.Clear()

        Gestores2.AddRange(Gestores)
        For Each gestor In Gestores2
            'context.GESTORES.Add(gestor)
            Gestores.Remove(gestor)
        Next
        Gestores2.Clear()

        socios2.AddRange(socios)
        For Each socio In socios2
            'context.SOCIOS.Add(socio)
            socios.Remove(socio)
        Next
        socios2.Clear()

        empresas2.AddRange(empresas)

        For Each x In empresas2
            '  context.Empresas.Add(x)
            empresas.Remove(x)
        Next

        empresas2.Clear()

        Try
            context.SaveChanges()
            Console.WriteLine("Entidades enviadas para o Banco de Dados")
        Catch ex As Validation.DbEntityValidationException
            FuncoesUteisDAO.TratarErroDBentity(ex, context)
        Catch ex As DbUpdateException
            Stop
            Dim x = ex.TargetSite
        End Try

    End Sub

    Public Sub TransferirEmpresa()


        If Empresa IsNot Nothing Then
            empresas.Add(FuncoesUteis.ObjectCopy(Empresa))
        End If

        Empresa = Nothing


    End Sub

    Sub AdicionarLinhas(linha As LINHA)
        Dim unico As Boolean = True

        For Each x In Linhas

            If x.NrDaLinha = linha.NrDaLinha Then
                unico = False
            End If
        Next
        If unico Then Linhas.Add(linha)

    End Sub

    Sub AdicionarEmpresas(empresa As ClienteVivo)
        Dim unico As Boolean = True

        If empresa IsNot Nothing Then
            Me.Empresa = empresa
        End If


    End Sub

    Sub AdicionarGestores(gestor As GESTOR)
        Dim unico As Boolean = True

        For Each x In Gestores

            If x.CPF = gestor.CPF Then
                unico = False
            End If
        Next
        If unico Then Gestores.Add(gestor)

    End Sub

    Sub AdicionarSocios(socio As SociosReceita)
        Dim unico As Boolean = True
        For Each x In socios

            If x.CnpjOuCpf = socio.CnpjOuCpf Then
                unico = False
            End If
        Next
        If unico Then socios.Add(socio)
    End Sub




    Protected Overrides Sub Finalize()

    End Sub
End Class
