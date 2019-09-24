Imports System.IO
Imports System.Text
Imports LibNovoRoboCorp

Module Module1
    Dim leitor As StreamReader

    Sub Main()

        Dim arquivo As String

        '#If DEBUG Then
        arquivo = "D:\F.K032001K.csv"
        '#Else
        arquivo = "Z:\F.K032001K.csv"
        '#End If
        Try


            Dim FluxoDoArquivo As New FileStream(arquivo, FileMode.OpenOrCreate)
            leitor = New StreamReader(FluxoDoArquivo)

        Console.WriteLine("NÃAAAAOOO FEEEECHAAARRR ESTAAAAA JANELAAAAAA ")
        Console.WriteLine("Este programa está processando todos os cnpsj da receita federal")


        Dim x As New RFContext



        MontarEmpresa(x)

        Catch ex As Exception
            Console.Write(ex.Message + Environment.NewLine + ex.StackTrace)

            Console.ReadLine()
        End Try



    End Sub


    Sub MontarEmpresa(x As RFContext)
        Dim empresa As CadastroCNPJ
        Dim ProximoCnpj As Boolean
        Dim vlinha = leitor.ReadLine.Replace(";", "")

        Dim escritorEmpresas As New StreamWriter("ResultadoEmpresas.csv")
        Dim escritorSocios As New StreamWriter("ResultadoSocios.csv")
        Dim escritorCnaes As New StreamWriter("ResultadoCnaes.csv")

        Do Until leitor.EndOfStream

            If vlinha.Substring(0, 1) = "1" Then

                If vlinha.Substring(223, 2) = "02" Then
                    Do

                        If vlinha.Substring(0, 1) = "1" Then
                            empresa = New CadastroCNPJ(vlinha)
                            escritorEmpresas.WriteLine(empresa.ToString)



                        ElseIf vlinha.Substring(0, 1) = "2" Then

                            Dim socio As New SociosReceita(vlinha)



                            escritorSocios.WriteLine(socio.ToString)

                        ElseIf vlinha.Substring(0, 1) = "6" Then

                            Dim CnaesSec As New CNAEsSecundarias(vlinha)

                            escritorCnaes.WriteLine(CnaesSec.ToString)


                        End If

                        vlinha = leitor.ReadLine.Replace(";", "")

                        ProximoCnpj = Not empresa.CNPJ.Equals(vlinha.Substring(3, 14))

                    Loop Until ProximoCnpj

                Else
                    vlinha = leitor.ReadLine.Replace(";", "")
                End If

                Try
                    x.SaveChangesAsync()
                Catch ex As Exception
                    Dim errors = x.GetValidationErrors()
                    For Each erro In errors
                        Console.WriteLine(erro.ToString)
                    Next

                End Try
            Else
                vlinha = leitor.ReadLine.Replace(";", "")
            End If





        Loop
    End Sub
End Module
