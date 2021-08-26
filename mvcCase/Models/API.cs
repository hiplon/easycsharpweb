using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
namespace mvcCase
{
    public class helloworld
    {
        public string message   { get; set; }
    }
    
    public class test
    {
        public string message   { get; set; }
    }
    
    public class postitem
    {
        public int id   { get; set; }
    }
    
    public class useritem
    {
        public string username   { get; set; }
        public string departname   { get; set; }
        public string created   { get; set; }
        public string password   { get; set; }
    }
    
    public class Qywxrespone
    {
        public int errcode   { get; set; }
        public string errmsg   { get; set; }
        public string access_token   { get; set; }
        public int expires_in   { get; set; }
    }
    
    public class Qywsendcontent
    {
        public string content   { get; set; }
    }
    
    public class Qywsendmessage
    {
        public string touser   { get; set; }
        public string agentid   { get; set; }
        public string msgtype   { get; set; }
        public int safe   { get; set; }
        public Qywsendcontent text   { get; set; }
    }
    
}
