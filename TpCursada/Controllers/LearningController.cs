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

        //string logFilePath = "C:\\Users\\sullc\\source\\repos\\TpCursada\\TpCursada\\Logs\\log.txt";
        string logFilePath = "C:\\Users\\sullc\\source\\repos\\TpCursada\\TpCursada\\Logs\\LogTrainig.txt";
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult StartTraining()
        {
                _productRecommenderIAService.trainingModelML();
                // Aquí colocas el código para el entrenamiento
                // ...
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now.ToString()}: Operación completada.");
                    // Agrega más registros si es necesario
                };

            string logContent = System.IO.File.ReadAllText(logFilePath);
            ViewBag.LogContent = logContent;
            return PartialView("_LogPartial");

        }

        public IActionResult LoadData()
        {    // Código de generador en base al Historial en la db
            //_productRecommenderIAService.generarArchivoTrainigTestDB();
            _productRecommenderIAService.generarArchivoTrainigDB();
            _productRecommenderIAService.trainingModelML();

           var records= _productRecommenderIAService.GenerarInformeDeEntrenamiento();
          
            return Json(new { records });
        }


       
    }
}

