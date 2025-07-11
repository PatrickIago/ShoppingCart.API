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

bash
Copiar
Editar
docker-compose up --build
A API ficará disponível em http://localhost:5000 (ou porta que você configurou).

Acesse o Swagger para documentação e testes em:
http://localhost:5000/swagger

Endpoints Principais
Autenticação
POST /api/auth/register — Registra um novo usuário

POST /api/auth/login — Realiza login e retorna token JWT

Clientes
GET /api/cliente — Lista clientes

POST /api/cliente — Cria cliente

GET /api/cliente/{id} — Consulta cliente por ID

PUT /api/cliente/{id} — Atualiza cliente

DELETE /api/cliente/{id} — Deleta cliente

Produtos
GET /api/produto — Lista produtos

POST /api/produto — Cria produto (autenticado)

GET /api/produto/{id} — Consulta produto

PUT /api/produto/{id} — Atualiza produto

DELETE /api/produto/{id} — Remove produto

Carrinho
GET /api/clientes/{clienteId}/carrinho — Obter carrinho do cliente

POST /api/clientes/{clienteId}/carrinho/itens — Adicionar item

PUT /api/clientes/{clienteId}/carrinho/itens/{produtoId} — Atualizar item

DELETE /api/clientes/{clienteId}/carrinho/itens/{produtoId} — Remover item

DELETE /api/clientes/{clienteId}/carrinho — Limpar carrinho

Pedidos
POST /api/clientes/{clienteId}/pedidos — Criar pedido a partir do carrinho

GET /api/clientes/{clienteId}/pedidos — Histórico de pedidos

GET /api/clientes/{clienteId}/pedidos/{pedidoId} — Detalhes do pedido

Administração de Pedidos
PATCH /api/admin/pedidos/{pedidoId}/pagar — Marcar pedido como pago

PATCH /api/admin/pedidos/{pedidoId}/enviar — Marcar pedido como enviado

PATCH /api/admin/pedidos/{pedidoId}/cancelar — Cancelar pedido

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

