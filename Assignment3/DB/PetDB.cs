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
    public class PetDB
    {
        FirebaseClient client = new FirebaseClient(
            "https://xamarin-assignment-3-pet-default-rtdb.firebaseio.com/"
        );

        public async Task<bool> Create(Pet pet)
        {
            var data = await client.Child(nameof(Pet)).PostAsync(JsonConvert.SerializeObject(pet));

            if (!String.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Pet>> ReadAll()
        {
            return (await client.Child(nameof(Pet)).OnceAsync<Pet>()).Select(item => new Pet
            {
                key = item.Key,
                ownerId = item.Object.ownerId,
                name = item.Object.name,
                birthDate = item.Object.birthDate,
                type = item.Object.type
            }).ToList();
        }

        public async Task<Pet> ReadById(string key)
        {
            return (await client.Child(nameof(Pet) + "/" + key).OnceSingleAsync<Pet>());
        }

        public async Task<List<Pet>> ReadByOwnerKey(string ownerKey)
        {
            return (await client.Child(nameof(Pet)).OnceAsync<Pet>()).Select(item => new Pet
            {
                key = item.Key,
                ownerId = item.Object.ownerId,
                name = item.Object.name,
                birthDate = item.Object.birthDate,
                type = item.Object.type
            }).Where(p => p.ownerId == ownerKey).ToList<Pet>();
        }

        public async Task<bool> Update(Pet pet)
        {

            await client.Child(nameof(Pet) + "/" + pet.key).PutAsync(JsonConvert.SerializeObject(pet));

            return true;

        }

        public async Task<bool> Delete(string key)
        {
            await client.Child(nameof(Pet) + "/" + key).DeleteAsync();
            return true;
        }
    }
}
