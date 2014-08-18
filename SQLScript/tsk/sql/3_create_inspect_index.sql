USE [Leoni_Tsk_JN]
GO

/****** Object:  Index [No_PK_Inspect]    Script Date: 08/14/2014 18:13:22 ******/
CREATE NONCLUSTERED INDEX [No_PK_Inspect] ON [dbo].[Inspect] 
(
	[TskNo] ASC,
	[LeoniNo] ASC,
	[CusNo] ASC,
	[ClipScanNo] ASC,
	[ClipScanTime1] ASC,
	[ClipScanTime2] ASC,
	[TskScanNo] ASC,
	[TskScanTime3] ASC,
	[Time3MinTime2] ASC,
	[OkOrNot] ASC,
	[CreatedAt] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

