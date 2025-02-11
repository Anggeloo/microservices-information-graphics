# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /serviceinformationgraphics

EXPOSE 90
EXPOSE 4000

COPY ./*.csproj ./
RUN dotnet restore 

COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:8.0 
WORKDIR /serviceinformationgraphics
COPY --from=build /serviceinformationgraphics/out .
ENTRYPOINT ["dotnet", "microservices-information-graphics.dll"]
