FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CSharpAdvanced.Oop/CSharpAdvanced.Oop.csproj", "CSharpAdvanced.Oop/"]
RUN dotnet restore "CSharpAdvanced.Oop/CSharpAdvanced.Oop.csproj"
COPY . .
WORKDIR "/src/CSharpAdvanced.Oop"
RUN dotnet build "CSharpAdvanced.Oop.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CSharpAdvanced.Oop.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CSharpAdvanced.Oop.dll"]
