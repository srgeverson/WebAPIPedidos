CREATE TABLE [dbo].[produtos]
(
	Codigo INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	Descricao VARCHAR(100),
	DataCadastro DATETIME,
	Valor DECIMAL(10,2)
);