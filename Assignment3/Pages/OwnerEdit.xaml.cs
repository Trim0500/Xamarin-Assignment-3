using Assignment3.DB;
using Assignment3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Assignment3.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnerEdit : ContentPage
    {
        private OwnerDB ownerDB = new OwnerDB();
        private PetDB petDB = new PetDB();
        private Owner currentOwner { get; set; }
        private List<Pet> petList = new List<Pet>();
        private string userKey = App.globalUser.key;

        public OwnerEdit()
        {
            InitializeComponent();

            GetOwner();
        }

        private async void GetOwner()
        {
            List<Owner> owners = await ownerDB.ReadAll();

            foreach(Owner temp in owners)
            {
                if(temp.userId == App.globalUser.key)
                {
                    currentOwner = temp;
                }
            }

            if(currentOwner == null)
            {
                await DisplayAlert("Owner Not Found", "The Owner could not be retirieved from the database", "OK");
                return;
            }
            else
            {
                txtOwnerKey.Text = currentOwner.key;
                txtFirstName.Text = currentOwner.firstName;
                txtLastName.Text = currentOwner.lastName;

                List<Pet> allPets = await petDB.ReadAll();
                
                foreach(Pet temp in allPets)
                {
                    if(temp.ownerId == currentOwner.key)
                    {
                        petList.Add(temp);
                    }
                }
                
                if(petList == null)
                {
                    await DisplayAlert("Pets Not Found", "The pets for the owner could not be retirieved from the database", "OK");
                    return;
                }
                else
                {
                    foreach(Pet temp in petList)
                    {
                        if(img_pet_1.Source == null)
                        {
                            switch (temp.type)
                            {
                                case "Dog":
                                    img_pet_1.Source = "dog";
                                break;
                                case "Cat":
                                    img_pet_1.Source = "cat";
                                    break;
                            }
                        }
                        else if(img_pet_2.Source == null && petList.Count == 2)
                        {
                            switch (temp.type)
                            {
                                case "Dog":
                                    img_pet_2.Source = "dog";
                                    break;
                                case "Cat":
                                    img_pet_2.Source = "cat";
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private async void pet1_tap_Tapped(object sender, EventArgs e)
        {
            var currentPet = petList[0];

            if (currentPet == null)
            {
                await DisplayAlert("Warning", "Pet was not found", "OK");
            }
            else
            {
                await Navigation.PushModalAsync(new PetRegistration(currentPet));
                OnAppearing();
            }
        }

        private async void pet2_tap_Tapped(object sender, EventArgs e)
        {
            var currentPet = petList[1];

            if (currentPet == null)
            {
                await DisplayAlert("Warning", "Pet was not found", "OK");
            }
            else
            {
                await Navigation.PushModalAsync(new PetRegistration(currentPet));
                OnAppearing();
            }
        }

        private async void add_toolbar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PetRegistration(currentOwner.key));
        }

        private async void update_info_btn_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLastName.Text) || string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                await DisplayActionSheet("Error", "Cancel", null, "Missing Fields, Please fill out properly.");
                return;
            }
            else
            {
                Owner owner = new Owner();

                owner.key = txtOwnerKey.Text;
                owner.userId = userKey;
                owner.firstName = txtFirstName.Text;
                owner.lastName = txtLastName.Text;

                var isUpdated = await ownerDB.Update(owner);

                if (isUpdated)
                {
                    await DisplayAlert("Success", "Saved", "Cancel");
                }
                else
                {
                    await DisplayAlert("Failed", "Did not save", "Cancel");
                }

                currentOwner = owner;
            }
        }

        private async void btnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}