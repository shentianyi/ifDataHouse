Function SendEMail(toAddress,subject,file)
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

oMessage.Subject = strSubject
oMessage.From = strSMTPFrom
oMessage.To = strSMTPTo
oMessage.TextBody = strTextBody
oMessage.AddAttachment file

oMessage.Send
end Function