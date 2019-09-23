
Imports System.Data.Entity
Imports System.Threading

Public Class GerenciadorAtividade

    Sub EncerrarProcessos()

        Do

            If ThreadsActivity + TimeSpan.FromMinutes(10) < Now Then
                Console.WriteLine("aplicação sem retorno há mais de 10 minutos, reiniciando navegador")
                ThreadPrincipal.Abort()
                Threading.Thread.Sleep(6000)
                For Each processo In ProcessosDoNavegador
                    Try
                        processo.Kill()
                    Catch
                    End Try
                    ThreadsActivity = Now

                Next


                ThreadPrincipal = New Thread(AddressOf Start.ATividadeCrawler)
                ThreadPrincipal.Start()



            End If

            Threading.Thread.Sleep(60000)

        Loop

    End Sub

End Class
