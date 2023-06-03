using Microsoft.AspNetCore.Mvc;
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

            var resultado = _productRecommenderIAService.RecommendTop5(productID);
         
            ViewBag.Prediccion = resultado;

            return View(resultado);
        }

    }
}
