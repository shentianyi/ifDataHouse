Dim fsObj : Set fsObj = CreateObject("Scripting.FileSystemObject")
'currentPath = fsObj.GetFile(WScript.ScriptFullName).ParentFolder.Path
currentPath="C:\InspectTmp"
If Not fsObj.FolderExists(currentPath) Then 
 fsObj.CreateFolder(currentPath)
End If
'load send email vbs
'Dim vbsFile : Set vbsFile = fsObj.OpenTextFile(currentPath & "\SendEMail.vbs", 1, False)
'Dim myFunctionsStr : myFunctionsStr = vbsFile.ReadAll
'vbsFile.Close

'Set vbsFile = Nothing
Set fsObj = Nothing
'ExecuteGlobal myFunctionsStr

Dim userTskEmail(4,2)
userTskEmail(0,0)="song.wang@cz-tek.com"
userTskEmail(0,1)="TSK544"

userTskEmail(1,0)="song.wang@cz-tek.com"
userTskEmail(1,1)="TSK544,TSK045"

userTskEmail(2,0)="song.wang@cz-tek.com"
userTskEmail(2,1)="TSK045"

userTskEmail(3,0)="song.wang@cz-tek.com"
userTskEmail(3,1)="TSK04445"

''MsgBox UBound(userTskEmail,1)
Dim i
For i=0 To UBound(userTskEmail,1)-1

	'query sqlserver with proce
	Dim objConnection, objCommand, objRecordset
	rowCount = 0
	
	Set objConnection = CreateObject("ADODB.Connection")
	objConnection.connectionString = "Provider = SQLOLEDB;Data Source=charlot-pc;Initial Catalog=Leoni_Tsk_JN;Persist Security Info=True;User ID=sa;Password=123456@"
	Set objCommand = CreateObject("ADODB.Command")
	objCommand.CommandType = 4
	objCommand.CommandText = "QueryUserTskDetailData" 
	' set parameter
	Dim p1
	Set p1 = CreateObject("ADODB.Parameter")
	p1.Name = "UserTskNo"
	p1.Type = 200
	p1.Size = 2000
	p1.Direction = 1
	p1.Value = userTskEmail(i,1)
	
	objCommand.Parameters.Append(p1)
	
	objConnection.Open
	Set objCommand.ActiveConnection = objConnection 
	Set objRecordset=objCommand.Execute
	
	'init excel
	Set objExcel = CreateObject("Excel.Application")
	objExcel.Visible = False
	Set objWorkbook = objExcel.Workbooks.Add()
	
	Do Until objRecordset.EOF
		'Wscript.Echo "Name: " & objRecordset.Fields.Item("Id")
		rowCount=rowCount+1
		''objExcel.Cells(rowCount,1).Value=Mid(objRecordset.Fields.Item("Id"),2,36)
		objExcel.Cells(rowCount,1).Value=objRecordset.Fields.Item("TskNo")
		objExcel.Cells(rowCount,2).Value=objRecordset.Fields.Item("LeoniNo")
		objExcel.Cells(rowCount,3).Value=objRecordset.Fields.Item("CusNo")
		objExcel.Cells(rowCount,4).Value=objRecordset.Fields.Item("ClipScanNo")
		objExcel.Cells(rowCount,5).Value=objRecordset.Fields.Item("ClipScanTime1")
		objExcel.Cells(rowCount,6).Value=objRecordset.Fields.Item("ClipScanTime2")
		objExcel.Cells(rowCount,7).Value=objRecordset.Fields.Item("TskScanNo")
		objExcel.Cells(rowCount,8).Value=objRecordset.Fields.Item("TskScanTime3")
		objExcel.Cells(rowCount,9).Value=objRecordset.Fields.Item("Time3MinTime2")
		objExcel.Cells(rowCount,10).Value=objRecordset.Fields.Item("OkOrNot")
		objRecordSet.MoveNext
	Loop
	
	
	objConnection.Close
	Set objConnection = Nothing
	''	MsgBox rowCount
	'call send email with attachment
	If rowCount>0 Then
		
		Set TypeLib = CreateObject("Scriptlet.TypeLib")
		strFileName=Year(Date()) & Right("0" & Month(Date()),2) & Right("0" & Day(Date()),2) & "_" & Mid(TypeLib.Guid, 2, 36) & ".xlsx"
 
		strFilePath=currentPath & "\" & strFileName
		objWorkbook.SaveAs(strFilePath)
	    
	    objWorkbook.Close    
		objExcel.Quit
		
		'Call SendEMail( userTskEmail(i,0),"Tsk Inspect Detail",strFilePath)
		'' send email
		strSMTPFrom = "iwangsong@163.com"
		strSMTPTo = toAddress
		strTextBody = subject
		strSubject = subject
		
		Set oMessage = CreateObject("CDO.Message")
		oMessage.Configuration.Fields.Item("http://schemas.microsoft.com/cdo/configuration/sendusing") = 2 
		oMessage.Configuration.Fields.Item("http://schemas.microsoft.com/cdo/configuration/smtpserver") = "smtp.163.com"
		oMessage.Configuration.Fields.Item("http://schemas.microsoft.com/cdo/configuration/sendusername") = "iwangsong@163.com"
		oMessage.Configuration.Fields.Item("http://schemas.microsoft.com/cdo/configuration/sendpassword") = "iwangsong520520"
		oMessage.Configuration.Fields.Item("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate") = 1
		oMessage.Configuration.Fields.Item("http://schemas.microsoft.com/cdo/configuration/smtpserverport") = 25 
		oMessage.Configuration.Fields.Update

		oMessage.Subject = "Tsk Inspect Detail"
		oMessage.From = strSMTPFrom
		oMessage.To = userTskEmail(i,0)
		oMessage.TextBody = strTextBody
		oMessage.AddAttachment strFilePath
		
		oMessage.Send
		Else 
		 objWorkbook.Close
	   	objExcel.Quit
	End If 
	objExcel.Quit
Next