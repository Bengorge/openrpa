﻿using System;
using System.Activities;
using OpenRPA.Interfaces;
using System.Activities.Presentation.PropertyEditing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace OpenRPA.Activities
{
    [System.ComponentModel.Designer(typeof(GetFileDesigner), typeof(System.ComponentModel.Design.IDesigner))]
    [System.Drawing.ToolboxBitmap(typeof(ResFinder), "Resources.toolbox.downloadfile.png")]
    //[designer.ToolboxTooltip(Text = "Find an Windows UI element based on xpath selector")]
    public class GetFile : AsyncTaskCodeActivity<int>
    {
        [RequiredArgument,OverloadGroupAttribute("Filename")]
        public InArgument<string> Filename { get; set; }
        [RequiredArgument,OverloadGroupAttribute("Id")]
        public InArgument<string> _id { get; set; }
        [RequiredArgument]
        public InArgument<string> LocalPath { get; set; }
        [RequiredArgument]
        public InArgument<bool> IgnorePath { get; set; } = false;
        protected async override Task<int> ExecuteAsync(AsyncCodeActivityContext context)
        {
            var filename = Filename.Get(context);
            var id = _id.Get(context);
            var filepath = LocalPath.Get(context);
            var ignorepath = IgnorePath.Get(context);
            // await global.webSocketClient.DownloadFileAndSave(filename, id, filepath, ignorepath);

            Uri baseUri = new Uri(global.openflowconfig.baseurl);
            filepath = Environment.ExpandEnvironmentVariables(filepath);

            var q = "{\"_id\": \"" + id + "\"}";
            if(!string.IsNullOrEmpty(filename)) q = "{\"filename\":\"" + filename + "\"}";
            var rows = await global.webSocketClient.Query<JObject>("files", q);
            if (rows.Length == 0) throw new Exception("File not found");
            if (rows.Length == 0) throw new Exception("File not found");
            filename = rows[0]["filename"].ToString();
            id = rows[0]["_id"].ToString();

            Uri downloadUri = new Uri(baseUri, "/download/" + id);
            var url = downloadUri.ToString();

            // if(string.IsNullOrEmpty(filename)) filename = "temp."
            // if (System.IO.File.Exists(filepath) && !overwrite) return 42;
            using (var client = new System.Net.WebClient())
            {
                // client.Headers.Add("Authorization", "jwt " + global.webSocketClient);
                client.Headers.Add(System.Net.HttpRequestHeader.Authorization, global.webSocketClient.jwt);
                await client.DownloadFileTaskAsync(new Uri(url), System.IO.Path.Combine(filepath, filename));
            }
            return 42;
        }
    }
}