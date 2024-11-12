# To-Do List API

Esta API RESTful permite gerenciar usuários, listas de tarefas e tarefas individuais, com autenticação baseada em tokens. As principais funcionalidades incluem operações CRUD completas para usuários, listas e tarefas, bem como recursos como finalização de tarefas e busca personalizada.

## Funcionalidades Principais

  - **Gestão de Usuários:** Crie, atualize, exclua e recupere usuários.
  - **Gestão de Listas de Tarefas:** Crie, atualize, exclua e recupere listas de tarefas.
  - **Gestão de Tarefas:** Crie, atualize, exclua e recupere tarefas individuais. Marque tarefas como concluídas.
  - **Busca Personalizada:** Busque usuários, listas e tarefas por nome, email e status.

## Endpoints

### Usuários

- **POST** `/api/v1/users/create`: Cria um novo usuário.
- **PUT** `/api/v1/users/update`: Atualiza os dados de um usuário.
- **DELETE** `/api/v1/users/delete/{id}`: Deleta um usuário.
- **GET** `/api/v1/users/get/{id}`: Obtém os dados de um usuário pelo ID.
- **GET** `/api/v1/users/getall`: Lista todos os usuários.
- **GET** `/api/v1/users/getbyemail/{email}`: Busca um usuário pelo email.
- **GET** `/api/v1/users/searchbyname/{name}`: Busca usuários por nome.

### Listas de Tarefas

- **POST** `/api/v1/assignmentlist/create`: Cria uma nova lista de tarefas.
- **PUT** `/api/v1/assignmentlist/update`: Atualiza uma lista de tarefas.
- **DELETE** `/api/v1/assignmentlist/delete/{id}`: Deleta uma lista de tarefas.
- **GET** `/api/v1/assignmentlist/getbyid/{id}`: Obtém uma lista de tarefas pelo ID.
- **GET** `/api/v1/assignmentlist/getall`: Lista todas as listas de tarefas.
- **GET** `/api/v1/assignmentlist/searchbylistname/{name}`: Busca listas de tarefas pelo nome.

### Tarefas

- **POST** `/api/v1/assignment/create`: Cria uma nova tarefa.
- **PUT** `/api/v1/assignment/update`: Atualiza uma tarefa.
- **DELETE** `/api/v1/assignment/delete/{id}`: Deleta uma tarefa.
- **GET** `/api/v1/assignment/assignmentbyid/{id}`: Obtém uma tarefa pelo ID.
- **GET** `/api/v1/assignment/allassignment`: Lista todas as tarefas.
- **GET** `/api/v1/assignment/getunfinished`: Lista as tarefas inacabadas.
- **PATCH** `/api/v1/assignment/{id}/conclude`: Conclui uma tarefa.

### Autenticação

- **POST** `/api/v1/auth/login`: Realiza o login e retorna um token de autenticação.

## Requisitos

- **.NET 6+**
- **Banco de dados (SQL Server ou outro)**
- **JWT para autenticação**

## Instalação

1. Clone o repositório:
   ```bash
   git clone https://github.com/usuario/todolist-api.git
   ```
2. Navegue até o diretório do projeto:
   ```bash
   cd todolist-api
   ```
3. Instale as dependências necessárias:
   ```bash
   dotnet restore
   ```
4. Configure a string de conexão no arquivo `appsettings.json`.
5. Execute as migrações do banco de dados:
   ```bash
   dotnet ef database update
   ```
6. Inicie a aplicação:
   ```bash
   dotnet run
   ```
