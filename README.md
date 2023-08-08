# WebAPIPedidos
API de Gerenciamento de Pedidos.

## 📌 Versão 1.0.0

## 💡Pré-requisitos
Antes de começar, você vai precisar ter instalado em sua máquina as seguintes ferramentas:
[Git](https://git-scm.com), [.NET Core](https://dotnet.microsoft.com/en-us/download), [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads), [Visual Studio](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/) e [SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)

## 🚀 Dependências utilizadas
* **Swashbuckle.AspNetCore 6.4.0**
* **Microsoft.AspNetCore.Mvc.Versioning 5.0.0**
* **Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer 5.0.0**

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
$ git commit -m "first commit"

# Definindo branch principal
$ git branch -M main

# Enviando para repositório remoto
$ git remote add origin https://github.com/srgeverson/WebAPIPedidos.git
$ git push -u origin main

# Biblioteca de versionamento
$ dotnet add package Microsoft.AspNetCore.Mvc.Versioning --version 5.0.0

```

## 🎲 Executando a aplicação

```bash

# Variável de ambiente que armazena a URL de conexão com o banco de dados
$ setx CONNECTION_STRING_WebAPIPedidos "Data Source=localhost; Initial Catalog=db_teste;User ID=user_teste;Password=12345678;Application Name=WebAPIPedidos;" /M

# Clonando o projeto
$ git clone https://github.com/srgeverson/WebAPIPedidos.git

# Entrando na pasta criada
$ cd WebAPIPedidos

# Restaurando/Instalando dependências
$ dotnet restore "./WebAPIPedidos.csproj"

# Executando aplicação
$ dotnet run --urls=https://localhost:44326

# Acessando swagger
$ https://localhost:44326/swagger/index.html

# Gerando publicação da aplicação
$ dotnet publish "WebAPIPedidos.csproj" -c Release -o /app/publish

# Criando a imagem docker
$ docker build -t WebAPIPedidos .

# Executando imagem docker
$ docker run -p 8080:80 WebAPIPedidos

# Executando docker compose
$ docker-compose up --build

```

## 👨‍💻 Equipe de Desenvolvimento

* **Geverson Souza** - [Geverson Souza](https://www.linkedin.com/in/srgeverson/)

## ✒️ Autor

* **Geverson Souza** - [Geverson Souza](https://www.linkedin.com/in/srgeverson/)