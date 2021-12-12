<%@ Page Language="C#" %> 
<%
    try
    {
        String strImageName;
        HttpFileCollection files = HttpContext.Current.Request.Files;
        HttpPostedFile uploadfile = files["RemoteFile"];
        strImageName = uploadfile.FileName;
        
        uploadfile.SaveAs(dms.Helper.GetTempDiskPath() + strImageName);
      


    }
    catch
    {
    }
%>