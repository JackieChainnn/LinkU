# LinkU

Recruitment platform for internal use

## Authors

- Trong Toan Nguyen:

      - [Linkedin](#)
      - [Github](#)

- Author 2:

      - [Linkedin](#)
      - [Github](#)

- Author 3:

      - [Linkedin](#)
      - [Github](#)

## Table of Contents

- [LinkU](#linku)
  - [Authors](#authors)
  - [Table of Contents](#table-of-contents)
  - [About](#about)
  - [Getting Started](#getting-started)

## About

This project is a recruitment platform for internal use. It is a platform where people can apply for a job and the company can manage the applications.

## Technologies

### Backend

- [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [SqlServer](https://www.microsoft.com/en-us/sql-server/sql-server-2019)
- [Docker](https://www.docker.com/)

### Frontend

- [Bootstrap](https://getbootstrap.com/)
- [React](https://reactjs.org/)

### Third-party services

- [QRCoder](https://www.nuget.org/packages/QRCoder/)
- [SendGrid](https://sendgrid.com/)
- [Google Analytics](https://analytics.google.com/)
- [Google Maps](https://cloud.google.com/maps-platform/)
- [Google reCAPTCHA](https://www.google.com/recaptcha/about/)
- [Facebook Login](https://developers.facebook.com/docs/facebook-login)
- [Google Login](https://developers.google.com/identity/protocols/oauth2)
- [Microsoft Login](https://docs.microsoft.com/en-us/azure/active-directory/develop/v2-overview)

## Getting Started

      git clone https://github.com/JackieChainnn/LinkU.git

At root folder run following commands

### Configure Database

> Configure connection string for Identity database.For sercurity, you should save this connection string in environment variable

    dotnet user-secrets init
    dotnet user-secrets set "ConnectionStrings:AppIdentityDbContextConnection" "Data Source=..."

### Run App

> Run command to create database

      dotnet ef database update

> Run command to run app

      dotnet dev-certs https --trust

      dotnet run --launch-profile https
