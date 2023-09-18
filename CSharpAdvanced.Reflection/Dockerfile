FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CSharpAdvanced.Reflection/CSharpAdvanced.Reflection.csproj", "CSharpAdvanced.Reflection/"]
RUN dotnet restore "CSharpAdvanced.Reflection/CSharpAdvanced.Reflection.csproj"
COPY . .
WORKDIR "/src/CSharpAdvanced.Reflection"
RUN dotnet build "CSharpAdvanced.Reflection.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CSharpAdvanced.Reflection.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CSharpAdvanced.Reflection.dll"]
