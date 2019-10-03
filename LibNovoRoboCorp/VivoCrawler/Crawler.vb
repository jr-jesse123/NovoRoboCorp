
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Threading
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome

Public Class Crawler

    Public Shared Event Log(texto As String)

    Public Sub run()

        Dim paginaDelogin As New PaginaDeLogin
        paginaDelogin.Login()

        'Do
        '    Dim db As New CrawlerContext

        '    Dim PaginaDepesquisa As New PaginaDePesquisa()



        '    'Dim Clientevivo = PaginaDepesquisa.LocalizarProximoCnpj(db.Empresas)

        '    Dim paginaDocliente As New PaginaDoCliente()
        '    ClienteVivo.GN = paginaDocliente.gn
        '    ClienteVivo.CARTEIRA = paginaDocliente.carteira

        '    Dim PaginaDosGestores = New PaginaDosGestores()
        '    Dim gestores = PaginaDosGestores.EnriquecerGestores()
        '    Dim gestoresunicos As New HashSet(Of GestorVivo)(gestores)

        '    Clientevivo.Gestores = gestoresunicos.ToList

        '    Dim PaginaDeConsultarLinhas = New PaginaDeConsultarLinhas()
        '    Dim linhas = PaginaDeConsultarLinhas.EnriquecerContasLinhas()
        '    Dim linhasunicas As New HashSet(Of LINHA)(linhas)

        '    Clientevivo.linhas = linhasunicas.ToList

        '    Dim PaginaDeConsultarSocios = New PaginaDeConsultarSocios(Clientevivo.Socios)
        '    PaginaDeConsultarSocios.EnriquecerSocios()

        '    Dim paginadeConsutlarPedidos = New PaginaDeConsultarPedidos(Clientevivo.linhas)
        '    paginadeConsutlarPedidos.VerificarFidelidades()


        '    db.SaveChanges()
        'Loop
    End Sub

    Public Shared Sub EnviarLog(log As String)
        RaiseEvent log(log)
    End Sub
End Class

