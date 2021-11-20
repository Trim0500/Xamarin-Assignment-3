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
    public class OwnerDB
    {
        FirebaseClient client = new FirebaseClient(
            "https://xamarin-assignment-3-owner-default-rtdb.firebaseio.com/"
        );

        public async Task<bool> Create(Owner owner)
        {
            var data = await client.Child(nameof(Owner)).PostAsync(JsonConvert.SerializeObject(owner));

            if (!String.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Owner>> ReadAll()
        {
            return (await client.Child(nameof(Owner)).OnceAsync<Owner>()).Select(item => new Owner
            {
                key = item.Key,
                ownerId = item.Object.ownerId,
                firstName = item.Object.firstName,
                lastName = item.Object.lastName,
                pet1 = item.Object.pet1,
                pet2 = item.Object.pet2
            }).ToList();
        }

        public async Task<Owner> ReadById(string key)
        {
            return await client.Child(nameof(Owner) + "/" + key).OnceSingleAsync<Owner>();
        }

        public async Task<bool> Update(Owner owner)
        {

            await client.Child(nameof(Owner) + "/" + owner.key).PutAsync(JsonConvert.SerializeObject(owner));

            return true;

        }

        public async Task<bool> Delete(string key)
        {
            await client.Child(nameof(Owner) + "/" + key).DeleteAsync();
            return true;
        }
    }
}
