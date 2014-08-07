USE [Leoni_Tsk_JN]
GO

/****** Object:  Table [dbo].[Inspect]    Script Date: 08/07/2014 19:02:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Inspect](
	[TskNo] [varchar](50) NULL,
	[LeoniNo] [varchar](50) NULL,
	[CusNo] [varchar](50) NULL,
	[ClipScanNo] [varchar](100) NULL,
	[ClipScanTime1] [datetime] NULL,
	[ClipScanTime2] [datetime] NULL,
	[TskScanNo] [varchar](100) NULL,
	[TskScanTime3] [datetime] NULL,
	[OkOrNot] [tinyint] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


