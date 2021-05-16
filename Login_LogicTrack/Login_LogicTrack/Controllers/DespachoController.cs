using Login_LogicTrack.API;
using Login_LogicTrack.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Login_LogicTrack.Controllers
{
    public class DespachoController : Controller
    {
        Backend _api = new Backend();
        // GET: Despacho
        public async Task<ActionResult> Index()
        {
            List<Despacho> despachos = new List<Despacho>();
            HttpClient client = _api.Initial();
            //HttpResponseMessage response = await client.GetAsync("despachosPorTransportista" + $"/{id}");
            var uri = "despachosPorTransportista/" + HttpContext.Session["IdTransportista"];
            HttpResponseMessage response = await client.GetAsync(uri);
            //HttpResponseMessage response = await client.GetAsync("despachos");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                despachos = JsonConvert.DeserializeObject<List<Despacho>>(result);
            }
            return View(despachos);
        }

        public async Task<ActionResult> Details(int id)
        {
            List<Despacho> despachos = new List<Despacho>();
            HttpClient client = _api.Initial();
            HttpContext.Session["IdDespacho"] = id;
            HttpResponseMessage response = await client.GetAsync("despachos");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                despachos = JsonConvert.DeserializeObject<List<Despacho>>(result);

            }

            Despacho despacho = new Despacho();

            for (int i = 0; i < despachos.Count(); i++)
            {
                if (id == despachos[i].idDespacho)
                {
                    despacho = despachos[i];

                    for (int k = 0; k < despacho.detallesDespacho.Count(); k++)
                    {
                        List<Medicamento> medicamentos = new List<Medicamento>();

                        HttpResponseMessage response2 = await client.GetAsync("medicamentos");
                        if (response2.IsSuccessStatusCode)
                        {
                            var result2 = response2.Content.ReadAsStringAsync().Result;
                            medicamentos = JsonConvert.DeserializeObject<List<Medicamento>>(result2);
                            for (int j = 0; j < medicamentos.Count(); j++)
                            {
                                if (despacho.detallesDespacho[k].idMedicamento == medicamentos[j].IdMedicamento)
                                {
                                    despacho.detallesDespacho[k].medicamento = medicamentos[j];
                                }
                            }

                        }
                    }
                }

            }
            if (despacho.estado == "Despacho")
            {
                return View("DetailsDespacho", despacho);
            }
            else if (despacho.estado == "Abierto")
            {
                return View("DetailsDespachoA", despacho);
            }
            return View(despacho);
        }

        public async Task<ActionResult> DetailsDespacho(int id)
        {
            List<Despacho> despachos = new List<Despacho>();
            HttpClient client = _api.Initial();
            HttpContext.Session["IdDespacho"] = id;
            HttpResponseMessage response = await client.GetAsync("despachos");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                despachos = JsonConvert.DeserializeObject<List<Despacho>>(result);

            }

            Despacho despacho = new Despacho();

            for (int i = 0; i < despachos.Count(); i++)
            {
                if (id == despachos[i].idDespacho)
                {
                    despacho = despachos[i];

                    for (int k = 0; k < despacho.detallesDespacho.Count(); k++)
                    {
                        List<Medicamento> medicamentos = new List<Medicamento>();

                        HttpResponseMessage response2 = await client.GetAsync("medicamentos");
                        if (response2.IsSuccessStatusCode)
                        {
                            var result2 = response2.Content.ReadAsStringAsync().Result;
                            medicamentos = JsonConvert.DeserializeObject<List<Medicamento>>(result2);
                            for (int j = 0; j < medicamentos.Count(); j++)
                            {
                                if (despacho.detallesDespacho[k].idMedicamento == medicamentos[j].IdMedicamento)
                                {
                                    despacho.detallesDespacho[k].medicamento = medicamentos[j];
                                }
                            }

                        }
                    }
                }
            }

            return View(despacho);
        }

        public async Task<ActionResult> DetailsDespachoA(int id)
        {
            List<Despacho> despachos = new List<Despacho>();
            HttpClient client = _api.Initial();
            HttpContext.Session["IdDespacho"] = id;
            HttpResponseMessage response = await client.GetAsync("despachos");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                despachos = JsonConvert.DeserializeObject<List<Despacho>>(result);

            }

            Despacho despacho = new Despacho();

            for (int i = 0; i < despachos.Count(); i++)
            {
                if (id == despachos[i].idDespacho)
                {
                    despacho = despachos[i];

                    for (int k = 0; k < despacho.detallesDespacho.Count(); k++)
                    {
                        List<Medicamento> medicamentos = new List<Medicamento>();

                        HttpResponseMessage response2 = await client.GetAsync("medicamentos");
                        if (response2.IsSuccessStatusCode)
                        {
                            var result2 = response2.Content.ReadAsStringAsync().Result;
                            medicamentos = JsonConvert.DeserializeObject<List<Medicamento>>(result2);
                            for (int j = 0; j < medicamentos.Count(); j++)
                            {
                                if (despacho.detallesDespacho[k].idMedicamento == medicamentos[j].IdMedicamento)
                                {
                                    despacho.detallesDespacho[k].medicamento = medicamentos[j];
                                }
                            }

                        }
                    }
                }
            }

            return View(despacho);
        }

        public ActionResult Seguimiento()
        {
            return View();
        }

        public async Task<ActionResult> EditarDespacho(int id)
        {
            ViewBag.idDespacho = id.ToString();

            List<Despacho> despachos = new List<Despacho>();
            HttpClient client = _api.Initial();

            HttpResponseMessage response = await client.GetAsync("despachos");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                despachos = JsonConvert.DeserializeObject<List<Despacho>>(result);
            }

            Despacho despacho = new Despacho();

            for (int i = 0; i < despachos.Count(); i++)
            {
                if (id == despachos[i].idDespacho)
                {
                    despacho = despachos[i];
                }
            }

            despacho.estado = "Transporte";

            var postTask = client.PutAsJsonAsync<Despacho>("despachos", despacho);
            postTask.Wait();
            var result1 = postTask.Result;

            if (result1.IsSuccessStatusCode)
            {
                ViewBag.Message = "El despacho se actualizó correctamente";
                var redirUrl = "../Details/" + despacho.idDespacho.ToString();
                return Redirect(redirUrl);
            }
            ViewBag.Message = "Error al registrar";

            return View(despacho);
        }

        [HttpPost]
        public async Task<JsonResult> CrearGraficaSeguimiento()
        {

            /* if (HttpContext.Session["IdTransportista"] == null)
             {
                 HttpContext.Session.SetString("idSolicitud", "3");
                 HttpContext.Session["IdTransportista"]("", "");
             }¨*/

            List<LogMedicamento> seguimiento = new List<LogMedicamento>();
            HttpClient client = _api.Initial();
            var uri = "logsMedicionesPorDespacho/" + HttpContext.Session["idDespacho"];
            HttpResponseMessage response = await client.GetAsync(uri);


            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                seguimiento = JsonConvert.DeserializeObject<List<LogMedicamento>>(result);
            }


            List<object> iData = new List<object>();
            //Creating sample data  
            DataTable dt = new DataTable();
            dt.Columns.Add("Expense", System.Type.GetType("System.String"));
            dt.Columns.Add("ExpenseValuesMin", System.Type.GetType("System.Int32"));
            dt.Columns.Add("ExpenseValuesMax", System.Type.GetType("System.Int32"));
            dt.Columns.Add("ExpenseValuesMedicion", System.Type.GetType("System.Int32"));

            for (int i = 0; i < seguimiento.Count(); i++)
            {
                DataRow dr = dt.NewRow();
                dr["Expense"] = seguimiento[i].hora;
                dr["ExpenseValuesMin"] = seguimiento[i].valorMinimo;
                dr["ExpenseValuesMax"] = seguimiento[i].valorMaximo;
                dr["ExpenseValuesMedicion"] = seguimiento[i].valorMedicion;
                dt.Rows.Add(dr);

            }

            //Looping and extracting each DataColumn to List<Object>  
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            ViewBag.ChartData = iData;
            //Source data returned as JSON  
            return Json(iData);
        }

    }
}