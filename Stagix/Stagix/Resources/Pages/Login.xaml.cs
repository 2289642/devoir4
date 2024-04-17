using Stagix.Models;
using System;
using System.Collections.Generic;

namespace Stagix.Resources.Pages
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        public void OnNewButtonClicked(object sender, EventArgs args)
        {
            statusMessage.Text = "";

            App.PersonRepo.AddNewPerson(newDA.Text, newPwd.Text);
            statusMessage.Text = App.PersonRepo.StatusMessage;
        }

        public void OnGetButtonClicked(object sender, EventArgs args)
        {
            statusMessage.Text = "";

            List<Person> people = App.PersonRepo.GetAllPeople();
            peopleList.ItemsSource = people;
        }

        public void OnDeleteButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            Person personToDelete = (Person)button.CommandParameter;

            App.PersonRepo.DeletePerson(personToDelete);
            statusMessage.Text = $"Person with ID {personToDelete.Id} deleted successfully.";
        }

        public void OnUpdateButtonClicked(object sender, EventArgs args)
        {
            statusMessage.Text = "";

            try
            {
                if (string.IsNullOrEmpty(updateId.Text) || string.IsNullOrEmpty(updateNewDA.Text) || string.IsNullOrEmpty(updateNewPwd.Text))
                    throw new Exception("All fields are required for update.");

                int idToUpdate = Convert.ToInt32(updateId.Text);

                Person personToUpdate = App.PersonRepo.GetPersonById(idToUpdate);

                if (personToUpdate == null)
                {
                    statusMessage.Text = $"Person with ID {idToUpdate} not found.";
                    return;
                }

                personToUpdate.numDA = updateNewDA.Text;
                personToUpdate.pwd = updateNewPwd.Text;

                App.PersonRepo.UpdatePerson(personToUpdate);
                statusMessage.Text = $"Person with ID {idToUpdate} updated successfully.";
            }
            catch (Exception ex)
            {
                statusMessage.Text = $"Failed to update person. Error: {ex.Message}";
            }
        }
    }
}
