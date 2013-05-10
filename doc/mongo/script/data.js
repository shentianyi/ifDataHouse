load("base.js");
load("setting.js");

var parts=[
 {partNr:'SN001',name:'part001-name',unitTime:60},
 {partNr:'SN002',name:'part002-name',unitTime:120},
  {partNr:'SN003',name:'part003-name',unitTime:180},
   {partNr:'SN004',name:'part004-name',unitTime:240},
];

insertData(db,"parts",parts);
