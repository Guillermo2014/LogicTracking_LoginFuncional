using Login_LogicTrack.API;
using Login_LogicTrack.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Login_LogicTrack.Controllers
{
    public class LoginController : Controller
    {
        Backend _api = new Backend();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(Login login)
        {
            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<Login>("iniciarSesion", login);
            postTask.Wait();
            var response = postTask.Result;
            Login _login = new Login();

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                _login = JsonConvert.DeserializeObject<Login>(result);
                HttpContext.Session["IdTransportista"] = _login.idTransportista;
                HttpContext.Session["rol"] = _login.rol;

                if (_login.idTransportista == 0)
                {
                    ViewBag.Message = "Usuario no encontrado";
                    //return RedirectToAction("ErroLogin");
                }
                else if (_login.rol.ToString() == "Solicitante")
                {
                    return RedirectToAction("../Solicitud/Index");
                }
                else if (_login.rol.ToString() == "Transportista")
                {
                    return RedirectToAction("../Despacho/Index");
                }
                else
                {
                    //return RedirectToAction("../Shared/Layout1.cshtml");
                    return RedirectToAction("../Home/Index");
                }
            }

            return View(_login);
        }

        public ActionResult ErroLogin()
        {
            return View();
        }
    }
}