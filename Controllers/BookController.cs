using LibraryManagement.DAL;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using ZXing;

namespace LibraryManagement.Controllers
{
    //[SessionAuthorize]
    public class BookController : Controller
    {
        public BookController()
        {
            ViewBag.SoftwareName = SoftwareInfo.SoftwareName;
        }
        // GET: Book
        public ActionResult Index()
        {
            /*
             * Creating object of DAL class `HomeDbUtil`
             */
            HomeDbUtil dbObj = new HomeDbUtil();

            List<Book> details = new List<Book>();
            details = dbObj.GetAllBooks();
            return View(details);
        }

        public ActionResult Add()
        {
            HomeDbUtil homeDbUtil = new HomeDbUtil();
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            Book obj = new Book();
            obj.ISBN = unixTimestamp;

            ViewBag.Category = new SelectList(homeDbUtil.GetAllCategory(), "id", "name");
            ViewBag.Publication = new SelectList(homeDbUtil.GetAllPublications(), "id","name");

            return View(obj);
        }

        [HttpPost]
        public ActionResult Add(Book model)
        {
            HomeDbUtil homeDbUtil = new HomeDbUtil();
            model.IsIssued = 0;
            // Image store code

            // To make sure the file name is unique
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
            if (model.Image != null)
            {
                string path = Server.MapPath("~/Images/BookCover/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                
                model.Image.SaveAs(path + uniqueFileName);
                model.ImgUrl = uniqueFileName;
            }

            int rows = homeDbUtil.insertBook(model);

            if (rows > 0)
            {
                return RedirectToAction("Details/" + model.ISBN);
            }
            else
            {
                return View();
            }
            
        }
        
        public ActionResult Edit(int id)
        {
            return View();
        }
       
        public ActionResult Details(int id)
        {
            HomeDbUtil homeDbUtil = new HomeDbUtil();
            
            Book bookObj = homeDbUtil.GetBookByISBN(id);
            if (bookObj.ID > 0)
            {
                Publication pubObj = homeDbUtil.GetPublicationByID(bookObj.PubID);
                bookObj.PubName = pubObj.Name;

                Category catObj = homeDbUtil.GetCategoryByID(bookObj.CatID);
                bookObj.CatName = catObj.Name;

                ViewBag.BarCode = BarcodeGenrator(Convert.ToString(bookObj.ISBN));
                return View(bookObj);
            }
            else
            {
                ViewBag.Error = 1;
                return View();
            }
        }

        private string BarcodeGenrator(string bar)
        {
            string barcodeimg;
            Image img = null;
            using (var ms = new MemoryStream())
            {
                var writer = new ZXing.BarcodeWriter() { Format = BarcodeFormat.CODE_128 };
                writer.Options.Height = 80;
                writer.Options.Width = 280;
                //writer.Options.PureBarcode = true;
                img = writer.Write(bar);
                img.Save(ms, ImageFormat.Png);
                barcodeimg = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
            }
            return barcodeimg;
        }
    }
}