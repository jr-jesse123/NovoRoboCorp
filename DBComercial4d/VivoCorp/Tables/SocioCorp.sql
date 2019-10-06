CREATE TABLE [VivoCorp].[SocioCorp] (
    [ID]                 INT            NOT NULL,
    [TelefoneCadastrado] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_VivoCorp.SocioCorp] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_VivoCorp.SocioCorp_ReceitaFederal.SocioCorp_ID] FOREIGN KEY ([ID]) REFERENCES [ReceitaFederal].[SocioCorp] ([ID])
);


GO
CREATE NONCLUSTERED INDEX [IX_ID]
    ON [VivoCorp].[SocioCorp]([ID] ASC);

