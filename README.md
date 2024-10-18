# Laboratorio 1

Integrantes: Alejandro Suarez, Hermann Hernandez y Santiago Yañez

## Descripción 🔎
El Laboratorio consiste en la implementación de una aplicación web monolítica utilizando el patrón Modelo-Vista-Controlador (MVC) y el patrón de acceso a datos DAO. Empleando tecnologías como .NET 8 y SQL Server, los estudiantes deben desarrollar un CRUD completo para un modelo de datos predefinido, que incluye entidades relacionadas como Profesión, Estudios, Persona, y Teléfono. El laboratorio también incluye la creación de endpoints REST documentados con Swagger, el uso de Entity Framework para el manejo de la base de datos, y la entrega de documentación detallada junto con el código fuente en un repositorio Git.

## Pasos de ejecución 📝
1. Ir a la carpeta ``personapi-dotnet``

        cd personapi-dotnet

2. Borrar el docker previo (opcional)

        docker-compose down

3. Construir el docker

        docker-compose up --build

4. Esperar que el docker este corriendo

5. Una vez el docker este inicializado ir a la página ``Localhost:8081/home``, o puedes acceder dando click [aqui](http://localhost:8081/home)