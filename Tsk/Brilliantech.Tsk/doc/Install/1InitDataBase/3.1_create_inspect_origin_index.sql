USE [Leoni_Tsk_JN]
GO

/****** Object:  Index [NO_PK_InspectOrigin]    Script Date: 08/16/2014 20:44:03 ******/
CREATE NONCLUSTERED INDEX [NO_PK_InspectOrigin] ON [dbo].[InspectOrigin] 
(
	[ProcessResult] ASC,
	[CreatedAt] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

