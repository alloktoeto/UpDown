using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UpDown.Models;
using UpDown.Repository;

namespace UpDown.Controllers
{
    public class PayPalController : Controller
    {
        //
        // GET: /PayPal/

        [HttpPost]
        public ActionResult Index(FormCollection formvalue)
        {
            if (Session["card"] == null)
            {
                //return RedirectToAction("Index", "Home");
            }

            var ls = Session["card"] as List<product>;
            TempData["products"] = ls;
            var keys = formvalue.AllKeys;

            var products = new List<ProductViewModel>();

            foreach (product p in ls)
            {
                if (keys.Contains(p.prodID.ToString()))
                {
                    ProductViewModel pvm = new ProductViewModel(p, formvalue[p.prodID.ToString()].Split(',').ToList().Count);
                    var formats = formvalue[p.prodID.ToString()].Split(',').ToList();
                    int format;
                    foreach (string f in formats)
                    {
                        if (int.TryParse(f, out format))
                        {
                            pvm.FormatsSelectedIdList.Add(format);
                        }
                    }
                    products.Add(pvm);
                }
            }

            foreach (string k in keys)
            {
                var values = formvalue[k].Split(',');
            }
            TempData["products"] = products;
            //PaymentRepository paymentRepository = new PaymentRepository();

            //string zzz = paymentRepository.DecryptString("XzX3iQQBNCBioJEN8vs8f43yYsywq/zQ9GKS3XIyG9DJ5VoF//wObSkGRxqwfNZ+BqgPtHdYCN8dzVAM35fAtFLIl3G0drs98cu96oYP1caKQ0OhMSI9FEkQi/oius/XkwtB6NLuXRiwpcd5jXgzKCFmBnwbU/b3pwSmc4LJBnc5Q1CGTjes3EnN7XMx6W5GRQ9HxJMacCtTAYVvpFFOF3JiR82tnDJNEhz/4IurC0kyFg+e1vjrxpQULsgW8XR+");
            //user u = new user();
            //u.userID= new Guid("0F8A304E-CE20-4802-8851-3152A308831A");
            //paymentRepository.SetDownLoads(products, u);
            return View(products);
        }


        [HttpGet]
        public ActionResult GetDataPayPal()
        {
            var getData = new GetDataPayPal();
            var order = getData.InformationOrder(getData.GetPayPalResponse(Request.QueryString["tx"]));
            ViewBag.tx = Request.QueryString["tx"];
            var resuls = getData.GetPayPalResponse(Request.QueryString["tx"]);
            if (resuls.StartsWith("SUCCESS"))
            {
                PaymentRepository paymentRepository = new PaymentRepository();
                paymentRepository.SetTransaction(order, Request.QueryString["tx"]);
                Logger.LogPayments(resuls);
                List<ProductViewModel> products = TempData["products"] as List<ProductViewModel>;
                //paymentRepository.SetDownLoads(products);
            }
            return View();
        }

    }
}
