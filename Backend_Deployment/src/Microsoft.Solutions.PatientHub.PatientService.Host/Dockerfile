# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the MIT License.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Microsoft.Solutions.PatientHub.PatientService.Host/Microsoft.Solutions.PatientHub.PatientService.Host.csproj", "Microsoft.Solutions.PatientHub.PatientService.Host/"]
RUN dotnet restore "Microsoft.Solutions.PatientHub.PatientService.Host/Microsoft.Solutions.PatientHub.PatientService.Host.csproj"
COPY . .
WORKDIR "/src/Microsoft.Solutions.PatientHub.PatientService.Host"
RUN dotnet build "Microsoft.Solutions.PatientHub.PatientService.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microsoft.Solutions.PatientHub.PatientService.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microsoft.Solutions.PatientHub.PatientService.Host.dll"]