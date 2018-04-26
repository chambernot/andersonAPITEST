<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AmigosProximos.aspx.cs" Inherits="API.Consumir.AmigosProximos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    
    <form id="form1" runat="server">
        <div>
               <asp:Button ID="btnCalcular" runat="server" Text="PesquisarAPI" OnClick="btnCalcular_Click" />  <br /> <br />
            <asp:Repeater ID="rptBand" runat="server" OnItemDataBound="rptBand_ItemDataBound">

                <ItemTemplate>

                    Nome:

                    <%# Eval("Nome") %>

                    <br />

                    
                    Endereços Proximos:

                    <asp:Repeater ID="rptBandItens" runat="server">

                        <HeaderTemplate>

                            <table>

                                <tr style="font-weight: bold">

                                    <td>

                                        Nome Proximo</td>

                                    <td>

                                        Endereço Proximo</td>

                                </tr>

                        </HeaderTemplate>

                        <FooterTemplate>

                            </table>

                        </FooterTemplate>

                        <ItemTemplate>

                            <tr style="background-color: Gray">

                                <td>

                                    <%# Eval("NomeProximo") %>

                                </td>

                                <td>

                                    <%# Eval("LocalizacaoProximo")%>

                                </td>

                            </tr>

                        </ItemTemplate>

                        <AlternatingItemTemplate>

                           <tr style="background-color: Gray">

                                <td>

                                    <%# Eval("NomeProximo") %>

                                </td>

                                <td>

                                    <%# Eval("LocalizacaoProximo")%>

                                </td>

                            </tr>


                        </AlternatingItemTemplate>

                    </asp:Repeater>

                </ItemTemplate>

                <SeparatorTemplate>

                    <hr />

                </SeparatorTemplate>

            </asp:Repeater>

             
        </div>
    </form>
</body>
</html>
