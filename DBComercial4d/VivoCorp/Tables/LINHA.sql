CREATE TABLE [VivoCorp].[LINHA] (
    [NrDaLinha]     NVARCHAR (128) NOT NULL,
    [CriadoEm]      NVARCHAR (MAX) NULL,
    [PortadoEm]     DATETIME       NOT NULL,
    [FidelizadoAte] DATETIME       NOT NULL,
    [Operadora]     INT            NOT NULL,
    [Empresa_CNPJ]  NVARCHAR (14)  NULL,
    CONSTRAINT [PK_VivoCorp.LINHA] PRIMARY KEY CLUSTERED ([NrDaLinha] ASC),
    CONSTRAINT [FK_VivoCorp.LINHA_VivoCorp.ClienteVivo_Empresa_CNPJ] FOREIGN KEY ([Empresa_CNPJ]) REFERENCES [VivoCorp].[ClienteVivo] ([CNPJ])
);


GO
CREATE NONCLUSTERED INDEX [IX_Empresa_CNPJ]
    ON [VivoCorp].[LINHA]([Empresa_CNPJ] ASC);

