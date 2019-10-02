Imports System.ComponentModel.DataAnnotations
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Text.RegularExpressions
Imports LibNovoRoboCorp

Module Module1
    Dim leitor As StreamReader

    Sub Main()

        Dim arquivos As String() = Directory.GetFiles(Directory.GetCurrentDirectory)

        For Each arquivo In arquivos.Where(Function(a) Not a.EndsWith("exe") And Not a.EndsWith("csv"))

            Try


                Dim FluxoDoArquivo As New FileStream(arquivo, FileMode.OpenOrCreate)
                leitor = New StreamReader(FluxoDoArquivo)

                Console.WriteLine("NÃAAAAOOO FEEEECHAAARRR ESTAAAAA JANELAAAAAA ")
                Console.WriteLine("Este programa está processando todos os cnpsj da receita federal")

                MontarEmpresa()

            Catch ex As Exception
                Stop
                Console.Write(ex.Message + Environment.NewLine + ex.StackTrace)

                Console.ReadLine()
            End Try


        Next
    End Sub


    Sub MontarEmpresa()
        Dim empresa As CadastroCNPJ
        Dim ProximoCnpj As Boolean
        Dim vlinha = leitor.ReadLine.RemoveSpecialCharacters

        Using escritorEmpresas As New StreamWriter("ResultadoEmpresas.csv")
            Using escritorEmpresasErro As New StreamWriter("ResultadoErroEmpresas.csv")
                Using escritorSocios As New StreamWriter("ResultadoSocios.csv")
                    Using escritorCnaes As New StreamWriter("ResultadoCnaes.csv")

                        Do Until leitor.EndOfStream

                            If vlinha.Substring(0, 1) = "1" Then

                                If vlinha.Substring(223, 2) = "02" Then
                                    Do
                                        Try

                                            If vlinha.Substring(0, 1) = "1" Then
                                                empresa = New CadastroCNPJ(vlinha)


                                                Try
                                                    Validator.ValidateObject(empresa, New ValidationContext(empresa))
                                                Catch ex As Exception
                                                    Stop
                                                    escritorEmpresasErro.WriteLine(empresa.ToString)
                                                End Try

                                                escritorEmpresas.WriteLine(empresa.ToString)

                                            ElseIf vlinha.Substring(0, 1) = "2" Then

                                                Dim socio As New SociosReceita(vlinha)

                                                escritorSocios.WriteLine(socio.ToString)

                                            ElseIf vlinha.Substring(0, 1) = "6" Then

                                                Dim CnaesSec As New CNAEsSecundarias(vlinha)
                                                escritorCnaes.WriteLine(CnaesSec.ToString)

                                            End If

                                        Catch ex As Exception
                                            Stop
                                            Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine)
                                            Console.WriteLine("******************************************")
                                            Console.WriteLine(vlinha)
                                            Console.WriteLine("******************************************")
                                        End Try

                                        vlinha = leitor.ReadLine.RemoveSpecialCharacters
                                        ProximoCnpj = Not empresa.CNPJ.Equals(vlinha.Substring(3, 14))

                                    Loop Until ProximoCnpj

                                Else
                                    vlinha = leitor.ReadLine.RemoveSpecialCharacters
                                End If

                            Else
                                vlinha = leitor.ReadLine.RemoveSpecialCharacters
                            End If

                        Loop

                    End Using
                End Using
            End Using
        End Using

    End Sub

    <Extension()>
    Public Function RemoveSpecialCharacters(str As String) As String

        Dim padrao As String = "[^a-zA-Z0-9_.| |*|@|,|(|)|-|/|-]+"
        Try

            Dim especiais = Regex.Matches(str, padrao)

            If especiais.Count > 0 Then
                'Stop
            End If
        Catch ex As Exception

        End Try


        Return Regex.Replace(str, padrao, " ", RegexOptions.Compiled)

    End Function
End Module
