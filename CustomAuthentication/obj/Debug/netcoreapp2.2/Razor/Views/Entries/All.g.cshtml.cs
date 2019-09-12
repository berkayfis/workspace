#pragma checksum "C:\Users\berkay.fis\Source\Repos\workspace\CustomAuthentication\Views\Entries\All.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fa533dc5d31ec638a89a22ee95ab8b9b688c6ebb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Entries_All), @"mvc.1.0.view", @"/Views/Entries/All.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Entries/All.cshtml", typeof(AspNetCore.Views_Entries_All))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\berkay.fis\Source\Repos\workspace\CustomAuthentication\Views\_ViewImports.cshtml"
using CustomAuthentication;

#line default
#line hidden
#line 2 "C:\Users\berkay.fis\Source\Repos\workspace\CustomAuthentication\Views\_ViewImports.cshtml"
using CustomAuthentication.Models;

#line default
#line hidden
#line 1 "C:\Users\berkay.fis\Source\Repos\workspace\CustomAuthentication\Views\Entries\All.cshtml"
using BibTeXLibrary;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fa533dc5d31ec638a89a22ee95ab8b9b688c6ebb", @"/Views/Entries/All.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6a0f577bc14eab14ae4800338f6bb8d8aade9388", @"/Views/_ViewImports.cshtml")]
    public class Views_Entries_All : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<BibEntry>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "C:\Users\berkay.fis\Source\Repos\workspace\CustomAuthentication\Views\Entries\All.cshtml"
  
    ViewData["Title"] = "All";

#line default
#line hidden
            BeginContext(85, 314, true);
            WriteLiteral(@"


<table class=""table"">
    <thead class=""thead-dark"">
        <tr>
            <th scope=""col"">#</th>
            <th scope=""col"">Title</th>
            <th scope=""col"">Author</th>
            <th scope=""col"">Year</th>
            <th scope=""col"">Detail</th>
        </tr>
    </thead>
    <tbody>
");
            EndContext();
#line 20 "C:\Users\berkay.fis\Source\Repos\workspace\CustomAuthentication\Views\Entries\All.cshtml"
         foreach (var entry in Model)
        {

#line default
#line hidden
            BeginContext(449, 50, true);
            WriteLiteral("            <tr>\r\n                <th scope=\"row\">");
            EndContext();
            BeginContext(500, 10, false);
#line 23 "C:\Users\berkay.fis\Source\Repos\workspace\CustomAuthentication\Views\Entries\All.cshtml"
                           Write(entry.Type);

#line default
#line hidden
            EndContext();
            BeginContext(510, 27, true);
            WriteLiteral("</th>\r\n                <td>");
            EndContext();
            BeginContext(538, 11, false);
#line 24 "C:\Users\berkay.fis\Source\Repos\workspace\CustomAuthentication\Views\Entries\All.cshtml"
               Write(entry.Title);

#line default
#line hidden
            EndContext();
            BeginContext(549, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(577, 12, false);
#line 25 "C:\Users\berkay.fis\Source\Repos\workspace\CustomAuthentication\Views\Entries\All.cshtml"
               Write(entry.Author);

#line default
#line hidden
            EndContext();
            BeginContext(589, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(617, 10, false);
#line 26 "C:\Users\berkay.fis\Source\Repos\workspace\CustomAuthentication\Views\Entries\All.cshtml"
               Write(entry.Year);

#line default
#line hidden
            EndContext();
            BeginContext(627, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
            BeginContext(870, 73, true);
            WriteLiteral("                <td><button type=\"button\" class=\"btn btn-outline-success\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 943, "\"", 1023, 3);
            WriteAttributeValue("", 953, "showDetail(", 953, 11, true);
#line 28 "C:\Users\berkay.fis\Source\Repos\workspace\CustomAuthentication\Views\Entries\All.cshtml"
WriteAttributeValue("", 964, Newtonsoft.Json.JsonConvert.SerializeObject(@entry._tags), 964, 58, false);

#line default
#line hidden
            WriteAttributeValue("", 1022, ")", 1022, 1, true);
            EndWriteAttribute();
            BeginContext(1024, 42, true);
            WriteLiteral(">Detail</button></td>\r\n            </tr>\r\n");
            EndContext();
#line 30 "C:\Users\berkay.fis\Source\Repos\workspace\CustomAuthentication\Views\Entries\All.cshtml"
        }

#line default
#line hidden
            BeginContext(1077, 1089, true);
            WriteLiteral(@"    </tbody>
</table>

<!-- Modal -->
<div class=""modal fade"" id=""detailDiv"" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalLabel"" aria-hidden=""true"">
  <div class=""modal-dialog"" role=""document"">
    <div class=""modal-content"">
      <div class=""modal-header"">
        <h5 class=""modal-title"" id=""exampleModalLabel"">Modal title</h5>
        <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
          <span aria-hidden=""true"">&times;</span>
        </button>
      </div>
      <div class=""modal-body"">
        ...
      </div>
      <div class=""modal-footer"">
        <button type=""button"" class=""btn btn-secondary"" data-dismiss=""modal"">Close</button>
      </div>
    </div>
  </div>
</div>

<script>
    function showDetail(tagsString) {
        console.log(tagsString);
        $("".modal-body"").html("""");
        $("".modal-body"").append(""<p>""+tagsString.title+""</p>"");
        $(""#detailDiv"").modal('show');        
    }

    function closeDetail() {");
            WriteLiteral("\n        $(\"#detailDiv\").modal(\'hide\');        \r\n    }\r\n</script>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<BibEntry>> Html { get; private set; }
    }
}
#pragma warning restore 1591
