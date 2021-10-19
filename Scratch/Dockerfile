FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Scratch/Scratch.csproj", "Scratch/"]
RUN dotnet restore "Scratch/Scratch.csproj"
COPY . .
WORKDIR "/src/Scratch"
RUN dotnet build "Scratch.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Scratch.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Scratch.dll"]
