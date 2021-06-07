# BomTrato Teste

## Descrição do Projeto
<p align="center">Esse projeto tem como objetivo trazer uma plataforma de manutenção de de aprovadores, escritórios e principalmente os processos que seram cudiados pela empresa BomTrato</p>

<h4 align="center"> 
  :white_check_mark: Bom Trato Teste : Finalizado :white_check_mark:
</h4>


### Features

- [x] Cadastro de Aprovadores
- [x] Cadastro de Escritórios
- [x] Cadastro de Processos
- [x] Chat

### 📋 Pré-requisitos

Antes de começar, você vai precisar ter instalado em sua máquina as seguintes ferramentas:
[Git](https://git-scm.com), [Node.js](https://nodejs.org/en/), 
[Angular 9](https://nodejs.org/en/), 
[Dotnet Core](https://dotnet.microsoft.com/), 
[Sql Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads), 
[Docker](https://www.docker.com/products/docker-desktop). 
Além disto é bom ter um editor para trabalhar com o código como [VSCode](https://code.visualstudio.com/) e [Visual Studio](https://visualstudio.microsoft.com/pt-br/downloads/)

### 🎲 Rodando o Front End

```
# Clone este repositório
$ git clone <https://github.com/FelipeZucchiNeves/BomTrato.git>

# Acesse a pasta do projeto no terminal/cmd
$ cd BomTratoFront

# Instale as dependências
$ npm install

# Execute a aplicação em modo de desenvolvimento
$ ng s

# O servidor inciará na porta:4200 - acesse <http://localhost:4200>
```

### 🎲 Rodando o Back End (servidor)

```bash
# Clone este repositório
$ git clone <https://github.com/FelipeZucchiNeves/BomTrato.git>

# Acesse a pasta do projeto
Abra a solution do projeto BomTratoBack.sln no Visual Studio

# Execute a aplicação em modo de desenvolvimento
$ crtl + F5

# O servidor inciará na porta:44394 - acesse <https://localhost:44394/swagger>
```

### 🎲 Rodando com Docker

```bash
# Clone este repositório
$ git clone <https://github.com/FelipeZucchiNeves/BomTrato.git>

# Acesse a pasta do projeto e 
Abra a solution do projeto BomTratoBack.sln no Visual Studio

# Acesse as classes
..\Bomtrato\BomTratoBack\BomTratoApi\Configurations\ApiIdentityConfig.cs
..\Bomtrato\BomTratoBack\BomTratoApi\Configurations\DatabaseConfig.cs
Altere a connectionString para -> "DockerConnection"

# Acesse a pasta do projeto no terminal/cmd
$ cd BomTratoBack

# Builde o projeto com o docker.
$ docker-compose build

# Suba os containeres.
$ docker-compose up

# O servidor inciará na 
$ Back -> porta:44394 - acesse <https://localhost:44394/swagger>
$ Front -> porta:4200 - acesse <https://localhost:4200/swagger>
$ Front -> porta:4201 - acesse <https://localhost:4201/swagger>
$ Front -> porta:4202 - acesse <https://localhost:4202/swagger>
```


## ⚙️ Executando os testes integração

```
# Clone este repositório
$ git clone <https://github.com/FelipeZucchiNeves/BomTrato.git>

# Acesse a pasta do projeto e 
Abra a solution do projeto BomTratoBack.sln no Visual Studio

# Acesse as classes
..\Bomtrato\BomTratoBack\BomTratoApi\Configurations\ApiIdentityConfig.cs
..\Bomtrato\BomTratoBack\BomTratoApi\Configurations\DatabaseConfig.cs
Altere a connectionString para -> "DefaultConnection"

# Abra o arquivo BomTratoTests seu SQL Server Management Studio
$ Rode todos os scripts de criação das tabelas

# Roder os testes de integração.
```

## 🎲 Chat Bom Trato

```
# Clone este repositório
$ git clone <https://github.com/FelipeZucchiNeves/BomTrato.git>

# Execute o Front-End da aplicação em modo de desenvolvimento
$ ng s --port 4200
$ ng s --port 4201

# Execute o Back-End em modo de desenvolvimento

# Se for o primeiro uso, siga os passos na seção de primeiro uso abaixo.

# Faça Login
$ Logue na aplicação com usuários distintos.

# Acesse o Chat no menu superior da aplicação web.

# Converse a vontade.
```

### <h2><strog>Primeiro uso</strong></h2>
```
$ O primeiro Aprovador deve ser criado via API

$ EX.
      curl -X 'POST' \
        'https://localhost:44394/aprovador-management' \
        -H 'accept: */*' \
        -H 'Content-Type: application/json' \
        -d '{
        "name": "SeuNome",
        "lastName": "SeuSobreNome",
        "email": "nomesobrenome@bt.com",
        "birthDate": "1990-06-07"
      }'

Faça o registo dentro da aplicação

Efetue o Login e Pronto você poderá utilizar todas as funcionalidades!
```

## 🛠️ Construído com

As seguintes ferramentas foram usadas na construção do projeto:

- [Dotnet](https://dotnet.microsoft.com/)
- [Node.js](https://nodejs.org/en/)
- [Angular](https://angular.io/)
- [Docker](https://www.docker.com/products/docker-desktop)
- [Sql Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)

### Autor
---

[![Linkedin Badge](https://img.shields.io/badge/-Felipe%20Neves-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/felipe-neves-a9b55116a/)](https://www.linkedin.com/in/felipe-neves-a9b55116a/)
