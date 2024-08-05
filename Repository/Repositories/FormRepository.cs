using Domain.Interface;
using Domain.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class FormRepository : FormInterface
    {
        static IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        static string strConnection = builder.Build().GetConnectionString("DefaultConnection");
        public string AddUser(_FormViewModel model)
        {
            try
            {
                //   var scon = new SqlConnection(""); PROCEDURE [dbo].[InsertUpdate]
                CallSP("[dbo].[InsertUpdate]", "text", JsonConvert.SerializeObject(model),"");

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return "error";
            }
            return "success";
        }

        public string DeleteUser(int Id)
        {
            CallSP("delete from Users where Id=@Id", "text", JsonConvert.SerializeObject(new { @Id = Id }), "text");
            return "success";
        }

        public _FormViewModel GetModelForEdit(int Id)
        {
            var dt = (DataTable)CallSP("[dbo].[GetUsersList]", "datatable", JsonConvert.SerializeObject(new { Id = Id }));
            return dt.AsEnumerable().Select(x => new _FormViewModel()
            {
                Id = Convert.ToInt32(x["Id"]),
                Name = Convert.ToString(x["Name"]),
                Address = Convert.ToString(x["Address"]),
                City = Convert.ToString(x["City"]),
                Email = Convert.ToString(x["Email"]),
                PhoneNo = Convert.ToString(x["Phone_No"]),
                State = Convert.ToString(x["State"])

            }).FirstOrDefault();
        }

        public List<_FormViewModel> GetUserList()
        {
            var dt = (DataTable)CallSP("[dbo].[GetUsersList]", "datatable","","");
          return  dt.AsEnumerable().Select(x => new _FormViewModel()
            {
                Id = Convert.ToInt32(x["Id"]),
                Name = Convert.ToString(x["Name"]),
                Address= Convert.ToString(x["Address"]),
                City= Convert.ToString(x["City"]),
                Email= Convert.ToString(x["Email"]),
                PhoneNo= Convert.ToString(x["Phone_No"]),
                State= Convert.ToString(x["State"])

            }).ToList();
        }

        public string UpdateUser(_FormViewModel model)
        {
            throw new NotImplementedException();
        }
        public static object CallSP(string CommandText,string type ="",string ParamJSONString="",string Commnattype="")
        {
            var t = new Object();
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnection))
                {

                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(CommandText, conn))
                    {

                        if (Commnattype == "text")
                        {
                            cmd.CommandType = CommandType.Text;
                        }
                        else
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                        }
                        if (!string.IsNullOrEmpty(ParamJSONString))
                        {

                            var _param = JsonConvert.DeserializeObject<Dictionary<string, object>>(ParamJSONString);
                            foreach (var item in _param)
                            {
                                cmd.Parameters.AddWithValue(item.Key, item.Value);
                            }
                        }
                        switch (type)
                        {
                            case "text":
                                cmd.ExecuteNonQuery();
                                break;
                            case "datatable":
                                cmd.CommandTimeout = 0;
                                SqlDataAdapter dbr = new SqlDataAdapter(cmd);
                                dbr.Fill(dt);
                                dbr.Dispose();
                                break;
                            case "scalar":
                                t = cmd.ExecuteScalar();
                                break;

                        }



                    }
                    conn.Close();
                    conn.Dispose();
                }
            }catch(Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return dt;

        }
    }
}
