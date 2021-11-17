using Assignment3.Models;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3.DB
{
    public class VetDB
    {
        FirebaseClient client = new FirebaseClient(
            "https://xamarin-assignment-3-vet-default-rtdb.firebaseio.com/"
        );

        public async Task<bool> Create(Vet vet)
        {
            var data = await client.Child(nameof(Vet)).PostAsync(JsonConvert.SerializeObject(vet));

            if (!String.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Vet>> ReadAll()
        {
            return (await client.Child(nameof(Vet)).OnceAsync<Vet>()).Select(item => new Vet
            {
                key = item.Key,
                vetId = item.Object.vetId,
                firstName = item.Object.firstName,
                lastName = item.Object.lastName,
                resume = item.Object.resume,
                workdays = item.Object.workdays,
                specialties = item.Object.specialties
            }).ToList();
        }

        public async Task<Vet> ReadById(string key)
        {
            return (await client.Child(nameof(Vet) + "/" + key).OnceSingleAsync<Vet>());
        }

        public async Task<bool> Update(Vet vet)
        {

            await client.Child(nameof(Vet) + "/" + vet.key).PutAsync(JsonConvert.SerializeObject(vet));

            return true;

        }

        public async Task<bool> Delete(string key)
        {
            await client.Child(nameof(Vet) + "/" + key).DeleteAsync();
            return true;
        }
    }
}
