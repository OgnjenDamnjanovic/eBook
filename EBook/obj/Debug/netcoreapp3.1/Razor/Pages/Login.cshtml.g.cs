#pragma checksum "E:\Napredne baze projekti\cassandra\EBook\Pages\Login.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "817216ff339975f6ed90e2c150d3b0e224881a63"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EBook.Pages.Pages_Login), @"mvc.1.0.razor-page", @"/Pages/Login.cshtml")]
namespace EBook.Pages
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
#line 1 "E:\Napredne baze projekti\cassandra\EBook\Pages\_ViewImports.cshtml"
using EBook;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"817216ff339975f6ed90e2c150d3b0e224881a63", @"/Pages/Login.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7933f3869c859b0b83991d20c9fb29026fc1ae66", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Login : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("loginform"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("#"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("signupform"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<!-- banner -->
        <div class=""banner-bg-inner"">
            <!-- banner-text -->
            <div class=""banner-text-inner"">
                <div class=""container"">
                    <h2 class=""title-inner"">
                        world of reading
                    </h2>

                </div>
            </div>
            <!-- //banner-text -->
        </div>
        <!-- //banner -->
        <!-- breadcrumbs -->
        <div class=""crumbs text-center"">
            <div class=""container"">
                <div class=""row"">
                    <ul class=""btn-group btn-breadcrumb bc-list"">
                        <li class=""btn btn1"">
                            <a href=""index.html"">
                                <i class=""glyphicon glyphicon-home""></i>
                            </a>
                        </li>
                        <li class=""btn btn2"">
                            <a href=""login.html"">sign in & sign up</a>
                        </li>
        ");
            WriteLiteral(@"            </ul>
                </div>
            </div>
        </div>
        <!--//breadcrumbs ends here-->
        <!-- signin and signup form -->
        <div class=""login-form section text-center"">
            <div class=""container"">
                <h4 class=""rad-txt"">
                    <span class=""abtxt1"">Sign in</span>
                    <span class=""abtext"">sign up</span>
                </h4>
                <div id=""loginbox"" style=""margin-top:30px;"" class=""mainbox  loginbox"">
                    <div class=""panel panel-info"">
                        <div class=""panel-heading"">
                            <div class=""panel-title"">Sign In</div>
                            <div class=""fpassword"">
                                <a href=""#"">Forgot password?</a>
                            </div>
                        </div>
                        <div style=""padding-top:30px"" class=""panel-body"">
                            <div style=""display:none"" id=""login-alert"" clas");
            WriteLiteral("s=\"alert alert-danger col-sm-12\"></div>\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "817216ff339975f6ed90e2c150d3b0e224881a636896", async() => {
                WriteLiteral(@"
                                <div style=""margin-bottom: 25px"" class=""input-group"">
                                    <span class=""input-group-addon"">
                                        <i class=""glyphicon glyphicon-user""></i>
                                    </span>
                                    <input id=""login-username"" type=""text"" class=""form-control"" name=""username""");
                BeginWriteAttribute("value", " value=\"", 2633, "\"", 2641, 0);
                EndWriteAttribute();
                WriteLiteral(" placeholder=\"username or email\"");
                BeginWriteAttribute("required", " required=\"", 2674, "\"", 2685, 0);
                EndWriteAttribute();
                WriteLiteral(@">
                                </div>

                                <div style=""margin-bottom: 25px"" class=""input-group"">
                                    <span class=""input-group-addon"">
                                        <i class=""glyphicon glyphicon-lock""></i>
                                    </span>
                                    <input id=""login-password"" type=""password"" class=""form-control"" name=""password"" placeholder=""password""");
                BeginWriteAttribute("required", " required=\"", 3153, "\"", 3164, 0);
                EndWriteAttribute();
                WriteLiteral(@">
                                </div>
                                <div class=""input-group"">
                                    <div class=""checkbox"">
                                        <label>
                                            <input id=""login-remember"" type=""checkbox"" name=""remember"" value=""1""> Remember me
                                        </label>
                                    </div>
                                </div>
                                <div style=""margin-top:10px"" class=""form-group"">
                                    <!-- Button -->
                                    <div class=""col-sm-12 controls"">
                                        <a id=""btn-login"" href=""#"" class=""btn btn-success"">Login </a>
                                        <a id=""btn-fblogin"" href=""#"" class=""btn btn-primary"">Login with Facebook</a>
                                    </div>
                                </div>
                                <div class");
                WriteLiteral(@"=""form-group"">
                                    <div class=""col-md-12 control"">
                                        <div style=""border-top: 1px solid#888; padding-top:15px; font-size:85%"">
                                            Don't have an account!
                                            <a href=""#"" onClick=""$('#loginbox').hide(); $('#signupbox').show()"">
                                                Sign Up Here
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                        </div>
                    </div>
                </div>
                <div id=""signupbox"" style=""display:none; margin-top:50px"" class=""mainbox loginbox"">
                    <div class=""panel panel-info"">
                        <div class=""panel-heading"">
                            <div class=""panel-title"">Sign Up</div>
                            <div style=""float:right; font-size: 85%; position: relative; top:-10px"">
                                <a id=""signinlink"" href=""#"" onclick=""$('#signupbox').hide(); $('#loginbox').show()"">Sign In</a>
                            </div>
                        </div>
                        <div class=""panel-body"">
                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "817216ff339975f6ed90e2c150d3b0e224881a6312490", async() => {
                WriteLiteral(@"
                                <div id=""signupalert"" style=""display:none"" class=""alert alert-danger"">
                                    <p>Error:</p>
                                    <span></span>
                                </div>
                                <div class=""form-group"">
                                    <label class=""col-md-3 col-sm-3 col-xs-3 control-label"">Email</label>
                                    <div class=""col-md-9 col-sm-9 col-xs-9"">
                                        <input type=""text"" class=""form-control"" name=""email"" placeholder=""Email Address""");
                BeginWriteAttribute("required", " required=\"", 6268, "\"", 6279, 0);
                EndWriteAttribute();
                WriteLiteral(@">
                                    </div>
                                </div>
                                <div class=""form-group"">
                                    <label class=""col-md-3 col-sm-3 col-xs-3 control-label"">First Name</label>
                                    <div class=""col-md-9 col-sm-9 col-xs-9"">
                                        <input type=""text"" class=""form-control"" name=""firstname"" placeholder=""First Name""");
                BeginWriteAttribute("required", " required=\"", 6736, "\"", 6747, 0);
                EndWriteAttribute();
                WriteLiteral(@">
                                    </div>
                                </div>
                                <div class=""form-group"">
                                    <label class=""col-md-3 col-sm-3 col-xs-3 control-label"">Last Name</label>
                                    <div class=""col-md-9 col-sm-9 col-xs-9"">
                                        <input type=""text"" class=""form-control"" name=""lastname"" placeholder=""Last Name""");
                BeginWriteAttribute("required", " required=\"", 7201, "\"", 7212, 0);
                EndWriteAttribute();
                WriteLiteral(@">
                                    </div>
                                </div>
                                <div class=""form-group"">
                                    <label class=""col-md-3 col-sm-3 col-xs-3 control-label"">Password</label>
                                    <div class=""col-md-9 col-sm-9 col-xs-9"">
                                        <input type=""password"" class=""form-control"" name=""passwd"" placeholder=""Password""");
                BeginWriteAttribute("required", " required=\"", 7666, "\"", 7677, 0);
                EndWriteAttribute();
                WriteLiteral(@">
                                    </div>
                                </div>
                                <div class=""form-group"">
                                    <!-- Button -->
                                    <div class=""signup-btn"">
                                        <button id=""btn-signup"" type=""button"" class=""btn btn-info"">
                                            <i class=""icon-hand-right""></i> &nbsp; Sign Up</button>
                                    </div>
                                </div>
                                <div style=""border-top: 1px solid #999; padding-top:20px"" class=""form-group"">

                                    <div class=""f-btn"">
                                        <button id=""btn-fbsignup"" type=""button"" class=""btn btn-primary"">
                                            <i class=""icon-facebook""></i>   Sign Up with Facebook</button>
                                    </div>
                                </div>
          ");
                WriteLiteral("                  ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <!--//signin and signup form ends here-->");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<MyApp.Namespace.LoginModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<MyApp.Namespace.LoginModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<MyApp.Namespace.LoginModel>)PageContext?.ViewData;
        public MyApp.Namespace.LoginModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
