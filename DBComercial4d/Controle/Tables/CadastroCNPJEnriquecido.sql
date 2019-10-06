CREATE TABLE [Controle].[CadastroCNPJEnriquecido] (
    [CNPJ]                 NVARCHAR (14) NOT NULL,
    [EnriquecidoVivoMovel] DATETIME      NOT NULL,
    [EnriquecidoPhenix]    DATETIME      NOT NULL,
    [EnriquecidoClaro]     DATETIME      NOT NULL,
    CONSTRAINT [PK_Controle.CadastroCNPJEnriquecido] PRIMARY KEY CLUSTERED ([CNPJ] ASC),
    CONSTRAINT [FK_Controle.CadastroCNPJEnriquecido_ReceitaFederal.CadastroCNPJ_CNPJ] FOREIGN KEY ([CNPJ]) REFERENCES [ReceitaFederal].[CadastroCNPJ] ([CNPJ])
);


GO
CREATE NONCLUSTERED INDEX [IX_CNPJ]
    ON [Controle].[CadastroCNPJEnriquecido]([CNPJ] ASC);

