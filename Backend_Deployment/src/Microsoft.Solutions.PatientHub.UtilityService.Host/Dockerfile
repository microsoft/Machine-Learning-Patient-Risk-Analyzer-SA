#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Microsoft.Solutions.PatientHub.UtilityService.Host/Microsoft.Solutions.PatientHub.UtilityService.Host.csproj", "Microsoft.Solutions.PatientHub.UtilityService.Host/"]
RUN dotnet restore "Microsoft.Solutions.PatientHub.UtilityService.Host/Microsoft.Solutions.PatientHub.UtilityService.Host.csproj"
COPY . .
WORKDIR "/src/Microsoft.Solutions.PatientHub.UtilityService.Host"
RUN dotnet build "Microsoft.Solutions.PatientHub.UtilityService.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microsoft.Solutions.PatientHub.UtilityService.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microsoft.Solutions.PatientHub.UtilityService.Host.dll"]