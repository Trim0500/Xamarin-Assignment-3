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
    public partial class Registration : ContentPage
    {
        private UserDB userDB = new UserDB();

        public Registration()
        {
            InitializeComponent();
        }

        async void btnRegister_Clicked(object sender, EventArgs e)
        {
            if (pass_edit.Text == null || pass_edit.Text.Length < 10)
            {
                await DisplayAlert("Register Result", "Failure, Password should have 10 characters", "OK");
                return;
            }
            else if (string.IsNullOrWhiteSpace(user_edit.Text) || string.IsNullOrWhiteSpace(pass_edit.Text) ||
                     string.IsNullOrWhiteSpace(fName_edit.Text) || string.IsNullOrWhiteSpace(lName_edit.Text) ||
                     string.IsNullOrWhiteSpace(email_edit.Text) || string.IsNullOrWhiteSpace(phone_edit.Text))
            {
                await DisplayActionSheet("Error", "Cancel", null, "Missing Fields, Please fill out properly.");
                return;
            }
            else if (user_edit.Text == "admin" || user_edit.Text == "Admin" || user_edit.Text == "ADMIN")
            {
                await DisplayActionSheet("Error", "Cancel", null, "Forbidden, Cannot use reserved username");
                return;
            }
            else
            {
                User newUser = new User();

                newUser.userName = user_edit.Text;
                newUser.password = pass_edit.Text;
                newUser.firstName = fName_edit.Text;
                newUser.lastName = lName_edit.Text;
                newUser.role = "viewer";
                newUser.email = email_edit.Text;
                newUser.phoneNum = phone_edit.Text;

                var isSaved = await userDB.Create(newUser);

                if (!isSaved)
                {
                    await DisplayAlert("Failed", "Did not save", "Cancel");
                    return;
                }

                await DisplayAlert("Success", "Saved", "OK");

                await Navigation.PushAsync(new Login());
            }
        }
    }
}