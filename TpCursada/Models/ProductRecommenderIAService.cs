using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
namespace TpCursada.Models
{
    public class ProductRecommenderIAService
    {

        public float recommend(int productID)
        {

            // Especifica la ubicación real de tus datos de entrenamiento
            string trainingDataPath = "C:\\TpWeb3_Final\\TpCursada\\TpCursada\\Data\\Amazon0302.txt";

            //STEP 1: Create MLContext to be shared across the model creation workflow objects
            MLContext mlContext = new MLContext();

            //STEP 2: Read the trained data using TextLoader by defining the schema for reading the product co-purchase dataset
            //        Do remember to replace amazon0302.txt with dataset from https://snap.stanford.edu/data/amazon0302.html
            var traindata = mlContext.Data.LoadFromTextFile(path: trainingDataPath,
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
            options.Alpha = 0.01;
            options.Lambda = 0.025;
            // For better results use the following parameters
            //options.K = 100;
            //options.C = 0.00001;

            //Step 4: Call the MatrixFactorization trainer by passing options.
            var est = mlContext.Recommendation().Trainers.MatrixFactorization(options);


            //STEP 5: Train the model fitting to the DataSet
            //Please add Amazon0302.txt dataset from https://snap.stanford.edu/data/amazon0302.html to Data folder if FileNotFoundException is thrown.
            ITransformer model = est.Fit(traindata);

            //STEP 6: Create prediction engine and predict the score for Product 63 being co-purchased with Product 3.
            //        The higher the score the higher the probability for this particular productID being co-purchased

            var predictionengine = mlContext.Model.CreatePredictionEngine<ProductEntry, Copurchase_prediction>(model);
            var prediction = predictionengine.Predict(
                                                         new ProductEntry()
                                                         {
                                                             ProductID = (uint)productID,
                                                             CoPurchaseProductID = 60
                                                         });

            return prediction.Score;

        }
    }
}
