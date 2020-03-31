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
                    new User{Username="user1",Password="user1",Address="some user adsress",IsAdmin=false,Gender=Gender.Female, BirthDate=new DateTime(1998, 8, 18), Telephone="0549989164"},
                    new User{Username="user2",Password="user2",Address="some user adsress",IsAdmin=false,Gender=Gender.Female, BirthDate=new DateTime(1998, 8, 18), Telephone="0549989164"},
                    new User{Username="user3",Password="user3",Address="some user adsress",IsAdmin=false,Gender=Gender.Female, BirthDate=new DateTime(1998, 8, 18), Telephone="0549989164"},
                    new User{Username="user4",Password="user4",Address="some user adsress",IsAdmin=false,Gender=Gender.Female, BirthDate=new DateTime(1998, 8, 18), Telephone="0549989164"},
                    new User{Username="user5",Password="user5",Address="some user adsress",IsAdmin=false,Gender=Gender.Female, BirthDate=new DateTime(1998, 8, 18), Telephone="0549989164"},
                    new User{Username="user6",Password="user6",Address="some user adsress",IsAdmin=false,Gender=Gender.Female, BirthDate=new DateTime(1998, 8, 18), Telephone="0549989164"},
                    new User{Username="user7",Password="user7",Address="some user adsress",IsAdmin=false,Gender=Gender.Female, BirthDate=new DateTime(1998, 8, 18), Telephone="0549989164"},
                    new User{Username="user8",Password="user8",Address="some user adsress",IsAdmin=false,Gender=Gender.Male, BirthDate=new DateTime(1998, 8, 18), Telephone="0549989164"},
                    new User{Username="user9",Password="user9",Address="some user adsress",IsAdmin=false,Gender=Gender.Male, BirthDate=new DateTime(1998, 8, 18), Telephone="0549989164"},
                    new User{Username="user10",Password="user10",Address="some user adsress",IsAdmin=false,Gender=Gender.Male, BirthDate=new DateTime(1998, 8, 18), Telephone="0549989164"},
                    new User{Username="user11",Password="user11",Address="some user adsress",IsAdmin=false,Gender=Gender.Male, BirthDate=new DateTime(1998, 8, 18), Telephone="0549989164"},
                    new User{Username="user12",Password="user12",Address="some user adsress",IsAdmin=false,Gender=Gender.Male, BirthDate=new DateTime(1998, 8, 18), Telephone="0549989164"},
                    new User{Username="user13",Password="user13",Address="some user adsress",IsAdmin=false,Gender=Gender.Male, BirthDate=new DateTime(1998, 8, 18), Telephone="0549989164"},
                    new User{Username="user14",Password="user14",Address="some user adsress",IsAdmin=false,Gender=Gender.Male, BirthDate=new DateTime(1998, 8, 18), Telephone="0549989164"},
                    new User{Username="user15",Password="user15",Address="some user adsress",IsAdmin=false,Gender=Gender.Male, BirthDate=new DateTime(1998, 8, 18), Telephone="0549989164"}
                };

            var pt = new ProductType { Gender = Gender.Male, Name = "מכנס גבר" };

            context.ProductTypes.Add(pt);
            context.SaveChanges();

            //Look for any Users
            if (!context.Users.Any())
                {
                    foreach (User u in users)
                    {
                        context.Users.Add(u);
                    }

                    context.SaveChanges();
                }
        }
    }
}