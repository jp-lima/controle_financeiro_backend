# Controle Financeiro — Back-end

API REST para o sistema de controle de gastos residenciais, desenvolvida como parte de um desafio técnico. Responsável pelo cadastro de pessoas, cadastro de transações (receitas/despesas) e fornecimento dos dados usados na consulta de totais.

## Tecnologias

- .NET 10 / ASP.NET Core Web API
- Entity Framework Core 8 (Npgsql provider)
- PostgreSQL (hospedado no Render)
- Swashbuckle (Swagger/OpenAPI)

## Pré-requisitos

- .NET SDK 10
- Uma instância PostgreSQL (local ou remota)

## Como rodar localmente

```bash
git clone https://github.com/jp-lima/controle_financeiro_backend
cd controle_financeiro_backend

# configura a connection string sem expor no appsettings.json
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:AppDbConnectionString" "Host=...;Port=5432;Database=...;Username=...;Password=..."

dotnet ef database update   # aplica as migrations
dotnet run
```

A API sobe por padrão em `https://localhost:5001` (ou porta configurada em `Properties/launchSettings.json`). Swagger disponível em `/swagger/index.html`.

## Configuração

A connection string do PostgreSQL é lida de `ConnectionStrings:AppDbConnectionString`, via:
- `dotnet user-secrets` em ambiente local (recomendado, não versionado)
- Variável de ambiente `ConnectionStrings__AppDbConnectionString` em produção (Render)

## Estrutura do projeto

Controller/ # endpoints da API (UsuariosController, TransacaoController)
Data/ # AppDbContext (EF Core)
DTO/ # objetos de entrada/saída (Create*, Get*)
Enums/ # TipoTransacao (Receita/Despesa)
Models/ # entidades do banco (Usuario, Transacao)
Migrations/ # histórico de migrations do EF Core


## Modelo de dados

**Usuario**
| Campo | Tipo   | Observação                  |
|-------|--------|-------------------------------|
| Id    | string | Guid, gerado automaticamente |
| Nome  | string |                                |
| Idade | int    |                                |

**Transacao**
| Campo     | Tipo           | Observação                      |
|-----------|----------------|-----------------------------------|
| Id        | string         | Guid, gerado automaticamente     |
| Descricao | string         |                                    |
| Tipo      | TipoTransacao  | `Receita` ou `Despesa` (enum)    |
| Valor     | float          |                                    |
| IdUsuario | string         | FK para Usuario                  |

## Endpoints

### Usuários

| Método | Rota                | Descrição                                              |
|--------|---------------------|-----------------------------------------------------------|
| POST   | `/api/Usuarios`     | Cria um usuário                                          |
| GET    | `/api/Usuarios`     | Lista usuários, cada um com suas transações aninhadas   |
| GET    | `/api/Usuarios/{id}`| Busca um usuário por Id                                  |
| DELETE | `/api/Usuarios/{id}`| Remove um usuário **e todas as suas transações**        |

### Transações

| Método | Rota               | Descrição                          |
|--------|--------------------|--------------------------------------|
| POST   | `/api/Transacao`   | Cria uma transação                  |
| GET    | `/api/Transacao`   | Lista todas as transações           |

Não há endpoints de edição/deleção de transação, conforme especificação do desafio.

## Regras de negócio

- **Exclusão em cascata**: ao deletar um usuário, todas as transações vinculadas a ele são removidas na mesma operação.
- **Restrição de menor de idade**: usuários com menos de 18 anos só podem ter transações do tipo `Despesa`. Uma tentativa de cadastrar `Receita` para um menor retorna `400 Bad Request`.
- **Consulta de totais**: o backend não expõe um endpoint dedicado a totais — o front-end consome `GET /api/Usuarios` (que já traz as transações de cada pessoa) e calcula receita total, despesa total e saldo, tanto por pessoa quanto o total geral.

## Deploy

- API: https://controle-financeiro-backend-kyde.onrender.com
- Swagger: https://controle-financeiro-backend-kyde.onrender.com/swagger/index.html
- Banco de dados: PostgreSQL hospedado no Render
