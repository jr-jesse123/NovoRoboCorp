'Public Class MontadorClienteVivo


'    Private Sub SalvarResultados()
'        Contextolivre = False

'        Console.WriteLine("salvando resultados")

'        Using context As New CrawlerContext

'            TransferirEntidades(context)

'            FuncoesUteisDAO.ReabastecerCNPJS(context)

'        End Using

'        FuncoesUteisDAO.sanitizarBD()

'        Contextolivre = True
'    End Sub



'    Private Sub TransferirEntidades(context As CrawlerContext)

'        Linhas2.AddRange(Linhas)

'        For Each linha In Linhas2
'            'context.LINHAS.Add(linha)
'            Linhas.Remove(linha)
'        Next

'        Linhas2.Clear()

'        Gestores2.AddRange(Gestores)
'        For Each gestor In Gestores2
'            'context.GESTORES.Add(gestor)
'            Gestores.Remove(gestor)
'        Next
'        Gestores2.Clear()

'        socios2.AddRange(socios)
'        For Each socio In socios2
'            'context.SOCIOS.Add(socio)
'            socios.Remove(socio)
'        Next
'        socios2.Clear()

'        empresas2.AddRange(empresas)

'        For Each x In empresas2
'            '  context.Empresas.Add(x)
'            empresas.Remove(x)
'        Next

'        empresas2.Clear()

'        Try
'            context.SaveChanges()
'            Console.WriteLine("Entidades enviadas para o Banco de Dados")
'        Catch ex As Validation.DbEntityValidationException
'            FuncoesUteisDAO.TratarErroDBentity(ex, context)
'        Catch ex As DbUpdateException
'            Stop
'            Dim x = ex.TargetSite
'        End Try

'    End Sub


'    Sub AdicionarLinhas(linha As LINHA)
'        Dim unico As Boolean = True

'        For Each x In Linhas

'            If x.NrDaLinha = linha.NrDaLinha Then
'                unico = False
'            End If
'        Next
'        If unico Then Linhas.Add(linha)

'    End Sub

'    Sub AdicionarEmpresas(empresa As ClienteVivo)
'        Dim unico As Boolean = True

'        If empresa IsNot Nothing Then
'            Me.Empresa = empresa
'        End If


'    End Sub

'    Sub AdicionarGestores(gestor As GESTOR)
'        Dim unico As Boolean = True

'        For Each x In Gestores

'            If x.CPF = gestor.CPF Then
'                unico = False
'            End If
'        Next
'        If unico Then Gestores.Add(gestor)

'    End Sub

'    Sub AdicionarSocios(socio As SociosReceita)
'        Dim unico As Boolean = True
'        For Each x In socios

'            If x.CnpjOuCpf = socio.CnpjOuCpf Then
'                unico = False
'            End If
'        Next
'        If unico Then socios.Add(socio)
'    End Sub



'End Class
