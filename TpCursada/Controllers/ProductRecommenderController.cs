﻿using Microsoft.AspNetCore.Mvc;
using TpCursada.Dominio;
using TpCursada.Models;

namespace TpCursada.Controllers
{
    public class ProductRecommenderController : Controller
    {

        private readonly ProductRecommenderIAService _productRecommenderIAService;

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
            ViewBag.idCoProduct = 5;
            return View(new Historical());
        }
        [HttpPost]
        public IActionResult Recommend(Historical historical )
        {
            _productRecommenderIAService.AddHistorico(historical);
            return View();
        }
        
        //public IActionResult Recommend(int productID)
        //{

        //    float percentaje = productRecommenderIAService.recommend(productID);

        //    ViewBag.percentajeView = percentaje;

        //    return View(percentaje);
        //}

        public IActionResult HistorialDeBusqueda()
        {
            _productRecommenderIAService.GetHistorico();
            return View();
        }
        
    }
}
