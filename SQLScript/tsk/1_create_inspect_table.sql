USE [Leoni_Tsk_JN]
GO

/****** Object:  Table [dbo].[Inspect]    Script Date: 08/08/2014 19:17:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Inspect](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[TskNo] [varchar](100) NULL,
	[LeoniNo] [varchar](100) NULL,
	[CusNo] [varchar](100) NULL,
	[ClipScanNo] [varchar](100) NULL,
	[ClipScanTime1] [varchar](50) NULL,
	[ClipScanTime2] [varchar](50) NULL,
	[TskScanNo] [varchar](100) NULL,
	[TskScanTime3] [varchar](50) NULL,
	[Time3MinTime2] [varchar](50) NULL,
	[OkOrNot] [varchar](50) NULL,
	[CreatedAt] [datetime] NULL,
	[OriginId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Inspect] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Inspect] ADD  CONSTRAINT [DF_Inspect_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[Inspect] ADD  CONSTRAINT [DF_Inspect_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO


