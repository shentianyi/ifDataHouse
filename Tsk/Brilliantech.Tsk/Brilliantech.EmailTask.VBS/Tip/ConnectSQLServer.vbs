Dim conn
Set conn = CreateObject("ADODB.Connection")
conn.connectionString = "Provider = SQLOLEDB;Data Source=charlot-pc;Initial Catalog=Leoni_Tsk_JN;Persist Security Info=True;User ID=sa;Password=123456@"
conn.Open
Dim m
m=MsgBox("Hello everyone!",65,"Example")
conn.Close
Set conn = Nothing