<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AmigosProximos.aspx.cs" Inherits="API.Consumir.AmigosProximos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    
    <form id="form1" runat="server">
        <div>
               <asp:Button ID="btnCalcular" runat="server" Text="PesquisarAPI" OnClick="btnCalcular_Click" /> 

             <asp:Repeater ID="rptBand" runat="server" OnItemDataBound="rptBand_ItemDataBound">

    <HeaderTemplate>

        <table>

            <thead>

                <tr style="background-color:#aaaaaa;">

                    
                    <th>Id</th>


                    <th>Nome</th>


                    <th>Nome Proximo</th>
                    <th>Localizacao Proxima</th>
                    
   


                </tr>

            </thead>

            <tbody>

    </HeaderTemplate>

    <ItemTemplate>

        <tr>

            <th><%# Container.ItemIndex + 1 %></th>

            <th><%# DataBinder.Eval(Container.DataItem,"Nome") %></th>

            <th></th>

            <th></th>

            <th></th>

        </tr>

        <asp:Repeater ID="rptBandItens" runat="server">

            <ItemTemplate>

                <tr>

                    <th></th>

                    <th></th>

                    <th><%# DataBinder.Eval(Container.DataItem,"Nome Proximo") %></th>

                    <th><%# ((amigosproximos_aspx.RetornoAPIItens)Container.DataItem).NomeProximo %></th>

                    <th><%# String.IsNullOrEmpty((String)DataBinder.Eval(Container.DataItem, "Comment")) ? "No comment" : DataBinder.Eval(Container.DataItem, "Comment")%></th>

                </tr>

            </ItemTemplate>

            <AlternatingItemTemplate>

                <tr style="background-color:#cccccc;">

                    <th></th>

                    <th></th>

                    <th><%# DataBinder.Eval(Container.DataItem, "Localizacao Proxima")%></th>

                    <th><%# ((amigosproximos_aspx.RetornoAPIItens)Container.DataItem).LocalizacaoProximo %></th>

                    <th><%# String.IsNullOrEmpty((String)DataBinder.Eval(Container.DataItem, "Comment")) ? "No comment" : DataBinder.Eval(Container.DataItem, "Comment")%></th>

                </tr>

            </AlternatingItemTemplate>

        </asp:Repeater>

    </ItemTemplate>
 <FooterTemplate>

            </tbody>

            <tfoot >

                
            </tfoot>

        </table>

    </FooterTemplate>

</asp:Repeater>
        </div>
    </form>
</body>
</html>
