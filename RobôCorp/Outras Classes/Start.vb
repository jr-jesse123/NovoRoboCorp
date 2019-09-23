Imports System.Data.Entity
Imports System.Threading
Imports OpenQA.Selenium
Imports RobôCorp

Module Start
    Public a As Char
    Public b As Char
    Public ThreadsActivity As Date
    Public ThreadPrincipal As Thread
    Public ThreadSecundaria As Thread
    Public ProcessosDoNavegador As New List(Of Process)
    Public listaDeCNPJ As List(Of CNPJ)
    Public CNPJ As CNPJ
    Public instancias As Integer
    Public crawler As Crawler





    Sub Main()

        instancias = FuncoesUteis.VerificarNumeroDeInstancias

        ThreadsActivity = Now

        ThreadPrincipal = New Thread(AddressOf ATividadeCrawler)

        Dim gerenciador As New GerenciadorAtividade
        ThreadSecundaria = New Thread(AddressOf gerenciador.EncerrarProcessos)


        DefnifirUFSeNaoDefnida()
        ObterPrimeirosCNPJS()

        ThreadPrincipal.Start()
        ThreadSecundaria.Start()



    End Sub

    Public Sub ATividadeCrawler()
        crawler = New Crawler
        Do
            Try
                crawler.EnriquecerCNPJS()
            Catch ex As ThreadAbortException
                crawler = Nothing
            Catch ex As Exception
                FuncoesUteis.debug(ex)
            End Try
        Loop
    End Sub

    Private Function ObterPrimeirosCNPJS()

        Using context As New CrawlerContext
            'verificar se esse formato funciona

            listaDeCNPJ = context.CNPJS.Where(Function(cnpj) cnpj.ENRIQUECIDO = Nothing And cnpj.UF = a + b) _
                .OrderBy(Function(cnpj) cnpj.CNPJS).Skip(200 * instancias).Take(100).ToList

            Console.WriteLine("primeiros CNPJS obtidos")

        End Using

    End Function

    Private Sub DefnifirUFSeNaoDefnida()
        If a = vbNullChar Then
            Console.WriteLine("Digite a UF a ser pesquisada")
            a = Console.ReadKey().KeyChar.ToString
            b = Console.ReadKey().KeyChar.ToString
            Console.WriteLine(vbCrLf)
        End If


    End Sub



End Module
