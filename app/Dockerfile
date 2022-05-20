# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
FROM ubuntu:20.04

RUN apt update && apt -y install wget
RUN wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
RUN dpkg -i packages-microsoft-prod.deb
RUN rm packages-microsoft-prod.deb
RUN apt update
RUN apt install -y apt-transport-https
RUN apt-get install -y dotnet-sdk-6.0
RUN apt install -y aspnetcore-runtime-6.0
RUN apt-get -y install systemctl
RUN adduser --disabled-password --gecos '' nonrootuser
WORKDIR /app

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out
RUN apt-get update && apt-get install -y procps

COPY ./privilege-separation-api.service /etc/systemd/system/privilege-separation-api.service
RUN systemctl daemon-reload
RUN systemctl enable privilege-separation-api
# ENTRYPOINT ["systemctl start privilege-separation-api"]

EXPOSE 8080
# CMD dotnet out/Monitor.dll
