FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CSharpAdvanced.Dynamic.Api/CSharpAdvanced.Dynamic.Api.csproj", "CSharpAdvanced.Dynamic.Api/"]
RUN dotnet restore "CSharpAdvanced.Dynamic.Api/CSharpAdvanced.Dynamic.Api.csproj"
COPY . .
WORKDIR "/src/CSharpAdvanced.Dynamic.Api"
RUN dotnet build "CSharpAdvanced.Dynamic.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CSharpAdvanced.Dynamic.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CSharpAdvanced.Dynamic.Api.dll"]
