using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Demo.ADO.NET.Models;
using InfrastructureData;
using Demo.ADO.NET.ViewsModels;
using Microsoft.Data.SqlClient;

namespace Demo.ADO.NET.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult getList()
        {
            string sql = @"select de.FirstName, de.LastName, de.MiddleName, de.NameStyle, de.Title, de.BirthDate, de.EmailAddress, de.Phone
                            FROM dbo.DimEmployee de
                            WHERE de.CurrentFlag = @CurrentFlag";

            var listParameter = new List<SqlParameter>();
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@CurrentFlag";
            param.Value = 1;
            listParameter.Add(param);


            var datas = SqlAccess.FillDataset(sql, listParameter.ToArray());
            int total = datas.Rows.Count;

            List<DimEmployee> EmployeeList = new List<DimEmployee>();

            for (int i = 0; i < total; i++)
            {
                DimEmployee item = new DimEmployee();

                item.FirstName = datas.Rows[i]["FirstName"].ToString();
                item.LastName = datas.Rows[i]["LastName"].ToString();
                item.MiddleName = datas.Rows[i]["MiddleName"].ToString();
                item.NameStyle = datas.Rows[i]["NameStyle"].ToString();
                item.Title = datas.Rows[i]["Title"].ToString();
                item.BirthDate = datas.Rows[i]["BirthDate"].ToString();
                item.EmailAddress = datas.Rows[i]["EmailAddress"].ToString();
                item.Phone = datas.Rows[i]["Phone"].ToString();

                EmployeeList.Add(item);
            }

            var obj = new
            {
                totalRow = total,
                datas = EmployeeList
            };
            return Json(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult find()
        {
            string sql = @"select de.FirstName
                            FROM dbo.DimEmployee de
                            WHERE de.CurrentFlag = 1";

            var datas = SqlAccess.ExecuteScalar(sql);

            var obj = new
            {
                totalRow = 1,
                datas = datas
            };
            return Json(obj);
        }

        public IActionResult update()
        {
            string sql = @"UPDATE dbo.DimEmployee SET FirstName = 'Son' WHERE EmployeeKey = 1";

            string response = SqlAccess.ExecuteNonQuery(sql);

            var obj = new
            {
                totalRow = 1,
                datas = response
            };
            return Json(obj);
        }
    }
}
