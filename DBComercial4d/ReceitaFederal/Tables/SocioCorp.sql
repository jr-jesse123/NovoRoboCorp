CREATE TABLE [ReceitaFederal].[SocioCorp] (
    [ID]                       INT            IDENTITY (1, 1) NOT NULL,
    [CNPJ]                     NVARCHAR (14)  NULL,
    [Categoria]                INT            NOT NULL,
    [Nome]                     NVARCHAR (MAX) NULL,
    [CnpjOuCpf]                NVARCHAR (MAX) NULL,
    [CodigoQualificacao]       NVARCHAR (MAX) NULL,
    [PercentualDoCapital]      FLOAT (53)     NOT NULL,
    [DataDeEntradaNaSociedade] DATETIME       NOT NULL,
    CONSTRAINT [PK_ReceitaFederal.SocioCorp] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ReceitaFederal.SocioCorp_ReceitaFederal.CadastroCNPJ_CNPJ] FOREIGN KEY ([CNPJ]) REFERENCES [ReceitaFederal].[CadastroCNPJ] ([CNPJ])
);


GO
CREATE NONCLUSTERED INDEX [IX_CNPJ]
    ON [ReceitaFederal].[SocioCorp]([CNPJ] ASC);

