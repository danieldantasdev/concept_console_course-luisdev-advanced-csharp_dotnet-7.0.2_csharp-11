FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CSharpAdvanced.Tpl/CSharpAdvanced.Tpl.csproj", "CSharpAdvanced.Tpl/"]
RUN dotnet restore "CSharpAdvanced.Tpl/CSharpAdvanced.Tpl.csproj"
COPY . .
WORKDIR "/src/CSharpAdvanced.Tpl"
RUN dotnet build "CSharpAdvanced.Tpl.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CSharpAdvanced.Tpl.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CSharpAdvanced.Tpl.dll"]
