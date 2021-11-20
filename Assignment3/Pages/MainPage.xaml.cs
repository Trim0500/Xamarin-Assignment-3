using Assignment3.DB;
using Assignment3.Models;
using Assignment3.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Assignment3
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
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

        async void btnUserList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserList());
        }
    }
}
