#pragma checksum "C:\Users\Murad\source\repos\FinalProjectMedimall\FinalProjectMedimall\Areas\Medimalladmin\Views\Dashboard\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4d4eaa05504608fce6a56d1c9f11f441ae8b3c1a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Medimalladmin_Views_Dashboard_Index), @"mvc.1.0.view", @"/Areas/Medimalladmin/Views/Dashboard/Index.cshtml")]
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
#nullable restore
#line 1 "C:\Users\Murad\source\repos\FinalProjectMedimall\FinalProjectMedimall\Areas\Medimalladmin\_ViewImports.cshtml"
using FinalProjectMedimall.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Murad\source\repos\FinalProjectMedimall\FinalProjectMedimall\Areas\Medimalladmin\_ViewImports.cshtml"
using FinalProjectMedimall.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4d4eaa05504608fce6a56d1c9f11f441ae8b3c1a", @"/Areas/Medimalladmin/Views/Dashboard/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5890871ce9f60842a2fc1a046bc1c319554e51ac", @"/Areas/Medimalladmin/_ViewImports.cshtml")]
    #nullable restore
    public class Areas_Medimalladmin_Views_Dashboard_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<HomeVM>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width: 3rem;margin-right:10px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/admin/images/best-price.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/image/app.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/image/box.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/admin/images/user.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Murad\source\repos\FinalProjectMedimall\FinalProjectMedimall\Areas\Medimalladmin\Views\Dashboard\Index.cshtml"
  
    int categorycount = 0;
    int productcount = 0;
    int usercount = 0;
    decimal Ordercount = 0;

    foreach (Category item in Model.Categories)
        categorycount++;

    foreach (Medicine item in Model.Medicines)
        productcount++;
    foreach (var item in Model.Orders)
    {
        if (item.Status==true)
        {
            Ordercount+=item.TotalPrice;
        }
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "C:\Users\Murad\source\repos\FinalProjectMedimall\FinalProjectMedimall\Areas\Medimalladmin\Views\Dashboard\Index.cshtml"
  foreach (AppUser user in ViewBag.users)
{
    usercount++;
}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<!-- partial -->
<div class=""main-panel"">
    <div class=""content-wrapper"">

        <div class=""row"">
            <div class=""col-md-12 grid-margin"">
                <div class=""d-flex justify-content-between flex-wrap"">
                </div>
            </div>
        </div>
        <div class=""row"">
            <div class=""col-md-12 grid-margin stretch-card"">
                <div class=""card"">
                    <div class=""card-body dashboard-tabs p-0"">
                        <ul class=""nav nav-tabs px-4"" role=""tablist"">
                            <li class=""nav-item"">
                                <a class=""nav-link active"" id=""overview-tab"" data-bs-toggle=""tab"" href=""#overview"" role=""tab"" aria-controls=""overview"" aria-selected=""true"">Main</a>
                            </li>
");
            WriteLiteral(@"                        </ul>
                        <div class=""tab-content py-0 px-0"">
                            <div class=""tab-pane fade show active"" id=""overview"" role=""tabpanel"" aria-labelledby=""overview-tab"">
                                <div class=""d-flex flex-wrap justify-content-xl-between"">
                                    <div class=""d-none d-xl-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item"">
                                        <i class=""mdi mdi-calendar-heart icon-lg me-3 text-primary""></i>
                                        <div class=""d-flex flex-column justify-content-around"">
                                            <small class=""mb-1 text-muted"">Start date</small>
                                            <div class=""dropdown"">
                                                <a class=""btn btn-secondary dropdown-toggle p-0 bg-transparent border-0 text-dark shadow-none font-weight-medium"" href=""#"" role=""button"" id=""dropdownM");
            WriteLiteral("enuLinkA\" data-bs-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">\r\n");
            WriteLiteral("                                                </a>\r\n");
            WriteLiteral(@"                                            </div>
                                        </div>
                                    </div>
                                    <div class=""d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item"">
                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "4d4eaa05504608fce6a56d1c9f11f441ae8b3c1a8609", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                                        <div class=""d-flex flex-column justify-content-around"">
                                            <small class=""mb-1 text-muted"">Revenue</small>
                                            <h5 class=""me-2 mb-0"">$");
#nullable restore
#line 74 "C:\Users\Murad\source\repos\FinalProjectMedimall\FinalProjectMedimall\Areas\Medimalladmin\Views\Dashboard\Index.cshtml"
                                                              Write(Ordercount.ToString("f0"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h5>
                                        </div>
                                    </div>
                                    <div class=""d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item"">
                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "4d4eaa05504608fce6a56d1c9f11f441ae8b3c1a10590", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                        <div class=\"d-flex flex-column justify-content-around\">\r\n                                            <small class=\"mb-1 text-muted\">Total Category</small>\r\n                                            <h5");
            BeginWriteAttribute("style", " style=\"", 4852, "\"", 4860, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"me-2 mb-0\">");
#nullable restore
#line 81 "C:\Users\Murad\source\repos\FinalProjectMedimall\FinalProjectMedimall\Areas\Medimalladmin\Views\Dashboard\Index.cshtml"
                                                                      Write(categorycount);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h5>
                                        </div>
                                    </div>
                                    <div class=""d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item"">
                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "4d4eaa05504608fce6a56d1c9f11f441ae8b3c1a12721", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                        <div class=\"d-flex flex-column justify-content-around\">\r\n                                            <small class=\"mb-1 text-muted\">Total Product</small>\r\n                                            <h5");
            BeginWriteAttribute("style", " style=\"", 5489, "\"", 5497, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"me-2 mb-0\">");
#nullable restore
#line 88 "C:\Users\Murad\source\repos\FinalProjectMedimall\FinalProjectMedimall\Areas\Medimalladmin\Views\Dashboard\Index.cshtml"
                                                                      Write(productcount);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h5>
                                        </div>
                                    </div>
                                    <div class=""d-flex py-3 border-md-right flex-grow-1 align-items-center justify-content-center p-3 item"">
                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "4d4eaa05504608fce6a56d1c9f11f441ae8b3c1a14855", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                        <div class=\"d-flex flex-column justify-content-around\">\r\n                                            <small class=\"mb-1 text-muted\">Users</small>\r\n                                            <h5 class=\"me-2 mb-0\">");
#nullable restore
#line 95 "C:\Users\Murad\source\repos\FinalProjectMedimall\FinalProjectMedimall\Areas\Medimalladmin\Views\Dashboard\Index.cshtml"
                                                             Write(usercount);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""sales"" role=""tabpanel"" aria-labelledby=""sales-tab"">
                                <div class=""d-flex flex-wrap justify-content-xl-between"">
                                    <div class=""d-none d-xl-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item"">
                                        <i class=""mdi mdi-calendar-heart icon-lg me-3 text-primary""></i>
                                        <div class=""d-flex flex-column justify-content-around"">
                                            <small class=""mb-1 text-muted"">Start date</small>
                                            <div class=""dropdown"">
                                                <a class=""btn btn-secondary dropdown-toggle p-0 bg-transparent border-0 text-dark sha");
            WriteLiteral(@"dow-none font-weight-medium"" href=""#"" role=""button"" id=""dropdownMenuLinkA"" data-bs-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">
                                                    <h5 class=""mb-0 d-inline-block"">26 Jul 2018</h5>
                                                </a>
                                                <div class=""dropdown-menu"" aria-labelledby=""dropdownMenuLinkA"">
                                                    <a class=""dropdown-item"" href=""#"">12 Aug 2018</a>
                                                    <a class=""dropdown-item"" href=""#"">22 Sep 2018</a>
                                                    <a class=""dropdown-item"" href=""#"">21 Oct 2018</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class=""d-flex border-md-right flex-grow-1 align-items-cente");
            WriteLiteral(@"r justify-content-center p-3 item"">
                                        <i class=""mdi mdi-download me-3 icon-lg text-warning""></i>
                                        <div class=""d-flex flex-column justify-content-around"">
                                            <small class=""mb-1 text-muted"">Downloads</small>
                                            <h5 class=""me-2 mb-0"">2233783</h5>
                                        </div>
                                    </div>
                                    <div class=""d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item"">
                                        <i class=""mdi mdi-eye me-3 icon-lg text-success""></i>
                                        <div class=""d-flex flex-column justify-content-around"">
                                            <small class=""mb-1 text-muted"">Total views</small>
                                            <h5 class=""me-2 mb-0"">9833550</h5>
                   ");
            WriteLiteral(@"                     </div>
                                    </div>
                                    <div class=""d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item"">
                                        <i class=""mdi mdi-currency-usd me-3 icon-lg text-danger""></i>
                                        <div class=""d-flex flex-column justify-content-around"">
                                            <small class=""mb-1 text-muted"">Revenue</small>
                                            <h5 class=""me-2 mb-0"">$577545</h5>
                                        </div>
                                    </div>
                                    <div class=""d-flex py-3 border-md-right flex-grow-1 align-items-center justify-content-center p-3 item"">
                                        <i class=""mdi mdi-flag me-3 icon-lg text-danger""></i>
                                        <div class=""d-flex flex-column justify-content-around"">
                ");
            WriteLiteral(@"                            <small class=""mb-1 text-muted"">Flagged</small>
                                            <h5 class=""me-2 mb-0"">3497843</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""purchases"" role=""tabpanel"" aria-labelledby=""purchases-tab"">
                                <div class=""d-flex flex-wrap justify-content-xl-between"">
                                    <div class=""d-none d-xl-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item"">
                                        <i class=""mdi mdi-calendar-heart icon-lg me-3 text-primary""></i>
                                        <div class=""d-flex flex-column justify-content-around"">
                                            <small class=""mb-1 text-muted"">Start date</small>
                                            ");
            WriteLiteral(@"<div class=""dropdown"">
                                                <a class=""btn btn-secondary dropdown-toggle p-0 bg-transparent border-0 text-dark shadow-none font-weight-medium"" href=""#"" role=""button"" id=""dropdownMenuLinkA"" data-bs-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">
                                                    <h5 class=""mb-0 d-inline-block"">26 Jul 2018</h5>
                                                </a>
                                                <div class=""dropdown-menu"" aria-labelledby=""dropdownMenuLinkA"">
                                                    <a class=""dropdown-item"" href=""#"">12 Aug 2018</a>
                                                    <a class=""dropdown-item"" href=""#"">22 Sep 2018</a>
                                                    <a class=""dropdown-item"" href=""#"">21 Oct 2018</a>
                                                </div>
                                            </div>
                                   ");
            WriteLiteral(@"     </div>
                                    </div>
                                    <div class=""d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item"">
                                        <i class=""mdi mdi-currency-usd me-3 icon-lg text-danger""></i>
                                        <div class=""d-flex flex-column justify-content-around"">
                                            <small class=""mb-1 text-muted"">Revenue</small>
                                            <h5 class=""me-2 mb-0"">$577545</h5>
                                        </div>
                                    </div>
                                    <div class=""d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item"">
                                        <i class=""mdi mdi-eye me-3 icon-lg text-success""></i>
                                        <div class=""d-flex flex-column justify-content-around"">
                                     ");
            WriteLiteral(@"       <small class=""mb-1 text-muted"">Total views</small>
                                            <h5 class=""me-2 mb-0"">9833550</h5>
                                        </div>
                                    </div>
                                    <div class=""d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item"">
                                        <i class=""mdi mdi-download me-3 icon-lg text-warning""></i>
                                        <div class=""d-flex flex-column justify-content-around"">
                                            <small class=""mb-1 text-muted"">Downloads</small>
                                            <h5 class=""me-2 mb-0"">2233783</h5>
                                        </div>
                                    </div>
                                    <div class=""d-flex py-3 border-md-right flex-grow-1 align-items-center justify-content-center p-3 item"">
                                        <i class=""m");
            WriteLiteral(@"di mdi-flag me-3 icon-lg text-danger""></i>
                                        <div class=""d-flex flex-column justify-content-around"">
                                            <small class=""mb-1 text-muted"">Flagged</small>
                                            <h5 class=""me-2 mb-0"">3497843</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- content-wrapper ends -->
    <!-- partial:partials/_footer.html -->
    <footer class=""footer"">
        <div class=""d-sm-flex justify-content-center justify-content-sm-between"">
            <span class=""text-muted text-center text-sm-left d-block d-sm-inline-block"">Copyright © <a href=""https://www.bootstrapdash.com/"" target=""_blank"">bootstrapdash.com </a>2021</span>
            <span class=""flo");
            WriteLiteral(@"at-none float-sm-right d-block mt-1 mt-sm-0 text-center"">Only the best <a href=""https://www.bootstrapdash.com/"" target=""_blank""> Bootstrap dashboard  </a> templates</span>
        </div>
    </footer>
    <!-- partial -->
</div>
<!-- main-panel ends -->
");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<HomeVM> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
