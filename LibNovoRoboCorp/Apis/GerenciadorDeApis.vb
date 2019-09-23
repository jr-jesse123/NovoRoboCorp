Friend Class GerenciadorDeApis
    Private linhas As List(Of LINHA)
    Private empresa As EMPRESA
    Private gestores As List(Of GESTOR)
    Private socios As List(Of Socio)

    Public Sub New(linhas As List(Of LINHA), empresa As EMPRESA, gestores As List(Of GESTOR), socios As List(Of Socio))
        Me.linhas = linhas
        Me.empresa = empresa
        Me.gestores = gestores
        Me.socios = socios
    End Sub

    Public Sub EnviarInformacoes()

        Dim Titulo As String = empresa.Nome
        Dim stringDeObservacoes As String = empresa.Observações + "<br>"
        Dim stringDeNrDeEnderecos As String = empresa.Endereço + "<br>"
        Dim stringDeNrDelinhas As String = $"Nr de Linhas:  {linhas.Count.ToString} <br>"
        Dim stringDeGestores As String = "Gestores: <br>"
        Dim stringDesocios As String = "Socios:  <br>"
        Dim apistring As String = ""
        Dim stringDeLinhas As String = "linhas: <br>"

        If empresa.Endereço IsNot Nothing Then
            Dim stringDeEndereco As String = $"Emdereço: {empresa.Endereço.ToString} <br>"
        Else
            Dim stringDeEndereco As String = "Sem endereços principais cadastrados"
        End If


        For Each gestor In gestores
            stringDeGestores = stringDeGestores + gestor.Master + " " + gestor.NOME + " " + gestor.TelefoneCelular + " " +
 gestor.TelefoneFixo + " " + gestor.EMAIL + "<br>"
        Next

        For Each socio In socios
            stringDesocios = stringDesocios + socio.NOME + " " + socio.Telefone + " " + "<br>"
        Next

        For Each linha In linhas
            stringDeLinhas = stringDeLinhas + linha.NrDaLinha + " " + linha.DataDeExpiracao + " " + " " + linha.TipoDeplano + "<br>"
        Next

        If linhas.Count >= 200 Then
            apistring = $"https://4dconsultoria.bitrix24.com.br/rest/52/l3mea29nw1b1o21b/crm.lead.add/?fields[TITLE]='{Titulo}'&fields[COMMENTS]='{stringDeGestores + stringDesocios + stringDeNrDeEnderecos + stringDeNrDelinhas}'&fields[UF_CRM_1551207871]=159&fields[STATUS_ID]=New"

            Dim resultado As String = ApiBitrix.RequestDadosWeb(apistring)
            If resultado Like "*result*" Then
                Console.WriteLine("cliente enviao ao bitrix")
            End If
        End If

    End Sub
End Class
