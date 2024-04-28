# Sử dụng base image từ Microsoft để build và publish
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Sao chép csproj và khôi phục các gói nuget
COPY *.csproj ./
RUN dotnet restore

# Sao chép toàn bộ dự án và build cho production
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "QuangCaoDieuHoaAPI.dll", "--urls", "http://+:51926"]
