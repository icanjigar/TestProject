<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="app_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1
        {
            width: 100%;
        }

        .auto-style2
        {
            width: 79px;
        }

        .auto-style3
        {
            width: 85px;
        }

        .auto-style4
        {
            width: 72px;
        }

        .auto-style5
        {
            width: 551px;
        }

        .auto-style6
        {
            text-align: left;
        }

        .auto-style7
        {
            width: 551px;
            text-align: left;
        }

        .auto-style8
        {
            background-color: #66FF66;
        }

        .auto-style9
        {
            width: 72px;
            background-color: #66FF66;
        }

        .auto-style10
        {
            width: 85px;
            background-color: #66FF66;
        }

        .auto-style11
        {
            width: 79px;
            background-color: #66FF66;
        }
    </style>
</head>
<body>
    
        <div>

            <table class="auto-style1">
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style4">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style7">
                        <a href="?lang=en" runat="server" id="linkEnglishLang">
                            <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:chienvh.language, langEnglish%>" /></a>
                        <a href="?lang=vi" runat="server" id="linkVietnameseLang">
                            <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:chienvh.language, langVietnamese%>" /></a>
                    </td>
                    <td class="auto-style6"></td>
                </tr>
                <tr>
                    <td class="auto-style11">
                        <a href="#">
                            <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:chienvh.language, mnuHome%>" /></a>
                    </td>
                    <td class="auto-style10">
                        <a href="#">
                            <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:chienvh.language, mnuIntroduction%>" /></a>
                    </td>
                    <td class="auto-style9">
                        <a href="#">
                            <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:chienvh.language, mnuProduct%>" /></a>
                    </td>
                    <td class="auto-style8">
                        <a href="#">
                            <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:chienvh.language, mnuContactUs%>" /></a>
                    </td>
                    <td class="auto-style5">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>

        </div>

</body>
</html>
