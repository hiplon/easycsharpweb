using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvcCase.Models;
using Microsoft.Data.Sqlite;
using System.Text.Json;
using Microsoft.AspNetCore.Session;

namespace mvcCase.Controllers
{
    public class GuideController:Controller
    {
        public IActionResult Text()
        {
            var systemversion = System.Environment.OSVersion.ToString();
            var csharpversion = System.Environment.Version.ToString();
            var versioninfo = System.Environment.MachineName.ToString();
            return new Microsoft.AspNetCore.Mvc.ContentResult
            {
                Content = "Hi there! From Guide ☺\n"+csharpversion+"@"+systemversion+" on "+versioninfo,
                ContentType = "text/plain; charset=utf-8"
            };
        }
        
        public IActionResult send(string message)
        {
            //string MSG = message;
            var corp_id = "wwc27f8c25bf0b362a";
            var app_secret = "T2-GWs9vxvgA2TttdTE27-Fm1CDQ8YDubGLu93qAqcU";
            var app_id = "1000004";
            HttpWebRequest req =  
                (HttpWebRequest)HttpWebRequest.Create("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid="+corp_id+"&corpsecret="+app_secret); 
            req.Method = "GET"; 
            using (var response = (HttpWebResponse)req.GetResponse())
            {
                var encoding = Encoding.GetEncoding(response.CharacterSet);

                using (var responseStream = response.GetResponseStream())
                using (var reader = new StreamReader(responseStream, encoding))
                {
                    string qywxres = reader.ReadToEnd();
                    string getsring = JsonSerializer.Deserialize<Qywxrespone>(qywxres).access_token;
                    
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token="+getsring);
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        var sendcontent = new Qywsendmessage();
                        var sendmessage = new Qywsendcontent();
                        sendmessage.content = message;
                        sendcontent.agentid = app_id;
                        sendcontent.touser = "@all";
                        sendcontent.msgtype = "text";
                        sendcontent.safe = 0;
                        sendcontent.text = sendmessage;
                        string json = JsonSerializer.Serialize(sendcontent);
                        Console.WriteLine(json);
                        streamWriter.Write(json);
                    }

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var postresult = streamReader.ReadToEnd();
                        return new Microsoft.AspNetCore.Mvc.ContentResult
                        {
                            Content = "Hi there! From Guide ☺\n"+message+" sent @"+DateTime.Now+"\n"+postresult,
                            ContentType = "text/plain; charset=utf-8"
                        };
                    }
                    
                }

                
            }

        }
        
        public ActionResult<test> Getjson()
        {
            var result = new test
            {
                message = "good"
            };
            return result;
        }

        public IActionResult json()
        {
            if (HttpContext.Session.GetInt32("authed") != 1)
            {
                return Json(new {message = "no authed"});
                
            }
            else
            {
                //var name = "";
                List<useritem> result = new List<useritem>();
                using (var connection = new SqliteConnection("Data Source=z_source.db"))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = @"SELECT * FROM userinfo";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new useritem() {
                                username= reader.GetString(1), 
                                departname=reader.GetString(2),
                                created=reader.GetString(3)
                            });
                        }
                    }
                    connection.Close();
                }
                return Json(new {message = result});
            }
        }
        
        public IActionResult auth()
        {
            HttpContext.Session.SetInt32("authed",1);
            return Json(new {message = "Authed!"});
        }

        public IActionResult Html()
        {
            var timenow = DateTime.Now;
            return new Microsoft.AspNetCore.Mvc.ContentResult
            {
                Content = "<h1>Hi there! From Guide ☺</h1></br><h2>" + timenow +"</h2>",
                ContentType = "text/html; charset=utf-8"
            };
        }
        
        public IActionResult Index()
        {
            var timenow = DateTime.Now;
            return new Microsoft.AspNetCore.Mvc.ContentResult
            {
                Content = "Hi there! From Guide Index ☺" + "\nServer Time: "+ timenow,
                ContentType = "text/plain; charset=utf-8"
            };
        }
        
        public IActionResult Sqltest()
        {
            var uid = 1;
            var name = "";
            var timenow = DateTime.Now;
            using (var connection = new SqliteConnection("Data Source=z_source.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                        SELECT username
                        FROM userinfo
                        WHERE uid = $uid
                    ";
                command.Parameters.AddWithValue("$uid", uid);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        name = reader.GetString(0);
                    }
                }
                connection.Close();
            }
            return new Microsoft.AspNetCore.Mvc.ContentResult
            {
                Content = "Hi there! From Guide Index ☺" + name + "\nServer Time: "+ timenow,
                ContentType = "text/plain; charset=utf-8"
            };
        }
    }
}