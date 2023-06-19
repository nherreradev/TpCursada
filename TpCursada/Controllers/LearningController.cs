using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IO;
using System.Collections.Generic;
using TpCursada.Models;

namespace TpCursada.Controllers
{
    public class LearningController : Controller
    {


        private ProductRecommenderIAService _productRecommenderIAService;

        public LearningController(ProductRecommenderIAService productRecommenderIAService)
        {
            _productRecommenderIAService = productRecommenderIAService;
        }

        public IActionResult Index()
        {
            var records = _productRecommenderIAService.ReadLastRecordFromLogFile();
            ViewData["Records"] = records;
            return View();

        }

        //public IActionResult StartTraining()
        public IActionResult LoadData()
        {    // Código de generador en base al Historial en la db
            _productRecommenderIAService.generarArchivoTrainigTestDB();
            //_productRecommenderIAService.generarArchivoTrainigDB();
            _productRecommenderIAService.trainingModelML();

           var records= _productRecommenderIAService.GenerarInformeDeEntrenamiento();
          
            return Json(new { records });
        }


       
    }
}

