USE [Leoni_Tsk_JN]
GO

/****** Object:  StoredProcedure [dbo].[QueryUserTskDetailData]    Script Date: 08/04/2014 08:58:03 ******/
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
@UserTskNo varchar(2000)
AS
BEGIN
 -- total
 select top 10 TskNo,LeoniNo,CusNo,ClipScanNo,ClipScanTime1,ClipScanTime2,TskScanNo,TskScanTime3,Time3MinTime2,OkOrNot
 from Inspect
 where (ClipScanTime1 between CONVERT(varchar(100), DateAdd(day,-1,GETDATE()), 23) + ' 06:59:59.999'
 and CONVERT(varchar(100), GETDATE(), 23) + ' 07:00:00.000')
 and TskNo in(select * from Split(@UserTskNo,','))
 order by ClipScanTime1 desc;
 -- detail
 select TskNo,LeoniNo,CusNo,ClipScanNo,ClipScanTime1,ClipScanTime2,TskScanNo,TskScanTime3,Time3MinTime2,OkOrNot,CreatedAt
 from Inspect
 where (ClipScanTime1 between CONVERT(varchar(100), DateAdd(day,-1,GETDATE()), 23) + ' 06:59:59.999'
 and CONVERT(varchar(100), GETDATE(), 23) + ' 07:00:00.000')
 and TskNo in(select * from Split(@UserTskNo,','))
 order by ClipScanTime1 desc;
END

GO


