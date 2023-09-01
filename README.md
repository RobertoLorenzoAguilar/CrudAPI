# CrudAPI
Desacoplando el Front-End 

<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Tabla de Contenido</summary>
  <ol>
    <li>
      <a href="#Acerca del Proyecto">Acerca del Proyecto</a>
    </li>
    <li>
      <a href="#El archivo principal>El archivo principal</a>
      <ul>
        <li><a href="#Prerrequisitos">Diagramado de Interfaz grafica de usuario (GUI) Web con Balsamiq</a></li>
        <li><a href="#Prerrequisitos">Modelado de bases de datos relacionales en UML</a></li>
        <li><a href="#Prerrequisitos">Creación de la Base de Datos en SQL Managment Studio</a></li>
        <li><a href="#Prerrequisitos">Creación Proyecto con Visual Studio tipo API Web Core 6.0</a></li>
        <li><a href="#Prerrequisitos">Creación modelos mediante Object Relacional Mapping (ORM) Entitiy Framework modo Databasefirst</a></li>
        <li><a href="#Prerrequisitos">Testeo manual de end-points "Get, Post, Put, Delete" mediante Visual Code extension Postman </a></li>
	<li><a href="#Prerrequisitos">Desarrollo de Front-End Javascript</a></li>
	<li><a href="#Prerrequisitos">Creacion de Pruebas Automatizadas mediante RobotFramework</a></li>	
        <li><a href="#TODO">TODO</a></li>
        <li><a href="#Tecnologias">Tecnologías</a></li>
      </ul>
    </li>
  </ol>
</details>


# Diagrama Entidad Relación
Desacoplando el Front-End 

 <img src="Images/ER.PNG" alt="Logo">


## SQL Query
 
``` sql
/* Creacion de la base de datos */
Create Database ControlAutosDB

/* Seleccion base de datos a trabajar */
Use ControlAutosDB;

/* creacion de tabla */
Create Table Ruta(
	IdRuta int primary key identity(1,1),
	NombreRuta varchar(60) not null,
	InicioRuta varchar(60) not null,
	Estatus   bit not null,		
)

/* creacion de tabla */
Create Table DestinoRuta(
	IdDestino int primary key identity(1,1),
	Destino varchar(100) not null,
	Estatus   bit not null,		
	IdRuta int not null,
	CONSTRAINT FK_Cargo FOREIGN KEY (IdRuta) REFERENCES Ruta(IdRuta)
)
-- Columna Estatus nota:
--No existe el tipo de dato boolean, pero sí el tipo de dato bit. 
--Y un bit, como todos sabemos, puede ser un 1 o un 0.

/* insercion en tabla */
Insert into Ruta(NombreRuta,InicioRuta, Estatus) VALUES ('Rio Sonora','29.07447500276877, -110.94526290893555',1)

/* insercion en tabla */
Insert into DestinoRuta(Destino, Estatus, IdRuta) VALUES 
('29.07447500276877, -111.00105285644531', 1, 1),
('29.094127073996578, -110.93273162841798', 1,1 ),
('29.094127073996578, -110.93273162841798', 1, 1)

select * from Ruta;
select * from DestinoRuta;
```




<!-- PROJECT LOGO -->
<br />



<p align="center">
  <a>
    <img src="Images/CrudAPI_MC.PNG" alt="Logo">
  </a>  
</p>

<p align="center">
  <a>
    <img src="Images/ProyectoTipoAPI.PNG" alt="Logo">
  </a>
</p>

<p align="center">
  <a>
    <img src="Images/diable_https.PNG" alt="Logo">
  </a>
</p>

<p align="center">
  <a>
    <img src="Images/PrimerVista.PNG" alt="Logo">
  </a>
</p>

<p align="center">
  <a>
    <img src="Images/EliminandoInfoDefault.PNG" alt="Logo">
  </a>
</p>

<p align="center">
  <a>
    <img src="Images/PostManExtension.PNG" alt="Logo">
  </a>
</p>

# Creación modelos mediante Object Relacional Mapping (ORM) Entitiy Framework modo Databasefirst

1) Una vez generado el proyecto instalar con el gestor de paquetes de nutget las siguientes librerias:
"procurar que sean las versiones recientes estables"
~~~
 microsoft.entityframeworkcore.tools
 microsoft.entityframeworkcore.sqlserver  
~~~

Generar el contexto y modelos a partir de la base de datos mediante package manager console:
mediante credenciales sql:
~~~
 Scaffold-DbContext "Server=(local); Database=ControlAutosDB; user id=lorenzo; pwd= 123; Encrypt=False" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models 
~~~

mediante autentificacion de windows:
~~~
Scaffold-DbContext "Server=(local); DataBase=ControlAutosDB; Integrated Security=true; Encrypt=False" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models
~~~

Recuerda tener habilitada las conecciones en el administrador de sql
<p align="center">
  <a>
    <img src="Images/Haabilitar_Autentificacion_SQL.PNG" alt="Logo">
  </a>  
</p>

Al terminar deberian aparecer la carpeta models, con los modelos y el contexto generado
 [<img src="Images/ModelsEF.PNG" alt="Logo">](url)

Debemos mover la cadena de \Models\ControlAutosDbContext.cs
[<img src="Images/CadenaContext.PNG" alt="Logo">](url)


appsettings.json
``` json
{
  "ConnectionStrings": {
    "cadenaSql": " Server=(local); DataBase=ControlAutosDB; Integrated Security=true; Encrypt=False"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

```

Es necesario agregar la cadena en Program.cs
~~~
builder.Services.AddDbContext<ControlAutosDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSql")));
~~~

using CrudAPICM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

# Generar un controller vacio con el siguiente codigo basico
``` C#
namespace CrudAPICM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlAutoController : Controller
    {
        private static ControlAutosDbContext _contexControlAuto;
        public ControlAutoController(ControlAutosDbContext context) { 
            _contexControlAuto = context;
        }        

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ruta>>> GetRutas() {

            return await _contexControlAuto.Ruta.ToListAsync();
        }
    }
}
```
