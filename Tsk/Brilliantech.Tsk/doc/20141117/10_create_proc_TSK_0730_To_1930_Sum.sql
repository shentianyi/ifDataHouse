USE [Leoni_Tsk_JN]
GO

/****** Object:  StoredProcedure [dbo].[TSK_0730_To_1930_Sum]    Script Date: 11/17/2014 14:19:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[TSK_0730_To_1930_Sum]
@UserTskNo varchar(2000)
AS
BEGIN
 -- total
 select a.LeoniNo,a.TskNo, COUNT( a.OkOrNot) as InspectNum, COUNT(distinct(a.TskNo)) as TskNum
 from Inspect a
 where (ClipScanTime1 between CONVERT(varchar(100), GETDATE() , 23) + ' 07:30:00.000'
 and CONVERT(varchar(100), GETDATE(), 23) + ' 19:29:59.999')
 and TskNo in(select * from Split(@UserTskNo,','))
 group by a.LeoniNo,a.TskNo
 WITH ROLLUP
 having a.LeoniNo is not null
 order by a.LeoniNo,a.TskNo;
 -- detail
 select TskNo,LeoniNo,CusNo,ClipScanNo,ClipScanTime1,ClipScanTime2,TskScanNo,TskScanTime3,Time3MinTime2,OkOrNot,CreatedAt
 from Inspect
 where (ClipScanTime1 between CONVERT(varchar(100), GETDATE() , 23) + ' 07:30:00.000'
 and CONVERT(varchar(100), GETDATE(), 23) + ' 19:29:59.999')
 and TskNo in(select * from Split(@UserTskNo,','))
 order by ClipScanTime1 desc;
END
GO


