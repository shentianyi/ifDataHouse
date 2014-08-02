strSMTPFrom = "iwangsong@163.com"
strSMTPTo = "song.wang@cz-tek.com"
strTextBody = "Body of your email"
strSubject = "Subject line"

Dim szBuf, i
i = 0
For Each Arg In WScript.Arguments
          i = i + 1
         szBuf = szBuf & i & ":" & Arg & vbCrLf
Next


Set oMessage = CreateObject("CDO.Message")
oMessage.Configuration.Fields.Item("http://schemas.microsoft.com/cdo/configuration/sendusing") = 2 
oMessage.Configuration.Fields.Item("http://schemas.microsoft.com/cdo/configuration/smtpserver") = "smtp.163.com"
oMessage.Configuration.Fields.Item("http://schemas.microsoft.com/cdo/configuration/sendusername") = "iwangsong@163.com"
oMessage.Configuration.Fields.Item("http://schemas.microsoft.com/cdo/configuration/sendpassword") = "iwangsong520520"
oMessage.Configuration.Fields.Item("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate") = 1
oMessage.Configuration.Fields.Item("http://schemas.microsoft.com/cdo/configuration/smtpserverport") = 25 
oMessage.Configuration.Fields.Update

oMessage.Subject = strSubject
oMessage.From = strSMTPFrom
oMessage.To = strSMTPTo
oMessage.TextBody = strTextBody & szBuf


oMessage.Send