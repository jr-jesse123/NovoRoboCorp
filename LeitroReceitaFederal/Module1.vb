Imports System.ComponentModel.DataAnnotations
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Text.RegularExpressions
Imports LibNovoRoboCorp
Imports Z.EntityFramework.Extensions

Module Module1
    Dim leitor As StreamReader
    Dim escritorEmpresas As New StreamWriter("ResultadoEmpresas.csv")
    Dim escritorEmpresasErro As New StreamWriter("ResultadoErroEmpresas.csv")
    Dim escritorSocios As New StreamWriter("ResultadoSocios.csv")
    Dim escritorCnaes As New StreamWriter("ResultadoCnaes.csv")


    Sub Main()

        Console.WriteLine("digite a pasta a ser pesquisada")
        Dim diretorio = Console.ReadLine
        Dim arquivos = Directory.GetFiles(diretorio) _
        .Where(Function(x)
                   Return Not x.EndsWith("exe") And
                   Not x.EndsWith("csv") And
                   Not x.EndsWith("pdb") And
                   Not x.EndsWith("xml") And
                   Not x.EndsWith("config") And
                   Not x.EndsWith("zip") And
                   Not x.EndsWith("dll")
               End Function)


        For Each arquivo In arquivos

            Dim FluxoDoArquivo As New FileStream(arquivo, FileMode.OpenOrCreate)
                leitor = New StreamReader(FluxoDoArquivo)

                Console.WriteLine("NÃAAAAOOO FEEEECHAAARRR ESTAAAAA JANELAAAAAA ")
                Console.WriteLine("Este programa está processando todos os cnpsj da receita federal")

                MontarEmpresa()


        Next

    End Sub


    Sub MontarEmpresa()
        Dim empresa As CadastroCNPJ
        Dim ProximoCnpj As Boolean
        Dim vlinha = leitor.ReadLine.RemoveSpecialCharacters

        Dim context As New CrawlerContext
        Dim context2 As New CrawlerContext
        Dim context3 As New CrawlerContext
        Dim empresas As New List(Of CadastroCNPJ)
        Dim socios As New List(Of SociosReceita)
        Dim cnaes As New List(Of CNAEsSecundarias)


        context.Configuration.AutoDetectChangesEnabled = False
        context2.Configuration.AutoDetectChangesEnabled = False
        context3.Configuration.AutoDetectChangesEnabled = False


        Do Until leitor.EndOfStream
            Try
                context.Empresas.AddRange(empresas)

                context.BulkSaveChanges

            Catch ex As Exception

                For Each empresa In empresas
                    escritorEmpresasErro.WriteLine(empresa)
                Next

            End Try


            empresas.Clear()
            socios.Clear()
            cnaes.Clear()

            For x = 1 To 300



                If vlinha.Substring(0, 1) = "1" Then

                    If vlinha.Substring(223, 2) = "02" Then
                        If empresa IsNot Nothing Then
                            empresas.Add(empresa)
                        End If
                        Do
                            Try

                                If vlinha.Substring(0, 1) = "1" Then
                                    empresa = New CadastroCNPJ(vlinha)


                                    Try
                                        Validator.ValidateObject(empresa, New ValidationContext(empresa))
                                    Catch ex As Exception
                                        Stop
                                        escritorEmpresasErro.WriteLine(empresa.ToString)
                                        empresa = Nothing
                                        Exit Do
                                    End Try

                                    'escritorEmpresas.WriteLine(empresa.ToString)

                                ElseIf vlinha.Substring(0, 1) = "2" Then

                                    Dim socio As New SociosReceita(vlinha)

                                    empresa.Socios.Add(socio)
                                    'escritorSocios.WriteLine(socio.ToString)



                                ElseIf vlinha.Substring(0, 1) = "6" Then

                                    Dim CnaesSec As New CNAEsSecundarias(vlinha)

                                    empresa.CnasesSecundarios.Add(CnaesSec)
                                    'escritorCnaes.WriteLine(CnaesSec.ToString)

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
            Next
        Loop


    End Sub

    <Extension()>
    Public Function RemoveSpecialCharacters(str As String) As String

        Dim padrao As String = "[^a-zA-Z0-9_.| |*|@|,|(|)|-|/|-|ç|ã|õ]+"
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
