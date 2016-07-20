<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertRequest.aspx.cs" Inherits="NotificationCenter.InsertRequest" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblType" runat="server" Text="Type:" />
            &nbsp;
       
            <asp:TextBox ID="tbType" runat="server" />
            <br />
            <br />
            <asp:Label ID="lblDate" runat="server" Text="Date: " />
            &nbsp;
       
            <asp:TextBox ID="tbDate" ReadOnly="true" runat="server" />
            <br />
            <br />
            <asp:Label ID="lblStatus" runat="server" Text="Status: " />
       
            <asp:TextBox ID="tbStatus" runat="server" />
            <br />
            <br />
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            <asp:Button ID="btn_back" runat="server" Text="Back to Home" OnClick="btn_back_Click" />
        </div>
    </form>
</body>
</html>
