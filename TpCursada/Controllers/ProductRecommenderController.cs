using Microsoft.AspNetCore.Mvc;
using TpCursada.Dominio;
using TpCursada.Models;

namespace TpCursada.Controllers
{
    public class ProductRecommenderController : Controller
    {

        private  ProductRecommenderIAService _productRecommenderIAService;

        public ProductRecommenderController(ProductRecommenderIAService productRecommenderIAService)
        {
            _productRecommenderIAService = productRecommenderIAService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Recommend()
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Recommend(int productID)
        {
           
            //var resultado = _productRecommenderIAService.RecommendTop5OLD(productID);
            var resultado = _productRecommenderIAService.RecommendTop5(productID);
            if (resultado != null) {  
                ViewBag.Prediccion =1;
            }
           
            _productRecommenderIAService.AddRowHistorical(resultado);

            return View(resultado);
        }
        public IActionResult HistorialDeBusqueda()
        {
            List<Historical> _historicalList = new List<Historical>();
            _historicalList = _productRecommenderIAService.GetHistorical();
            return View(_historicalList);
        }
    }
}
