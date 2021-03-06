
ALTER DATABASE [Tsk_Junnuo] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Tsk_Junnuo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Tsk_Junnuo] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [Tsk_Junnuo] SET ANSI_NULLS OFF
GO
ALTER DATABASE [Tsk_Junnuo] SET ANSI_PADDING OFF
GO
ALTER DATABASE [Tsk_Junnuo] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [Tsk_Junnuo] SET ARITHABORT OFF
GO
ALTER DATABASE [Tsk_Junnuo] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [Tsk_Junnuo] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [Tsk_Junnuo] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [Tsk_Junnuo] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [Tsk_Junnuo] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [Tsk_Junnuo] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [Tsk_Junnuo] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [Tsk_Junnuo] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [Tsk_Junnuo] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [Tsk_Junnuo] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [Tsk_Junnuo] SET  DISABLE_BROKER
GO
ALTER DATABASE [Tsk_Junnuo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [Tsk_Junnuo] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [Tsk_Junnuo] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [Tsk_Junnuo] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [Tsk_Junnuo] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [Tsk_Junnuo] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [Tsk_Junnuo] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [Tsk_Junnuo] SET  READ_WRITE
GO
ALTER DATABASE [Tsk_Junnuo] SET RECOVERY SIMPLE
GO
ALTER DATABASE [Tsk_Junnuo] SET  MULTI_USER
GO
ALTER DATABASE [Tsk_Junnuo] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [Tsk_Junnuo] SET DB_CHAINING OFF
GO
USE [Tsk_Junnuo]
GO
/****** Object:  Table [dbo].[InspectOrigin]    Script Date: 08/23/2016 14:04:54 ******/
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
CREATE NONCLUSTERED INDEX [NO_PK_InspectOrigin] ON [dbo].[InspectOrigin] 
(
	[ProcessResult] ASC,
	[CreatedAt] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inspect]    Script Date: 08/23/2016 14:04:54 ******/
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
	[ClipScanTime1] [datetime] NULL,
	[ClipScanTime2] [datetime] NULL,
	[TskScanNo] [varchar](100) NULL,
	[TskScanTime3] [datetime] NULL,
	[Time3MinTime2] [float] NULL,
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
/****** Object:  Table [dbo].[User]    Script Date: 08/23/2016 14:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Role] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE UNIQUE NONCLUSTERED INDEX [Name_Index] ON [dbo].[User] 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[Split]    Script Date: 08/23/2016 14:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   FUNCTION [dbo].[Split]  
(  
@c VARCHAR(MAX) ,  
@split VARCHAR(50)  
)  
RETURNS @t TABLE ( col VARCHAR(50) )  
AS  
BEGIN  
    WHILE ( CHARINDEX(@split, @c) <> 0 )  
        BEGIN  
            INSERT  @t( col )  
            VALUES  ( SUBSTRING(@c, 1, CHARINDEX(@split, @c) - 1) )  
            SET @c = STUFF(@c, 1, CHARINDEX(@split, @c), '')  
        END  
    INSERT  @t( col ) VALUES  ( @c )  
    RETURN  
END
GO
/****** Object:  StoredProcedure [dbo].[QueryUserTskDetailData]    Script Date: 08/23/2016 14:04:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[QueryUserTskDetailData]
@UserTskNo varchar(2000),
@StartTime varchar(2000),
@EndTime varchar(2000)
AS
BEGIN
 --select top 100 * from Inspect
 --where (ClipScanTime1 between CONVERT(varchar(100), DateAdd(day,-1,GETDATE()), 23) + ' 06:59:59.999'
 --and CONVERT(varchar(100), GETDATE(), 23) + ' 07:00:00.000')
 --and TskNo in(select * from Split(@UserTskNo,','))
 --order by ClipScanTime1 desc
 select top 100 * from Inspect
 where (ClipScanTime1 between @StartTime
 and @EndTime)
 and TskNo in(select * from Split(@UserTskNo,','))
 order by ClipScanTime1 desc
 
END
GO
/****** Object:  Table [dbo].[UserTsk]    Script Date: 08/23/2016 14:04:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserTsk](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[TskNo] [varchar](50) NULL,
 CONSTRAINT [PK_UserTsk] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[UserInspect]    Script Date: 08/23/2016 14:04:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[UserInspect]
AS
SELECT     dbo.Inspect.Id, dbo.Inspect.TskNo, dbo.Inspect.LeoniNo, dbo.Inspect.CusNo, dbo.Inspect.ClipScanNo, dbo.Inspect.ClipScanTime1, dbo.Inspect.ClipScanTime2, 
                      dbo.Inspect.TskScanNo, dbo.Inspect.TskScanTime3, dbo.Inspect.Time3MinTime2, dbo.Inspect.OkOrNot, dbo.Inspect.CreatedAt, dbo.Inspect.OriginId, 
                      dbo.UserTsk.UserId
FROM         dbo.Inspect INNER JOIN
                      dbo.UserTsk ON dbo.Inspect.TskNo = dbo.UserTsk.TskNo
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Inspect"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 173
               Right = 199
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "UserTsk"
            Begin Extent = 
               Top = 6
               Left = 237
               Bottom = 146
               Right = 379
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserInspect'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserInspect'
GO
/****** Object:  Default [DF_InspectOrigin_Id]    Script Date: 08/23/2016 14:04:54 ******/
ALTER TABLE [dbo].[InspectOrigin] ADD  CONSTRAINT [DF_InspectOrigin_Id]  DEFAULT (newid()) FOR [Id]
GO
/****** Object:  Default [DF_InspectOrigin_CreatedAt]    Script Date: 08/23/2016 14:04:54 ******/
ALTER TABLE [dbo].[InspectOrigin] ADD  CONSTRAINT [DF_InspectOrigin_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
/****** Object:  Default [DF_Inspect_Id]    Script Date: 08/23/2016 14:04:54 ******/
ALTER TABLE [dbo].[Inspect] ADD  CONSTRAINT [DF_Inspect_Id]  DEFAULT (newid()) FOR [Id]
GO
/****** Object:  Default [DF_Inspect_CreatedAt]    Script Date: 08/23/2016 14:04:54 ******/
ALTER TABLE [dbo].[Inspect] ADD  CONSTRAINT [DF_Inspect_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
/****** Object:  ForeignKey [FK_UserTsk_User]    Script Date: 08/23/2016 14:04:55 ******/
ALTER TABLE [dbo].[UserTsk]  WITH CHECK ADD  CONSTRAINT [FK_UserTsk_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserTsk] CHECK CONSTRAINT [FK_UserTsk_User]
GO
