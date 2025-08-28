# LibraryManager

Esta é uma aplicação moderna, robusta e escalável construída com **.NET 8**, utilizando as mais avançadas práticas de arquitetura de software para garantir um código de alta qualidade, manutenível e performático.

## 🏛️ Arquitetura e Design

A fundação do projeto foi meticulosamente planejada para garantir uma estrutura limpa e um design que reflete as melhores práticas do mercado.

### **Clean Architecture**
A aplicação segue rigorosamente os princípios da **Clean Architecture**, garantindo uma separação clara de responsabilidades entre as camadas (Core, Application, Infrastructure, Persistence, Presentation). A comunicação entre as camadas é feita através de interfaces, promovendo baixo acoplamento e facilitando a testabilidade e a manutenção.

### **Domain-Driven Design (DDD)**
O coração da aplicação é o seu domínio, modelado com as melhores práticas do DDD:
- **Value Objects**: Para representar conceitos imutáveis e com validações intrínsecas.
- **Padrões de Organização**: O código do domínio é altamente organizado, refletindo fielmente as regras de negócio.
- **Domain Errors**: Centralização e padronização das mensagens de erro do domínio para garantir consistência e clareza.

---
## ✨ Features Principais

### **API RESTful e Bem Documentada**
A API foi construída seguindo o padrão REST e está completamente documentada com **Swagger (OpenAPI)**.
- **Paginação**: Para otimizar a performance em consultas que retornam grandes volumes de dados.
- **Padronização de Retornos**: Uso de `ProblemDetails` e um `Result Pattern` bem definido para padronizar as respostas de sucesso e erro, tornando a API previsível e fácil de ser consumida.

### **Autenticação e Autorização com JWT**
Sistema de segurança robusto utilizando **JSON Web Tokens (JWT)**.
- **Refresh Tokens**: Implementação de refresh tokens para manter sessões ativas de forma segura.
- **Senhas Seguras**: Geração e verificação de senhas utilizando algoritmos de hash modernos para garantir a segurança das credenciais.

### **CQRS com Mediator**
O padrão **CQRS (Command Query Responsibility Segregation)** foi implementado com a biblioteca **MediatR**, separando operações de escrita (Commands) das de leitura (Queries).
- **Behaviors Avançados**: O pipeline do MediatR foi enriquecido com behaviors para:
    - **Logging**: Rastreabilidade completa da execução.
    - **Validação**: Integração com **FluentValidator** para validações automáticas.
    - **Resiliência de Transações**: Uso de políticas do Polly para garantir a consistência.

### **Background Jobs Resilientes**
Um dos pontos mais fortes da aplicação é seu sistema de tarefas em segundo plano.
- **Filas e Agendamentos**: Suporte para jobs em fila (queues) e agendados (scheduled jobs).
- **Alta Resiliência**: Os jobs foram construídos para serem extremamente resilientes, com **reagendamentos automáticos em caso de falhas**, garantindo a execução de tarefas críticas.

### **Envio de E-mails em Segundo Plano**
- **FluentEmail**: Utilização para uma construção de e-mails fluente e limpa via SMTP.
- **Filas de E-mail**: O envio é desacoplado da requisição principal. Os e-mails são enfileirados no banco de dados e processados por um background job, garantindo que a experiência do usuário não seja impactada.

---
## 🔧 Qualidade de Código e Padrões

### **Injeção de Dependência (IoC)**
A aplicação faz uso exemplar do contêiner de Injeção de Dependência nativo do .NET. **Todos os serviços, repositórios e outras abstrações são registrados e resolvidos via IoC**, resultando em um código desacoplado e altamente testável. A configuração dos serviços da camada de infraestrutura é um excelente exemplo da aplicação madura deste padrão.

### **Result Pattern para um Código Mais Limpo**
Um grande diferencial do projeto é a adoção do **Result Pattern**. Este padrão é utilizado para o retorno de métodos de serviço, **evitando o lançamento de exceções para controle de fluxo**. Isso resulta em um código mais claro, previsível e com uma distinção explícita entre sucesso e falha, melhorando drasticamente a legibilidade.

### **Resiliência com Polly**
A resiliência é um pilar desta aplicação. A biblioteca **Polly** é amplamente utilizada para criar políticas de `Retry`, com destaque para:
- **Transações de Banco de Dados Resilientes**: Garantindo que operações críticas sejam concluídas mesmo sob condições adversas.
- **Comunicação com Serviços Externos**.

### **Tratamento de Exceções Centralizado**
- **Global Exception Handlers**: Middlewares para tratamento de exceções de forma centralizada, convertendo erros inesperados e de validação em respostas `ProblemDetails` padronizadas.

### **Logging Estruturado com Serilog**
Toda a aplicação é instrumentada com logging estruturado utilizando **Serilog**, gerando logs detalhados para requisições, execução de casos de uso, erros e background jobs.

### **Acesso a Dados com EF Core**
- **Repository Pattern & Unit of Work**: Abstração da camada de dados para encapsular a lógica de consulta e garantir a atomicidade das transações com o Unit of Work.
- **Melhores Práticas**: As entidades do EF Core foram configuradas seguindo as melhores práticas para performance e manutenibilidade.

### **Configurações com Options Pattern**
As configurações da aplicação são fortemente tipadas utilizando o **Options Pattern**, tornando o acesso a elas seguro e organizado.

---
## 🚀 Tecnologias Utilizadas

- **Framework**: .NET 8
- **Banco de Dados**: Microsoft SQL Server
- **Containerização**: Docker e Docker Compose
- **Bibliotecas Principais**:
  - MediatR
  - FluentValidation
  - Serilog
  - Quartz
  - Polly
  - Entity Framework Core 8
  - FluentEmail
  - JWT Bearer Authentication
  - HybridCache

---
## 🏁 Como Executar o Projeto

1. **Pré-requisitos**:
   - .NET 8 SDK
   - Docker Desktop

2. **Configuração**:
   - Clone o repositório.
   - Navegue até a pasta do projeto.
   - Configure suas variáveis de ambiente ou o arquivo `appsettings.Development.json` com as credenciais necessárias.

3. **Executando com Docker Compose** (Recomendado):
   - Na raiz do projeto, execute o comando abaixo para iniciar a API e o banco de dados:
     ```bash
     docker-compose up -d
     ```

4. **Executando Localmente**:
   - Execute as migrations do Entity Framework:
     ```bash
     dotnet ef database update
     ```
   - Inicie a aplicação:
     ```bash
     dotnet run --project LibraryManager.API
     ```

A API estará disponível em `http://localhost:5001` e a documentação do Swagger em `http://localhost:5001/swagger`.
