using Assignment3.DB;
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
    public partial class VetRegistration : ContentPage
    {
        private VetDB vetDB = new VetDB();

        public VetRegistration()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var vets = await vetDB.ReadAll();

            vet_list_view.ItemsSource = vets;
        }

        private async void edit_tap_Tapped(object sender, EventArgs e)
        {
            string key = ((TappedEventArgs)e).Parameter.ToString();

            await DisplayAlert("Show ID", key, "OK");

            var currentVet = await vetDB.ReadById(key);

            if (currentVet == null)
            {
                await DisplayAlert("Warning", "Vet was not found", "OK");
            }
            else
            {
                currentVet.key = key;

                await Navigation.PushModalAsync(new VetEntry(currentVet));
                OnAppearing();
            }
        }

        private async void delete_tap_Tapped(object sender, EventArgs e)
        {
            string key = ((TappedEventArgs)e).Parameter.ToString();

            await DisplayAlert("Show ID", key, "OK");

            if (key == null)
            {
                await DisplayAlert("Warning", "Vet was not found", "OK");
            }
            else
            {
                var isDeleted = await vetDB.Delete(key);

                if(isDeleted)
                {
                    await DisplayAlert("Deletion Successful", "Vet was deleted!", "OK");
                }
                else
                {
                    await DisplayAlert("Deletion Failed", "Vet could not be deleted", "OK");
                }
            }
        }

        private void add_toolbar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new VetEntry());
        }
    }
}