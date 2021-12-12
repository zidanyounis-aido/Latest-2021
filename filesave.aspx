<%@ Page Language="C#" %>
<%
    try{
        String strImageName;
        HttpFileCollection files = HttpContext.Current.Request.Files;
        HttpPostedFile uploadfile = files["RemoteFile"];
        strImageName = uploadfile.FileName;
        uploadfile.SaveAs(dms.Helper.GetUploadDiskPath(strImageName));
    }
    catch(Exception ex){
        Response.Write(ex.Message);
	}
%>