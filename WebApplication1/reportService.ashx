//ver:[120814]
<%@ WebHandler Language="C#" Class="reportService" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Specialized;

using C1.Web.Wijmo.Controls;
using C1.Web.Wijmo.Controls.C1ReportViewer;
using C1.Web.Wijmo.Controls.C1ReportViewer.ReportService;

public class reportService : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

	public void ProcessRequest(HttpContext context)
    {
		object answer;
        NameValueCollection args = context.Request.Params;
        string command = args["command"];        
        if (string.IsNullOrEmpty(command))
            command = "";
		string fileName = (string)args["fileName"];
		string resolvedFileName = fileName;
		if (!string.IsNullOrEmpty(resolvedFileName))
		{
			if (!resolvedFileName.StartsWith("~/"))
			{
				resolvedFileName = "~/" + resolvedFileName.Replace(@"\", "/");
			}
			resolvedFileName = HttpContext.Current.Server.MapPath(resolvedFileName);
		}
		string clientId = args["clientId"];
		if (string.IsNullOrEmpty(clientId))
		{
			throw new HttpException("Parameter clientId can not be empty.");
		}
		string reportName = args["reportName"];
		string cookie = args["cookie"];
		string documentKey = string.IsNullOrEmpty(args["documentKey"]) ? "" : args["documentKey"];
		int dpi = string.IsNullOrEmpty(args["dpi"]) ? 96 : int.Parse(args["dpi"]);
		int zoom = string.IsNullOrEmpty(args["zoom"]) ? 100 : int.Parse(args["zoom"]);
		int pageIndex = string.IsNullOrEmpty(args["pageIndex"]) ? 0 : int.Parse(args["pageIndex"]);
		bool printTarget = string.IsNullOrEmpty(args["printTarget"]) ? false : bool.Parse(args["printTarget"]);
		
		IC1WebReportService reportService = null;			
		
		if (!string.IsNullOrEmpty(documentKey))
		{
			reportService = C1WebReportServiceHelper.MakeHelper(documentKey);
		}
		if (reportService == null)
		{
			object inMemoryDoc = null;
			if (C1.Web.Wijmo.Controls.C1ReportViewer.C1ReportViewer.IsDocumentRegistered((string)args["fileName"])
				&& !C1.Web.Wijmo.Controls.C1ReportViewer.C1ReportViewer.HasCachedDocument(resolvedFileName, reportName))
			{
				inMemoryDoc = C1.Web.Wijmo.Controls.C1ReportViewer.C1ReportViewer.GetDocument((string)args["fileName"]);
			}			
			string reportsFolderPath = (string)HttpContext.Current.Cache[clientId + "_ReportsFolderPath"];
			ReportCache reportCache = (ReportCache)HttpContext.Current.Cache[clientId + "_ReportCache"];
			System.Collections.Generic.Dictionary<string, object> extraOptions =
				(System.Collections.Generic.Dictionary<string, object>)HttpContext.Current.Cache[clientId + "_ExtraOptions"];
			if (reportCache == null)
			{
				reportCache = new ReportCache();
			}
			if (extraOptions == null)
			{
				extraOptions = new System.Collections.Generic.Dictionary<string, object>();
			}
			if (string.IsNullOrEmpty(reportsFolderPath))
			{
				reportsFolderPath = "~/tempReports";
			}
									
			reportService = new C1WebReportServiceHelper(inMemoryDoc,
				reportCache,
				extraOptions,
				ViewType.PageImages,
				HttpContext.Current.Server.MapPath(reportsFolderPath), VirtualPathUtility.ToAbsolute(reportsFolderPath));
		}
        byte[] buffer;        
        switch (command)
        {
			case "error":
				throw new Exception("test error");
			case "generate":
			case "status":
				DocumentStatus documentStatus = reportService.GetDocumentStatus(resolvedFileName, reportName, ParseReportParams(args["reportParams"]), cookie);				
                context.Response.ContentType = "application/json";
				answer = JsonHelper.ObjectToString(documentStatus, null, true);
				context.Response.Write(answer);
                context.Response.End();
                break;
			case "markup":
				context.Response.ContentType = "application/json";
				answer = reportService.GetPageImagesMarkup(documentKey,
						int.Parse(args["dpi"]), int.Parse(args["zoom"]),
						(int[])((System.Collections.ArrayList)ToArrayList(args["pageIndices"])).ToArray(typeof(int)),
						bool.Parse(args["getImagesOnly"]));
				context.Response.Write(C1.Web.Wijmo.Controls.JsonHelper.ObjectToString(answer, null, true));
				context.Response.End();				
				break;			
            case "dialog":
                context.Response.ContentType = "text/x-html";
                context.Response.Write(GetDialogTemplateContent((string)args["name"]));
                context.Response.End();
                break;
			case "search":
				context.Response.ContentType = "application/json";
				answer = reportService.SearchText(documentKey, (string)args["query"],
						bool.Parse(args["caseSensitive"]),
						bool.Parse(args["useRegExp"]));
				context.Response.Write(C1.Web.Wijmo.Controls.JsonHelper.ObjectToString(answer, null, true));
				context.Response.End();				
				break;
			case "outline":
				context.Response.ContentType = "application/json";
				answer = reportService.GetDocumentOutline(documentKey);
				context.Response.Write(C1.Web.Wijmo.Controls.JsonHelper.ObjectToString(answer, null, true));
				context.Response.End();
				break;				
            case "Export":
                /*
                Response.Buffer = false;
                Response.AppendHeader("Content-Type", "octet-stream");
                Response.AppendHeader("Content-Disposition", "attachment");
                Response.Flush();
                */
                string exportFormat = args["exportFormat"];
                
				
                string url = reportService.ExportToFile(args["documentKey"], exportFormat);
                context.Response.ContentType = "text/html";

                System.Text.StringBuilder htmlBuilder = new System.Text.StringBuilder();
                htmlBuilder.AppendLine("<html>");
                htmlBuilder.AppendLine("<header>");
                htmlBuilder.AppendLine("<title>Download " + exportFormat + "</title>");
                if (!url.StartsWith("error:"))
                    htmlBuilder.AppendLine("<meta http-equiv=\"REFRESH\" CONTENT=\"5; url=" + url + "\">");
                htmlBuilder.AppendLine("</header>");
                htmlBuilder.AppendLine("<body>");
                if (!url.StartsWith("error:"))
                {
                    htmlBuilder.AppendLine("<h1>");
                    htmlBuilder.AppendLine("Exported document download.");
                    htmlBuilder.AppendLine("</h1>");
                    htmlBuilder.AppendLine("<p>");
                    htmlBuilder.AppendLine("Your download should begin shortly. If your download does not start in approximately 10 seconds, you can ");
                    htmlBuilder.AppendLine("<a href=\"" + url + "\">");
                    htmlBuilder.AppendLine("click here");
                    htmlBuilder.AppendLine("</a>");
                    htmlBuilder.AppendLine(" to launch the download.</p>");

                    htmlBuilder.AppendLine("<div style=\"background-color:#cccccc;\">");
                    htmlBuilder.AppendLine("Download URL: " + "<a href=\"" + url + "\">" + url + "</a>");
                    htmlBuilder.AppendLine("<br />");
                    htmlBuilder.AppendLine("Export Format: " + exportFormat);
                    htmlBuilder.AppendLine("<br />");
                    htmlBuilder.AppendLine("Internal Document Key: " + args["documentKey"]);
                    htmlBuilder.AppendLine("</div>");                                        
                    
                }
                else
                {
                    htmlBuilder.AppendLine("<h2 style=\"color:red;\">" + url + "</h2>");
                }
                htmlBuilder.AppendLine("</body>");
                htmlBuilder.AppendLine("</html>");
                context.Response.Write(htmlBuilder.ToString());      
                context.Response.End();
                break;
            case "":
            default:
                context.Response.Clear();
                context.Response.ContentType = "image/png";
				buffer = reportService.GetPageImage(documentKey, dpi, zoom, pageIndex, printTarget);
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.End();
                break;
        }
	}


	ReportParameterValue[] ParseReportParams(string jsonStr)
	{
		ReportParameterValue[] reportParams = null;
		if (!string.IsNullOrEmpty(jsonStr))
		{
			Hashtable reportParamsObj = JsonHelper.StringToObject(jsonStr);
			if (reportParamsObj != null)
			{
				ArrayList reportParamsArr = (ArrayList)reportParamsObj["reportParameters"];
				//reportParams
				reportParams = new ReportParameterValue[reportParamsArr.Count];
				for (int i = 0; i < reportParamsArr.Count; i++)
				{
					Hashtable par = (Hashtable)reportParamsArr[i];
					reportParams[i] = new ReportParameterValue();
					reportParams[i].n = (string)par["n"];
					reportParams[i].vs = ((ArrayList)par["vs"]).ToArray();
				}
			}
		}
		return reportParams;
	}
	
    string GetDialogTemplateContent(string dialogName)
    {
		string localPath = HttpContext.Current.Server.MapPath("~/ReportDialogs/" + dialogName + ".htm");
        if (System.IO.File.Exists(localPath))
        {
            using (System.IO.StreamReader reader = new System.IO.StreamReader(localPath))
            {
                string s = reader.ReadToEnd();
                return s;
            }
        }
		return C1.Web.Wijmo.Controls.C1ReportViewer.C1ReportViewer.GetDialogTemplateContent(dialogName);
    }

	System.Collections.ArrayList ToArrayList(string s)
	{
		int k;
		s = s.Trim(new char[] { '[', ']' });
		string[] indicesStrArr = s.Split(',');
		System.Collections.ArrayList arr = new System.Collections.ArrayList();
		for (int i = 0; i < indicesStrArr.Length; i++)
		{
			if (int.TryParse(indicesStrArr[i], out k))
			{
				arr.Add(k);
			}
		}
		return arr;
	}

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }


    
}



                