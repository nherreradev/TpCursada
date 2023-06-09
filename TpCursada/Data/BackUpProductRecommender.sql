USE MASTER
GO

CREATE DATABASE PW3Tienda
GO

USE PW3Tienda
GO
/****** Object:  Table [dbo].[product]    Script Date: 1/6/2023 23:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](100) NOT NULL,
	[imagen] [varchar](100) NULL,
	[precio] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[historical]    Script Date: 1/6/2023 23:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[historical](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idProducto] [int] NULL,
	[idCoproducto] [int] NULL,
	[score] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[historical]  WITH CHECK ADD FOREIGN KEY([idProducto])
REFERENCES [dbo].[product] ([id])
GO
ALTER TABLE [dbo].[historical]  WITH CHECK ADD FOREIGN KEY([idCoproducto])
REFERENCES [dbo].[product] ([id])
GO
/****** Object:  Table [dbo].[product sales-history]    Script Date: 1/6/2023 23:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product sales-history](
	[id_producto] [int] NULL,
	[id_coproducto] [int] NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[product] ON 

INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (1, N'Teléfono móvil', N'https://images.fravega.com/f300/a867d2e098f00e8299dd3648700cf654.png.webp', CAST(499.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (2, N'Auriculares inalámbricos', N'https://images.fravega.com/f500/39662bfef8000d260f2d55e287b4c76a.jpg', CAST(79.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (3, N'Smartwatch', N'https://images.fravega.com/f300/a72015f45487019ed3a041e785a7b543.jpg.webp', CAST(199.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (4, N'Cargador portátil', N'https://images.fravega.com/f300/7412cd8547e22b1e62d1e096ed99e414.jpg.webp', CAST(29.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (5, N'Tableta', N'https://images.fravega.com/f300/96fae658d31f249edcd251fa404475db.jpg.webp', CAST(299.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (6, N'Altavoz Bluetooth', N'https://images.fravega.com/f300/64e159be9ec19d9278a7c2b851bb7b4a.png.webp', CAST(89.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (7, N'Teclado inalámbrico', N'https://images.fravega.com/f300/d3afb30785ea3f473b76f4540a66f8b7.jpg.webp', CAST(49.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (8, N'Ratón óptico', N'https://images.fravega.com/f300/0f5d216fc4c4c8104ed1ecda8da6f7aa.png.webp', CAST(19.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (9, N'Funda protectora', N'https://images.fravega.com/f300/8ca7bf6e2f9a223bd1fc4702cdd05fbb.jpg.webp', CAST(9.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (10, N'Cable USB', N'https://images.fravega.com/f300/04edca370e59f74dc1d73c76f2f31374.jpg.webp', CAST(14.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (11, N'Monitor  Daewoo DW-MON19 led 19" negro 100V/240V', N'https://http2.mlstatic.com/D_NQ_NP_783695-MLA48691081510_122021-O.webp', CAST(24900.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (12, N'Impresora a color multifunción HP Deskjet Ink Advantage 3775 con wifi blanca y azul 200V - 240V', N'https://http2.mlstatic.com/D_NQ_NP_729863-MLA50606451366_072022-O.webp', CAST(1490.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (13, N'Disco duro externo Toshiba Canvio Basics HDTB410XK3AA', N'https://http2.mlstatic.com/D_NQ_NP_828440-MLA40152365365_122019-O.webp', CAST(7900.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (14, N'Cámara digital Nikon Z7 sin espejo color negro', N'https://http2.mlstatic.com/D_NQ_NP_613612-MLA40926557882_022020-O.webp', CAST(1990.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (15, N'Router WIFI TP-Link TL-WR840N blanco', N'https://http2.mlstatic.com/D_NQ_NP_602052-MLA41052895113_032020-O.webp', CAST(590.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (16, N'Laptop Lenovo Ideapad 1i Intel I3 1215u 4gb Ram ', N'https://http2.mlstatic.com/D_NQ_NP_787180-MLA54518190876_032023-O.webp', CAST(8990.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (17, N'Memoria USB SanDisk Ultra Dual Drive Luxe 256GB 3.1 Gen 1 plateado', N'https://http2.mlstatic.com/D_NQ_NP_728250-MLA47447537418_092021-O.webp', CAST(240.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (18, N'Proyector Gadnic Starpro Wifi 7000 Lúmenes Hdmi Usb Mirror', N'https://http2.mlstatic.com/D_NQ_NP_950709-MLA48418960967_122021-O.webp', CAST(3990.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (19, N'Placa de video AMD XFX SWIFT309 Radeon RX 6700 Series RX 6700 XT RX-67XTYPBDP 12GB', N'https://http2.mlstatic.com/D_NQ_NP_977440-MLA52684349322_122022-O.webp', CAST(2990.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (20, N'Microfono Corbatero Inalambrico Android Ios Usb C Lightning', N'https://http2.mlstatic.com/D_NQ_NP_732337-MLU54984481116_052023-O.webp', CAST(490.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (21, N'Smart TV LED 32” JVC 32DA31252', N'https://images.fravega.com/f300/d672da32d8c299bb586a6aa21579ef31.png.webp', CAST(69900.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (22, N'Teclado Mecanico Redragon Kumara K552 Led Rgb Negro Gamer', N'https://http2.mlstatic.com/D_NQ_NP_858207-MLA49693533889_042022-O.webp', CAST(7900.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (23, N'Mouse gamer Logitech G Series Lightspeed G603 negro', N'https://http2.mlstatic.com/D_NQ_NP_935858-MLA32149626950_092019-O.webp', CAST(49.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (24, N'Silla Gamer Ergonómica Negra y Roja', N'https://images.fravega.com/f300/93833c1e3d5f039f090e39f44e7675c4.jpg.webp', CAST(79900.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (25, N'Lámpara De Escritorio Morsa Brazo Flexible', N'https://http2.mlstatic.com/D_NQ_NP_709396-MLA51301571291_082022-O.webp', CAST(2900.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (26, N'Cargador Rápido iPhone 5 5s Se 6s 6 7 8 Plus X Xr Xs 11 iPad', N'https://http2.mlstatic.com/D_NQ_NP_942719-MLA49656571038_042022-O.webp', CAST(4000.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (27, N'Tarjeta  De Sonido Externa V9x Pro Computer Live Broadcast', N'https://http2.mlstatic.com/D_NQ_NP_998800-MLA50768428207_072022-O.webp', CAST(3900.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (28, N'Cámara Web Full HD Bons BM061169', N'https://images.fravega.com/f300/b9a876e5808775e25303328ac603b258.jpg.webp', CAST(19000.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (29, N'Soporte de Aluminio Reforzado Plegable Regulable para Notebook Laptop', N'https://images.fravega.com/f300/792174241e4b82dd95bc6e17b4c3ee07.jpg.webp', CAST(4000.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (30, N'Procesador AMD Ryzen Threadripper 3970X 100-100000011WOF de 32 núcleos y 4.5GHz de frecuencia', N'https://http2.mlstatic.com/D_NQ_NP_985890-MLA42162577679_062020-O.webp', CAST(1190000.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (31, N'Audífonos Bluetooth', N'https://http2.mlstatic.com/D_NQ_NP_796449-MLA53016429244_122022-O.webp', CAST(6900.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (32, N'Monitor curvo Samsung C24RG5 LCD 23.5" ', N'https://http2.mlstatic.com/D_NQ_NP_893032-MLA52223848687_102022-O.webp', CAST(49000.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (33, N'Teclado Inalámbrico Aluminio retroiluminado', N'https://http2.mlstatic.com/D_NQ_NP_983984-MLA50912679171_072022-O.webp', CAST(29000.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (34, N'Mouse Xiaomi Mi Dual mode inalámbrico', N'https://http2.mlstatic.com/D_NQ_NP_918645-MLA45538604635_042021-O.webp', CAST(19000.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (35, N'Mesa de escritorio', N'https://images.fravega.com/f300/5a04a6de6d251c49efa9af0c126ea436.jpg.webp', CAST(149000.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (36, N'Lápiz óptico Capacitivo Punta Fina Pen Para Tablets iPad', N'https://http2.mlstatic.com/D_NQ_NP_951911-MLA49703617315_042022-O.webp', CAST(9000.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (37, N'Soplador Aire Comprimido Inalambrico de limpieza para computadoras', N'https://http2.mlstatic.com/D_NQ_NP_681721-MLA51582424634_092022-O.webp', CAST(49000.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (38, N'Adaptador Hdmi A Vga', N'https://http2.mlstatic.com/D_NQ_NP_684087-MLA43063895334_082020-O.webp', CAST(2000.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (39, N'Disco  sólido interno Kingston SA400S37/240G 240GB negro', N'https://http2.mlstatic.com/D_NQ_NP_825890-MLA54124091906_032023-O.webp', CAST(12900.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (40, N'Batería externa + Cable Micro Usb Banco De Carga 20000 Mah', N'https://http2.mlstatic.com/D_NQ_NP_613649-MLA69464542297_052023-O.webp', CAST(19000.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (41, N'Estabilizador de corriente', N'https://http2.mlstatic.com/D_NQ_NP_756076-MLA53566888125_022023-O.webp', CAST(10009.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (42, N'Impresora  Xerox Emilia B230 Laser Monocromatica Wifi Duplex', N'https://http2.mlstatic.com/D_NQ_NP_689953-MLA52443502651_112022-O.webp', CAST(90109.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (43, N'Escáner De Documentos Dúplex A Color Epson Ds-770 Ii', N'https://http2.mlstatic.com/D_NQ_NP_841261-MLA53197115663_012023-O.webp', CAST(256009.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (44, N'Parlantes 2.1 Z213 LOGITECH', N'https://tiendatecnet.com/wp-content/uploads/2022/05/parlantes-logitech-z213-14w-100x100.webp', CAST(215000.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (45, N'Teclado numérico 100 GENIUS', N'https://tiendatecnet.com/wp-content/uploads/2022/06/TECLADO-GENIUS-NUMPAD-100.webp', CAST(19000.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (46, N'Tarjeta Red 2x Rj-45 10gb Pcie X8 Intel X540-t2 X540', N'https://http2.mlstatic.com/D_NQ_NP_851816-MLA43794729840_102020-O.webp', CAST(69000.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (47, N'Hub Usb 4 Puertos Usb 3.0 Slim Multiplicador 5gbps Pc Mac', N'https://http2.mlstatic.com/D_NQ_NP_940688-MLA54776962687_032023-O.webp', CAST(2500.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (48, N'Pantalla de proyección Electrica 120 A Control Remoto 16:9', N'https://http2.mlstatic.com/D_NQ_NP_640960-MLA53110330917_122022-O.webp', CAST(179000.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (49, N'Webcam con micrófono', N'https://http2.mlstatic.com/D_NQ_NP_717276-MLA44429474045_122020-O.webp', CAST(4590.99 AS Decimal(10, 2)))
INSERT [dbo].[product] ([id], [nombre], [imagen], [precio]) VALUES (50, N'Adaptador Bluetooth', N'https://http2.mlstatic.com/D_NQ_NP_737328-MLA53735186791_022023-O.webp', CAST(9100.99 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[product] OFF
GO

--Datos extras para generar 60 productos
INSERT INTO PRODUCT (nombre, imagen, precio)
VALUES ('Kit Proyector Gadnic ', 'https://http2.mlstatic.com/D_NQ_NP_744058-MLA46964058735_082021-O.webp', 2990.99),
       ('Bocinas Bluetooth-Torre Aiwa Bluetooth', 'https://http2.mlstatic.com/D_NQ_NP_886547-MLA69189956111_052023-O.webp', 6900.99),
       ('Disco duro duro externo Western Digital', 'https://http2.mlstatic.com/D_NQ_NP_691474-MLA46039744011_052021-O.webp', 8900.99),
       ('Monitor LED Gamer BenQ Estilizados GW2283 led 21.5" negro 100V/240V', 'https://http2.mlstatic.com/D_NQ_NP_613690-MLA44737119650_012021-O.webp', 4900.99),
       ('Mouse gaming Corsair Gaming Katar Pro negro', 'https://http2.mlstatic.com/D_NQ_NP_777913-MLA48956178421_012022-O.webp', 49.99),
       ('Cámara de seguridad  Smart Tech N8-200W-IR con resolución de 3MP visión nocturna incluida blanca', 'https://http2.mlstatic.com/D_NQ_NP_707195-MLA48859066326_012022-O.webp', 39000.99),
       (' Gamer Mecanizado Constrictor By Aiwa Qwerty Metal', 'https://http2.mlstatic.com/D_NQ_NP_996602-MLA48182022479_112021-O.webp', 10029.99),
       ('Cable Hdmi 3 Metros Full Hd 1080p 1.4v Pc Tv Mallado Filtros', 'https://http2.mlstatic.com/D_NQ_NP_881199-MLA54770638232_032023-O.webp', 1300.99),
       ('Cargador Inalámbrico 20w Carga Rápida Usb C Gadnic Portátil', 'https://http2.mlstatic.com/D_NQ_NP_969413-MLA69346062999_052023-O.webp', 6899),
       ('Micrófono Alctron UM900 condensador cardioide', 'https://http2.mlstatic.com/D_NQ_NP_682809-MLA25671549855_062017-O.webp', 20000.3);

ALTER TABLE [dbo].[product sales-history]  WITH CHECK ADD FOREIGN KEY([id_coproducto])
REFERENCES [dbo].[product] ([id])
GO
ALTER TABLE [dbo].[product sales-history]  WITH CHECK ADD FOREIGN KEY([id_producto])
REFERENCES [dbo].[product] ([id])
GO


CREATE TABLE [dbo].[product sales-history](
	[id_producto] [int] NULL,
	[id_coproducto] [int] NULL
) ON [PRIMARY]
GO

-- simulacion de compras


INSERT INTO [product sales-history] (id_producto, id_coproducto)
VALUES
(1, 2),
(1, 2),
(1, 2),
(1, 2),
(6, 10),
(6, 10),
(6, 10),
(51, 58),
(51, 58),
(51, 58),
(51, 58),
(35, 34),
(35, 34),
(35, 34),
(2, 1),
(2, 1),
(2, 1),
(2, 1),
(3, 1),
(3, 1),
(3, 1),
(3, 1),
(4, 1),
(4, 1),
(4, 1),
(4, 1),
(5, 6),
(5, 6),
(5, 6),
(5, 6),
(7, 8),
(7, 8),
(7, 8),
(7, 8),
(9, 1),
(9, 1),
(9, 1),
(9, 1),
(10, 1),
(10, 1),
(10, 1),
(10, 1),
(11, 12),
(11, 12),
(11, 12),
(11, 12),
(13, 11),
(13, 11),
(13, 11),
(13, 11),
(14, 10),
(14, 10),
(14, 10),
(14, 10),
(15, 16),
(15, 16),
(15, 16),
(15, 16),
(17, 16),
(17, 16),
(17, 16),
(17, 16),
(18, 24),
(18, 24),
(18, 24),
(18, 24),
(19, 22),
(19, 22),
(19, 22),
(19, 22),
(20, 24),
(20, 24),
(20, 24),
(20, 24),
(21, 24),
(21, 24),
(21, 24),
(21, 24),
(22, 21),
(22, 21),
(22, 21),
(22, 21),
(23, 22),
(23, 22),
(23, 22),
(23, 22),
(24, 22),
(24, 22),
(24, 22),
(24, 22),
(25, 35),
(25, 35),
(25, 35),
(25, 35),
(26, 1),
(26, 1),
(26, 1),
(26, 1),
(27, 30),
(27, 30),
(27, 30),
(27, 30),
(28, 32),
(28, 32),
(28, 32),
(28, 32),
(29, 35),
(29, 35),
(29, 35),
(29, 35),
(30, 27),
(30, 27),
(30, 27),
(30, 27),
(31, 1),
(31, 1),
(31, 1),
(31, 1),
(32, 28),
(32, 28),
(32, 28),
(32, 28),
(33, 32),
(33, 32),
(33, 32),
(33, 32),
(34, 35),
(34, 35),
(34, 35),
(34, 35),
(35, 34),
(35, 34),
(35, 34),
(35, 34),
(36, 35),
(36, 35),
(36, 35),
(36, 35),
(37, 16),
(37, 16),
(37, 16),
(37, 16),
(38, 16),
(38, 16),
(38, 16),
(38, 16),
(39, 16),
(39, 16),
(39, 16),
(39, 16),
(40, 16),
(40, 16),
(40, 16),
(40, 16),
(41, 16),
(41, 16),
(41, 16),
(41, 16),
(42, 16),
(42, 16),
(42, 16),
(42, 16),
(43, 16),
(43, 16),
(43, 16),
(43, 16),
(44, 16),
(44, 16),
(44, 16),
(44, 16),
(45, 16),
(45, 16),
(45, 16),
(45, 16),
(46, 37),
(46, 37),
(46, 37),
(46, 37),
(47, 16),
(47, 16),
(47, 16),
(47, 16),
(48, 49),
(48, 49),
(48, 49),
(48, 49),
(49, 48),
(49, 48),
(49, 48),
(49, 48),
(50, 16),
(50, 16),
(50, 16),
(50, 16),
(51, 58),
(51, 58),
(51, 58),
(51, 58),
(52, 16),
(52, 16),
(52, 16),
(52, 16),
(53, 16),
(53, 16),
(53, 16),
(53, 16),
(54, 16),
(54, 16),
(54, 16),
(54, 16),
(55, 16),
(55, 16),
(55, 16),
(55, 16),
(56, 15),
(56, 15),
(56, 15),
(56, 15),
(57, 16),
(57, 16),
(57, 16),
(57, 16),
(58, 54),
(58, 54),
(58, 54),
(58, 54),
(59, 1),
(59, 1),
(59, 1),
(59, 1),
(60, 16),
(60, 16),
(60, 16),
(60, 16);