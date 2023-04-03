# MedicalSimulator

This project is dedicated to the graduation project.

It consists of:

* WebGL Application &ndash; info is empty

* VR Application &ndash; info is empty

* gRPC Application &ndash; info is empty

* Blazor Application &ndash; info is empty

## Necessary links

* [WebGL Application](http://app.medical-sumulator.h1n.ru/) &ndash; Ссылка на приложение WebGL (При ошибках очистить кеш браузера или запустить в режиме инкогнито).

* [Web Application](http://server.medical-sumulator.h1n.ru/) &ndash; Ссылка на веб-приложение для пользователей.

* [Server Application](http://filatik.somee.com/) &ndash; Серверная часть приложения. 

## Information

Unity: Version 2021.3.20f1

.NET Core: Version 7.0

# Information about methods for obtaining server data (http://filatik.somee.com)

```
# Получить количество всех пользователей.
GET - /count/users
```
```
# Получить количество пользователей по выборке.
GET - /count/users/{predicate}
      /count/users/x=>x.Id>4&&x.Id<=6
```
```
# Получить информацию о пользователе.
GET - /user/{id}
      /user/4
```
```
# Получить информацию о всех пользователях.
GET - /users
```
```
# Получить информацию о пользователях по выборке.
GET - /users/{predicate}
      /users/x=>x.Id>4&&x.Id<=6
```

# Information about others

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <packageSources>
        <!-- Add this repository to the list of available repositories -->
        <add key="gRPC repository" value="https://grpc.jfrog.io/grpc/api/nuget/v3/grpc-nuget-dev" />
    </packageSources>
</configuration>
```


