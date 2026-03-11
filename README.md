# API de Gestión de Citas Médicas

## Descripción

Este proyecto consiste en una **API REST para la gestión de citas médicas**, desarrollada con **ASP.NET Core**.

La API permite realizar operaciones sobre:

- **Citas médicas**
- **Pacientes**
- **Médicos**

Entre las funcionalidades principales se encuentran:

- Registrar citas
- Consultar citas
- Actualizar citas
- Eliminar citas

El sistema incluye además un **mecanismo de notificaciones basado en interfaces**, que permite simular el envío de mensajes por distintos medios cuando se realizan operaciones sobre las citas.

Las notificaciones implementadas son:

- SMS
- Correo electrónico
- WhatsApp

Este proyecto fue desarrollado con fines académicos para demostrar el uso de **APIs REST, controladores, interfaces y lógica de aplicación en .NET**.

---

# Tecnologías utilizadas

Este proyecto fue desarrollado utilizando las siguientes tecnologías:

- ASP.NET Core
- C#
- Visual Studio
- Postman

---

# Arquitectura del sistema

El sistema sigue una arquitectura basada en API REST.

```
Cliente (Postman)
       |
       v
ASP.NET Core API
       |
       v
Controladores
       |
       v
Modelos en memoria
       |
       v
Sistema de Notificaciones
```

Las solicitudes HTTP son procesadas por los controladores, los cuales gestionan la lógica del sistema y activan el sistema de notificaciones cuando corresponde.

---

# Estructura del proyecto

El proyecto se encuentra organizado de la siguiente manera:

```
Controllers/
    CitaController.cs
    MedicoController.cs
    PacienteController.cs

Models/
    Persona.cs
    Paciente.cs
    Medico.cs
    Cita.cs

Interfaces/
    INotificacion.cs

Notificaciones/
    NotificacionSMS.cs
    NotificacionCorreo.cs
    NotificacionWhatsApp.cs

docs/
    Informe_API_Citas_Medicas.pdf

postman/
    CitasAPI.postman_collection.json
```

---

# Requisitos previos

Antes de ejecutar el proyecto es necesario instalar las siguientes herramientas.

## 1. Instalar Visual Studio

Descargar e instalar **Visual Studio**.

Durante la instalación seleccionar la carga de trabajo:

**ASP.NET y desarrollo web**

Esto instalará automáticamente:

- .NET SDK
- herramientas necesarias para ejecutar proyectos ASP.NET.

---

## 2. Instalar .NET

Si no se instaló junto con Visual Studio, descargar **.NET**.

Verificar la instalación ejecutando en la terminal:

```
dotnet --version
```

---

## 3. Instalar Postman

Para probar los endpoints de la API se recomienda utilizar **Postman**.

---

# Instalación del proyecto

## 1. Clonar el repositorio

```
git clone https://github.com/AndyTalledo/gestion-citas-api
```

O descargar el proyecto como archivo ZIP desde GitHub.

---

## 2. Abrir el proyecto

1. Abrir Visual Studio  
2. Seleccionar **Open Project**  
3. Abrir el archivo `.sln` del proyecto  

---

# Ejecutar la aplicación

Una vez abierto el proyecto existen dos formas de ejecutarlo.

## Opción 1 (recomendada)

Presionar **Run / Start** dentro de Visual Studio.

---

## Opción 2 (por terminal)

Abrir una terminal dentro de la carpeta del proyecto y ejecutar:

```
dotnet run
```

La API se iniciará en una dirección similar a:

```
http://localhost:5000
```

o

```
https://localhost:5001
```

---

# Endpoints de la API

## Citas

### Obtener todas las citas

```
GET /api/citas
```

### Obtener una cita por ID

```
GET /api/citas/{id}
```

### Registrar una nueva cita

```
POST /api/citas
```

### Actualizar una cita

```
PUT /api/citas/{id}
```

### Eliminar una cita

```
DELETE /api/citas/{id}
```

---

## Médicos

### Obtener todos los médicos

```
GET /api/medicos
```

### Obtener un médico por ID

```
GET /api/medicos/{id}
```

### Registrar un nuevo médico

```
POST /api/medicos
```

### Actualizar un médico

```
PUT /api/medicos/{id}
```

### Eliminar un médico

```
DELETE /api/medicos/{id}
```

---

## Pacientes

### Obtener todos los pacientes

```
GET /api/pacientes
```

### Obtener un paciente por ID

```
GET /api/pacientes/{id}
```

### Registrar un nuevo paciente

```
POST /api/pacientes
```

### Actualizar un paciente

```
PUT /api/pacientes/{id}
```

### Eliminar un paciente

```
DELETE /api/pacientes/{id}
```

---

# Validaciones implementadas

La API incluye diversas validaciones para asegurar la integridad de los datos ingresados.

Entre ellas se encuentran:

- La **fecha de la cita es obligatoria**
- El **estado de la cita tiene una longitud mínima y máxima**
- El **médico debe existir para registrar una cita**
- El **paciente debe existir para registrar una cita**
- La **cita debe existir antes de eliminarse**
- El **DNI del médico debe tener 8 dígitos**
- El **correo del paciente debe tener un formato válido**

Estas validaciones ayudan a evitar el registro de información incorrecta dentro del sistema.

---

# Pruebas con Postman

El repositorio incluye una **colección de Postman** que permite probar fácilmente los endpoints de la API.

Para utilizarla:

1. Abrir Postman  
2. Seleccionar **Import**  
3. Importar la colección incluida en la carpeta `postman`  
4. Ejecutar las solicitudes disponibles  

Antes de realizar las pruebas, asegurarse de que la API se encuentre en ejecución.

---

# Sistema de Notificaciones

El sistema implementa una **interfaz de notificación** que permite definir distintos tipos de mensajes cuando se realizan operaciones sobre las citas.

Las notificaciones se ejecutan cuando se:

- registra una cita
- actualiza una cita
- elimina una cita

La estructura utilizada es la siguiente:

```
INotificacion
   |
   |-- NotificacionSMS
   |-- NotificacionCorreo
   |-- NotificacionWhatsApp
```

Este diseño permite agregar nuevos tipos de notificación sin modificar la lógica principal del sistema.

---

# Control de datos mediante objetos anónimos

En algunos endpoints de la API se utilizan objetos anónimos para construir las respuestas JSON. Esto permite:

* Mostrar únicamente los campos relevantes de Paciente y Médico.
* Ocultar información que no es necesaria para el cliente.
* Organizar mejor la estructura de los datos en la respuesta.

---

# Control de auditoría de registros

La entidad Cita incluye dos campos para el control de auditoría de los registros:

**FechaRegistro**: se asigna automáticamente al momento de crear una cita y representa la fecha en que fue registrada en el sistema.

**FechaActualizacion**: se actualiza cada vez que se modifica una cita, permitiendo identificar la última actualización realizada.

Estos campos permiten llevar un control temporal de los registros y son una práctica común en el desarrollo de APIs y sistemas de gestión.

---

# Información adicional del proyecto

- La información de las citas se almacena **en memoria** dentro de la aplicación.
- No se utiliza **base de datos** en este proyecto.
- La **edad del paciente se calcula dinámicamente** a partir de la fecha de nacimiento.
- Se permite registrar un **médico o paciente** con un solo apellido, ya que se pueden presentar dichos casos.

---

# Autor

**Grupo 06**

* Jimenez Rojas, Jonathan Jose
* Revilla Huapaya, Javier Alberto
* Talledo Ceverino, Andy Steve


Proyecto desarrollado como parte de un trabajo académico sobre desarrollo de **APIs REST con .NET**.