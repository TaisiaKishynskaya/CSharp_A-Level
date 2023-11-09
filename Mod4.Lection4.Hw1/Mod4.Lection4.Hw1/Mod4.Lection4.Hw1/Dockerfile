FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./Mod4.Lection4.Hw1.csproj", "/src/Mod4.Lection4.Hw1/"]
RUN dotnet restore "Mod4.Lection4.Hw1/Mod4.Lection4.Hw1.csproj"


WORKDIR "/src/Mod4.Lection4.Hw1"
COPY . .


FROM build AS publish
RUN dotnet publish "Mod4.Lection4.Hw1.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mod4.Lection4.Hw1.dll"]