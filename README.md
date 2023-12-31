# <a href="https://webapipedidos.azurewebsites.net/swagger/index.html">WebAPIPedidos - API de Gerenciamento de Pedidos.</a>

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
* **Microsoft.EntityFrameworkCore.Tools 7.0.10**
* **IdentityServer4 4.1.2**

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

# Gerar o arquivo de migração SQL
$ Add-Migration v1

# Executar a migração anteriormente gerada
$ Update-Database

# Generate a self-signed certificate.
$ openssl req -x509 -newkey rsa:4096 -keyout localhost.key -out localhost.crt -subj "/CN=localhost" -addext "subjectAltName=DNS:localhost,DNS:api-webapipedidos,DNS:webapipedidos"
$ openssl pkcs12 -export -in localhost.crt -inkey localhost.key -out localhost.pfx -name "Adding API Resources to IdentityServer 5"

# Import the self-signed certificate.
$ certutil -f -user -importpfx Root localhost.pfx

# Variável de ambiente que armazena nome do arquivo do certificado
$ setx CERTIFICATE_NAME "NOME_DO_ARQUIVO_AQUI"

# Variável de ambiente que armazena senha do arquivo do certificado
$ setx CERTIFICATE_PASSWORD "SENHA_DO_ARQUIVO_AQUI"

# Variável de ambiente que armazena URL do servidor de autorização
$ setx URL_AUTHORIZE "URL_DO_SERVIDOR_AQUI"

```

## 🎲 Executando a aplicação

```bash

# Clonando o repositório
$ git clone https://github.com/srgeverson/WebAPIPedidos.git

# Entrando na pasta criada
$ cd WebAPIPedidos/

# Executanto a aplicação localmente
$ dotnet run

# Abrindo o swagger da aplicação
$ start https://localhost:44370/swagger/index.html

# Publicar automaticamente se integrado com Azure DevOps basta fazer o merge ou push com a branch main
$ git checkout -f -d && git checkout main && git fetch origin && git pull && git merge origin/develop

# Para abrir o swagger da aplicação na Azure DevOps
$ start https://webapipedidos.azurewebsites.net/swagger/index.html 

#
$ https://localhost:44370/.well-known/openid-configuration

```

## 👨‍💻 Equipe de Desenvolvimento

* **Geverson Souza** - [Geverson Souza](https://www.linkedin.com/in/srgeverson/)

## ✒️ Autor

* **Geverson Souza** - [Geverson Souza](https://www.linkedin.com/in/srgeverson/)

## 📌 Versão 1.0.0

É utilizado o [Github](https://github.com/) para controle de versão.
