<%@ Page Title="" Language="C#" MasterPageFile="~/AppWeb.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="AppWeb.Index" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="content" ContentPlaceHolderID="content" runat="server">
    <form id="form1" runat="server">
    <div class="jumbotron">
        <h1>Titulo de ejemplo</h1>
        <p>Este es un párrafo de ejemplo que no se que va a tener, me imagino 
            que bastante texto para que se llene con palabras y poder ver como queda,
            así que termina acá.
        </p>
        <p>
            <a class="btn btn-lg btn-primary" href="#" role="button">Lindo botón</a>
        </p>
        <asp:ListBox ID="ListBox1" runat="server"></asp:ListBox>
    </div>
    </form>
</asp:Content>
