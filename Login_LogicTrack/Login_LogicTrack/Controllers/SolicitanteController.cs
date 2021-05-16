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
    public class SolicitanteController : Controller
    {
        Backend _api = new Backend();
        public async Task<ActionResult> Index()
        {
            List<Solicitante> solicituds = new List<Solicitante>();
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync("despachos");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                solicituds = JsonConvert.DeserializeObject<List<Solicitante>>(result);
            }
            return View(solicituds);
        }

        public async Task<ActionResult> Details(int id)
        {


            List<Solicitante> solicitudes = new List<Solicitante>();
            HttpClient client = _api.Initial();

            HttpResponseMessage response = await client.GetAsync("despachos");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                solicitudes = JsonConvert.DeserializeObject<List<Solicitante>>(result);

            }

            Solicitante solicitante = new Solicitante();

            for (int i = 0; i < solicitudes.Count(); i++)
            {
                if (id == solicitudes[i].idDespacho)
                {
                    solicitante = solicitudes[i];

                    for (int k = 0; k < solicitante.detallesDespacho.Count(); k++)
                    {
                        List<Medicamento> medicamentos = new List<Medicamento>();

                        HttpResponseMessage response2 = await client.GetAsync("medicamentos");
                        if (response2.IsSuccessStatusCode)
                        {
                            var result2 = response2.Content.ReadAsStringAsync().Result;
                            medicamentos = JsonConvert.DeserializeObject<List<Medicamento>>(result2);
                            for (int j = 0; j < medicamentos.Count(); j++)
                            {
                                if (solicitante.detallesDespacho[k].idMedicamento == medicamentos[j].IdMedicamento)
                                {
                                    solicitante.detallesDespacho[k].medicamento = medicamentos[j];
                                }
                            }

                        }

                    }
                }
            }
            ViewBag.estado = solicitante.estado.ToString();
            return View(solicitante);
        }

        //Crear detalle despacho

        public ActionResult CreateDetalleDespacho(int id)
        {
            ViewBag.idDespacho = id.ToString();

            return View();
        }



        public ActionResult Create()
        {

            return View();
        }

        //Editar despacho

        public async Task<ActionResult> EditarDespacho(int id)
        {
            ViewBag.idDespacho = id.ToString();

            List<Solicitante> solicitudes = new List<Solicitante>();
            HttpClient client = _api.Initial();

            HttpResponseMessage response = await client.GetAsync("despachos");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                solicitudes = JsonConvert.DeserializeObject<List<Solicitante>>(result);

            }

            Solicitante solicitud = new Solicitante();

            for (int i = 0; i < solicitudes.Count(); i++)
            {
                if (id == solicitudes[i].idDespacho)
                {
                    solicitud = solicitudes[i];


                }
            }

            return View(solicitud);
        }

        [HttpPost]
        public ActionResult Create(Solicitante solicitud)
        {

            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<Solicitante>("despachos", solicitud);
            postTask.Wait();
            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
            {
                ViewBag.Message = "La solicitud se registro correctamente";
                return RedirectToAction("Index");

            }
            ViewBag.Message = "Error al registrar";
            return View();
        }

        [HttpPost]
        public ActionResult EditarDespacho(Solicitante solicitud)
        {

            HttpClient client = _api.Initial();
            var postTask = client.PutAsJsonAsync<Solicitante>("despachos", solicitud);
            postTask.Wait();
            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
            {
                ViewBag.Message = "La solicitud se actualizó correctamente";
                var redirUrl = "../Details/" + solicitud.idDespacho.ToString();
                return Redirect(redirUrl);
            }
            ViewBag.Message = "Error al registrar";
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateDetalleDespacho(DetalleDespacho detalle)
        {
            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<DetalleDespacho>("DetallesDespacho", detalle);
            postTask.Wait();
            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
            {
                ViewBag.Message = "El detalle se agregó correctamente";
                var redirUrl = "../Details/" + detalle.idDespacho.ToString();
                return Redirect(redirUrl);
            }
            ViewBag.Message = "Error al registrar";
            return View();
        }




        public ActionResult Seguimiento(int id)
        {
            HttpContext.Session["idSolicitud"] = id;
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> CrearGraficaSeguimiento()
        {

            if (HttpContext.Session["idSolicitud"] == null)
            {
                HttpContext.Session["idSolicitud"] = "3";
            }

            List<LogMedicamento> seguimiento = new List<LogMedicamento>();
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync("logsMedicionesPorDespacho/" + HttpContext.Session["idSolicitud"].ToString());


            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                seguimiento = JsonConvert.DeserializeObject<List<LogMedicamento>>(result);
            }


            List<object> iData = new List<object>();
            //Creating sample data  
            DataTable dt = new DataTable();
            dt.Columns.Add("Expense", System.Type.GetType("System.String"));
            dt.Columns.Add("ExpenseValuesMin", System.Type.GetType("System.Double"));
            dt.Columns.Add("ExpenseValuesMax", System.Type.GetType("System.Double"));
            dt.Columns.Add("ExpenseValuesMedicion", System.Type.GetType("System.Double"));

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