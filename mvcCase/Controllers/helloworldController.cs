using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Data;

namespace mvcCase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class helloworldController : ControllerBase
    {
        // GET
        [HttpGet()]
        public ActionResult<helloworld> Get(int id)
        {
            var uid = id;
            var name = "test: no get PARAMETERS input";
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
            var result = new helloworld
            {
                message = name
            };
            return result;
        }
        [HttpPost()]
        public ActionResult<helloworld> Post(postitem postitem)
        {
            var uid = postitem.id;
            var name = "test: no POST PARAMETERS input";
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
            var result = new helloworld
            {
                message = name
            };
            return result;
        }
    }

    [ApiController]
    [Route("[controller]/again")]
    public class helloworldagainController : ControllerBase
    {
        [HttpGet()]
        public ActionResult<helloworld> Get(int id)
        {
            var uid = id;
            var name = "againtest: no get PARAMETERS input";
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
            var result = new helloworld
            {
                message = name
            };
            return result;
        }
    }


}