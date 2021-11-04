# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the MIT License.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Microsoft.Solutions.PatientHub.BatchInference.ChangefeedWatcherWorker/Microsoft.Solutions.PatientHub.BatchInference.ChangefeedWatcherWorker.csproj", "Microsoft.Solutions.PatientHub.BatchInference.ChangefeedWatcherWorker/"]
RUN dotnet restore "Microsoft.Solutions.PatientHub.BatchInference.ChangefeedWatcherWorker/Microsoft.Solutions.PatientHub.BatchInference.ChangefeedWatcherWorker.csproj"
COPY . .
WORKDIR "/src/Microsoft.Solutions.PatientHub.BatchInference.ChangefeedWatcherWorker"
RUN dotnet build "Microsoft.Solutions.PatientHub.BatchInference.ChangefeedWatcherWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microsoft.Solutions.PatientHub.BatchInference.ChangefeedWatcherWorker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microsoft.Solutions.PatientHub.BatchInference.ChangefeedWatcherWorker.dll"]