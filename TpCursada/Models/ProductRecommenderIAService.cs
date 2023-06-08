using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using TpCursada.Dominio;

namespace TpCursada.Models
{
    public class ProductRecommenderIAService
    {

        private  PW3TiendaContext _contextBD;

        public ProductRecommenderIAService(PW3TiendaContext context)
        {
            _contextBD = context;
        }
        /*1*/
        public void trainingModelML()
        {
            try
            {
               
                // Código de generador en base al Historial en la db
                //generarArchivoTrainigDB();
                // Código de entrenamiento aquí

                //STEP 1: Create MLContext to be shared across the model creation workflow objects
                MLContext mlContext = new MLContext();

                //STEP 2: Read the trained data using TextLoader by defining the schema for reading the product co-purchase dataset
                //        Do remember to replace amazon0302.txt with dataset from https://snap.stanford.edu/data/amazon0302.html
                // Especifica la ubicación real de tus datos de entrenamiento 
                //string TrainingDataLocation = "C:\\web3\\Pruebas\\LibreriaHtmlAgilityPack\\ProductRecommendation\\Data\\Amazon0302.txt";
                string TrainingDataLocation = DataLocationRelative;
                var traindata = mlContext.Data.LoadFromTextFile(path: TrainingDataLocation,
                                           columns: new[]
                                           {
                                            new TextLoader.Column("Label", DataKind.Single, 0),
                                            new TextLoader.Column(name:nameof(ProductEntry.ProductID), dataKind:DataKind.UInt32, source: new [] { new TextLoader.Range(0) }, keyCount: new KeyCount(262111)),
                                            new TextLoader.Column(name:nameof(ProductEntry.CoPurchaseProductID), dataKind:DataKind.UInt32, source: new [] { new TextLoader.Range(1) }, keyCount: new KeyCount(262111))
                                           },
                                           hasHeader: true,
                                           separatorChar: '\t');

                
                /*Esta clase se utiliza para establecer y ajustar los parámetros específicos del algoritmo de factorización
                 * de matrices durante el entrenamiento de modelos de recomendación o filtrado colaborativo.*/
                MatrixFactorizationTrainer.Options options = new MatrixFactorizationTrainer.Options();
                options.MatrixColumnIndexColumnName = nameof(ProductEntry.ProductID);
                options.MatrixRowIndexColumnName = nameof(ProductEntry.CoPurchaseProductID);
                options.LabelColumnName = "Label";
                options.LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass; //Funcion de perdida durante el entrenamiento
                options.Alpha = 0.001; /* Determina cuánto deben actualizarse los valores de las variables del modelo en cada iteración  */
                options.Lambda = 0.001; /* Ayuda a evitar que el modelo se ajuste en exceso a los datos de entrenamiento y, en cambio, favorece la simplicidad y la estabilidad del modelo. */

                /* Nutrimos el MatrixFactorization con la configuracion establecida en el paso anterior */
                var est = mlContext.Recommendation().Trainers.MatrixFactorization(options);

                /*utiliza el (Recommendation) y el conjunto de datos de entrenamiento (traindata) para entrenar el modelo
                 * y guarda el modelo entrenado en la variable model*/
                ITransformer model = est.Fit(traindata);

                /* Guardar el modelo para aligerar la ejecucion */
                mlContext.Model.Save(model, traindata.Schema, ModelPath);
                Console.WriteLine("Archivo Model Generado....---");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error durante el entrenamiento del modelo: " + ex.Message);
            }
        }

        /* 1 - Generar Archivo */
        public void generarArchivoTrainigDB()
        {

            // Obtiene la lista de productos-coproductos del historia-productos
            /**********************************************/
            var historialProdCoprod = _contextBD.ProductSalesHistories.ToList();

            // Construye la cadena de texto con las etiquetas
            var text = "ProductID\tProductID_Copurchased" + Environment.NewLine;
            text += string.Join(Environment.NewLine, historialProdCoprod.Select(r => $"{r.IdProducto}\t{r.IdCoproducto}"));

            // Guarda la cadena de texto en un archivo
            string filePath = DataLocationRelative;
            File.WriteAllText(filePath, string.Empty); // Borra el contenido del archivo
            File.WriteAllText(filePath, text);

            /**********************Fin SQL************************/
        }


        public static string GetAbsolutePath(string relativeDatasetPath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativeDatasetPath);

            return fullPath;
        }

        // Genero el path absoluto desde el relativo del proyecto leer el archivo trainig VIA AMAZON
        private static readonly string BaseDataSetRelativePath = @"../../../DATA";
        private static string TrainingDataRelativePath = $"{BaseDataSetRelativePath}/Amazon0302.txt";
        private static string TrainingDataLocationRelative = GetAbsolutePath(TrainingDataRelativePath);

        // Genero el path absoluto desde el relativo del proyecto para guardar la entrada del SQL
        /****************Consumos.txt es el archivo que almacana las relaciones entre producto y coproductos*****************/
        private static string DataRelativePath = $"{BaseDataSetRelativePath}/Consumos.txt";
        private static string DataLocationRelative = GetAbsolutePath(DataRelativePath);

        private static string BaseModelRelativePath = @"../../../ModelML";
        private static string ModelRelativePath = $"{BaseModelRelativePath}/model.zip";
        private static string ModelPath = GetAbsolutePath(ModelRelativePath);
        //Generador de Logs de entrenamientos
        private static readonly string BaseLogSetRelativePath = @"../../../Logs";
        private static string LogRelativePath = $"{BaseDataSetRelativePath}/LogTrainig.txt";
        private static string LogLocationRelative = GetAbsolutePath(LogRelativePath);


        public  PredictionEngine<ProductEntry, CopurchasePrediction> consumirModelML()
        {
            MLContext mlContext = new MLContext();
            /* Cargar el modelo y su esquema de entrada desde el archivo model.zip */
            var model = mlContext.Model.Load(ModelPath, out var schema);

            /* Crea un motor de predicción */
            var predictionengine = mlContext.Model.CreatePredictionEngine<ProductEntry, CopurchasePrediction>(model);
            return predictionengine;
        }

        public float recomendarByIdProCopurId(int productID, int CoPurchaseProductID)
        {
            //Apartir de aca seria usar el modelo para crear la predicion
            //
            //Seguinda parte o metodo que debe ejecutarse al pedir las solicitudes
            PredictionEngine<ProductEntry, CopurchasePrediction> predictionengine = consumirModelML();

            var prediction = predictionengine.Predict(
                                                         new ProductEntry()
                                                         {
                                                             ProductID = (uint)productID,
                                                             CoPurchaseProductID = (uint)CoPurchaseProductID
                                                         }); ;
            return prediction.Score;

        }

        public ProductListViewModel RecommendTop5OLD(int productID)
        {
            //Apartir de aca seria usar el modelo para crear la predicion
            //
            //Seguinda parte o metodo que debe ejecutarse al pedir las solicitudes
            PredictionEngine<ProductEntry, CopurchasePrediction> predictionengine = consumirModelML();

            // find the top 5 combined products for product 
    
            var top5 = (from m in Enumerable.Range(1, 60)
                        let p = predictionengine.Predict(
                           new ProductEntry()
                           {
                               ProductID = (uint)productID,
                               CoPurchaseProductID = (uint)m
                           })
                        orderby p.Score descending
                        select (CoPurchaseProductID: m, p.Score));
            ProductListViewModel listaResultado = new ProductListViewModel();
            Product productIngr = new Product();
            productIngr = _contextBD.Products.Find(productID);
            ////Cargar con la DB
            listaResultado.product = productIngr;
            listaResultado._productsRecommendersList = new List<ProductsRecommendersViewModel>();
            //
            foreach (var t in top5)
            {
           
                        if (t.Score > 0.80 && listaResultado._productsRecommendersList.Count < 5)
                        {
                            ProductsRecommendersViewModel prodpredi = new ProductsRecommendersViewModel();
                            ////Cargar desde la DB
                            Product pr = new Product();
                            pr = _contextBD.Products.Find(t.CoPurchaseProductID);
                            //pr.Id = t.CoPurchaseProductID;
                            prodpredi.CoproductRecomend = pr;
                            ////
                            prodpredi.predictionScore = (float)Math.Round(t.Score, 2);
                            listaResultado._productsRecommendersList.Add(prodpredi);

                            //=new ProductsRecommendersViewModel(t.ProductID,t.ProductID,productID);
                            Console.WriteLine($"  Score:{t.CoPurchaseProductID}\tProduct: {t.Score}");
                        }
                        else
                            Console.WriteLine("No se encontro recomendados");
                }
                ///Returna la lista Coimpleta con el producto a comparar y sus recomendaciones cargadas con la db
                return listaResultado;
            }

            public ProductListViewModel RecommendTop5(int productID)
        {
            //Apartir de aca seria usar el modelo para crear la predicion
            //Seguinda parte o metodo que debe ejecutarse al pedir las solicitudes
            PredictionEngine<ProductEntry, CopurchasePrediction> predictionengine = consumirModelML();      

            // find the top 5 combined products for product 6
            Console.WriteLine("Calculating the top 5 products for product "+productID+"...");

            List<int> idsValidos = _contextBD.Products.Select(p => p.Id).ToList();

            // Realizar la predicción para los IDs de productos válidos

            var top = (from id in idsValidos
                       let p = predictionengine.Predict(
                          new ProductEntry()
                          {
                              ProductID = (uint)productID,
                              CoPurchaseProductID = (uint)id
                          })
                       orderby p.Score descending
                       select (CoPurchaseProductID: id, p.Score));

            ProductListViewModel listaResultado = new ProductListViewModel();
            Product productIngr = _contextBD.Products.Find(productID);
          
            ////Cargar con la DB
            listaResultado.product= productIngr;
            listaResultado._productsRecommendersList = new List<ProductsRecommendersViewModel>();
          
            //Foreach con validaciones en las busquedas
            foreach (var t in top) {
                if (t.Score > 0.80 && listaResultado._productsRecommendersList.Count < 5)
                {
                    Product recommendedProduct = _contextBD.Products.Find(t.CoPurchaseProductID);
                    if (recommendedProduct != null)
                    {
                        ///Cargar desde la DB
                        ProductsRecommendersViewModel prodpredi = new ProductsRecommendersViewModel()
                        {
                            CoproductRecomend = recommendedProduct,
                            predictionScore = (float)Math.Round(t.Score, 2)
                        };
                        ///Guardar los recomendados encontrados
                        listaResultado._productsRecommendersList.Add(prodpredi);
                        Console.WriteLine($"  Score:{t.CoPurchaseProductID}\tProduct: {t.Score}");
                    }
                    else {
                        Console.WriteLine("No se encontro el producto recomendado" + t.CoPurchaseProductID);
                    }
                }
            }
               ///Returna la lista Completa con el producto a comparar y sus recomendaciones cargadas con la db
            return listaResultado;
        }
        public void AddRowHistorical(ProductListViewModel _ProductListViewModel)
        {
            if (_ProductListViewModel._productsRecommendersList.Count > 0)
            {
                Historical historical = new Historical();
                historical.IdProductoNavigation = _contextBD.Products.FirstOrDefault(x => x.Id == _ProductListViewModel.product.Id) ;
                historical.IdCoproductoNavigation= _contextBD.Products.FirstOrDefault(x => x.Id == GetCoProductWithMaxScore(_ProductListViewModel).CoproductRecomend.Id); 
                historical.IdProducto=_ProductListViewModel.product.Id;
                historical.IdCoproducto = historical.IdCoproductoNavigation.Id;
                historical.Score = GetCoProductWithMaxScore(_ProductListViewModel).predictionScore;
                _contextBD.Historicals.Add(historical);
                _contextBD.SaveChanges();
            }
            else
                Console.WriteLine("No se obtiene lista para guardar");
        }
        public ProductsRecommendersViewModel GetCoProductWithMaxScore(ProductListViewModel ProductListViewModel)
        {
            ProductsRecommendersViewModel _CoproductWithScore = new ProductsRecommendersViewModel();
            _CoproductWithScore = ProductListViewModel._productsRecommendersList.FirstOrDefault();

            return _CoproductWithScore;
        }
        public List<Historical> GetHistorical()
        {
            // Obtiene la lista de productos desde el contexto y la devuelve            
            return _contextBD.Historicals.Include("IdCoproductoNavigation").Include("IdProductoNavigation").ToList();
        }

        public List<string> GenerarInformeDeEntrenamiento() {
            // Simulate data loading from a file
            // Here, we'll simply generate a list of timestamps
            List<string> records = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                string timestamp = DateTime.Now.ToString();
                // Guardar la fecha actual en el archivo de registro
                using (StreamWriter writer = new StreamWriter(LogLocationRelative, true))
                {
                    writer.WriteLine(timestamp);
                }


            }

            string lastRecord = ReadLastRecordFromLogFile();
            records.Add(lastRecord);
            return records;

        }
        private string ReadLastRecordFromLogFile()
        {
            if (System.IO.File.Exists(LogLocationRelative))
            {
                try
                {
                    // Leer todas las líneas del archivo
                    string[] lines = System.IO.File.ReadAllLines(LogLocationRelative);

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
