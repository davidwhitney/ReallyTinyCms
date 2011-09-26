<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<string>" %>
<div>
    <strong>Text from template:</strong>    
    <p>
        <textarea name="<%:ViewData.TemplateInfo.HtmlFieldPrefix%>" cols="100" rows="5"><%:Model %></textarea>
    </p>
</div>