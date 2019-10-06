CREATE TABLE [VivoCorp].[ClienteVivo] (
    [CNPJ]            NVARCHAR (14)  NOT NULL,
    [GestorVivo_CPF]  NVARCHAR (11)  NULL,
    [GestorVivo_CPF1] NVARCHAR (11)  NULL,
    [GN]              NVARCHAR (MAX) NULL,
    [CARTEIRA]        NVARCHAR (MAX) NULL,
    [Observações]     NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_VivoCorp.ClienteVivo] PRIMARY KEY CLUSTERED ([CNPJ] ASC),
    CONSTRAINT [FK_VivoCorp.ClienteVivo_Controle.CadastroCNPJEnriquecido_CNPJ] FOREIGN KEY ([CNPJ]) REFERENCES [Controle].[CadastroCNPJEnriquecido] ([CNPJ]),
    CONSTRAINT [FK_VivoCorp.ClienteVivo_VivoCorp.GestorVivo_GestorVivo_CPF] FOREIGN KEY ([GestorVivo_CPF]) REFERENCES [VivoCorp].[GestorVivo] ([CPF]),
    CONSTRAINT [FK_VivoCorp.ClienteVivo_VivoCorp.GestorVivo_GestorVivo_CPF1] FOREIGN KEY ([GestorVivo_CPF1]) REFERENCES [VivoCorp].[GestorVivo] ([CPF])
);


GO
CREATE NONCLUSTERED INDEX [IX_CNPJ]
    ON [VivoCorp].[ClienteVivo]([CNPJ] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_GestorVivo_CPF]
    ON [VivoCorp].[ClienteVivo]([GestorVivo_CPF] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_GestorVivo_CPF1]
    ON [VivoCorp].[ClienteVivo]([GestorVivo_CPF1] ASC);

