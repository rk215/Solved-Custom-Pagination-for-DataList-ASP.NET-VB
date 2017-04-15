<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .button {
            width: 65px;
            height: 34px;
            text-align: center;
            background-color: #5ba9e1;
            color: #e3e3e3;
            border-radius: 4px;
            border: 1px solid #d3d3d3;
            font-family: Verdana, Geneva, Tahoma, sans-serif;
        }

        .buttonp {
            width: 70px;
            height: 34px;
            color: #e3e3e3;
            border-radius: 4px;
            text-align: center;
            background-color: #5ba9e1;
            border: 1px solid #d3d3d3;
            font-family: Verdana, Geneva, Tahoma, sans-serif;
        }

            .button:hover, .buttonp:hover {
                background-color: #0094ff;
                color: #e5e5e5;
                cursor: pointer;
                box-shadow: 0 0 7px #0094ff;
            }

        h2 {
            font-family: Verdana, Geneva, Tahoma, sans-serif;
            font-size: 24px;
            color: #333333;
        }
       .gridline{
           border:1px solid #999;
       }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>The Example of Pagination with Datalist control </h2>
            <asp:DataList ID="DataList1" runat="server" DataKeyField="num"
                DataSourceID="SqlDataSource1" RepeatLayout="Flow" >
                <ItemTemplate>
                   
                    num:
                    <asp:Label ID="numLabel" runat="server" Text='<%# Eval("num") %>' />
                    <br />
                    nam:
                    <asp:Label ID="namLabel" runat="server" Text='<%# Eval("nam") %>' />
                    <br />
                    product:
                    <asp:Label ID="productLabel" runat="server" Text='<%# Eval("product") %>' />
                    <br />
<br />
                </ItemTemplate>
                <SeparatorStyle CssClass="gridline" />
            </asp:DataList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:testConnectionString %>" 
                SelectCommand="SELECT * FROM [tb]"></asp:SqlDataSource>
            <br />
            <asp:Button ID="Button1" CssClass="button" runat="server" Text="First" />
            &nbsp;&nbsp;
        <asp:Button ID="Button2" CssClass="button" runat="server" Text="Next" />
            &nbsp;&nbsp;
        <asp:Button ID="Button3" runat="server" CssClass="buttonp" Text="Previous" />
            &nbsp;&nbsp;
        <asp:Button ID="Button4" CssClass="button" runat="server" Text="Last" />
        </div>
    </form>
</body>
</html>
