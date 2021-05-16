using Login_LogicTrack.API;
using Login_LogicTrack.Models;
using Microsoft.AspNetCore.Http;
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
    public class EmpresaController : Controller
    {
        Backend _api = new Backend();

        // GET: EmpresaController
        public async Task<ActionResult> Index()
        {
            List<Empresa> empresas = new List<Empresa>();
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync("empresasCliente");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                empresas = JsonConvert.DeserializeObject<List<Empresa>>(result);
            }
            return View(empresas);
        }

        // GET: EmpresaController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            List<Empresa> empresas = new List<Empresa>();
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync("empresasCliente");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                empresas = JsonConvert.DeserializeObject<List<Empresa>>(result);
            }

            Empresa empresadetail = new Empresa();

            for (int i = 0; i < empresas.Count(); i++)
            {
                if (id == empresas[i].idEmpresaCliente)
                {
                    empresadetail = empresas[i];
                }
            }

            return View(empresadetail);
        }

        // GET: EmpresaController/Create
        public async Task<ActionResult> Create()
        {
            List<Empresa> empresas = new List<Empresa>();

            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync("empresasCliente");


            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                empresas = JsonConvert.DeserializeObject<List<Empresa>>(result);
            }


            ViewBag.Proveedores = new SelectList(empresas, "idEmpresaCliente", "razonSocial");
            return View();
        }

        // POST: EmpresaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Empresa empresa)
        {
            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<Empresa>("empresasCliente", empresa);
            postTask.Wait();
            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
            {
                ViewBag.Message = "La empresa se registro correctamente";
                return RedirectToAction("Index");

            }
            ViewBag.Message = "Error al registrar";
            return View();
        }

        // GET: EmpresaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmpresaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmpresaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmpresaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}