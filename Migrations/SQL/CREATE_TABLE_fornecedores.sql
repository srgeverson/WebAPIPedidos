CREATE TABLE [dbo].[fornecedores]
(
	CNPJ BIGINT NOT NULL PRIMARY KEY,
	UF CHAR(2),
	EmailContato VARCHAR(100),
	NomeContato VARCHAR(100),
);