using Login_LogicTrack.API;
using Login_LogicTrack.Models;
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
    public class MedicamentoController : Controller
    {
        Backend _api = new Backend();

        // GET: MedicamentosController
        public async Task<ActionResult> Index()
        {
            List<Medicamento> medicamentos = new List<Medicamento>();
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync("medicamentos");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                medicamentos = JsonConvert.DeserializeObject<List<Medicamento>>(result);
            }
            return View(medicamentos);
        }

        public async Task<ActionResult> Create()
        {
            List<Proveedor> proveedors = new List<Proveedor>();

            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync("proveedores");


            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                proveedors = JsonConvert.DeserializeObject<List<Proveedor>>(result);
            }


            ViewBag.Proveedores = new SelectList(proveedors, "idProveedor", "razonSocial");
            return View();
        }


        public async Task<ActionResult> Details(int id)
        {
            List<Medicamento> medicamentos = new List<Medicamento>();
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync("medicamentos");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                medicamentos = JsonConvert.DeserializeObject<List<Medicamento>>(result);
            }

            Medicamento medetail = new Medicamento();

            for (int i = 0; i < medicamentos.Count(); i++)
            {
                if (id == medicamentos[i].IdMedicamento)
                {
                    medetail = medicamentos[i];
                }
            }

            return View(medetail);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Medicamento medicamento)
        {
            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<Medicamento>("medicamentos", medicamento);
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

        //Crear condicion
        public async Task<ActionResult> CreateCondicion(int id)
        {
            ViewBag.idMedicamento = id.ToString();
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateCondicion(CondicionMedicamento condicion)
        {
            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<CondicionMedicamento>("condicionesMedicamentos", condicion);
            postTask.Wait();
            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
            {
                ViewBag.Message = "La condicion se registro correctamente";
                var redirUrl = "../Details/" + condicion.idMedicamento.ToString();
                return Redirect(redirUrl);
            }
            ViewBag.Message = "Error al registrar";
            return View();
        }

        /*[HttpGet]
        public IActionResult Details(Medicamento medicamento)
        {
            HttpClient client = _api.Initial();
            var getTask = client.<Medicamento>("api/Medicamento", medicamento);
            getTask.Wait();
            var result = getTask.Result;
            return View();
        }
        */
    }
}