using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using System.Reflection;
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

        public void generarArchivoTrainigDB() {

            // Obtiene la lista de productos-coproductos del historia-productos
            /**********************************************/

            var historialProdCoprod = _contextBD.ProductSalesHistories.ToList();

            // Construye la cadena de texto con las etiquetas

            var text = "ProductID\tProductID_Copurchased" + Environment.NewLine;
            text += string.Join(Environment.NewLine, historialProdCoprod.Select(r => $"{r.IdProducto}\t{r.IdCoproducto}"));

            // Guarda la cadena de texto en un archivo
            string filePath = DataLocationRelative;
            File.WriteAllText(filePath, text);

            /**********************Fin SQL************************/
        }
        public void trainigModelML() {

            try
            {
                // Código de generador en base al Historial en la db
                generarArchivoTrainigDB();
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

                //STEP 3: Your data is already encoded so all you need to do is specify options for MatrxiFactorizationTrainer with a few extra hyperparameters
                //        LossFunction, Alpa, Lambda and a few others like K and C as shown below and call the trainer.
                MatrixFactorizationTrainer.Options options = new MatrixFactorizationTrainer.Options();
                options.MatrixColumnIndexColumnName = nameof(ProductEntry.ProductID);
                options.MatrixRowIndexColumnName = nameof(ProductEntry.CoPurchaseProductID);
                options.LabelColumnName = "Label";
                options.LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass;
                //options.Alpha = 0.01;
                //options.Lambda = 0.025;
                // For better results use the following parameters
                //options.K = 100;
                //options.C = 0.00001;

                //Step 4: Call the MatrixFactorization trainer by passing options.
                var est = mlContext.Recommendation().Trainers.MatrixFactorization(options);

                //STEP 5: Train the model fitting to the DataSet
                //Please add Amazon0302.txt dataset from https://snap.stanford.edu/data/amazon0302.html to Data folder if FileNotFoundException is thrown.
                ITransformer model = est.Fit(traindata);

                //STEP EXTRA:Guardar el modelo para aligerar la ejecucion
                mlContext.Model.Save(model, traindata.Schema, ModelPath);
                Console.WriteLine("Archivo Model Generado....---");
                //En esta parte termina el Trainig
                //Apartir de aca seria usar el modelo para crear la predicion
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error durante el entrenamiento del modelo: " + ex.Message);
            }
        }

        public  PredictionEngine<ProductEntry, CopurchasePrediction> consumirModelML()
        {
            MLContext mlContext = new MLContext();
            var model = mlContext.Model.Load(ModelPath, out var schema);

            //STEP 6: Create prediction engine and predict the score for Product 63 being co-purchased with Product 3.
            //        The higher the score the higher the probability for this particular productID being co-purchased

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

        public ProductListViewModel RecommendTop5(int productID)
        {
            //Apartir de aca seria usar el modelo para crear la predicion
            //
            //Seguinda parte o metodo que debe ejecutarse al pedir las solicitudes
            PredictionEngine<ProductEntry, CopurchasePrediction> predictionengine = consumirModelML();
    
            // find the top 5 combined products for product 6
            Console.WriteLine("Calculating the top 5 products for product 3...");
         
            var top5 = (from m in Enumerable.Range(1, 50)
                        let p = predictionengine.Predict(
                           new ProductEntry()
                           {
                               ProductID = (uint)productID,
                               CoPurchaseProductID = (uint)m
                           })
                        orderby p.Score descending
                        select (CoPurchaseProductID: m, p.Score)).Take(5);
            ProductListViewModel listaResultado = new ProductListViewModel();
            Product productIngr = new Product();
            productIngr = _contextBD.Products.Find(productID);
            //productIngr.Id = productID;
            //productIngr.Nombre ="TV";
            ////Cargar con la DB
            listaResultado.product= productIngr;
            listaResultado._productsRecommendersList = new List<ProductsRecommendersViewModel>();
            //
            foreach (var t in top5) {

                ProductsRecommendersViewModel prodpredi = new ProductsRecommendersViewModel();
                ////Cargar desde la DB
                Product pr = new Product();
                pr=_contextBD.Products.Find(t.CoPurchaseProductID);
                //pr.Id = t.CoPurchaseProductID;
                prodpredi.CoproductRecomend=pr;
                ////
                prodpredi.predictionScore = (float)Math.Round(t.Score, 2);
                listaResultado._productsRecommendersList.Add(prodpredi);
               
                //=new ProductsRecommendersViewModel(t.ProductID,t.ProductID,productID);
                Console.WriteLine($"  Score:{t.CoPurchaseProductID}\tProduct: {t.Score}");
            }
               ///Returna la lista Coimpleta con el producto a comparar y sus recomendaciones cargadas con la db
            return listaResultado;
        }
    }
}
