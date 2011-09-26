<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ReallyTinyCms.Mvc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%:Html.ContentFor("Index-Heading1", () => "Welcome to ASP.NET MVC!")%></h2>
    <p>
        <%:Html.ContentFor("Index-Paragraph1", () => "This is a server side example of TinyCms. Click on the Edit button to edit the content of this page.")%>
    </p>

    <%:Html.ContentFor("Index-TemplateExample1", () => "Here is an example using a template")%>

    <p>
        <%if(Html.EditEnabledForCurrentRequest()){%>
            Whoop editing enabled for this request.
        <% } %>    
    </p>
</asp:Content>
