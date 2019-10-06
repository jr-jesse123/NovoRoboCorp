CREATE TABLE [dbo].[CNAEsSecundarias] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [CNPJ]        NVARCHAR (14)  NULL,
    [CnaesSecStr] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.CNAEsSecundarias] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.CNAEsSecundarias_ReceitaFederal.CadastroCNPJ_CNPJ] FOREIGN KEY ([CNPJ]) REFERENCES [ReceitaFederal].[CadastroCNPJ] ([CNPJ])
);


GO
CREATE NONCLUSTERED INDEX [IX_CNPJ]
    ON [dbo].[CNAEsSecundarias]([CNPJ] ASC);

