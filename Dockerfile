#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Fynd.Api/Fynd.Api.csproj", "Fynd.Api/"]
RUN dotnet restore "Fynd.Api/Fynd.Api.csproj"
COPY . .
WORKDIR "/src/Fynd.Api"
RUN dotnet build "Fynd.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fynd.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Fynd.Api.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Fynd.Api.dll