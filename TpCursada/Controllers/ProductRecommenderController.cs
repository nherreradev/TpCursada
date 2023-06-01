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

        public IActionResult Recommend(int productID)
        {

            float percentaje = _productRecommenderIAService.recomendarById(productID);

            ViewBag.percentajeView = percentaje;

            return View(percentaje);
        }

        //public IActionResult Recommend(int productID)
        //{

        //    float percentaje = productRecommenderIAService.recommend(productID);

        //    ViewBag.percentajeView = percentaje;

        //    return View(percentaje);
        //}

        public IActionResult HistorialDeBusqueda()
        {

            return View();
        }
        
    }
}
