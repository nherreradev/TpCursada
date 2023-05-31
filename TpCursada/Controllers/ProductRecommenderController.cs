using Microsoft.AspNetCore.Mvc;
using TpCursada.Models;

namespace TpCursada.Controllers
{
    public class ProductRecommenderController : Controller
    {
        ProductRecommenderIAService productRecommenderIAService = new ProductRecommenderIAService();
        public IActionResult Recommend(int productID)
        {

            float percentaje = productRecommenderIAService.recommend(3);

            ViewBag.percentajeView = percentaje;

            return View(percentaje);
        }
    }
}
