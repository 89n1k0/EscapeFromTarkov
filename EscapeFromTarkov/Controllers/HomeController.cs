using EscapeFromTarkov.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace EscapeFromTarkov.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private EscapeFromTarkovContext db = new EscapeFromTarkovContext();

        public HomeController(ILogger<HomeController> logger, EscapeFromTarkovContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MainWindow()
        {
            return View();
        }
        public IActionResult PrivateAcc()
        {
            Пользователь пользователь = db.Пользовательs.Where(x => x.ПользовательId == CurrentUser.CurrentClientId).FirstOrDefault();
            return View(пользователь);
        }
        public class PrivateAccViewModel
        {
            public IEnumerable<Персонажи>? NPS { get; set; } 
            public IEnumerable<Босс>? Boss { get; set; }
            public IEnumerable<Карта>? Card { get; set; }
            public string name;
            public string image;
            public string description;
        }
        public IActionResult NPS()
        {
            var персонажи = db.Персонажиs.ToList();
            var ViewModel = new PrivateAccViewModel()
            {
                NPS = персонажи
            };
            return View(ViewModel);
        }
        public IActionResult GetBossInfo(string bossName)
        {
            Босс boss = db.Боссs.FirstOrDefault(x => x.Наименование == bossName);

            if (boss != null)
            {
                var imageBase64 = Convert.ToBase64String(boss.Изображение);
                var bossInfo = new
                {
                    name = boss.Наименование,
                    image = "data:image/jpeg;base64," + imageBase64, // здесь указывается тип изображения и сама строка Base64
                    description = boss.Описание
                };

                return Json(bossInfo);
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult GetNPSInfo(string bossName)
        {
            Персонажи boss = db.Персонажиs.FirstOrDefault(x => x.Наименование == bossName);

            if (boss != null)
            {
                var imageBase64 = Convert.ToBase64String(boss.Изображение);
                var bossInfo = new
                {
                    name = boss.Наименование,
                    image = "data:image/jpeg;base64," + imageBase64, // здесь указывается тип изображения и сама строка Base64
                    description = boss.Описание
                };

                return Json(bossInfo);
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult GetCardInfo(string bossName)
        {
            Карта boss = db.Картаs.FirstOrDefault(x => x.Наименование == bossName);

            if (boss != null)
            {
                var imageBase64 = Convert.ToBase64String(boss.Изображение);
                var bossInfo = new
                {
                    name = boss.Наименование,
                    image = "data:image/jpeg;base64," + imageBase64, // здесь указывается тип изображения и сама строка Base64
                    description = boss.Описание
                };

                return Json(bossInfo);
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult Bosses()
        {
            var персонажи = db.Боссs.ToList();
            var ViewModel = new PrivateAccViewModel()
            {
                Boss = персонажи
            };
            return View(ViewModel);
        }
        public IActionResult Cards()
        {
            var персонажи = db.Картаs.ToList();
            var ViewModel = new PrivateAccViewModel()
            {
                Card = персонажи
            };
            return View(ViewModel);
        }
        [AllowAnonymous]
        public IActionResult Authorization()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Registration()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authorization(string login, string password)
        {
            Пользователь пользователь = db.Пользовательs.Where(x => x.Логин == login && x.Пароль == password).FirstOrDefault();
            CurrentUser.CurrentClientId = пользователь.ПользовательId;
            if (CurrentUser.CurrentClientId > 0 && пользователь.РолиId == 1)
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, "test"),
                new Claim(ClaimTypes.Email, "testc@mail.ru")};
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("MainWindow", "Home");
            }
            else if (CurrentUser.CurrentClientId > 0 && пользователь.РолиId == 2)
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, "test"),
                new Claim(ClaimTypes.Email, "testc@mail.ru")};
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("AdminPanelBoss", "Home");
            }
            else
            {
                var result = new SuccessResponse
                {
                    Success = false,
                    Message = "Данные неверные",
                };
                return ViewBag.Enter = result.Message;
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Registration(string login, string password)
        {
            Пользователь Clients = (from c in db.Пользовательs where c.Логин == login select c).FirstOrDefault();
            if (Clients != null)
            {
                return RedirectToAction("Authorization", "Home");
            }
            else
            if (password == null || login == null)
            {
                var result = new SuccessResponse
                {
                    Success = false,
                    Message = "Некоторые поля пустые",
                };
                return ViewBag.Enter = result.Message;
            }
            else
            {
                db.Add(new Пользователь { Логин = login, Пароль = password, РолиId = 1 });
                db.SaveChanges();
                return RedirectToAction("Authorization", "Home");
            }
        }

        public IActionResult AdminPanelBoss()
        {
            var персонажи = db.Боссs.ToList();
            var ViewModel = new PrivateAccViewModel()
            {
                Boss = персонажи
            };
            return View(ViewModel);
        }
        public IActionResult AdminPanelCard()
        {
            var персонажи = db.Картаs.ToList();
            var ViewModel = new PrivateAccViewModel()
            {
                Card = персонажи
            };
            return View(ViewModel);
        }
        public IActionResult AdminPanelNPS()
        {
            var персонажи = db.Персонажиs.ToList();
            var ViewModel = new PrivateAccViewModel()
            {
                NPS = персонажи
            };
            return View(ViewModel);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}