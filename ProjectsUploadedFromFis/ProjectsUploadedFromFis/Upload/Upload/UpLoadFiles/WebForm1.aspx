<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="UpLoadFiles.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
       <script language="JavaScript" type="text/javascript">  
         MyObject = new ActiveXObject( "WScript.Shell" )  
         function RunExe()   
         {  
            MyObject.Run("file:///C:/WindowsFormsApplication1.exe") ;  
            return false;
        }  
      
    </script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Run a Program</h1> 
    This script launch the file any Exe File<p> 
    <button onclick="RunExe()">Run Exe File</button> 
    </div>
    </form>
</body>
</html>


