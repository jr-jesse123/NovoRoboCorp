CREATE TABLE [VivoCorp].[GestorVivo] (
    [CPF]              NVARCHAR (11)  NOT NULL,
    [EMAIL]            NVARCHAR (MAX) NULL,
    [TelefoneCelular]  NVARCHAR (MAX) NULL,
    [TelefoneFixo]     NVARCHAR (MAX) NULL,
    [Master]           BIT            NOT NULL,
    [NOME]             NVARCHAR (MAX) NULL,
    [ClienteVivo_CNPJ] NVARCHAR (14)  NULL,
    CONSTRAINT [PK_VivoCorp.GestorVivo] PRIMARY KEY CLUSTERED ([CPF] ASC),
    CONSTRAINT [FK_VivoCorp.GestorVivo_VivoCorp.ClienteVivo_ClienteVivo_CNPJ] FOREIGN KEY ([ClienteVivo_CNPJ]) REFERENCES [VivoCorp].[ClienteVivo] ([CNPJ])
);


GO
CREATE NONCLUSTERED INDEX [IX_ClienteVivo_CNPJ]
    ON [VivoCorp].[GestorVivo]([ClienteVivo_CNPJ] ASC);

