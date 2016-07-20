<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditRequest.aspx.cs" Inherits="NotificationCenter.EditRequest" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lbl_type" runat="server" Text="Type: "></asp:Label>
            &nbsp;
            <asp:TextBox ID="tb_type" ReadOnly="true" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lbl_Date"  runat="server" Text="Date: "></asp:Label>
            &nbsp;
            <asp:TextBox ID="tb_Date" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lbl_Status" runat="server" Text="Status: " />
            <asp:TextBox ID="tb_Status" runat="server" />
            <br />
            <br />
            <asp:Button ID="btn_Update" runat="server" Text="Update changes" OnClick="btn_Update_Click" />
            <asp:Button ID="btn_BackToMainScreen" runat="server" Text="Back to Home" OnClick="btn_BackToMainScreen_Click" />
        </div>
    </form>
</body>
</html>
