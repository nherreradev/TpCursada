using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IO;
using System.Collections.Generic;
using TpCursada.Models;
using Microsoft.Win32;

namespace TpCursada.Controllers
{
    public class LearningController : Controller
    {


        private ProductRecommenderIAService _productRecommenderIAService;
        public static RegistroEntrenamiento UltimoEntrenamientoEjecutado=null;
        public static List<RegistroEntrenamiento> registros=new List<RegistroEntrenamiento>();
       
        public LearningController(ProductRecommenderIAService productRecommenderIAService)
        {
            _productRecommenderIAService = productRecommenderIAService;
        }

        public IActionResult Index()
        {
            //Deberia ejecutarce 1 vez al ejecutar la app
            if (UltimoEntrenamientoEjecutado == null) {   
             UltimoEntrenamientoEjecutado = _productRecommenderIAService.ReadLastRecordFromLogFile();
            }
            ViewData["Records"] = UltimoEntrenamientoEjecutado;
            List<RegistroEntrenamiento> ordenado = registros.OrderByDescending(r => r.Fecha).ToList();
            return View(ordenado);
        }

        //public IActionResult StartTraining()
       
        public IActionResult LoadData()
        {    // Código de generador en base al Historial en la db
            _productRecommenderIAService.generarArchivoTrainigTestDB();
            //_productRecommenderIAService.generarArchivoTrainigDB();
            _productRecommenderIAService.trainingModelML();
            registros.Add(_productRecommenderIAService.GenerarInformeDeEntrenamiento());
          
            //ViewData["Records"] = records;
            return RedirectToAction("Index");
        }


       
    }
}

