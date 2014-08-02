strFileName = "c:\test.xlsx"

Set objExcel = CreateObject("Excel.Application")
objExcel.Visible = True

Set objWorkbook = objExcel.Workbooks.Add()
objWorkbook.SaveAs(strFileName)

objExcel.Quit