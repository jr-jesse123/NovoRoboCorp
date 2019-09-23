Imports System.Data.Entity
Imports System.Threading
Imports OpenQA.Selenium
Imports RobôCorp

Module Start
    Public UF As String = "SP"
    Public ThreadsActivity As Date

    Public listaDeCNPJ As List(Of CNPJ)
    Public CNPJ As CNPJ

    Public crawler As Crawler

    Sub Main()

        ThreadsActivity = Now

        Dim ThreadPrincipal = New Thread(AddressOf ATividadeCrawler)

        listaDeCNPJ = FuncoesBanco.ObterPrimeirosCNPJS()

        ThreadPrincipal.Start()

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




End Module
