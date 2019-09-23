Public Class contador
    Private NrDeTentativas As Integer

    Sub contador(tentativasMax As Integer, Optional ex As Exception = Nothing)
        NrDeTentativas = NrDeTentativas + 1
        Threading.Thread.Sleep(1000)
        If NrDeTentativas > tentativasMax Then

            Throw New Exception("tentativas excedidas", ex)
        End If

    End Sub

End Class
