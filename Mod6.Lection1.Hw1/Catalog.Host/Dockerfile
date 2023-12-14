FROM build AS publish
COPY . /src
WORKDIR /src/Catalog/Catalog.Host
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Catalog.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM nginx
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Host.dll"]
