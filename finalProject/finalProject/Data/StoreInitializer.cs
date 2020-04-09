using finalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static finalProject.Enums.Enums;

namespace finalProject.Data
{
    public class StoreInitializer
    {
        public static void Initialize(StoreContext context)
        {
            var users = new User[]
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

            var suppliers = new Supplier[]
                {
                    new Supplier{Name="Adidas"},
                    new Supplier{Name="Nike"},
                    new Supplier{Name="Puma"},
                };

            var branches = new Branch[]
                {
                    new Branch{Lat=5, Long=5, City="באר שבע", Address="שלדג 3", Telephone="0502516789"},
                    new Branch{Lat=5, Long=5, City="תל אביב", Address="המייסדים 3", Telephone="0502516788"},
                    new Branch{Lat=5, Long=5, City="חיפה", Address="פרחוני 8", Telephone="0502516799"}
                };

            var productTypes = new ProductType[]
            {
                new ProductType{Gender=Gender.Male,Name="חולצת ספורט גבר"},
                new ProductType{Gender=Gender.Female,Name="מכנס ספורט אישה"},
                new ProductType{Gender=Gender.Male,Name="מכנס ספורט גבר"},
                new ProductType{Gender=Gender.Female,Name="חולצת ספורט אישה"},
                new ProductType{Gender=Gender.Male,Name="נעלי ספורט גבר"},
                new ProductType{Gender=Gender.Female,Name="נעלי ספורט אישה"}
            };

            if (!context.Users.Any())
            {
                foreach (User u in users)
                {
                    context.Users.Add(u);
                }

                context.SaveChanges();
            }

            if (!context.Suppliers.Any())
            {
                foreach (Supplier s in suppliers)
                {
                    context.Suppliers.Add(s);
                }
                context.SaveChanges();
            }

            if (!context.ProductTypes.Any())
            {
                foreach (ProductType u in productTypes)
                {
                    context.ProductTypes.Add(u);
                }
                context.SaveChanges();
            }
        }
    }
}