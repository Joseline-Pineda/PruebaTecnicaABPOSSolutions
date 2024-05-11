PruebaTecnicaABPOSSolutions

Se necesita crear una aplicación que satisfaga la necesidad de Administrar negocios
por parte de los cliente, esta aplicación debe permitir:
❖ Crear Usuarios al menos de dos tipos (Cliente y Administrador)
❖ Crear negocios
❖ Crear Menus para los negocios
❖ Utilizar Roles para cada uno de los usuarios (deseable)

## Requisitos Previos

- .NET SDK 6

## Configuración

1. Clona este repositorio en tu máquina local.
2. Abre el proyecto en tu editor de código preferido.
3. Modifica la cadena de conexión en `appsettings.json` para que apunte a tu base de datos.

## Instalación

1. Abre una terminal en el directorio raíz del proyecto.
2. Ejecuta el comando `dotnet restore` para restaurar las dependencias del proyecto.
3. Ejecuta el comando `dotnet ef database update` para aplicar las migraciones y crear la base de datos.
