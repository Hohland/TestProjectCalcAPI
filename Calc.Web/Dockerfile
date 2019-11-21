FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Calc.Web/Calc.Web.csproj", "Calc.Web/"]
RUN dotnet restore "Calc.Web/Calc.Web.csproj"
COPY . .
WORKDIR "/src/Calc.Web"
RUN dotnet build "Calc.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Calc.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Calc.Web.dll"]