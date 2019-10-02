Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial


Partial Public Class CNPJ
    <Key>
    <StringLength(14)>
    Public Property CNPJS As String

    Public Property ENRIQUECIDO As EnriquecidoEnum

    Public Property UF As UFEnum

End Class

Public Enum EnriquecidoEnum
    NaoConsultado
    Erro
    Descartado
    EnriqueciLegado
    EnriquecidoCliente
End Enum

Public Enum UFEnum
    AC
    AL
    AP
    AM
    BA
    CE
    DF
    ES
    GO
    MA
    MT
    MS
    MG
    PA
    PB
    PR
    PE
    PI
    RJ
    RN
    RS
    RO
    RR
    SC
    SP
    SE
    [TO]
End Enum
