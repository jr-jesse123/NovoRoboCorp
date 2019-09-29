Imports System.IO
Imports System.Text.RegularExpressions

Module Module1

    Sub Main()

        Console.WriteLine("Digite abaixo o nome do arquivo a ser dividido:")
        Dim arquivoName As String = Console.ReadLine

        Dim FluxoDoArquivo As FileStream

        Console.WriteLine("Digite O número de registros por arquivo:")
        Dim QtdRegistro As Integer = Console.ReadLine

        Console.WriteLine("Digite Eventual Verificador Regexer se for necessário:")
        Dim PadraoRegxer = Console.ReadLine


        'Dim opcoesRegex As RegexOptions = {RegexOptions.Compiled, RegexOptions.CultureInvariant,
        'RegexOptions.IgnoreCase, RegexOptions.Singleline}


        Try
            FluxoDoArquivo = New FileStream(arquivoName, FileMode.OpenOrCreate)
        Catch ex As Exception
            Console.Write(ex.Message)
            Console.ReadLine()
            Throw
        End Try




        Dim parte As Integer
        Dim count As Integer
        Dim line As String
        Dim leitor = New StreamReader(FluxoDoArquivo)

        line = leitor.ReadLine()
        Dim escritorErro = New StreamWriter(Path.GetFileNameWithoutExtension(arquivoName) + "Erros" + Path.GetExtension(arquivoName))

        Do Until leitor.EndOfStream
            Dim escritor = New StreamWriter(Path.GetFileNameWithoutExtension(arquivoName) + parte.ToString + Path.GetExtension(arquivoName))

            Do Until count = QtdRegistro Or leitor.EndOfStream

                If Regex.IsMatch(line, PadraoRegxer, RegexOptions.Compiled) Then
                    escritor.WriteLine(line)
                Else
                    escritorErro.WriteLine(line)
                End If
                count += 1

                line = leitor.ReadLine()

            Loop
            count = 0
            line = leitor.ReadLine()
            parte += 1
        Loop

    End Sub
End Module
