USE [ControlAutosDB]
GO
/****** Object:  Table [dbo].[DestinoRuta]    Script Date: 9/7/2023 12:08:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DestinoRuta](
	[IdDestino] [int] IDENTITY(1,1) NOT NULL,
	[NombreDestino] [varchar](100) NOT NULL,
	[Estatus] [bit] NOT NULL,
	[IdRuta] [int] NOT NULL,
	[RutaDestino] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDestino] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ruta]    Script Date: 9/7/2023 12:08:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ruta](
	[IdRuta] [int] IDENTITY(1,1) NOT NULL,
	[NombreRuta] [varchar](60) NOT NULL,
	[RutaInicio] [varchar](60) NOT NULL,
	[Estatus] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRuta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DestinoRuta]  WITH CHECK ADD  CONSTRAINT [FK_Cargo] FOREIGN KEY([IdRuta])
REFERENCES [dbo].[Ruta] ([IdRuta])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DestinoRuta] CHECK CONSTRAINT [FK_Cargo]
GO
