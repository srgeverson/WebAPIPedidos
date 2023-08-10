# <a href="https://webapipedidos.azurewebsites.net/swagger/index.html">WebAPIPedidos - API de Gerenciamento de Pedidos</a>

## 📌 Versão 1.0.0

## 💡Pré-requisitos
Antes de começar, você vai precisar ter instalado em sua máquina as seguintes ferramentas:
[Git](https://git-scm.com), [.NET Core](https://dotnet.microsoft.com/en-us/download), [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads), [Visual Studio](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/) e [SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)

## 🚀 Dependências utilizadas
* **AutoMapper 12.0.1**
* **AutoMapper.Extensions.Microsoft.DependencyInjection 12.0.1**
* **Microsoft.AspNetCore.Mvc.Versioning 5.1.0**
* **Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer 5.1.0**
* **Microsoft.EntityFrameworkCore.SqlServer 7.0.10**
* **Swashbuckle.AspNetCore 6.5.0**

## 🛠️ Constrção da aplicação

```bash

# Criando o projeto
$ dotnet new webapi -o WebAPIPedidos

# Entrando na pasta criada
$ cd WebAPIPedidos

# Habilitando protocolo https
$ dotnet dev-certs https --trust

# Criando README.md 
$ echo "# API C#" >> README.md

# Inicializando repositório
$ git init

# Commitando pela primeira vez
$ git add README.md
$ git commit -m "Initial commit"

# Definindo branch principal
$ git branch -M main

# Enviando para repositório remoto
$ git remote add origin https://github.com/srgeverson/WebAPIPedidos.git
$ git push -u origin main

# Variável de ambiente que armazena a URL de conexão com o banco de dados
$ setx URL_DB_WebAPIPedidos "Data Source=localhost; Initial Catalog=db_teste;User ID=user_teste;Password=@G12345678;Application Name=WebAPIPedidos;TrustServerCertificate=True;" /M

```

## 🎲 Executando a aplicação

```bash



```

## 👨‍💻 Equipe de Desenvolvimento

* **Geverson Souza** - [Geverson Souza](https://www.linkedin.com/in/srgeverson/)

## ✒️ Autor

* **Geverson Souza** - [Geverson Souza](https://www.linkedin.com/in/srgeverson/)