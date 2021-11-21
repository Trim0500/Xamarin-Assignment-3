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
    public partial class PetRegistration : ContentPage
    {
        private string ownerKey { get; set; }
        private PetDB petDB = new PetDB();

        public PetRegistration()
        {
            InitializeComponent();
        }

        public PetRegistration(Pet pet)
        {
            InitializeComponent();
            pet_id_edit.Text = pet.key;
            name_edit.Text = pet.name;
            type_edit.Text = pet.type;
            birth_date_picker.Date = pet.birthDate;
        }

        public PetRegistration(string ownerKey)
        {
            InitializeComponent();
            this.ownerKey = ownerKey;
        }

        private async void add_toolbar_Clicked(object sender, EventArgs e)
        {
            string ownerKey = this.ownerKey;
            string name = name_edit.Text;
            string type = type_edit.Text;
            DateTime birthDate = birth_date_picker.Date;

            if (String.IsNullOrEmpty(name))
            {
                await DisplayAlert("Required", "Enter name", "Cancel");
            }
            if (String.IsNullOrEmpty(type))
            {
                await DisplayAlert("Required", "Enter type", "Cancel");
            }
            if (birthDate == null)
            {
                await DisplayAlert("Required", "Enter birth date", "Cancel");
            }

            Pet newPet = new Pet();

            newPet.ownerId = ownerKey;
            newPet.name = name;
            newPet.type = type;
            newPet.birthDate = birthDate;

            var isUpdated = await petDB.Create(newPet);

            if (isUpdated)
            {
                await DisplayAlert("Success", "Saved", "Cancel");
            }
            else
            {
                await DisplayAlert("Failed", "Did not save", "Cancel");
            }
        }

        private async void edit_toolbar_Clicked(object sender, EventArgs e)
        {
            string key = pet_id_edit.Text;
            string name = name_edit.Text;
            string type = type_edit.Text;
            DateTime birthDate = birth_date_picker.Date;

            if (String.IsNullOrEmpty(key))
            {
                await DisplayAlert("Required", "Enter key", "Cancel");
            }
            if (String.IsNullOrEmpty(name))
            {
                await DisplayAlert("Required", "Enter name", "Cancel");
            }
            if (String.IsNullOrEmpty(type))
            {
                await DisplayAlert("Required", "Enter type", "Cancel");
            }
            if (birthDate == null)
            {
                await DisplayAlert("Required", "Enter birth date", "Cancel");
            }

            Pet newPet = new Pet();

            newPet.key = key;
            newPet.name = name;
            newPet.type = type;
            newPet.birthDate = birthDate;

            var isUpdated = await petDB.Update(newPet);

            if (isUpdated)
            {
                await DisplayAlert("Success", "Saved", "Cancel");
            }
            else
            {
                await DisplayAlert("Failed", "Did not save", "Cancel");
            }
        }

        private async void delete_toolbar_Clicked(object sender, EventArgs e)
        {
            string petKey = pet_id_edit.Text;

            var isUpdated = await petDB.Delete(petKey);

            if (isUpdated)
            {
                await DisplayAlert("Success", "Deleted", "Cancel");
            }
            else
            {
                await DisplayAlert("Failed", "Did not delete", "Cancel");
            }
        }

        private async void back_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}