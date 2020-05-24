using finalProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Accord.MachineLearning;
using Accord.Statistics.Filters;
using GeoCoordinatePortable;

namespace finalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly StoreContext _context = new StoreContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public JsonResult NearestBranch(float lat, float lng)
        {
            var coord = new GeoCoordinate(lat, lng);
            var branches = _context.Branches.Select(x => new Models.Branch
            {
                Id = x.Id,
                Lat = x.Lat,
                Long = x.Long,
                Address = x.Address,
                City = x.City,
                Telephone = x.Telephone
            }).ToList();

            var nearestBranch = branches.OrderBy(x => new GeoCoordinate(x.Lat, x.Long).GetDistanceTo(coord))
                                   .First();
            return Json(nearestBranch);
        }

        [HttpGet]
        public JsonResult PredictPossibleProducts()
        {
            //var userIdString = HttpContext.Session.GetString("userid");
            var userIdString = "2";
            var userId = 0;
            var didParsed = Int32.TryParse(userIdString, out userId);
            int knnNum = 5;
            int clusterNum = 4;

            if (!didParsed || userId == -1)
            {
                return Json(new { errorCode = 1, errorMessage = "יוזר לא חוקי" });
            }

            var userGender = _context.Users
                .Where(x => x.Id == userId)
                .Select(x => x.Gender)
                .SingleOrDefault();

            var trainData = _context.Purchases
                .OrderBy(x => x.UserId)
                .Where(x => x.Product != null)
                .Select(x => new
                {
                    userId = x.UserId.Value,
                    size = x.Product.Size,
                    type = x.Product.ProductTypeId,
                    gender = x.Product.ProductType.Gender,
                    genderUser = x.User.Gender
                })
                .ToList();

            if (trainData.Count < knnNum || trainData.Count < clusterNum)
            {
                return Json(new { errorCode = 2, errorMessage = "אין מספיק מידע" });
            }
            var inputs = trainData.Select(x =>
            {
                double[] res = new double[]
                {
                    Convert.ToInt32(x.gender),
                    Convert.ToInt32(x.genderUser),
                    x.type.Value,
                    x.size
                };

                return res;
            })
            .ToArray();

            var codification = new Codification<double>()
            {
                CodificationVariable.Categorical,
                CodificationVariable.Categorical,
                CodificationVariable.Categorical,
                CodificationVariable.Discrete
            };

            // Learn the codification from observations
            var model = codification.Learn(inputs);

            // Transform the mixed observations into only continuous:
            double[][] newInputs = model.ToDouble().Transform(inputs);

            KMedoids kmeans = new KMedoids(k: clusterNum);
            var clusters = kmeans.Learn(newInputs);
            int[] labels = clusters.Decide(newInputs);

            var knn5 = new KNearestNeighbors(k: knnNum);

            knn5.Learn(newInputs, labels);

            var purchasesById = _context.Purchases
                .Where(x => x.Product != null)
                .Select(x => new
                {
                    userId = x.UserId.Value,
                    size = x.Product.Size,
                    type = x.Product.ProductTypeId,
                    gender = x.Product.ProductType.Gender,
                    genderUser = x.User.Gender
                })
                .GroupBy(x => x.userId)
                .ToList();

            IList<Tuple<int, int[]>> labelsForUsers = new List<Tuple<int, int[]>>();
            for (int i = 0; i < purchasesById.Count; i++)
            {
                var userInputs = purchasesById[i].
                    Select(x =>
                    {
                        double[] res = new double[]
                        {
                            Convert.ToInt32(x.gender),
                            Convert.ToInt32(x.genderUser),
                            x.type.Value,
                            x.size
                        };

                        return res;
                    })
                    .ToArray();

                double[][] newUserInputs = model.ToDouble().Transform(userInputs);
                labelsForUsers.Add(new Tuple<int, int[]>(purchasesById[i].Key, clusters.Decide(newUserInputs).Distinct().ToArray()));
            }

            var productIdsUserBought = _context.Purchases
                .Where(x => x.UserId == userId)
                .Select(x => x.ProductId)
                .Distinct()
                .ToList();

            var validProductTypeIds = _context.Purchases
                .Where(x => x.UserId == userId)
                .Select(x => x.Product.ProductTypeId)
                .Distinct()
                .ToList();

            var productsToPredict = _context.Products
                .Where(x => !productIdsUserBought.Contains(x.Id))
                .Where(x => validProductTypeIds.Contains(x.ProductTypeId))
                .Select(x => new
                {
                    id = x.Id,
                    size = x.Size,
                    type = x.ProductTypeId,
                    gender = x.ProductType.Gender,
                    genderUser = userGender
                })
                .ToList();

            var predInputs = productsToPredict.Select(x =>
            {
                double[] res = new double[]
                {
                    Convert.ToInt32(x.gender),
                    Convert.ToInt32(x.genderUser),
                    x.type.Value,
                    x.size
                };

                return res;
            })
            .ToArray();
            double[][] newPredInputs = model.ToDouble().Transform(predInputs);

            int[] newLabels = knn5.Decide(newPredInputs);

            IList<int> productIdsPrediction = new List<int>();
            var userLabels = labelsForUsers.Where(x => x.Item1 == userId).FirstOrDefault() != null ?
                labelsForUsers.Where(x => x.Item1 == userId).FirstOrDefault().Item2 : new int[0];
            for (int i = 0; i < newLabels.Length; i++)
            {
                if (userLabels.Contains(newLabels[i]))
                {
                    productIdsPrediction.Add(productsToPredict[i].id);
                }
            }

            var predictedProduct = _context.Products
                .Where(x => productIdsPrediction.Contains(x.Id))
                .Select(x => new
                {
                    id = x.Id,
                    name = x.Name,
                    price = x.Price,
                    size = x.Size,
                    picture = x.PictureName

                })
                .ToList();

            return Json(new { products = predictedProduct });
        }
    }
}