using ACPA_Lab02.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ACPA_Lab02.Controllers
{
    public class InfoController : Controller
    {

		public ActionResult Menu()
		{
			return View();
		}
		public ActionResult Pedido()
		{
			return View();
		}
		public ActionResult Orden ()
		{
			return View();
		}
		public ActionResult Restablecer()
		{
			return View();
		}
        // GET: Info
        public ActionResult Index()
        {
            return View(new List<CustomerModel>());
        }
		[HttpPost]
		public ActionResult Index(HttpPostedFileBase postedFile)
		{
			List<CustomerModel> customers = new List<CustomerModel>();
			string filePath = string.Empty;
			if (postedFile != null)
			{
				string path = Server.MapPath("~/Uploads/");
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
				filePath = path + Path.GetFileName(postedFile.FileName);
				string extension = Path.GetExtension(postedFile.FileName);
				postedFile.SaveAs(filePath);

				string csvData = System.IO.File.ReadAllText(filePath);
				foreach(string row in csvData.Split('\n'))
				{
					if (!string.IsNullOrEmpty(row))
					{
						customers.Add(new CustomerModel
						{
							id = row.Split(',')[0],
							nombre = row.Split(',')[1],
							descripcion = row.Split(',')[2],
							productora = row.Split(',')[3],
							precio = row.Split(',')[4],
							existencia = row.Split(',')[5],
						});
					}
				}
			}
			return View(customers);
		}
    }
}