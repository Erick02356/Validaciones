drop DATABASE Proyecto;
CREATE DATABASE Proyecto;
USE Proyecto; 


CREATE TABLE Propietario (
Id INT IDENTITY PRIMARY KEY,
Cedula INT  NOT NULL,
Nombre_uno VARCHAR (50)NOT NULL,
Nombre_dos VARCHAR (50),
Apellido_uno VARCHAR(50),
Apellido_dos VARCHAR(50),
Telefono INT NOT NULL, 
Email VARCHAR (150)NOT NULL,
Usuario VARCHAR (150)NOT NULL,
Clave VARCHAR (50)NOT NULL,
Estado VARCHAR (20) NOT NULL,
);

INSERT INTO Propietario (Cedula, Nombre_uno, Nombre_dos, Telefono, Email, Usuario, Clave, Estado) 
VALUES (1, 'Jimena', 'jota', 345465, 'jimenavelasco@gmail.com', 'J29', '12345', 'Activo');
INSERT INTO Propietario (Cedula, Nombre_uno, Nombre_dos, Telefono, Email, Usuario, Clave, Estado) 
VALUES (2, 'Erick', 'Samuel', 3434, 'Ek@gmail.com', '29', '123', 'Desactivo');
INSERT INTO Propietario (Cedula, Nombre_uno, Nombre_dos, Telefono, Email, Usuario, Clave, Estado) 
VALUES (3, 'Laurenn', 'Sofia', 345776, 'La@gmail.com', '2', '1234', 'Activo');

CREATE PROCEDURE Mostrar
AS BEGIN
SELECT * FROM Propietario
END;

CREATE PROCEDURE Insertar 
@Cedula BIGINT,  
@Nombre_uno VARCHAR (50),
@Nombre_dos VARCHAR (50),
@Apellido_uno VARCHAR(50),
@Apellido_dos VARCHAR(50),
@Telefono INT, 
@Email VARCHAR (150),
@Usuario VARCHAR (150),
@Clave VARCHAR (50),
@Estado VARCHAR (20)
AS BEGIN 
INSERT INTO Propietario (Cedula, Nombre_uno, Nombre_dos, Apellido_uno, Apellido_dos, Telefono, Email, Usuario, Clave, Estado) 
VALUES (@Cedula, @Nombre_uno, @Nombre_dos, @Apellido_uno, @Apellido_dos, @Telefono, @Email, @Usuario, @Clave, @Estado);
END;


CREATE PROCEDURE Editar 
@Id INT,
@Cedula BIGINT,  
@Nombre_uno VARCHAR (50),
@Nombre_dos VARCHAR (50),
@Apellido_uno VARCHAR(50),
@Apellido_dos VARCHAR(50),
@Telefono INT, 
@Email VARCHAR (150),
@Usuario VARCHAR (150),
@Clave VARCHAR (50),
@Estado VARCHAR (20)
AS BEGIN 
    UPDATE Propietario
    SET Cedula = @Cedula, 
        Nombre_uno = @Nombre_uno, 
        Nombre_dos = @Nombre_dos, 
        Apellido_uno = @Apellido_uno,
        Apellido_dos = @Apellido_dos,
        Telefono = @Telefono,
        Email = @Email,
        Usuario = @Usuario,
        Clave = @Clave,
        Estado = @Estado
    WHERE Id = @Id;
END;


CREATE PROCEDURE Eliminar
@Id INT
AS BEGIN 
    DELETE FROM Propietario WHERE Id = @Id;
END;