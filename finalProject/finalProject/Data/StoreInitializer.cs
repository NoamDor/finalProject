using finalProject.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using WebGrease.Css.Extensions;
using static finalProject.Enums.Enums;

namespace finalProject.Data
{
    public class StoreInitializer
    {
        public static void Initialize(StoreContext context)
        {
            context.Users.RemoveRange(context.Users);
            context.Suppliers.RemoveRange(context.Suppliers);
            context.ProductTypes.RemoveRange(context.ProductTypes);
            context.Products.RemoveRange(context.Products);
            context.Branches.RemoveRange(context.Branches);
            context.Purchases.RemoveRange(context.Purchases);
            context.SaveChanges();

            var users = new List<User>
                {
                    new User{Username="admin",Password="admin",Address="some admin adsress",IsAdmin=true,Gender=Gender.Female, BirthDate=new DateTime(1998, 8, 18), Telephone="0549989164"},
                    new User{Username="user1",Password="user1",Address="some user adsress",IsAdmin=false,Gender=Gender.Female, BirthDate=new DateTime(1997, 7, 13), Telephone="0542354576"},
                    new User{Username="user2",Password="user2",Address="some user adsress",IsAdmin=false,Gender=Gender.Female, BirthDate=new DateTime(2005, 12, 24), Telephone="0524579285"},
                    new User{Username="user3",Password="user3",Address="some user adsress",IsAdmin=false,Gender=Gender.Female, BirthDate=new DateTime(1984, 11, 30), Telephone="0509893456"},
                    new User{Username="user4",Password="user4",Address="some user adsress",IsAdmin=false,Gender=Gender.Female, BirthDate=new DateTime(1967, 5, 12), Telephone="0525679385"},
                    new User{Username="user5",Password="user5",Address="some user adsress",IsAdmin=false,Gender=Gender.Female, BirthDate=new DateTime(1982, 8, 4), Telephone="0549794835"},
                    new User{Username="user6",Password="user6",Address="some user adsress",IsAdmin=false,Gender=Gender.Female, BirthDate=new DateTime(1978, 7, 7), Telephone="0502184756"},
                    new User{Username="user7",Password="user7",Address="some user adsress",IsAdmin=false,Gender=Gender.Female, BirthDate=new DateTime(2000, 1, 1), Telephone="0522248675"},
                    new User{Username="user8",Password="user8",Address="some user adsress",IsAdmin=false,Gender=Gender.Male, BirthDate=new DateTime(1999, 3, 10), Telephone="0549989892"},
                    new User{Username="user9",Password="user9",Address="some user adsress",IsAdmin=false,Gender=Gender.Male, BirthDate=new DateTime(2002, 5, 13), Telephone="0509823544"},
                    new User{Username="user10",Password="user10",Address="some user adsress",IsAdmin=false,Gender=Gender.Male, BirthDate=new DateTime(1998, 2, 22), Telephone="0549984394"},
                    new User{Username="user11",Password="user11",Address="some user adsress",IsAdmin=false,Gender=Gender.Male, BirthDate=new DateTime(1990, 4, 18), Telephone="0528457277"},
                    new User{Username="user12",Password="user12",Address="some user adsress",IsAdmin=false,Gender=Gender.Male, BirthDate=new DateTime(1987, 10, 13), Telephone="0544988769"},
                    new User{Username="user13",Password="user13",Address="some user adsress",IsAdmin=false,Gender=Gender.Male, BirthDate=new DateTime(1983, 4, 2), Telephone="0503433456"},
                    new User{Username="user14",Password="user14",Address="some user adsress",IsAdmin=false,Gender=Gender.Male, BirthDate=new DateTime(2003, 7, 9), Telephone="0549898748"},
                    new User{Username="user15",Password="user15",Address="some user adsress",IsAdmin=false,Gender=Gender.Male, BirthDate=new DateTime(1997, 8, 15), Telephone="0522345345"}
                };

            var branches = new List<Branch>
            {
                new Branch{Lat=32.176,Long=34.894,City="כפר סבא",Address="כתובת בכפר סבא",Telephone="0586417813"},
                new Branch{Lat=32.08,Long=34.77,City="תל אביב",Address="כתובת בתל אביב",Telephone="0586417813"},
                new Branch{Lat=32.017,Long=34.78,City="חולון",Address="כתובת בחולון",Telephone="0586417813"},
                new Branch{Lat=32.015,Long=34.753,City="בת ים",Address="כתובת בבת ים",Telephone="0586417813"}
            };

            var suppliers = new List<Supplier>
                {
                    new Supplier{Name="Adidas", PictureName="adidas-logo.jpg"},
                    new Supplier{Name="Nike", PictureName="nike-logo.jpg"},
                    new Supplier{Name="Puma", PictureName="puma-logo.jpg"},
                    new Supplier{Name="Asics", PictureName="asics-logo.jpg"}
            };


            var productTypes = new List<ProductType>
            {
                new ProductType{Gender=Gender.Male,Name="חולצת ספורט גבר"},
                new ProductType{Gender=Gender.Male,Name="מכנס ספורט גבר"},
                new ProductType{Gender=Gender.Male,Name="נעלי ספורט גבר"},
                new ProductType{Gender=Gender.Female,Name="חולצת ספורט אישה"},
                new ProductType{Gender=Gender.Female,Name="מכנס ספורט אישה"},
                new ProductType{Gender=Gender.Female,Name="נעלי ספורט אישה"}
            };

            var products = new List<Product>
            {
                new Product{Name="חולצה גבר 1",PictureName="MenShirt1.jpg",Price=75,ProductType=productTypes[0],Size=44,Supplier=suppliers[0]},
                new Product{Name="חולצה גבר 1",PictureName="MenShirt1.jpg",Price=75,ProductType=productTypes[0],Size=45,Supplier=suppliers[0]},
                new Product{Name="חולצה גבר 1",PictureName="MenShirt1.jpg",Price=75,ProductType=productTypes[0],Size=46,Supplier=suppliers[0]},
                new Product{Name="חולצה גבר 1",PictureName="MenShirt1.jpg",Price=75,ProductType=productTypes[0],Size=47,Supplier=suppliers[0]},

                new Product{Name="חולצה גבר 2",PictureName="MenShirt2.jpg",Price=89,ProductType=productTypes[0],Size=44,Supplier=suppliers[1]},
                new Product{Name="חולצה גבר 2",PictureName="MenShirt2.jpg",Price=89,ProductType=productTypes[0],Size=45,Supplier=suppliers[1]},
                new Product{Name="חולצה גבר 2",PictureName="MenShirt2.jpg",Price=89,ProductType=productTypes[0],Size=46,Supplier=suppliers[1]},
                new Product{Name="חולצה גבר 2",PictureName="MenShirt2.jpg",Price=89,ProductType=productTypes[0],Size=47,Supplier=suppliers[1]},

                new Product{Name="מכנס גבר 1",PictureName="MenPants1.jpg",Price=119,ProductType=productTypes[1],Size=42,Supplier=suppliers[3]},
                new Product{Name="מכנס גבר 1",PictureName="MenPants1.jpg",Price=119,ProductType=productTypes[1],Size=43,Supplier=suppliers[3]},
                new Product{Name="מכנס גבר 1",PictureName="MenPants1.jpg",Price=119,ProductType=productTypes[1],Size=44,Supplier=suppliers[3]},
                new Product{Name="מכנס גבר 1",PictureName="MenPants1.jpg",Price=119,ProductType=productTypes[1],Size=45,Supplier=suppliers[3]},

                new Product{Name="מכנס גבר 2",PictureName="MenPants2.jpg",Price=99,ProductType=productTypes[1],Size=42,Supplier=suppliers[2]},
                new Product{Name="מכנס גבר 2",PictureName="MenPants2.jpg",Price=99,ProductType=productTypes[1],Size=43,Supplier=suppliers[2]},
                new Product{Name="מכנס גבר 2",PictureName="MenPants2.jpg",Price=99,ProductType=productTypes[1],Size=44,Supplier=suppliers[2]},
                new Product{Name="מכנס גבר 2",PictureName="MenPants2.jpg",Price=99,ProductType=productTypes[1],Size=45,Supplier=suppliers[2]},

                new Product{Name="נעליים גבר 1",PictureName="MenShoes1.jpg",Price=199,ProductType=productTypes[2],Size=43,Supplier=suppliers[3]},
                new Product{Name="נעליים גבר 1",PictureName="MenShoes1.jpg",Price=199,ProductType=productTypes[2],Size=44,Supplier=suppliers[3]},
                new Product{Name="נעליים גבר 1",PictureName="MenShoes1.jpg",Price=199,ProductType=productTypes[2],Size=45,Supplier=suppliers[3]},
                new Product{Name="נעליים גבר 1",PictureName="MenShoes1.jpg",Price=199,ProductType=productTypes[2],Size=46,Supplier=suppliers[3]},

                new Product{Name="נעליים גבר 2",PictureName="MenShoes2.jpg",Price=229,ProductType=productTypes[2],Size=43,Supplier=suppliers[2]},
                new Product{Name="נעליים גבר 2",PictureName="MenShoes2.jpg",Price=229,ProductType=productTypes[2],Size=44,Supplier=suppliers[2]},
                new Product{Name="נעליים גבר 2",PictureName="MenShoes2.jpg",Price=229,ProductType=productTypes[2],Size=45,Supplier=suppliers[2]},
                new Product{Name="נעליים גבר 2",PictureName="MenShoes2.jpg",Price=229,ProductType=productTypes[2],Size=46,Supplier=suppliers[2]},

                new Product{Name="חולצה אישה 1",PictureName="WomenShirt1.jpg",Price=85,ProductType=productTypes[3],Size=36,Supplier=suppliers[1]},
                new Product{Name="חולצה אישה 1",PictureName="WomenShirt1.jpg",Price=85,ProductType=productTypes[3],Size=37,Supplier=suppliers[1]},
                new Product{Name="חולצה אישה 1",PictureName="WomenShirt1.jpg",Price=85,ProductType=productTypes[3],Size=38,Supplier=suppliers[1]},
                new Product{Name="חולצה אישה 1",PictureName="WomenShirt1.jpg",Price=85,ProductType=productTypes[3],Size=39,Supplier=suppliers[1]},

                new Product{Name="חולצה אישה 2",PictureName="WomenShirt2.jpg",Price=109,ProductType=productTypes[3],Size=36,Supplier=suppliers[0]},
                new Product{Name="חולצה אישה 2",PictureName="WomenShirt2.jpg",Price=109,ProductType=productTypes[3],Size=37,Supplier=suppliers[0]},
                new Product{Name="חולצה אישה 2",PictureName="WomenShirt2.jpg",Price=109,ProductType=productTypes[3],Size=38,Supplier=suppliers[0]},
                new Product{Name="חולצה אישה 2",PictureName="WomenShirt2.jpg",Price=109,ProductType=productTypes[3],Size=39,Supplier=suppliers[0]},

                new Product{Name="מכנס אישה 1",PictureName="WomenPants1.jpg",Price=125,ProductType=productTypes[4],Size=36,Supplier=suppliers[3]},
                new Product{Name="מכנס אישה 1",PictureName="WomenPants1.jpg",Price=125,ProductType=productTypes[4],Size=37,Supplier=suppliers[3]},
                new Product{Name="מכנס אישה 1",PictureName="WomenPants1.jpg",Price=125,ProductType=productTypes[4],Size=38,Supplier=suppliers[3]},
                new Product{Name="מכנס אישה 1",PictureName="WomenPants1.jpg",Price=125,ProductType=productTypes[4],Size=39,Supplier=suppliers[3]},

                new Product{Name="מכנס אישה 2",PictureName="WomenPants2.jpg",Price=139,ProductType=productTypes[4],Size=36,Supplier=suppliers[1]},
                new Product{Name="מכנס אישה 2",PictureName="WomenPants2.jpg",Price=139,ProductType=productTypes[4],Size=37,Supplier=suppliers[1]},
                new Product{Name="מכנס אישה 2",PictureName="WomenPants2.jpg",Price=139,ProductType=productTypes[4],Size=38,Supplier=suppliers[1]},
                new Product{Name="מכנס אישה 2",PictureName="WomenPants2.jpg",Price=139,ProductType=productTypes[4],Size=39,Supplier=suppliers[1]},

                new Product{Name="נעליים אישה 1",PictureName="WomenShoes1.jpg",Price=199,ProductType=productTypes[5],Size=37,Supplier=suppliers[2]},
                new Product{Name="נעליים אישה 1",PictureName="WomenShoes1.jpg",Price=199,ProductType=productTypes[5],Size=38,Supplier=suppliers[2]},
                new Product{Name="נעליים אישה 1",PictureName="WomenShoes1.jpg",Price=199,ProductType=productTypes[5],Size=39,Supplier=suppliers[2]},
                new Product{Name="נעליים אישה 1",PictureName="WomenShoes1.jpg",Price=199,ProductType=productTypes[5],Size=40,Supplier=suppliers[2]},

                new Product{Name="נעליים אישה 2",PictureName="WomenShoes2.jpg",Price=259,ProductType=productTypes[5],Size=37,Supplier=suppliers[1]},
                new Product{Name="נעליים אישה 2",PictureName="WomenShoes2.jpg",Price=259,ProductType=productTypes[5],Size=38,Supplier=suppliers[1]},
                new Product{Name="נעליים אישה 2",PictureName="WomenShoes2.jpg",Price=259,ProductType=productTypes[5],Size=39,Supplier=suppliers[1]},
                new Product{Name="נעליים אישה 2",PictureName="WomenShoes2.jpg",Price=259,ProductType=productTypes[5],Size=40,Supplier=suppliers[1]},
            };

            var purchases = new List<Purchase>
            {
                new Purchase{Count=3,Date=new DateTime(2020,9,22),Product=products[0],Branch=branches[0],User=users[0]},
                new Purchase{Count=3,Date=new DateTime(2020,9,22),Product=products[1],Branch=branches[1],User=users[0]},
                new Purchase{Count=3,Date=new DateTime(2020,9,22),Product=products[20],Branch=branches[2],User=users[0]},
                new Purchase{Count=3,Date=new DateTime(2020,9,22),Product=products[21],Branch=branches[3],User=users[0]},
                new Purchase{Count=3,Date=new DateTime(2020,9,22),Product=products[40],Branch=branches[2],User=users[0]},
                new Purchase{Count=3,Date=new DateTime(2020,9,22),Product=products[41],Branch=branches[0],User=users[0]},

                new Purchase{Count=1,Date=new DateTime(2020,11,22),Product=products[8],Branch=branches[0],User=users[1]},
                new Purchase{Count=2,Date=new DateTime(2020,8,23),Product=products[9],Branch=branches[1],User=users[1]},
                new Purchase{Count=5,Date=new DateTime(2020,11,25),Product=products[10],Branch=branches[2],User=users[1]},
                new Purchase{Count=3,Date=new DateTime(2020,7,12),Product=products[12],Branch=branches[3],User=users[1]},
                new Purchase{Count=8,Date=new DateTime(2020,4,5),Product=products[13],Branch=branches[2],User=users[1]},
                new Purchase{Count=3,Date=new DateTime(2020,5,15),Product=products[14],Branch=branches[0],User=users[1]},
            };

            users.ForEach(user => context.Users.Add(user));
            context.SaveChanges();

            branches.ForEach(branch => context.Branches.Add(branch));
            context.SaveChanges();

            suppliers.ForEach(supplier => context.Suppliers.Add(supplier));
            context.SaveChanges();

            productTypes.ForEach(productType => context.ProductTypes.Add(productType));
            context.SaveChanges();

            products.ForEach(product => context.Products.Add(product));
            context.SaveChanges();

            purchases.ForEach(purchase => context.Purchases.Add(purchase));
            context.SaveChanges();
        }
    }
}