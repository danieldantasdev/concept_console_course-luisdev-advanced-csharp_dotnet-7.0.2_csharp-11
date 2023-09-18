FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CSharpAdvanced.Dynamic/CSharpAdvanced.Dynamic.csproj", "CSharpAdvanced.Dynamic/"]
RUN dotnet restore "CSharpAdvanced.Dynamic/CSharpAdvanced.Dynamic.csproj"
COPY . .
WORKDIR "/src/CSharpAdvanced.Dynamic"
RUN dotnet build "CSharpAdvanced.Dynamic.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CSharpAdvanced.Dynamic.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CSharpAdvanced.Dynamic.dll"]
