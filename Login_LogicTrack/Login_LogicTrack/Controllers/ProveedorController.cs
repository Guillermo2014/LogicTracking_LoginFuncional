using Login_LogicTrack.API;
using Login_LogicTrack.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Login_LogicTrack.Controllers
{
    public class ProveedorController : Controller
    {
        Backend _api = new Backend();

        // GET: MedicamentosController
        public async Task<ActionResult> Index()
        {
            List<Proveedor> proveedors = new List<Proveedor>();
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync("proveedores");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                proveedors = JsonConvert.DeserializeObject<List<Proveedor>>(result);
            }
            return View(proveedors);
        }


        public async Task<ActionResult> Create()
        {
            List<Representante> representantes = new List<Representante>();

            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync("representantes");


            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                representantes = JsonConvert.DeserializeObject<List<Representante>>(result);
            }


            ViewBag.Representantes = new SelectList(representantes, "idRepresentante", "nombre");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Proveedor proveedor)
        {

            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<Proveedor>("proveedores", proveedor);
            postTask.Wait();
            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
            {
                ViewBag.Message = "El medicamento se registro correctamente";
                return RedirectToAction("Index");

            }
            ViewBag.Message = "Error al registrar";
            return View();
        }


    }
}