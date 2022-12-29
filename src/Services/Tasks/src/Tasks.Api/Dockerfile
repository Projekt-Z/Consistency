FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Tasks/src/Tasks.Api/Tasks.Api.csproj", "Tasks.Api/"]
RUN dotnet restore "src/Services/Tasks/src/Tasks.Api/Tasks.Api.csproj"
COPY . .
WORKDIR "/src/Tasks.Api"
RUN dotnet build "Tasks.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tasks.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tasks.Api.dll"]
