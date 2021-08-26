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
    public class AdminController:Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("authed") != 1)
            {
                return Json(new {message = "no authed"});
            }
            else
            {
                ViewData["username"] = HttpContext.Session.GetString("username");
                ViewData["departname"] = HttpContext.Session.GetString("departname");
                ViewData["timenow"] = DateTime.Now;
                return View();
            }
        }
        
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult doLogin(useritem user)
        {
            
            if (user.username != null)
            {
                var userfind = new useritem();
                using (var connection = new SqliteConnection("Data Source=z_source.db"))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = @"SELECT * FROM userinfo WHERE username = $username";
                    command.Parameters.AddWithValue("username", user.username);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userfind.username = reader.GetString(1);
                            userfind.departname = reader.GetString(2);
                            userfind.created = reader.GetString(3);
                            userfind.password = reader.GetString(4);
                        }
                    }
                    connection.Close();
                }

                if (userfind.password == user.password)
                {
                    HttpContext.Session.SetInt32("authed",1);
                    HttpContext.Session.SetString("username",userfind.username);
                    HttpContext.Session.SetString("departname",userfind.departname);
                    var result = new test
                    {
                        message = "登录成功"
                    };
                    return Json(new {result});
                }
                else
                {
                    var result = new test
                    {
                        message = "登录失败"
                    };
                    return Json(new {result});
                }


            }
            else
            {
                var result = new test
                {
                    message = "No Input"
                };
                return Json(new {result});
            }

            //HttpContext.Session.SetInt32("authed",1);
            //return Json(new {message = "Authed!"});
            
        }
        
    }
}