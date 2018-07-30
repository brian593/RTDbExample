using Firebase.Xamarin.Database;
using RTDbExample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTDbExample.Database
{
   public class DbFirebase
    {
        FirebaseClient client;
        public DbFirebase()
        {
            client = new FirebaseClient("https://elgasuio.firebaseio.com/");
        }

        public async Task<List<Student>> getList()
        {
            var list = (await client
                .Child("Student")
                .OnceAsync<Student>())
                .Select(item =>
                        new Student
                        {
                            age = item.Object.age,
                            name = item.Object.name
                        }).ToList();


            return list;

        }
    }
}
