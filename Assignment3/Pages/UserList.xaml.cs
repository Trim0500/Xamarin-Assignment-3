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
    public partial class UserList : ContentPage
    {
        private UserDB userDB = new UserDB();
        private VetDB vetDB = new VetDB();
        private OwnerDB ownerDB = new OwnerDB();

        public UserList()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            List<User> users = await userDB.ReadAll();

            user_list_collectionView.ItemsSource = users;
        }

        async void OnButtonClicked(object sender, EventArgs e)
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
                if (viewer_rad_btn.IsChecked)
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
                }
                else if (vet_rad_btn.IsChecked)
                {
                    User newUser = new User();

                    newUser.userName = user_edit.Text;
                    newUser.password = pass_edit.Text;
                    newUser.firstName = fName_edit.Text;
                    newUser.lastName = lName_edit.Text;
                    newUser.role = "vet";
                    newUser.email = email_edit.Text;
                    newUser.phoneNum = phone_edit.Text;

                    var isSaved = await userDB.Create(newUser);

                    if (!isSaved)
                    {
                        await DisplayAlert("Failed", "Did not save", "Cancel");
                        return;
                    }

                    await DisplayAlert("Success", "Saved", "OK");

                    User vetUser = null;
                    List<User> userList = await userDB.ReadAll();
                    foreach (User temp in userList)
                    {
                        if (temp.key != null)
                        {
                            if (temp.userName == user_edit.Text && temp.password == pass_edit.Text)
                            {
                                vetUser = temp;
                            }
                        }
                        else
                        {
                            await DisplayAlert("Firebase Key Error", "The firebase record for this user was found to be null", "OK");
                            return;
                        }
                    }

                    await vetDB.Create(new Vet
                    {
                        vetId = vetUser.key,
                        firstName = fName_edit.Text,
                        lastName = lName_edit.Text
                    });
                }
                else if (owner_rad_btn.IsChecked)
                {
                    User newUser = new User();

                    newUser.userName = user_edit.Text;
                    newUser.password = pass_edit.Text;
                    newUser.firstName = fName_edit.Text;
                    newUser.lastName = lName_edit.Text;
                    newUser.role = "owner";
                    newUser.email = email_edit.Text;
                    newUser.phoneNum = phone_edit.Text;

                    var isSaved = await userDB.Create(newUser);

                    if (!isSaved)
                    {
                        await DisplayAlert("Failed", "Did not save", "Cancel");
                        return;
                    }

                    await DisplayAlert("Success", "Saved", "OK");

                    User ownerUser = null;
                    List<User> userList = await userDB.ReadAll();
                    foreach (User temp in userList)
                    {
                        if (temp.key != null)
                        {
                            if (temp.userName == user_edit.Text && temp.password == pass_edit.Text)
                            {
                                ownerUser = temp;
                            }
                        }
                        else
                        {
                            await DisplayAlert("Firebase Key Error", "The firebase record for this user was found to be null", "OK");
                            return;
                        }
                    }

                    await ownerDB.Create(new Owner
                    {
                        ownerId = ownerUser.key,
                        firstName = fName_edit.Text,
                        lastName = lName_edit.Text
                    });
                }
                await DisplayAlert("Register Result", "Success", "OK");
            }
        }

        async void Button_Clicked_1(object sender, EventArgs e)
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
                if (viewer_rad_btn.IsChecked)
                {
                    bool isUpdated = await userDB.Update(new User
                    {
                        key = key_edit.Text,
                        userName = user_edit.Text,
                        password = pass_edit.Text,
                        firstName = fName_edit.Text,
                        lastName = lName_edit.Text,
                        role = "viewer",
                        email = email_edit.Text,
                        phoneNum = phone_edit.Text
                    });

                    if (isUpdated)
                    {
                        await DisplayAlert("Success", "Saved", "Cancel");
                        return;
                    }
                    else
                    {
                        await DisplayAlert("Failed", "Did not save", "Cancel");
                        return;
                    }
                }
                else if (vet_rad_btn.IsChecked)
                {
                    bool isUpdated = await userDB.Update(new User
                    {
                        key = key_edit.Text,
                        userName = user_edit.Text,
                        password = pass_edit.Text,
                        firstName = fName_edit.Text,
                        lastName = lName_edit.Text,
                        role = "vet",
                        email = email_edit.Text,
                        phoneNum = phone_edit.Text
                    });

                    if (isUpdated)
                    {
                        await DisplayAlert("Success", "Saved", "Cancel");
                        return;
                    }
                    else
                    {
                        await DisplayAlert("Failed", "Did not save", "Cancel");
                        return;
                    }
                }
                else if (owner_rad_btn.IsChecked)
                {
                    bool isUpdated = await userDB.Update(new User
                    {
                        key = key_edit.Text,
                        userName = user_edit.Text,
                        password = pass_edit.Text,
                        firstName = fName_edit.Text,
                        lastName = lName_edit.Text,
                        role = "owner",
                        email = email_edit.Text,
                        phoneNum = phone_edit.Text
                    });

                    if (isUpdated)
                    {
                        await DisplayAlert("Success", "Saved", "Cancel");
                        return;
                    }
                    else
                    {
                        await DisplayAlert("Failed", "Did not save", "Cancel");
                        return;
                    }
                }
                await DisplayAlert("Update Result", "Success", "OK");
            }
        }

        async void Button_Clicked_2(object sender, EventArgs e)
        {
            var isDeleted = await userDB.Delete(key_edit.Text);

            if(isDeleted)
            {
                await DisplayAlert("Deletion Successful", "The user was deleted!", "OK");
                return;
            }
            else
            {
                await DisplayAlert("Deletion failed", "The user couldn't be deleted", "OK");
                return;
            }
        }

        private async void key_tapped(object sender, EventArgs e)
        {
            string key = ((TappedEventArgs)e).Parameter.ToString();

            await DisplayAlert("Show ID", key, "OK");

            User user = await userDB.ReadById(key);

            key_edit.Text = key;
            user_edit.Text = user.userName;
            pass_edit.Text = user.password;
            fName_edit.Text = user.firstName;
            lName_edit.Text = user.lastName;
            switch (user.role)
            {
                case "viewer":
                    viewer_rad_btn.IsChecked = true;
                    break;
                case "owner":
                    owner_rad_btn.IsChecked = true;
                    break;
                case "vet":
                    vet_rad_btn.IsChecked = true;
                    break;
                default:
                    break;
            }
            email_edit.Text = user.email;
            phone_edit.Text = user.phoneNum;
        }

        async void btnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}