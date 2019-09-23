Imports System.Net.Mail
Imports System.Net.Mime

Public Class APIGMAIL2

    Shared Sub EnviarPorEamil(assunto As String, corpo As String, destinatario As String)
        Dim SmtpServer As New SmtpClient()
        SmtpServer.Credentials = New Net.NetworkCredential("leadb2bdf@gmail.com", "reboliso")
        SmtpServer.Port = 587
        SmtpServer.Host = "smtp.gmail.com"
        SmtpServer.EnableSsl = True
        Dim mail = New MailMessage()

        Dim addr() As String = {destinatario}

        Try
            mail.From = New MailAddress("leadb2bdf@gmail.com", "leadsb2b", System.Text.Encoding.UTF8)
            Dim i As Byte
            For i = 0 To addr.Length - 1
                mail.To.Add(addr(i))
            Next
            mail.Subject = assunto
            mail.Body = corpo
            ' If lstAnexos.Items.Count <> 0 Then
            'For i = 0 To lstAnexos.Items.Count - 1
            'mail.Attachments.Add(New Attachment(lstAnexos.Items.Item(i)))
            'Next
            'End If
            'If path <> Nothing Then
            'Dim logo As New LinkedResource(path)
            'logo.ContentId = "Logo"
            Dim htmlview As String
            htmlview = "<html><body><table border=2><tr width=100%><td><img src=cid:Logo alt=nomeempresa />
</td><td>http://www.macoratti.net</td></tr></table><hr/></body></html>"
            'Dim alternateView1 As AlternateView = AlternateView.CreateAlternateViewFromString(htmlview +
            'txtAssunto.Text, Nothing, MediaTypeNames.Text.Html)
            'alternateView1.LinkedResources.Add(logo)
            'mail.AlternateViews.Add(alternateView1)

            'End If

            mail.IsBodyHtml = True
            'mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
            'mail.ReplyTo = New MailAddress(txtDestino.Text)
            SmtpServer.Send(mail)
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub


End Class
