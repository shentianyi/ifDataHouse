select a.LeoniNo,a.TskNo, COUNT( a.OkOrNot) as InspectNum, COUNT(distinct(a.TskNo)) as TskNum
 from Inspect a
 where (ClipScanTime1 between CONVERT(varchar(100), DateAdd(day,-1,GETDATE()), 23) + ' 06:59:59.999'
 and CONVERT(varchar(100), GETDATE(), 23) + ' 07:00:00.000') 
 group by a.LeoniNo,a.TskNo
  WITH ROLLUP
 having a.LeoniNo is not null
 order by a.LeoniNo,a.TskNo
 
 select CONVERT(varchar(100), GETDATE() , 23) + ' 07:29:59.999'
 
 select a.LeoniNo,a.TskNo, COUNT( a.OkOrNot) as InspectNum, COUNT(distinct(a.TskNo)) as TskNum
 from Inspect a
 where (ClipScanTime1 between CONVERT(varchar(100), GETDATE() , 23) + ' 07:30:00.000'
 and CONVERT(varchar(100), GETDATE(), 23) + ' 19:29:59.999')
 group by a.LeoniNo,a.TskNo
  WITH ROLLUP
 having a.LeoniNo is not null
 order by a.LeoniNo,a.TskNo
 
 select a.LeoniNo,a.TskNo, COUNT( a.OkOrNot) as InspectNum, COUNT(distinct(a.TskNo)) as TskNum
 from Inspect a
 where (ClipScanTime1 between CONVERT(varchar(100), GETDATE() , 23) + ' 07:30:00.000'
 and CONVERT(varchar(100), GETDATE(), 23) + ' 19:29:59.999')
 and TskNo in(select * from Split('TSK047,TSK044,TSK045,TSK046',','))
 group by a.LeoniNo,a.TskNo
 WITH ROLLUP
 having a.LeoniNo is not null
 order by a.LeoniNo,a.TskNo;
 