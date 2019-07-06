﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="NotificationCenter.Home" %>

<%@ Import Namespace="NotificationCenter" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style.css" rel="stylesheet" />
    <script src="/Scripts/jquery-1.6.4.js"></script>
    <script src="/Scripts/jquery.signalR-2.2.0.js"></script>
    <!--Reference the autogenerated SignalR hub script. -->
    <script src="/signalr/hubs"></script>
    <script type="text/javascript">

        var loaded = '<% = Messages.Instance.IsCheckCertificateExpiration %>';
        var notificationMessage = '<% = Messages.Instance.NotificationMessages %>';
        $(function () {

            setInterval(poll, 20000)
            setInterval(pollRequests, 5000)

            // Declare a proxy to reference the hub.
            var notifications = $.connection.messagesHub;
            // Create a function that the hub can call to broadcast messages.
            notifications.client.updateMessages = function (msg) {
                if (msg == null) return;
                if (msg.length > 0) {
                    alert(msg);
                   getNotifications();
                }
            };
            // Start the connection.
            $.connection.hub.start().done(function () {
                getNotifications();
            }).fail(function (e) {
                alert(e);
            });
        });


        function poll() {
            if (loaded == 'True') return;
            var clientID = document.getElementById('<%= clientID.ClientID %>').value
            var jsonData = JSON.stringify({ clientID: clientID });
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("~/Home.aspx/IsValidCertificate") %>',
                data: jsonData,
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json", // dataType is json format
                success: handleData
            })
        };

        var handleData = function (data) {
            if (data.d.length > 0) {
                loaded = 'True';
            }
        }

        function pollRequests() {

            var clientID = document.getElementById('<%= clientID.ClientID %>').value
            var jsonData = JSON.stringify({ clientID: clientID });
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("~/Home.aspx/CheckStatusNotification") %>',
                data: jsonData,
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json" // dataType is json format
            })
        };

        function getNotifications() {
            var $tbl = $('#tblJobInfo');
            var clientID = document.getElementById('<%= clientID.ClientID %>').value
                var jsonData = JSON.stringify({ clientID: clientID });
                $.ajax({
                    type: "POST",
                    url: '<%=ResolveUrl("~/Home.aspx/GetNotifications") %>',
                    data: jsonData,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json", // dataType is json format
                    success: function (data) {
                        if (data.d.length > 0) {
                            $tbl.empty();
                            $tbl.append(' <tr><th>ID</th><th>Name</th><th>TypeGroup</th><th>Criteria</th><th>Channel</th></tr>');
                            var rows = [];
                            for (var i = 0; i < data.d.length; i++) {
                                rows.push(' <tr><td>' + data.d[i].NotificationID + '</td><td>' + data.d[i].Name + '</td><td>' + data.d[i].TypeGroup + '</td><td>' + data.d[i].CriteriaGroup +
                                    '</td><td>' + data.d[i].Channel + '</td></tr>');
                            }
                            $tbl.append(rows.join(''));
                        }
                    }
                });
            }

    </script>
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm" runat="server" EnablePageMethods="true" />
        <div>
            Welcome
        <br />
            <asp:LoginName ID="LoginName1" runat="server" Font-Bold="true" />
            <br />
            <br />

            <div>
                <span>Notifications</span>
                <table id="tblJobInfo" style="text-align: center; margin-left: 10px">
                </table>
            </div>
            <span>Client Requests</span>
            <asp:Repeater ID="rptRequestTable" runat="server">
                <HeaderTemplate>
                    <table>
                        <tr class="pb-header">
                            <th scope="col">
                                <asp:LinkButton ID="lnkreq_id" runat="server" ForeColor="white">ID</asp:LinkButton></th>
                            <th scope="col">
                                <asp:LinkButton ID="linreq_type" runat="server" OnClick="linreq_type_Click" ForeColor="white">Type</asp:LinkButton>
                            </th>
                            <th scope="col">
                                <asp:LinkButton ID="lnkreq_date" runat="server" OnClick="lnkreq_date_Click" ForeColor="white">Date</asp:LinkButton>
                            </th>
                            <th scope="col">
                                <asp:LinkButton ID="linreq_status" runat="server" OnClick="linreq_status_Click" ForeColor="white">Status</asp:LinkButton>
                            </th>
                            <th scope="col">Edit</th>
                            <th scope="col">Delete</th>
                        </tr>
                </HeaderTemplate>
                <AlternatingItemTemplate>
                    <tr class="pb-altRow">
                        <td><%# Eval("ClientID") %></td>
                        <td><%# Eval("Type") %> </td>
                        <td><%# Eval("Date") %></td>
                        <td><%# Eval("Status") %></td>
                        <td><a href='<%# "EditRequest.aspx?id=" +  Eval("RequestID") %>'>Edit</a></td>
                        <td>
                            <asp:LinkButton ID="lbtn_Delete" runat="server" OnCommand="lbtn_Delete_Command" CommandArgument='<%# Eval("RequestID") %>'>
                            Delete</asp:LinkButton>
                        </td>

                    </tr>
                </AlternatingItemTemplate>
                <ItemTemplate>
                    <tr class="pb-row">
                        <td><%# Eval("ClientID") %></td>
                        <td><%# Eval("Type") %> </td>
                        <td><%# Eval("Date") %></td>
                        <td><%# Eval("Status") %></td>
                        <td><a href='<%# "EditRequest.aspx?id="  +  Eval("RequestID") %>'>Edit</a></td>
                        <td>
                            <asp:LinkButton ID="lbtn_Delete" runat="server" OnCommand="lbtn_Delete_Command" CommandArgument='<%# Eval("RequestID") %>'>
                            Delete</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <br />
            <asp:Button ID="btnSearch" runat="server" Text="Search " OnClick="btnSearch_Click" />
            <asp:TextBox ID="tb_SearchText" runat="server" />
            <br />
            <br />
            <asp:Button ID="btn_InsetNewRequest" runat="server" Text="Insert New Request" OnClick="btn_InsetNewRequest_Click" />
            <br />
            <br />
            <asp:LoginStatus ID="LoginStatus1" runat="server" />
            <asp:HiddenField ID="clientID" runat="server" />
        </div>
    </form>
</body>
</html>