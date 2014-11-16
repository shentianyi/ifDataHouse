select a.LeoniNo,a.TskNo, a.OkOrNot,  count( case when a.OkOrNot='1' then 1 else 0 end) as 'OK', count( case when a.OkOrNot='0' then 0 else 1 end) as 'NOK'
 from Inspect a
 where (ClipScanTime1 between CONVERT(varchar(100), DateAdd(day,-1,GETDATE()), 23) + ' 06:59:59.999'
 and CONVERT(varchar(100), GETDATE(), 23) + ' 07:00:00.000') 
  
 group by a.LeoniNo,a.TskNo,a.OkOrNot
  WITH ROLLUP
  having a.LeoniNo is not null
 order by a.LeoniNo,a.TskNo,a.OkOrNot
 
 