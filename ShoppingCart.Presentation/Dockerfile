# Imagem base para rodar a aplicação .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Imagem com o SDK para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia o arquivo .csproj e restaura as dependências
COPY ["ShoppingCart.Presentation/ShoppingCart.Presentation.csproj", "ShoppingCart.Presentation/"]
RUN dotnet restore "ShoppingCart.Presentation/ShoppingCart.Presentation.csproj"

# Copia todo o resto do código fonte
COPY . .
WORKDIR "/src/ShoppingCart.Presentation"

# Compila o projeto
RUN dotnet build "ShoppingCart.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publica o projeto
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ShoppingCart.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Estágio final, usa a imagem base (menor)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShoppingCart.Presentation.dll"]
