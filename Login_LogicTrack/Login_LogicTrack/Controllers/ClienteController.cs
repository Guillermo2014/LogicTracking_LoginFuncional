using Login_LogicTrack.API;
using Login_LogicTrack.Models;
using Microsoft.AspNetCore.Http;
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
    public class ClienteController : Controller
    {
        Backend _api = new Backend();

        // GET: ClienteController
        public async Task<ActionResult> Index()
        {
            List<Cliente> clientes = new List<Cliente>();
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync("clientes");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                clientes = JsonConvert.DeserializeObject<List<Cliente>>(result);
            }
            return View(clientes);
        }

        // GET: ClienteController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            List<Cliente> clientes = new List<Cliente>();
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync("clientes");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                clientes = JsonConvert.DeserializeObject<List<Cliente>>(result);
            }

            Cliente clientedetail = new Cliente();

            for (int i = 0; i < clientes.Count(); i++)
            {
                if (id == clientes[i].id)
                {
                    clientedetail = clientes[i];
                }
            }

            return View(clientedetail);
        }

        // GET: ClienteController/Create
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


            ViewBag.Empresas = new SelectList(empresas, "idEmpresaCliente", "razonSocial");
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Cliente cliente)
        {
            var empresa = new Empresa();
            HttpClient client = _api.Initial();
            List<Empresa> empresas = new List<Empresa>();

            HttpResponseMessage response = await client.GetAsync("empresasCliente");

            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                empresas = JsonConvert.DeserializeObject<List<Empresa>>(res);
            }

            for (int i = 0; i < empresas.Count(); i++)
            {
                if (cliente.idEmpresa == empresas[i].idEmpresaCliente)
                {
                    empresa = empresas[i];
                    cliente.empresa = empresa;
                }
            }

            var postTask = client.PostAsJsonAsync<Cliente>("clientes", cliente);
            postTask.Wait();
            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
            {
                ViewBag.Message = "El cliente se registro correctamente";
                return RedirectToAction("Index");

            }
            ViewBag.Message = "Error al registrar";
            return View();
        }

        // GET: ClienteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClienteController/Edit/5
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

        // GET: ClienteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClienteController/Delete/5
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