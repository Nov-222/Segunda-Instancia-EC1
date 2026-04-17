CREATE DATABASE REC
USE REC
GO;

CREATE TABLE Huesped (
	Id int primary key,
	Nombre varchar(50),
	Apellido_Paterno varchar(50),
	Apellido_Materno varchar(50),
	Documento varchar(50),
	Telefono varchar(50),
	Email varchar(50),
	Fecha_Nacimiento Date
);

Create Table Tipo_Habitacion(
	Id int primary key,
	Nombre varchar(50),
	Capacidad int,
	Descripcion varchar(100),
	Imagen varchar(100),
	Precio_Noche int
);


Create Table Habitacion(
	Id int primary key,
	Id_TH int,
	Estado varchar(50),
	Foreign Key (Id_TH) references Tipo_Habitacion(Id)
);

Create table Estadia (
	Id int primary key,
	Fecha_Inicio date,
	Fecha_Finalizacion date,
	Estado varchar(20),
	Id_Habitacion int,
	Precio_Total int,
	Foreign key (Id_Habitacion) references Habitacion(Id)
);

Create table Huesped_Estadia (
    Id_Estadia int,
    Id_Huesped int,
    Primary Key (Id_Estadia, Id_Huesped),
    Foreign key (Id_Estadia) references Estadia(Id),
    Foreign key (Id_Huesped) references Huesped(Id)
);

Create table Detalle_Estadia (
	Id_Estadia int primary key,
	Registro_Ingreso DateTime,
	Registro_Salida DateTime,
	Multa int,
	Foreign key (Id_Estadia) references Estadia(Id)
)

Create Table Informaciones(
	Id int primary key,
	Nombre varchar(100),
	Direccion varchar(100),
	Contacto varchar(20),
	Email varchar(30)
)

Create table Politicas(
	Id int primary key,
	Id_Infor int,
	Horario_Atencion varchar(50),
	Hora_Maxima_Salida int,
	Hora_Minima_Ingreso int,
	Mora int,
	Foreign Key (Id_Infor) references Informaciones(Id)
)
