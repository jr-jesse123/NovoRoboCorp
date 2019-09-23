Imports System.IO
Imports System.Text
Imports LibNovoRoboCorp

Module Module1
    Dim leitor As StreamReader

    Sub Main()

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


        Dim x As New RFContext



        MontarEmpresa(x)



    End Sub

    Function EscreverBuffer(Buffer As Byte()) As String

        Dim vretorno As String = ""
        For Each MeuByte As Byte In Buffer
            vretorno += MeuByte.ToString
        Next
        Return vretorno
    End Function

    Function EscreverUtEscreverBufferUTF(buffer As Byte(), bytesLidos As Integer)
        Dim UTF As New UTF8Encoding
        Dim Vretorno As String
        Vretorno = UTF.GetString(buffer, 0, bytesLidos)
        Return Vretorno

    End Function


    Sub gravarInformacoes(linha As String, caminhoDoArquivo As String)

        Using FluxoDoArquvio As New FileStream(caminhoDoArquivo, FileMode.Append)
            Using escritor = New StreamWriter(FluxoDoArquvio)
                escritor.Write(linha)
            End Using
        End Using

    End Sub

    Function Formatarcsv(empresa As LibNovoRoboCorp.CadastroCNPJ) As String

        Dim vretorno As String = $"{empresa.CNPJ};{empresa.MatrizOuFilial};{empresa.RazaoSocial};{empresa.NomeFantasia};{empresa.SituacaoCadastral};{empresa.CodigoDaNaturezaJudica};{empresa.DataDeInicioDaAtividade};{empresa.CNAE};{empresa.Endereco};{empresa.Bairro};{empresa.CEP};{empresa.UF};{empresa.Cidade};{empresa.Telefone};{empresa.Telefone2};{empresa.Email};{empresa.QualificacaoResponsavel};{empresa.CapitalSocial};{empresa.PorteDaEmpresa};{empresa.OptanteSimples};{empresa.Mei};{vbCrLf}"

        Return vretorno
    End Function

    Sub MontarEmpresa(x As RFContext)
        Dim empresa As CadastroCNPJ
        Dim ProximoCnpj As Boolean
        Dim vlinha = leitor.ReadLine

        Do Until leitor.EndOfStream

            If vlinha.Substring(0, 1) = "1" Then

                If vlinha.Substring(223, 2) = "02" Then
                    Do

                        If vlinha.Substring(0, 1) = "1" Then


                            empresa = New CadastroCNPJ(vlinha)
                            x.CNPJS.Add(empresa)

                        ElseIf vlinha.Substring(0, 1) = "2" Then

                            Dim socio As New SociosReceita(vlinha)
                            x.Socios.Add(socio)

                        ElseIf vlinha.Substring(0, 1) = "6" Then

                            Dim CnaesSec As New CNAEsSecundarias(vlinha)
                            x.CNAEsSecundarios.Add(CnaesSec)

                        Else
                            Stop

                        End If

                        vlinha = leitor.ReadLine

                        ProximoCnpj = Not empresa.CNPJ.Equals(vlinha.Substring(3, 14))

                    Loop Until ProximoCnpj

                Else
                    vlinha = leitor.ReadLine
                End If

                Try
                    x.SaveChangesAsync()
                Catch ex As Exception
                    Dim errors = x.GetValidationErrors()
                    Stop
                End Try
            Else
                vlinha = leitor.ReadLine
            End If





        Loop
    End Sub
End Module
