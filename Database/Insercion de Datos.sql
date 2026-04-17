INSERT INTO Tipo_Habitacion (Id, Nombre, Capacidad, Descripcion, Precio_Noche, Imagen)
VALUES 
(1, 'Suite', 2, 'Habitación de lujo con vista panorámica, área de estar independiente y acabados de alta gama.', 1000, ''),
(2, 'Matrimonial', 2, 'Confortable habitación con cama de dos plazas, ideal para parejas, incluye escritorio y wifi.', 500, ''),
(3, 'Singular', 1, 'Habitación funcional optimizada para viajeros individuales o de negocios con cama sencilla.', 100, '');


INSERT INTO Habitacion (Id, Id_TH, Estado) 
VALUES 
(1, 1, 'Disponible'),
(2, 1, 'Disponible'),
(3, 2, 'Disponible'),
(4, 2, 'Disponible'),
(5, 3, 'Disponible'),
(6, 3, 'Disponible')

INSERT INTO Huesped (Id, Nombre, Apellido_Paterno, Apellido_Materno, Documento, Telefono, Email, Fecha_Nacimiento)
VALUES
(1,'Jose','Mendoza','Oliva','A-123456','12345678','jose@gmail.com','2006-11-10'),
(2,'Agustin','Mendoza','Oliva','A-123457','12345679','agus@gmail.com','2005-11-10'),
(3,'Camila','Avalos','Oliva','B-123456','12245678','camila@gmail.com','2006-11-05'),
(4,'Kal','Wardin','Kestis','B-123457','12335678','kal@gmail.com','2006-11-11'),
(5,'Juan','Paco','Mar','C-123456','12345668','paco@gmail.com','2006-03-04'),
(6,'Rose','Cuarzo','Susano','C-133456','12345578','cuarzo@gmail.com','2000-01-01'),
(7,'Nier','Callisto','Branco','11123456','12345679','nier@gmail.com','2000-11-10'),
(8,'Oscar','Ota','Yonashiro','22123456','12345888','ota@gmail.com','2006-07-10'),
(9,'Emanuel','Neuer','Kant','33123456','12347778','kant@gmail.com','2005-10-10'),
(10,'Joseph','Stalin','Panzer','23123456','43215678','fire@gmail.com','1999-11-10');