FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CSharpAdvanced.Api/CSharpAdvanced.Api.csproj", "CSharpAdvanced.Api/"]
RUN dotnet restore "CSharpAdvanced.Api/CSharpAdvanced.Api.csproj"
COPY . .
WORKDIR "/src/CSharpAdvanced.Api"
RUN dotnet build "CSharpAdvanced.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CSharpAdvanced.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CSharpAdvanced.Api.dll"]
