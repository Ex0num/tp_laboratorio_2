USE [PersonajesTP4-LopezGasal]
GO
/****** Object:  Table [dbo].[TablaPersonajes]    Script Date: 18/11/2021 23:10:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TablaPersonajes](
	[nombre] [varchar](40) NOT NULL,
	[nivelTotal] [int] NOT NULL,
	[origenElemental] [int] NOT NULL,
	[tipoArma] [int] NOT NULL,
	[ptsAtaqueArma] [int] NOT NULL,
	[ptsDefensaArma] [int] NOT NULL,
	[batallasJugadas] [int] NOT NULL,
	[batallasGanadas] [int] NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Personajes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
