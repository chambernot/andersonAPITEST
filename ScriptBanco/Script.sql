CREATE DATABASE APICALCULO
GO
CREATE TABLE CalculoHistoricoLog (
    Id UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
    Nome VARCHAR (100),
    Localizacao VARCHAR (200),
	NomeProximo VARCHAR (100),
	LocalizacaoProximo VARCHAR (100),
	distancia decimal (18,2),
	IdidenticacaoExterno UNIQUEIDENTIFIER
)
GO
