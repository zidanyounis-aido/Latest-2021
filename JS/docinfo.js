$(document).on("click", ".signOpen", function (e) {
    e.preventDefault();
    //alert($(this).attr("data-id"));
    //try {
    //    $('#signModal').dialog({ });
    //} catch (e) {
    //    alert(e.message);
    //}
    $.ajax({
        type: "POST",
        url: "/AjexServer/ajexresponse.aspx/GetDocSignture",
        data: "{ID:'" + $(this).attr("data-id") + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsdata = JSON.parse(data.d);
            var html = "";
            for (var i = 0; i < jsdata.length; i++) {
                html += "<tr c>";
                html += '<td text-align: right;>' + jsdata[i].Name + '</td>';
                html += "                    <\/tr>";
            }
            $("#tblSign").html(html);
            $('#signModal').dialog({});
        },
        error: function (result) {
            // alert("Error");
        }
    });
});