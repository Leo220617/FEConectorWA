CREATE TABLE [dbo].[BandejaEntrada](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[XmlFactura] [varbinary](max) NULL,
	[XmlConfirmacion] [varchar](max) NULL,
	[Pdf] [varbinary](max) NULL,
	[FechaIngreso] [datetime] NOT NULL,
	[Procesado] [varchar](1) NULL,
	[FechaProcesado] [datetime] NULL,
	[Mensaje] [varchar](max) NULL,
	[Asunto] [varchar](max) NULL,
	[Remitente] [varchar](max) NULL,
	[NumeroConsecutivo] [varchar](100) NULL,
	[TipoDocumento] [varchar](20) NULL,
	[FechaEmision] [varchar](20) NULL,
	[NombreEmisor] [varchar](200) NULL,
	[IdEmisor] [varchar](100) NULL,
	[CodigoMoneda] [varchar](20) NULL,
	[TotalComprobante] [money] NULL,
	[tipo] [varchar](3) NULL,
	[Impuesto] [money] NULL,
	[DetalleMensaje] [varchar](500) NULL,
	[CodigoActividad] [varchar](20) NULL,
	[CondicionImpuesto] [varchar](3) NULL,
	[impuestoAcreditar] [money] NULL,
	[gastoAplicable] [money] NULL,
	[situacionPresentacion] [varchar](2) NULL,
	[tipoIdentificacionEmisor] [varchar](3) NULL,
	[JSON] [varchar](max) NULL,
	[RespuestaHacienda] [varchar](50) NULL,
	[XMLRespuesta] [varchar](max) NULL,
	[ClaveReceptor] [varchar](100) NULL,
	[ConsecutivoReceptor] [varchar](50) NULL,
	[IVA0] [money] NULL,
	[IVA1] [money] NULL,
	[IVA2] [money] NULL,
	[IVA4] [money] NULL,
	[IVA8] [money] NULL,
	[IVA13] [money] NULL,
 CONSTRAINT [PK_BandejaEntrada] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[BandejaEntrada] ADD  CONSTRAINT [DF_BandejaEntrada_FechaIngreso]  DEFAULT (getdate()) FOR [FechaIngreso]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'05 aceptacion, 06 parcial, 07 rechazado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BandejaEntrada', @level2type=N'COLUMN',@level2name=N'tipo'
GO
------------------------------------------------

CREATE TABLE [dbo].[Barrios](
	[CodProvincia] [int] NOT NULL,
	[CodCanton] [int] NOT NULL,
	[CodDistrito] [int] NOT NULL,
	[CodBarrio] [int] NOT NULL,
	[NomBarrio] [varchar](100) NULL,
 CONSTRAINT [PK_Barrios] PRIMARY KEY CLUSTERED 
(
	[CodProvincia] ASC,
	[CodCanton] ASC,
	[CodDistrito] ASC,
	[CodBarrio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

---------------------------
CREATE TABLE [dbo].[BitacoraErrores](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[DocNum] [varchar](max) NULL,
	[Type] [varchar](2) NULL,
	[Descripcion] [varchar](max) NULL,
	[StackTrace] [varchar](max) NULL,
	[Fecha] [datetime] NULL,
 CONSTRAINT [PK_BitacoraErrores] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[BitacoraErrores] ADD  CONSTRAINT [DF_BitacoraErrores_Fecha]  DEFAULT (getdate()) FOR [Fecha]
GO

--------------------------------------
CREATE TABLE [dbo].[Cantones](
	[CodProvincia] [int] NOT NULL,
	[CodCanton] [int] NOT NULL,
	[NomCanton] [varchar](30) NULL,
 CONSTRAINT [PK_Cantones] PRIMARY KEY CLUSTERED 
(
	[CodProvincia] ASC,
	[CodCanton] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

----------------------------------
CREATE TABLE [dbo].[Clientes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](200) NULL,
	[Cedula] [varchar](12) NULL,
	[TipoCedula] [varchar](2) NULL,
	[Email] [varchar](100) NULL,
	[CodPais] [varchar](50) NULL,
	[Telefono] [varchar](50) NULL,
	[provincia] [int] NULL,
	[canton] [varchar](2) NULL,
	[distrito] [varchar](2) NULL,
	[barrio] [varchar](2) NULL,
	[sennas] [varchar](250) NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

---------------------------------
CREATE TABLE [dbo].[CondicionesVenta](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codCyber] [varchar](2) NULL,
	[Nombre] [varchar](100) NULL,
 CONSTRAINT [PK_CondicionesVenta] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
----------------------------------------
CREATE TABLE [dbo].[CorreosRecepcion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[RecepcionEmail] [varchar](500) NULL,
	[RecepcionPassword] [varchar](500) NULL,
	[RecepcionHostName] [varchar](50) NULL,
	[RecepcionUseSSL] [bit] NULL,
	[RecepcionPort] [int] NULL,
	[RecepcionUltimaLecturaImap] [datetime] NULL,
 CONSTRAINT [PK_CorreosRecepcion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
----------------------------------------------
CREATE TABLE [dbo].[DetDocumento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idEncabezado] [int] NOT NULL,
	[NumLinea] [int] NULL,
	[partidaArancelaria] [varchar](15) NULL,
	[exportacion] [money] NULL,
	[CodCabys] [varchar](13) NULL,
	[tipoCod] [varchar](2) NULL,
	[codPro] [varchar](20) NULL,
	[cantidad] [money] NULL,
	[unidadMedida] [varchar](5) NULL,
	[unidadMedidaComercial] [varchar](20) NULL,
	[NomPro] [varchar](200) NULL,
	[PrecioUnitario] [money] NULL,
	[MontoTotal] [money] NULL,
	[MontoDescuento] [money] NULL,
	[NaturalezaDescuento] [varchar](80) NULL,
	[SubTotal] [money] NULL,
	[baseImponible] [money] NULL,
	[idImpuesto] [int] NULL,
	[montoImpuesto] [money] NULL,
	[factorIVA] [money] NULL,
	[exonTipoDoc] [varchar](2) NULL,
	[exonNumdoc] [varchar](40) NULL,
	[exonNomInst] [varchar](160) NULL,
	[exonFecEmi] [datetime] NULL,
	[exonPorExo] [int] NULL,
	[exonMonExo] [money] NULL,
	[impNeto] [money] NULL,
	[totalLinea] [money] NULL,
 CONSTRAINT [PK_DetDocumento] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DetDocumento]  WITH CHECK ADD  CONSTRAINT [FK_DetDocumento_EncDocumento] FOREIGN KEY([idEncabezado])
REFERENCES [dbo].[EncDocumento] ([id])
GO

ALTER TABLE [dbo].[DetDocumento] CHECK CONSTRAINT [FK_DetDocumento_EncDocumento]
GO

ALTER TABLE [dbo].[DetDocumento]  WITH CHECK ADD  CONSTRAINT [FK_DetDocumento_Impuestos] FOREIGN KEY([idImpuesto])
REFERENCES [dbo].[Impuestos] ([id])
GO

ALTER TABLE [dbo].[DetDocumento] CHECK CONSTRAINT [FK_DetDocumento_Impuestos]
GO

---------------------------------------------
CREATE TABLE [dbo].[Distritos](
	[CodProvincia] [int] NOT NULL,
	[CodCanton] [int] NOT NULL,
	[CodDistrito] [int] NOT NULL,
	[NomDistrito] [varchar](30) NULL,
 CONSTRAINT [PK_Distritos] PRIMARY KEY CLUSTERED 
(
	[CodProvincia] ASC,
	[CodCanton] ASC,
	[CodDistrito] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

------------------------------------
CREATE TABLE [dbo].[EncDocumento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idSucursal] [varchar](3) NOT NULL,
	[consecutivoInterno] [int] NULL,
	[TipoDocumento] [varchar](2) NULL,
	[Fecha] [datetime] NULL,
	[CodActividadEconomica] [varchar](50) NULL,
	[CodCliente] [int] NULL,
	[NombreCliente] [varchar](200) NULL,
	[CedulaCliente] [varchar](12) NULL,
	[Email] [varchar](100) NULL,
	[TipoIdentificacion] [varchar](2) NULL,
	[condicionVenta] [int] NULL,
	[plazoCredito] [int] NULL,
	[medioPago] [varchar](15) NULL,
	[montoOtrosCargos] [money] NULL,
	[moneda] [varchar](3) NULL,
	[tipoCambio] [money] NULL,
	[totalserviciogravado] [money] NULL,
	[totalservicioexento] [money] NULL,
	[totalservicioexonerado] [money] NULL,
	[totalmercaderiagravado] [money] NULL,
	[totalmercaderiaexonerado] [money] NULL,
	[totalmercaderiaexenta] [money] NULL,
	[totalgravado] [money] NULL,
	[totalexento] [money] NULL,
	[totalexonerado] [money] NULL,
	[totalventa] [money] NULL,
	[totaldescuentos] [money] NULL,
	[totalventaneta] [money] NULL,
	[totalimpuestos] [money] NULL,
	[totalivadevuelto] [money] NULL,
	[totalotroscargos] [money] NULL,
	[totalcomprobante] [money] NULL,
	[RefTipoDocumento] [varchar](2) NULL,
	[RefNumeroDocumento] [varchar](50) NULL,
	[RefFechaEmision] [datetime] NULL,
	[RefCodigo] [varchar](2) NULL,
	[RefRazon] [varchar](180) NULL,
	[procesadaHacienda] [bit] NULL,
	[RespuestaHacienda] [varchar](500) NULL,
	[XMLFirmado] [varchar](max) NULL,
	[ClaveHacienda] [varchar](55) NULL,
	[ConsecutivoHacienda] [varchar](25) NULL,
	[ErrorCyber] [varchar](max) NULL,
	[code] [int] NULL,
	[JSON] [varchar](max) NULL,
 CONSTRAINT [PK_EncDocumento] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
-------------------------------------------

CREATE TABLE [dbo].[Exoneraciones](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TipoDoc] [varchar](50) NULL,
	[NumDoc] [varchar](50) NULL,
	[NomInst] [varchar](500) NULL,
	[FechaEmision] [datetime] NULL,
	[PorExon] [int] NULL,
 CONSTRAINT [PK_Exoneraciones] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--------------------------------------

CREATE TABLE [dbo].[Impuestos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codigo] [varchar](2) NULL,
	[codigoTarifa] [varchar](2) NULL,
	[tarifa] [decimal](4, 2) NULL,
 CONSTRAINT [PK_Impuestos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
---------------------------------

CREATE TABLE [dbo].[Login](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idRol] [int] NULL,
	[Email] [varchar](200) NULL,
	[Nombre] [varchar](100) NULL,
	[Activo] [bit] NULL,
	[Clave] [varchar](500) NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
---------------------------------
CREATE TABLE [dbo].[OtrosCargos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idEncabezado] [int] NULL,
	[tipoDocumento] [varchar](2) NULL,
	[detalle] [varchar](160) NULL,
	[porcentaje] [decimal](5, 5) NULL,
	[monto] [money] NULL,
 CONSTRAINT [PK_OtrosCargos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
-----------------------------
CREATE TABLE [dbo].[OtrosTextos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idEncabezado] [int] NULL,
	[codigo] [varchar](2) NULL,
	[detalle] [varchar](50) NULL,
 CONSTRAINT [PK_OtrosTextos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-------------------------------
CREATE TABLE [dbo].[Parametros](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[urlCyber] [varchar](500) NULL,
	[urlCyberRespHacienda] [varchar](500) NULL,
	[urlCyberAceptacion] [varchar](500) NULL,
	[urlCyberReenvio] [varchar](500) NULL,
	[urlWebApi] [varchar](500) NULL,
	[urlCyberConsultaHacienda] [varchar](500) NULL,
 CONSTRAINT [PK_Parametros] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-----------------------------------
CREATE TABLE [dbo].[Productos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TipoCodigo] [varchar](2) NULL,
	[Codigo] [varchar](20) NULL,
	[UnidadMedida] [int] NULL,
	[Nombre] [varchar](200) NULL,
	[PrecioUnitario] [money] NULL,
	[Impuesto] [int] NULL,
	[Cabys] [varchar](13) NULL,
 CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
------------------------------
CREATE TABLE [dbo].[RespuestasCyber](
	[id] [int] NOT NULL,
	[Detalle] [varchar](300) NULL,
 CONSTRAINT [PK_RespuestasCyber] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

---------------------------------------
CREATE TABLE [dbo].[Roles](
	[idRol] [int] IDENTITY(1,1) NOT NULL,
	[NombreRol] [varchar](50) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[idRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
----------------------------
CREATE TABLE [dbo].[SeguridadModulos](
	[CodModulo] [int] NOT NULL,
	[Descripcion] [varchar](150) NOT NULL,
 CONSTRAINT [PK_SeguridadModulos_1] PRIMARY KEY CLUSTERED 
(
	[CodModulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
----------------------------------
CREATE TABLE [dbo].[SeguridadRolesModulos](
	[CodRol] [int] NOT NULL,
	[CodModulo] [int] NOT NULL,
 CONSTRAINT [PK_SeguridadRolesModulos_1] PRIMARY KEY CLUSTERED 
(
	[CodRol] ASC,
	[CodModulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
-------------------------------
CREATE TABLE [dbo].[Sucursales](
	[codSuc] [varchar](3) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[NombreComercial] [varchar](100) NULL,
	[Terminal] [varchar](5) NULL,
	[TipoCedula] [varchar](2) NULL,
	[Cedula] [varchar](12) NULL,
	[Provincia] [varchar](1) NULL,
	[Canton] [varchar](2) NULL,
	[Distrito] [varchar](2) NULL,
	[Barrio] [varchar](2) NULL,
	[sennas] [varchar](250) NULL,
	[Telefono] [varchar](20) NULL,
	[Correo] [varchar](100) NULL,
	[Logo] [varchar](max) NULL,
	[ApiKey] [varchar](max) NULL,
	[consecFac] [int] NULL,
	[consecTiq] [int] NULL,
	[consecNC] [int] NULL,
	[consecND] [int] NULL,
	[consecFEC] [int] NULL,
	[consecFEE] [int] NULL,
	[consecAFC] [int] NULL,
	[codPais] [varchar](3) NULL,
	[idConexion] [int] NOT NULL,
	[CodActividadComercial] [varchar](6) NULL,
 CONSTRAINT [PK_Sucursaless] PRIMARY KEY CLUSTERED 
(
	[codSuc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
-------------------------------
CREATE TABLE [dbo].[UnidadesMedida](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codCyber] [varchar](7) NULL,
	[Nombre] [varchar](200) NULL,
 CONSTRAINT [PK_UnidadesMedida] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO