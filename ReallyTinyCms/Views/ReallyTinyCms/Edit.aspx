<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="ReallyTinyCms.Core.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<% var model = ViewData.Model as CmsContentItem; %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Really Tiny Cms - Admin!</title>
</head>
<body>
    <h1>Content Item Name: <%:model.Name%></h1>
    <h2>Content</h2>
    <div style="width: 80%; border: 1px solid black; padding: 50px;">
        <%= model.Content%>    
    </div>    
    <h2>Update</h2>
    <div style="width: 80%; border: 1px solid black; padding: 50px;">
    <% var routeValue = new RouteValueDictionary();
       foreach (var key in Request.QueryString.AllKeys)
       {
           var value = Request.QueryString[key];
           routeValue.Add(key, value);
       }
       %>

        <% using (Html.BeginForm("Edit", "ReallyTinyCms", routeValue)){%>
        <input type="hidden" name="name" id="name" value="<%=model.Name%>"/>
        <textarea id="content" name="content" rows="1" cols="1" style="width: 80%; border: 1px solid black; padding: 50px;"><%=model.Content%></textarea> 
        <input type="submit" value="Save changes"/>  
        <% } %>
    </div>
</body>
</html>
