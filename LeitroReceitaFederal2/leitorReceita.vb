Imports System.IO
Imports LibNovoRoboCorp

Public Class leitorReceita

    Shared leitor As StreamReader
    Shared ctxs As RFContext() = {New RFContext, New RFContext, New RFContext, New RFContext}
    Event EmpresaEncontrada(vlinha As String)
    Event SocioEncontrada(vlinha As String)
    Event CnaeEncontrada(vlinha As String)

    Sub run()



        Dim arquivo As String

#If DEBUG Then
        arquivo = "D:\F.K032001K.csv"
#Else
        arquivo = "z:\F.K032001K.csv"
#End If



        Dim FluxoDoArquivo As New FileStream(arquivo, FileMode.Open)
        leitor = New StreamReader(FluxoDoArquivo)

        Console.WriteLine("NÃAAAAOOO FEEEECHAAARRR ESTAAAAA JANELAAAAAA ")
        Console.WriteLine("Este programa está processando todos os cnpsj da receita federal")

        MontarEmpresa()



    End Sub

    Sub MontarEmpresa()
        Dim cnpj As String
        Dim ProximoCnpj As Boolean
        Dim vlinha = leitor.ReadLine



        Do Until leitor.EndOfStream

            If vlinha.Substring(0, 1) = "1" Then

                If vlinha.Substring(223, 2) = "02" Then
                    cnpj = vlinha.Substring(3, 14)
                    Do

                        If vlinha.Substring(0, 1) = "1" Then

                            iniciarsalvarempresa(vlinha)


                        ElseIf vlinha.Substring(0, 1) = "2" Then

                            iniciarsalvarSocio(vlinha)



                        ElseIf vlinha.Substring(0, 1) = "6" Then

                            iniciarsalvarcnaes(vlinha)

                        Else
                            Stop

                        End If

                        vlinha = leitor.ReadLine

                        ProximoCnpj = Not cnpj.Equals(vlinha.Substring(3, 14))

                    Loop Until ProximoCnpj

                Else
                    vlinha = leitor.ReadLine
                End If


            Else
                vlinha = leitor.ReadLine
            End If





        Loop
    End Sub

    Private Sub iniciarsalvarcnaes(vlinha As String)

        Dim thread As New Threading.ThreadStart(Sub() salvarCnaes(vlinha))



    End Sub

    Private Sub iniciarsalvarSocio(vlinha As String)

        Dim thread As New Threading.ThreadStart(Sub() salvarSocio(vlinha))

    End Sub

    Private Sub iniciarsalvarempresa(vlinha As String)

        Threading.Thread.Sleep(10000)

        Dim thread As New Threading.ThreadStart(Sub() salvarEmpresa(vlinha))

        Dim thread2 As New Threading.Thread(Sub(x) salvarEmpresa(vlinha))

        thread2.Start()
        Stop
    End Sub

    Private Sub salvarCnaes(vlinha As String)
        Dim context = ObterContextoLIvre()

        Dim CnaesSec As New CNAEsSecundarias(vlinha)
        context.CNAEsSecundarios.Add(CnaesSec)

        Try
            Dim x = context.SaveChangesAsync()
        Catch ex As Exception
            Dim errors = context.GetValidationErrors()
            Console.WriteLine(errors.ToString)
            Stop
        End Try


        context.Disponivel = True

    End Sub

    Private Sub salvarSocio(vlinha As String)
        Dim context = ObterContextoLIvre()

        Dim socio As New SociosReceita(vlinha)
        context.Socios.Add(socio)


        Try
            Dim x = context.SaveChangesAsync()
        Catch ex As Exception
            Dim errors = context.GetValidationErrors()
            Console.WriteLine(errors.ToString)
            Stop
        End Try

        context.Disponivel = True
    End Sub

    Private Sub salvarEmpresa(vlinha As String)

        Dim context = ObterContextoLIvre()

        Try
            Dim EMPRESA = New CadastroCNPJ(vlinha)
            context.CNPJS.Add(EMPRESA)


            Dim x = context.SaveChangesAsync()
        Catch ex As Exception
            Dim errors = context.GetValidationErrors()
            Console.WriteLine(errors.ToString)
            Stop
        End Try

        context.Disponivel = True

    End Sub

    Private Function ObterContextoLIvre() As RFContext

        Dim output As RFContext

        SyncLock (RFContext.lock)
inicio:

            Try
                output = ctxs.Where(Function(c)
                                        If c.Disponivel Then
                                            c.Disponivel = False
                                            Return True
                                        Else
                                            Return False
                                        End If
                                    End Function).First



            Catch ex As Exception
                GoTo inicio
            End Try


            Return output
        End SyncLock
    End Function
End Class
