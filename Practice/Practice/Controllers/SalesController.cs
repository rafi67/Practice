using Microsoft.AspNetCore.Mvc;
using Practice.Models;
using Practice.ViewModel;
using System.Runtime.InteropServices.ObjectiveC;

namespace Practice.Controllers
{
    public class SalesController : Controller
    {

        private readonly Context db = null;
        public SalesController(Context db)
        {
            this.db = db;
        }
        [HttpGet]
        public IActionResult Single()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Get()
        {
            var SM = db.SaleMasters.ToList();
            return Json(SM);
        }

        [HttpGet]
        public JsonResult GetById(long id)
        {
            var SM = db.SaleMasters.Find(id);
            var sale = new VmSale();
            sale.SaleId = id;
            sale.CreateDate = SM.CreateDate;
            sale.CustomerName = SM.CustomerName;
            sale.CustomerAddress = SM.CustomerAddress;
            sale.Gender = SM.Gender;
            var listSD = new List<VmSale.VmSaleDetail>();
            var lsd = db.SaleDetails.Where(x=>x.SaleId==id).ToList();
            foreach(var list in lsd)
            {
                var SD = new VmSale.VmSaleDetail();
                SD.ProductName = list.ProductName;
                SD.SaleId = sale.SaleId;
                SD.SaleDetailId = list.SaleDetailId;
                SD.Price = list.Price;
                listSD.Add(SD);
            }
            sale.SaleDetails = listSD;
            return Json(sale);
        }

        [HttpPost]
        public JsonResult Add([FromBody] VmSale model)
        {
            object resdata = null; string message = "No action."; bool resstate = false;
            var SM = new SaleMaster();
            SM.CreateDate = model.CreateDate;
            SM.CustomerName = model.CustomerName;
            SM.CustomerAddress = model.CustomerAddress;
            SM.Gender = model.Gender;
            db.SaleMasters.Add(SM);
            db.SaveChanges();
            var listSD = new List<SaleDetail>();
            foreach(var list in model.SaleDetails)
            {
                var SD = new SaleDetail();
                SD.ProductName = list.ProductName;
                SD.SaleId = SM.SaleId;
                SD.Price = list.Price;
                listSD.Add(SD);
            }
            db.SaleDetails.AddRange(listSD);
            db.SaveChanges();
            resstate = true;
            message = "Added Successfully";
            resdata = new
            {
                resstate,
                message
            };
            return Json(resdata);
        }

        [HttpPost]
        public JsonResult Update([FromBody] VmSale model)
        {
            object resdata = null; string message = "No action."; bool resstate = false;
            var SM = new SaleMaster();
            var smr = db.SaleMasters.Find(model.SaleId);
            db.SaleMasters.Remove(smr);
            db.SaveChanges();
            var sdr = db.SaleDetails.Where(x => x.SaleId == model.SaleId).ToList();
            db.SaleDetails.RemoveRange(sdr);
            db.SaveChanges();
            SM.CreateDate = model.CreateDate;
            SM.CustomerName = model.CustomerName;
            SM.CustomerAddress = model.CustomerAddress;
            SM.Gender = model.Gender;
            db.SaleMasters.Add(SM);
            db.SaveChanges();
            var listSD = new List<SaleDetail>();
            foreach (var list in model.SaleDetails)
            {
                var SD = new SaleDetail();
                SD.ProductName = list.ProductName;
                SD.SaleId = SM.SaleId;
                SD.Price = list.Price;
                listSD.Add(SD);
            }
            db.SaleDetails.AddRange(listSD);
            db.SaveChanges();
            resstate = true;
            message = "Updated Successfully";
            resdata = new
            {
                resstate,
                message
            };
            return Json(resdata);
        }

        [HttpPost]
        public JsonResult Remove(long id)
        {
            object resdata = null; string message = "No action."; bool resstate = false;
            var SM = db.SaleMasters.Find(id);
            
            if(SM != null)
            {
                var SD = db.SaleDetails.Where(x => x.SaleId == id).ToList();
                db.SaleDetails.RemoveRange(SD);
                db.SaleMasters.Remove(SM);
                db.SaveChanges();
            }
            message = "Deleted Successfully";
            resstate = true;
            resdata = new
            {
                resstate,
                message
            };
            return Json(resdata);
        }
    }
}
