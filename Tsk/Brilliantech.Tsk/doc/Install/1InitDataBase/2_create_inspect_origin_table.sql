USE [Leoni_Tsk_JN]
GO

/****** Object:  Table [dbo].[InspectOrigin]    Script Date: 08/13/2014 18:02:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[InspectOrigin](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Text] [text] NULL,
	[ProcessResult] [bit] NULL,
	[ProcessMessage] [varchar](1000) NULL,
	[CreatedAt] [datetime] NULL,
 CONSTRAINT [PK_InspectOrigin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[InspectOrigin] ADD  CONSTRAINT [DF_InspectOrigin_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[InspectOrigin] ADD  CONSTRAINT [DF_InspectOrigin_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO

