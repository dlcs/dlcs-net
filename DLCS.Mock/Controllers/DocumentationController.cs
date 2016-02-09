using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using DLCS.Client.Config;
using Hydra;
using DLCS.Mock.ApiApp;
using Hydra.Model;
using WebGrease.Css.Ast.Selectors;

namespace DLCS.Mock.Controllers
{
    public class DocumentationController : ApiController
    {
        private static Dictionary<string, object> _supportedClasses;

        [HttpGet]
        public IHttpActionResult Vocab(string format = null)
        {
            EnsureClasses();
            var classes = _supportedClasses.Values.Cast<Class>().ToArray();
            var vocab = new ApiDocumentation(Constants.Vocab, Constants.Vocab, classes);
            if (format != null)
            {
                return new DocResult(vocab, format, Request);
            }
            return Json(vocab);
        }

        private static readonly object InitLock = new object();

        private void EnsureClasses()
        {
            if (_supportedClasses == null)
            {
                lock (InitLock)
                {
                    if (_supportedClasses == null)
                    {
                        _supportedClasses = AttributeUtil.GetAttributeMap("DLCS.Client", typeof(HydraClassAttribute));
                    }
                }
            }
        }
    }

    public class DocResult : IHttpActionResult
    {
        string _format;
        HttpRequestMessage _request;
        ApiDocumentation _vocab;

        public DocResult(ApiDocumentation vocab, string format, HttpRequestMessage request)
        {
            _vocab = vocab;
            _format = format;
            _request = request;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                Content = GetStringContent(_vocab, _format),
                RequestMessage = _request
            };
            return Task.FromResult(response);
        }

        private HttpContent GetStringContent(ApiDocumentation vocab, string format)
        {
            var sb = new StringBuilder();
            sb.Heading(format, 1, "Vocab");
            foreach (var clazz in vocab.SupportedClasses)
            {
                sb.Heading(format, 1, clazz.Label);
                sb.Para(format, clazz.Description);
                sb.Code(format, clazz.UriTemplate);
                if (clazz.SupportedProperties != null && clazz.SupportedProperties.Length > 0)
                {
                    sb.Heading(format, 2, "Supported properties");
                    foreach (SupportedProperty prop in clazz.SupportedProperties)
                    {
                        sb.Heading(format, 3, prop.Title);
                        sb.Para(format, prop.Description);
                        sb.StartTable(format, "domain", "range", "readonly", "writeonly");
                        sb.TableRow(format, NameSpace(prop.Property.Domain), NameSpace(prop.Property.Range), prop.ReadOnly.ToString(), prop.WriteOnly.ToString());
                        sb.EndTable(format);
                        var linkProp = prop.Property as HydraLinkProperty;
                        if (linkProp != null)
                        {
                            sb.Para(format, "Supported operations on link:");
                            sb.Bold(format, "Template: ");
                            sb.Code(format, clazz.UriTemplate + "/" + linkProp.Label);
                            AppendSupportedOperationsTable(sb, format, linkProp.SupportedOperations);
                        }
                    }
                }
                if (clazz.SupportedOperations != null && clazz.SupportedOperations.Length > 0)
                {
                    sb.Heading(format, 2, "Supported operations");
                    sb.Bold(format, "Template: ");
                    sb.Code(format, clazz.UriTemplate);
                    AppendSupportedOperationsTable(sb, format, clazz.SupportedOperations);
                }
            }
            return new StringContent(sb.ToString(), Encoding.UTF8, "text/" + format);
        }

        private void AppendSupportedOperationsTable(StringBuilder sb, string format, Operation[] supportedOperations)
        {
            if (supportedOperations != null && supportedOperations.Length > 0)
            {
                sb.StartTable(format, "Method", "Label", "Expects", "Returns", "Status");
                foreach (var op in supportedOperations)
                {
                    string statuses = "";
                    if (op.StatusCodes != null && op.StatusCodes.Length > 0)
                    {
                        statuses = string.Join(", ",
                            op.StatusCodes.Select(code => code.StatusCode + " " + code.Description));
                    }
                    sb.TableRow(format, op.Method, op.Label, NameSpace(op.Expects), NameSpace(op.Returns), statuses);
                }
                sb.EndTable(format);
            }
        }

        public static string NameSpace(string s)
        {
            return Names.GetNamespacedVersion(s);
        }
    }

    


    public static class VocabHelpers
    {
        private const string Markdown = "markdown";

        public static void Heading(this StringBuilder sb, string format, int level, string text)
        {
            sb.AppendLine();
            if (format == Markdown)
            {
                sb.AppendLine(new string('#', level) + " " + text);
            }
            else
            {
                sb.AppendFormat("<h{0}>{1}</h{0}>", level, text);
                sb.AppendLine();
            }
            sb.AppendLine();
        }

        public static void Para(this StringBuilder sb, string format, string text)
        {
            if (format == Markdown)
            {
                sb.AppendLine(text);
            }
            else
            {
                sb.AppendFormat("<p>{0}</p>", text);
            }
            sb.AppendLine();
        }

        public static void NewLine(this StringBuilder sb, string format)
        {
            if (format == Markdown)
            {
                sb.AppendLine();
            }
            else
            {
                sb.AppendLine("<br/>");
            }

        }

        public static void StartTable(this StringBuilder sb, string format, params string[] headings)
        {
            sb.AppendLine();
            if (format == Markdown)
            {
                foreach (var heading in headings)
                {
                    sb.Append("|" + heading);
                }
                sb.AppendLine("|");
                foreach (var heading in headings)
                {
                    sb.Append("|--");
                }
                sb.AppendLine("|");
            }
            else
            {
                sb.AppendLine("<table><tr>");
                foreach (var heading in headings)
                {
                    sb.AppendFormat("<th>{0}</th>", heading);
                }
                sb.AppendLine("</tr>");
            }
        }

        public static void TableRow(this StringBuilder sb, string format, params string[] cells)
        {
            if (format == Markdown)
            {
                foreach (var cell in cells)
                {
                    sb.Append("|" + cell);
                }
                sb.AppendLine("|");
            }
            else
            {
                sb.AppendLine("<tr>");
                foreach (var cell in cells)
                {
                    sb.AppendFormat("<td>{0}</td>", cell);
                }
                sb.AppendLine("</tr>");
            }
        }

        public static void EndTable(this StringBuilder sb, string format)
        {
            if (format == Markdown)
            {
            }
            else
            {
                sb.AppendLine("</table>");
            }
            sb.AppendLine();
        }

        public static void Code(this StringBuilder sb, string format, string code)
        {
            if (format == Markdown)
            {
                sb.AppendLine("```javascript");
                sb.AppendLine(code);
                sb.AppendLine("```");
            }
            else
            {
                sb.AppendFormat("<pre>{0}</pre>", code);
                sb.AppendLine("<br/>");
            }
            sb.AppendLine();
        }

        public static void Bold(this StringBuilder sb, string format, string text)
        {
            if (format == Markdown)
            {
                sb.AppendFormat("**{0}**", text);
            }
            else
            {
                sb.AppendFormat("<b>{0}</b>", text);
            }
        }
    }
}
