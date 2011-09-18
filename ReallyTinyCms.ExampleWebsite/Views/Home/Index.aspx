<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ReallyTinyCms.Mvc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: ViewData["Message"] %></h2>
    <p>
        To learn more about ASP.NET MVC visit <a href="http://asp.net/mvc" title="ASP.NET MVC Website">http://asp.net/mvc</a>.
    </p>

    <p>
        <%= Html.Cms().ContentFor("HomePageTop") %>
        <%= Html.Cms().ContentFor("HomePageTop2", ()=>@"<b>This is default bold text</b>") %>
    </p>

    <p>
    <%if(Html.Cms().EditEnabledForCurrentRequest()){%>
        Whoop editing enabled for this request.
    <% } %>    
    </p>
</asp:Content>
