USE [master]
GO

CREATE DATABASE PW3Tienda;

USE [PW3Tienda]
GO


CREATE TABLE [dbo].[historical](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[idProducto] [int] NULL,
	[idCoproducto] [int] NULL,
	[puntaje] [int] NULL,
);

CREATE TABLE [dbo].[product] (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    imagen VARCHAR(100),
    precio DECIMAL(10, 2)
);

CREATE TABLE [dbo].[product sales-history](
    id_producto INT,
    id_coproducto INT,
    FOREIGN KEY (id_producto) REFERENCES PRODUCTO(id),
    FOREIGN KEY (id_coproducto) REFERENCES PRODUCTO(id)
);
GO

-- Inserción de 10 productos en la tabla PRODUCTO
INSERT INTO [dbo].[product]  (nombre, imagen, precio)
VALUES ('Teléfono móvil', 'https://images.fravega.com/f300/a867d2e098f00e8299dd3648700cf654.png.webp', 499.99),
       ('Auriculares inalámbricos', 'https://images.fravega.com/f500/39662bfef8000d260f2d55e287b4c76a.jpg', 79.99),
       ('Smartwatch', 'https://images.fravega.com/f300/a72015f45487019ed3a041e785a7b543.jpg.webp', 199.99),
       ('Cargador portátil', 'https://images.fravega.com/f300/7412cd8547e22b1e62d1e096ed99e414.jpg.webp', 29.99),
       ('Tableta', 'https://images.fravega.com/f300/96fae658d31f249edcd251fa404475db.jpg.webp', 299.99),
       ('Altavoz Bluetooth', 'https://images.fravega.com/f300/64e159be9ec19d9278a7c2b851bb7b4a.png.webp', 89.99),
       ('Teclado inalámbrico', 'https://images.fravega.com/f300/d3afb30785ea3f473b76f4540a66f8b7.jpg.webp', 49.99),
       ('Ratón óptico', 'https://images.fravega.com/f300/0f5d216fc4c4c8104ed1ecda8da6f7aa.png.webp', 19.99),
       ('Funda protectora', 'https://images.fravega.com/f300/8ca7bf6e2f9a223bd1fc4702cdd05fbb.jpg.webp', 9.99),
       ('Cable USB', 'https://images.fravega.com/f300/04edca370e59f74dc1d73c76f2f31374.jpg.webp', 14.99);

-- Inserción de registros en la tabla HISTORIAL_PRODUCTO
-- Ejemplo de algunos registros de historial relacionados con productos electrónicos
INSERT INTO [dbo].[product sales-history] (id_producto, id_coproducto)
VALUES (1, 2),  -- Teléfono móvil con auriculares inalámbricos
       (1, 4),  -- Teléfono móvil con cargador portátil
       (2, 6),  -- Auriculares inalámbricos con altavoz Bluetooth
       (3, 5),  -- Smartwatch con tableta
       (4, 8),  -- Cargador portátil con ratón óptico
       (5, 9),  -- Tableta con funda protectora
       (6, 7),  -- Altavoz Bluetooth con teclado inalámbrico
       (7, 10), -- Teclado inalámbrico con cable USB
       (8, 9),  -- Ratón óptico con funda protectora
       (9, 10); -- Funda protectora con cable USB

--Scaffold-DbContext "Server=CHONII;Database=HistoricoConsultas;User Id=sa;Password=1234;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entidades