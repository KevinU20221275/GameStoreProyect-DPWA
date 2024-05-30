using System.Security.Claims;
using GameStore.Data;
using GameStore.Models;
using GameStore.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(MUser mUser)
        {
            string password = mUser.Password;
            mUser.Password = Utility.ConvertSHA256(password);
            MUser user = _appDbContext.tbl_user.FirstOrDefault(u => u.Email == mUser.Email && u.Password == mUser.Password);
            if (user != null)
            {
                if (!user.Signed)
                {
                    ViewBag.Mensaje = "Falat confirmar correo";
                }

                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString())
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Home");
            } else
            {
                ViewBag.Mensaje = "Contraseña o Correo incorrectos";
            }

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

            return RedirectToAction("SignIn");
        }

        public IActionResult Confirmar(string token)
        {
            MUser mUser = _appDbContext.tbl_user.FirstOrDefault(u => u.Token == token);

            if (mUser != null)
            {
                ViewBag.Respuesta = true;
                mUser.Signed = true;
                _appDbContext.Update(mUser);
                _appDbContext.SaveChanges();

                
            } else
            {
                ViewBag.Respuesta = false;
            }
            return View("Confirm");
        }

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("SignIn", "Login");
        }

    }
}
