#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PivotIt.Web/PivotIt.Web.csproj", "PivotIt.Web/"]
COPY ["PivotIt.Infrastructure/PivotIt.Infrastructure.csproj", "PivotIt.Infrastructure/"]
COPY ["PivotIt.Core/PivotIt.Core.csproj", "PivotIt.Core/"]
COPY ["PivotIt.Search/PivotIt.Search.csproj", "PivotIt.Search/"]
RUN dotnet restore "PivotIt.Web/PivotIt.Web.csproj"
COPY . .
WORKDIR "/src/PivotIt.Web"
RUN dotnet build "PivotIt.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PivotIt.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PivotIt.Web.dll"]