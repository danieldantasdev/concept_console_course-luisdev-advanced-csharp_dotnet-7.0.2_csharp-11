FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CSharpAdvanced.AsyncAwait/CSharpAdvanced.AsyncAwait.csproj", "CSharpAdvanced.AsyncAwait/"]
RUN dotnet restore "CSharpAdvanced.AsyncAwait/CSharpAdvanced.AsyncAwait.csproj"
COPY . .
WORKDIR "/src/CSharpAdvanced.AsyncAwait"
RUN dotnet build "CSharpAdvanced.AsyncAwait.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CSharpAdvanced.AsyncAwait.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CSharpAdvanced.AsyncAwait.dll"]
