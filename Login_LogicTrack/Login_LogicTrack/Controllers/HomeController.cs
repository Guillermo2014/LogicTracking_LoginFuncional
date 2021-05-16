using Login_LogicTrack.API;
using Login_LogicTrack.Models;
using Microsoft.Extensions.Logging;
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
    public class HomeController : Controller
    {
        /*private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }

        Backend _api = new Backend();

        [HttpPost]
        public async Task<JsonResult> CrearGraficaMedicamentos()
        {

            List<Grafica> medicamentos = new List<Grafica>();
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync("StockPorMedicamento");


            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                medicamentos = JsonConvert.DeserializeObject<List<Grafica>>(result);
            }


            List<object> iData = new List<object>();
            //Creating sample data  
            DataTable dt = new DataTable();
            dt.Columns.Add("Expense", System.Type.GetType("System.String"));
            dt.Columns.Add("ExpenseValues", System.Type.GetType("System.Int32"));

            for (int i = 0; i < medicamentos.Count(); i++)
            {
                DataRow dr = dt.NewRow();
                dr["Expense"] = medicamentos[i].nombre;
                dr["ExpenseValues"] = medicamentos[i].valor;
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


        [HttpPost]
        public async Task<JsonResult> CrearGraficaMedicamentosPorProveedor()
        {

            List<Grafica> medicamentos = new List<Grafica>();
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync("RecuentoMedicamentosPorProveedor");


            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                medicamentos = JsonConvert.DeserializeObject<List<Grafica>>(result);
            }


            List<object> iData = new List<object>();
            //Creating sample data  
            DataTable dt = new DataTable();
            dt.Columns.Add("Expense", System.Type.GetType("System.String"));
            dt.Columns.Add("ExpenseValues", System.Type.GetType("System.Int32"));

            for (int i = 0; i < medicamentos.Count(); i++)
            {
                DataRow dr = dt.NewRow();
                dr["Expense"] = medicamentos[i].nombre;
                dr["ExpenseValues"] = medicamentos[i].valor;
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


        [HttpPost]
        public async Task<JsonResult> CrearGraficaSolicitudPorCliente()
        {

            List<Grafica> medicamentos = new List<Grafica>();
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync("RecuentoSolicitudPorCliente");


            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                medicamentos = JsonConvert.DeserializeObject<List<Grafica>>(result);
            }


            List<object> iData = new List<object>();
            //Creating sample data  
            DataTable dt = new DataTable();
            dt.Columns.Add("Expense", System.Type.GetType("System.String"));
            dt.Columns.Add("ExpenseValues", System.Type.GetType("System.Int32"));

            for (int i = 0; i < medicamentos.Count(); i++)
            {
                DataRow dr = dt.NewRow();
                dr["Expense"] = medicamentos[i].nombre;
                dr["ExpenseValues"] = medicamentos[i].valor;
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


        [HttpPost]
        public async Task<JsonResult> CrearGraficaDespachosFechaCreacion()
        {

            List<Grafica> medicamentos = new List<Grafica>();
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync("DespachosPorFechaCreacion");


            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                medicamentos = JsonConvert.DeserializeObject<List<Grafica>>(result);
            }


            List<object> iData = new List<object>();
            //Creating sample data  
            DataTable dt = new DataTable();
            dt.Columns.Add("Expense", System.Type.GetType("System.String"));
            dt.Columns.Add("ExpenseValues", System.Type.GetType("System.Int32"));

            for (int i = 0; i < medicamentos.Count(); i++)
            {
                DataRow dr = dt.NewRow();
                dr["Expense"] = medicamentos[i].nombre;
                dr["ExpenseValues"] = medicamentos[i].valor;
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