<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PesquisaLocalizacao.aspx.cs" Inherits="API.ConsumirWEBForm.PesquisaLocalizacao" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnPesquisa" runat="server" Text="Pesquisar" OnClick="btnPesquisa_Click" />
            <asp:GridView ID="dgvDados" runat="server"></asp:GridView>

        </div>
    </form>
</body>
</html>
