using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IO;
using System.Collections.Generic;

namespace TpCursada.Controllers
{
    public class LearningController : Controller
    {

        //string logFilePath = "C:\\Users\\sullc\\source\\repos\\TpCursada\\TpCursada\\Logs\\log.txt";
        string logFilePath = "C:\\Users\\sullc\\source\\repos\\TpCursada\\TpCursada\\Logs\\LogTrainig.txt";
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult StartTraining()
        {

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
        {
            // Simulate data loading from a file
            // Here, we'll simply generate a list of timestamps
            List<string> records = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                string timestamp = DateTime.Now.ToString();
                // Guardar la fecha actual en el archivo de registro
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine(timestamp);
                }
            
             
            }  
         
            string lastRecord = ReadLastRecordFromLogFile(); 
            records.Add(lastRecord);
            return Json(new { records });
        }


        private string ReadLastRecordFromLogFile()
        {
            if (System.IO.File.Exists(logFilePath))
            {
                try
                {
                    // Leer todas las líneas del archivo
                    string[] lines = System.IO.File.ReadAllLines(logFilePath);

                    // Obtener la última línea
                    if (lines.Length > 0)
                    {
                        return lines[lines.Length - 1];
                    }
                }
                catch (Exception ex)
                {
                    // Registrar el error en la consola
                    Console.WriteLine($"Error reading log file: {ex.Message}");
                }
            }

            return null;
        }

    }
}

