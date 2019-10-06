Imports System.ComponentModel.DataAnnotations.Schema
<Table(NameOf(CNAEsSecundarias), Schema:="ReceitaFederal")>
Public Class CNAEsSecundarias ' tipo 6
    Private _Id As Integer
    Private _CNPJ As String
    Private Shared count As Long
    Private _Empresa As CadastroCNPJ

    Property Empresa As CadastroCNPJ
        Get
            Return _Empresa
        End Get
        Set
            _Empresa = Value
        End Set
    End Property

    Property Id As Integer
        Get
            Return _Id
        End Get
        Private Set
            _Id = Value
        End Set
    End Property

    Property CNPJ As String ' 3 A 17
        Get
            Return _CNPJ
        End Get
        Private Set
            _CNPJ = Value
        End Set
    End Property

    <Column>
    Private Property CnaesSecStr As String ' 17 a 116 7 números para cada cnae
    <NotMapped>
    Public ReadOnly Property CnaesSecundarias As List(Of String)
        Get
            Dim nums = CnaesSecStr.ToList

            Dim ListaCnaes As New List(Of String)
            Dim cnae As String
            Dim fim As Boolean
            Do Until fim
                Try
                    cnae = nums.Take(7)
                    ListaCnaes.Add(cnae)
                    nums.RemoveRange(0, 7)

                Catch ex As ArgumentNullException
                    fim = True
                End Try
            Loop

            Return ListaCnaes
        End Get

    End Property

    Private Sub New()

    End Sub

    Public Sub New(CNPJ As String, CnaesSecStr As String)
        Me.CNPJ = CNPJ
        Me.CnaesSecStr = CnaesSecStr
    End Sub

    Public Sub New(vlinha As String)

        Me.CNPJ = vlinha.Substring(3, 14)

        Dim cnaes = vlinha.Substring(17, vlinha.Length - 17)
        cnaes = cnaes.Replace("0000000", "")
        cnaes = cnaes.Substring(0, cnaes.IndexOf("  "))
        Me.CnaesSecStr = cnaes

    End Sub

    Public Overrides Function ToString() As String

        Dim output As String

        output = $"{count};{CNPJ};{CnaesSecStr};"
        count += 1


        Return output
    End Function

End Class

Public Enum CategoriaEnum
    PessoaJuridica = 1
    PessoaFisica = 2
    Estrangeiro = 3
End Enum

Public Enum OpcaoSimplesEnum
    NaoOptante = 0
    OptanteSimples = 5
    ExcluidoSimples = 6
    OptanteSimples2 = 7
    ExcluidoSimples2 = 8
End Enum

Public Enum PorteEmpresaEnum
    NaoInformado = 0
    MicroEmprsa = 1
    EPP = 3
    Demais = 5
End Enum

