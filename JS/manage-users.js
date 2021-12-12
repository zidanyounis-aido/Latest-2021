function LoadAllUsersByMeta() {
    $("#tabledialog").find("tbody").html("");
   // alert();
    $.ajax({
        type: "POST",
        url: "/AjexServer/ajexresponse.aspx/BindAllusers",
        data: "{ID:'" + openMetaId + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsdata = JSON.parse(data.d);
            var html = "";
            for (var i = 0; i < jsdata.length; i++) {
                html += "<tr class=\"truser\" style=\"color: Black; background-color: #EEEEEE;\" data-id='" + jsdata[i].Id + "'>";
                html += "                        <td style=\"width: 8%;\">" + jsdata[i].Id + "<\/td>";
                html += "                        <td style=\"width: 15%;\">";
                html += "                            <span id=\"ContentPlaceHolderBody_grdWFDet_lblRcType_0\">" + jsdata[i].Name + "<\/span>";
                html += "                        <\/td>";
                html += "                        <td style=\"width: 15%;\">";
                html += "                            <span id=\"ContentPlaceHolderBody_grdWFDet_lblRcpID_0\">";
                if (jsdata[i].isRead) {
                    html += "                                <input type=\"checkbox\" class=\"chkread\" data-id='" + jsdata[i].Id + "' checked='checked' \/>";
                }
                else {
                    html += "                                <input type=\"checkbox\" class=\"chkread\" data-id='" + jsdata[i].Id + "'  \/>";
                }
                html += "                       <\/span> <\/td>";
                html += "                        <td style=\"width: 15%;\">";
                html += "                            <span id=\"ContentPlaceHolderBody_grdWFDet_lblCmpID_0\">";
                if (jsdata[i].isEdit) {
                    html += "                                <input type=\"checkbox\" checked='checked' class=\"chkedit\" data-id=data-id='" + jsdata[i].Id + "' \/>";
                }
                else {
                    html += "                                <input type=\"checkbox\"  class=\"chkedit\" data-id=data-id='" + jsdata[i].Id + "' \/>";
                }
                html += "                        <\/span><\/td>";
                html += "                    <\/tr>";
            }
            $("#tabledialog").find("tbody").html(html);
            //$('#ContentPlaceHolder1__scheduleControl_ddlRooms').empty();
            //$.each(jsdata, function (key, value) {

            //    $('#ContentPlaceHolder1__scheduleControl_ddlRooms').append($("<option></option>").val(value.ID).html(value.Name));
            //});
            //fillProcAJX();
        },
        error: function (result) {
            // alert("Error");
        }
    });
}

function SaveMetaDataUsers() {
    var userList = [];
    var collection = $("#tabledialog").find("tbody").find("tr");
    for (var i = 0; i < collection.length; i++) {
        var user = { Id: $(collection[i]).data("id"), isRead: $(collection[i]).find("td").find(".chkread").is(":checked"), isEdit: $(collection[i]).find("td").find(".chkedit").is(":checked") };
        userList.push(user);
    }
    $.ajax({
        type: "POST",
        url: "/AjexServer/ajexresponse.aspx/SavePermisstion",
        data: "{jsonData:'" + JSON.stringify(userList) + "',metaId:'" + openMetaId + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsdata = JSON.parse(data.d);
            if (jsdata) {
                $(".ui-icon-closethick").click()
            }
            else {
                alert("error please try agan later");
            }
        },
        error: function (result) {
            // alert("Error");
        }
    });
}