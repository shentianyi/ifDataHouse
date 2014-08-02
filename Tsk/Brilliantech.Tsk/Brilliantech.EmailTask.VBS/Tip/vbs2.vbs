Dim fsObj : Set fsObj = CreateObject("Scripting.FileSystemObject")
vbsPath = fsObj.GetFile(Wscript.ScriptFullName).ParentFolder.Path
Dim vbsFile : Set vbsFile = fsObj.OpenTextFile(vbsPath & "\vbs1.vbs", 1, False)
Dim myFunctionsStr : myFunctionsStr = vbsFile.ReadAll
vbsFile.Close
Set vbsFile = Nothing
Set fsObj = Nothing
ExecuteGlobal myFunctionsStr

Call SendEMail("song.wang@cz-tek.com","C:\\log.txt")

