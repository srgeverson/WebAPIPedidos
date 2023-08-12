CREATE TABLE [dbo].[pedidos]
(
	CodigoPedido INT NOT NULL,
	Fornecedor BIGINT NOT NULL FOREIGN KEY REFERENCES fornecedores(CNPJ),
	Produto INT NOT NULL FOREIGN KEY REFERENCES produtos(Codigo),
	QuantidadeProduto INT,
	DataPedido DATETIME,
	ValorPedido DECIMAL(10,2),
	PRIMARY KEY (CodigoPedido, Fornecedor, Produto)
);