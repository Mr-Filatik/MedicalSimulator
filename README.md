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

## Information about methods for obtaining server data

Information about methods, request paths and their types is displayed [HERE](http://filatik.somee.com/swagger)

```
{id} and {page} - integers
```
```
{predicate} - expression for selection
* example: x => x.Id > 4 && x.Id <= 6
* example: x=>x.Id>4&&x.Id<=6
```

Information for create expressions

```csharp
class User
{
      int Id { get; set; }
      string Login { get; set; }
      string PasswordHash { get; set; }
      string Surname { get; set; }
      string Name { get; set; }
      string? Patronymic { get; set; }
      string FullName { get; }
      string? Email { get; set; }
      string? Phone { get; set; }
      int? Age { get; set; }
}
```

## Information about others



