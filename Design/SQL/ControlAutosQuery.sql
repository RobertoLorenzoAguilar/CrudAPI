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

