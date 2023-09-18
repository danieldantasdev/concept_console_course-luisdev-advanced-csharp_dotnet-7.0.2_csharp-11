FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CSharpAdvanced.DelegateEvents/CSharpAdvanced.DelegateEvents.csproj", "CSharpAdvanced.DelegateEvents/"]
RUN dotnet restore "CSharpAdvanced.DelegateEvents/CSharpAdvanced.DelegateEvents.csproj"
COPY . .
WORKDIR "/src/CSharpAdvanced.DelegateEvents"
RUN dotnet build "CSharpAdvanced.DelegateEvents.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CSharpAdvanced.DelegateEvents.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CSharpAdvanced.DelegateEvents.dll"]
