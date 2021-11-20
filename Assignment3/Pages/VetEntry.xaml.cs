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
    public partial class VetEntry : ContentPage
    {
        private VetDB vetDB = new VetDB();

        public VetEntry()
        {
            InitializeComponent();
        }

        public VetEntry(Vet vet)
        {
            InitializeComponent();
            vet_id_edit.Text = vet.key;
            fName_edit.Text = vet.firstName;
            lName_edit.Text = vet.lastName;
            resume_edit.Text = vet.resume;
            workdays_edit.Text = vet.workdays;
            specialties_edit.Text = vet.specialties;
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            string id = vet_id_edit.Text;
            string fName = fName_edit.Text;
            string lName = lName_edit.Text;
            string resume = resume_edit.Text;
            string workdays = workdays_edit.Text;
            string specialties = specialties_edit.Text;

            if (String.IsNullOrEmpty(id))
            {
                await DisplayAlert("Required", "Enter last name", "Cancel");
            }
            if (String.IsNullOrEmpty(fName))
            {
                await DisplayAlert("Required", "Enter first name", "Cancel");
            }
            if (String.IsNullOrEmpty(lName))
            {
                await DisplayAlert("Required", "Enter last name", "Cancel");
            }
            if (String.IsNullOrEmpty(resume))
            {
                await DisplayAlert("Required", "Enter email", "Cancel");
            }
            if (String.IsNullOrEmpty(workdays))
            {
                await DisplayAlert("Required", "Enter email", "Cancel");
            }
            if (String.IsNullOrEmpty(specialties))
            {
                await DisplayAlert("Required", "Enter email", "Cancel");
            }

            Vet newVet = new Vet();

            if(id != null)
            {
                newVet.key = id;
                newVet.firstName = fName;
                newVet.lastName = lName;
                newVet.resume = resume;
                newVet.workdays = workdays;
                newVet.specialties = specialties;

                var isUpdated = await vetDB.Update(newVet);

                if(isUpdated)
                { 
                    await DisplayAlert("Success", "Saved", "Cancel");
                }
                else
                {
                    await DisplayAlert("Failed", "Did not save", "Cancel");
                }

                ClearFields();
            }
            else
            {
                newVet.firstName = fName;
                newVet.lastName = lName;
                newVet.resume = resume;
                newVet.workdays = workdays;
                newVet.specialties = specialties;

                var isUpdated = await vetDB.Create(newVet);

                if (isUpdated)
                {
                    await DisplayAlert("Success", "Saved", "Cancel");
                }
                else
                {
                    await DisplayAlert("Failed", "Did not save", "Cancel");
                }

                ClearFields();
            }
        }

        async void btnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserPage());
        }

        private void ClearFields()
        {
            vet_id_edit.Text = String.Empty;
            fName_edit.Text = String.Empty;
            lName_edit.Text = String.Empty;
            resume_edit.Text = String.Empty;
            workdays_edit.Text = String.Empty;
            specialties_edit.Text = String.Empty;
        }
    }
}