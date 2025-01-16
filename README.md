# Maxima.ProManager
 
1. Desafio tecnico para demonstrar conhecimento em .Net Core API e Angular.
2. Segue o link do desafio: [Exercico prático - BackendC#.txt](https://github.com/augusto95cesar/Maxima.ProManager/blob/master/aDocs/Exercico%20pr%C3%A1tico%20-%20BackendC%23.txt)

---

## **Pré-requisitos**
1. **Windows** com o IIS habilitado:
   - Instale o IIS via "Ativar ou Desativar Recursos do Windows".
2. **.NET Hosting Bundle**:
   - Faça o download do pacote em [Microsoft .NET Hosting Bundle](https://dotnet.microsoft.com/download/dotnet). 
3. **Install Iss no windowns**
    - crie o site da api
    - configure o clr pool para versão 'sem código gerenciado'
4. **Install Postgress SQL**
    - baixe e instale 14 ou superior o [Postgress 14](https://www.enterprisedb.com/postgresql-tutorial-resources-training-1?uuid=140fdf8e-34e6-4b1b-ac32-532e5ac826c4&campaignId=Product_Trial_PostgreSQL_14)
    - crie o banco de dados com o nome ProManagerDB**
    - execute o scrit para criar as tabelas [script postgres](https://github.com/augusto95cesar/Maxima.ProManager/blob/master/aDocs/Script.txt)

---
## Configurar o Projeto .NET Core API no IIS

1. Este guia descreve como hospedar uma aplicação **.NET Core API** no **IIS (Internet Information Services)**.

---
 
## **1. Publicando o Projeto .NET Core API**
### Passo 1: Publicar a API
1. Abra o terminal no diretório do projeto API.
2. Execute o comando:
    ```bash
    dotnet publish -c Release -o ./publish
    ```
3. copie todos os arquivos da pasta publish e leve para a pasta do site configurado no iis acima.
4. *verifique o swagger*

 ---
 ### Passo 2: Configurar o appsettings.json
 1. abra o arquivo e edite a string de conexao.     
    ```bash
    "ConnectionStrings": {
        "DefaultConnection": "Host=localhost; Database=ProManagerDB; Username=postgres; Password=seu-pass"
    }
    ```
2. abra o arquivo e edite a strig de configuração de cors, com a url do app angular.
    ```bash
    "CorsSettings": {
        "AllowedOrigins": [ "http://localhost:4200", "https:localhost:4200" ]
    }
    ```
---
 # Publicando um App Angular no IIS

Este guia irá ajudá-lo a publicar um aplicativo Angular em um servidor IIS (Internet Information Services).

## Requisitos

Antes de começar, você precisa garantir que tem os seguintes pré-requisitos:

- [Node.js](https://nodejs.org/) instalado no seu sistema.
- [Angular CLI](https://angular.io/cli) instalado globalmente.
- Servidor IIS configurado e em funcionamento.
---

### Passo 1: Construir o Aplicativo Angular
1. abra o projeto e vá no diretorio *"Maxima.ProManager\FrontEnd\ProManager.App\src\config\"*  [api-cofig.ts](https://github.com/augusto95cesar/Maxima.ProManager/blob/master/FrontEnd/ProManager.App/src/config/api-config.ts)
2. abra com um editor o arquivo *api-config.ts*
3. edite a variavel BASE_URL para url da api que você configurou no iis.
```bash 
export const API_CONFIG = {
    BASE_URL: 'http://api.pro.manager.com' 
  };
```

### Passo 2: Construir o Aplicativo Angular

1. Abra o terminal ou prompt de comando.
2. Navegue até o diretório do seu projeto Angular.
3. Execute o seguinte comando para gerar uma versão de produção do seu aplicativo:
```bash 
ng build --prod
```
4. copie os aqurivos gerados no build para o diretorio do site configurado para o app angular no iis
5. verifique a url do site se está funcionando corretamente.
---


 
