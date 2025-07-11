# ShoppingCart API

API para simular um carrinho de compras, com funcionalidades de gerenciamento de clientes, produtos, pedidos, carrinho e autenticação JWT.

## Tecnologias

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server (Docker)
- Identity + JWT para autenticação
- Docker & Docker Compose
- AutoMapper

## Funcionalidades

- CRUD de Clientes, Produtos, Endereços de Entrega
- Gestão de Carrinho de Compras por cliente
- Criação e gestão de Pedidos (admin e cliente)
- Autenticação e registro de usuários com JWT
- Validação e tratamento de exceções centralizado

## Rodando o projeto com Docker

### Requisitos

- Docker instalado
- Docker Compose instalado

### Passos

1. Clone o repositório:
   ```bash
   git clone https://github.com/seuusuario/shoppingcart-api.git
   cd shoppingcart-api
Ajuste a string de conexão no docker-compose.yml se necessário (senha do SQL Server etc).

Execute o Docker Compose para subir a API e o banco SQL Server:

## Rodando o projeto com Docker

### Requisitos

- Docker instalado
- Docker Compose instalado

### Passos

1. Ajuste a string de conexão no `docker-compose.yml` se necessário (senha do SQL Server etc).

2. Execute o Docker Compose para subir a API e o banco SQL Server:
   ```bash
   docker-compose up --build
A API ficará disponível em http://localhost:5000 

Configuração do JWT
No appsettings.json você deve configurar a seção:

json
Copiar
Editar
"Jwt": {
  "Key": "SuaChaveSuperSecretaAqui",
  "Issuer": "https://localhost:5000",
  "Audience": "https://localhost:5000"
}
Observações
Use o Swagger para testar os endpoints e gerar tokens JWT para autenticação.

As rotas que requerem autenticação têm o atributo [Authorize].

A string de conexão padrão usa o container SQL Server no Docker.

Contato
Patrick Mendes — Mendespatrick720@gmail.com
