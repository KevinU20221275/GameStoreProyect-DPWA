using GameStore.Data;
using GameStore.Models;
using GameStore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LoginController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(MUser mUser)
        {
            mUser.Password=Utility.ConvertSHA256(mUser.Password);
            mUser.Token=Utility.GenerarToken();
            mUser.Reset = false;
            mUser.Signed = false;
            _appDbContext.tbl_user.Add(mUser);
            _appDbContext.SaveChanges();

            string path = _webHostEnvironment.WebRootPath;

            string upload = path + SSP.TemplatePath;
            string content = System.IO.File.ReadAllText(upload);
            string url = string.Format("{0}://{1}{2}", HttpContext.Request.Scheme, Request.Headers["host"], "/Login/Confirmar?token=" + mUser.Token);
            string htmlBody = string.Format(content, mUser.UserName, url);
            Email email = new Email()
            {
                EmailTo = mUser.Email,
                Subject = "Correo de Confirmacion",
                Content = htmlBody
            };

            bool enviado = EmailService.SendEmail(email);

            return RedirectToAction("Index");
        }

    }
}
