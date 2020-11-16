FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=caltzone
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/logs.api.pfx
EXPOSE 5000

# Copy everything else and build
COPY . ./
RUN dotnet restore ./CacheAPI/CacheAPI.csproj
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "CacheAPI.dll", "--urls", "https://0.0.0.0:5000"]