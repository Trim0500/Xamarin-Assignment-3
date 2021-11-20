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
    public partial class UserPage : ContentPage
    {
        User user = App.globalUser;

        public UserPage()
        {
            InitializeComponent();

            if(user.role == "viewer")
            {
                vet_list_toolbar.IsEnabled = false;
                pet_list_toolbar.IsEnabled = false;
            }
            else if(user.role == "owner")
            {
                vet_list_toolbar.IsEnabled = false;
            }
            else if (user.role == "vet")
            {
                pet_list_toolbar.IsEnabled = false;
            }
        }

        async void btnLogout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }

        async void btnPetRegistration_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PetRegistration());
        }

        async void btnVetRegistration_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VetRegistration());
        }
    }
}