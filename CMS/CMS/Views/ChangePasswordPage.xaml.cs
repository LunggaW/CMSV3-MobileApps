using CMS.DataSource;
using CMS.Models;
using CMS.ViewModels;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Controls;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace CMS.Views
{
    public partial class ChangePasswordPage : ContentPage
    {
        private DSUser dsUser = new DSUser();
        private User user = new User();
        private String UserID;
        private String OldPassword;

        public ChangePasswordPage()
        {
            InitializeComponent();

           
        }

       
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            int errorcount = 0;


            OldPassword = oldPassword.Text;
            UserID = App.userLogged.userid;
            user = dsUser.Get(UserID);

            if (string.IsNullOrWhiteSpace(oldPassword.Text))
            {
                await DisplayAlert("Error", "Old Password must be inserted", "OK");
                errorcount++;
                oldPassword.PlaceholderColor = Color.Red;
                oldPassword.Focus();
            }
            if (string.IsNullOrWhiteSpace(newPassword.Text))
            {
                await DisplayAlert("Error", "New Password must be inserted", "OK");
                errorcount++;
                newPassword.PlaceholderColor = Color.Red;
                newPassword.Focus();
            }
            if (string.IsNullOrWhiteSpace(confirmPassword.Text))
            {
                await DisplayAlert("Error", "Confirm Password must be inserted", "OK");
                errorcount++;
                confirmPassword.PlaceholderColor = Color.Red;
                confirmPassword.Focus();
            }
            if (newPassword.Text != confirmPassword.Text)
            {
                await DisplayAlert("Error", "New Password and Confirm Password is not the same", "OK");
                errorcount++;
                confirmPassword.PlaceholderColor = Color.Red;
                confirmPassword.Focus();
            }
            if (user.password != oldPassword.Text)
            {
                await DisplayAlert("Error", "Current Password and Old Password is not the same", "OK");
                errorcount++;
                oldPassword.PlaceholderColor = Color.Red;
                oldPassword.Focus();
            }
            if (oldPassword.Text == newPassword.Text)
            {
                await DisplayAlert("Error", "Old Password and New Password is the same", "OK");
                errorcount++;
                newPassword.PlaceholderColor = Color.Red;
                newPassword.Focus();
            }

            //if (string.IsNullOrWhiteSpace(Nota.Text))
            //{
            //    errorcount++;
            //    Nota.PlaceholderColor = Color.Red;
            //    Nota.Focus();
            //}

            if (errorcount == 0)
            {
                
                string NewPassword = newPassword.Text;

                user.password = NewPassword;
                
                try
                {

                    bool isconnected = await CrossConnectivity.Current.IsRemoteReachable(App.hostname, App.port, 1000);
                    if (isconnected)
                    {
                        ServiceWrapper serviceWrapper = new ServiceWrapper();
                        bool syncstat = await serviceWrapper.ChangePassword(user);


                        if (syncstat)
                        {
                            dsUser.Save(user);

                            oldPassword.Text = "";
                            newPassword.Text = "";
                            confirmPassword.Text = "";

                            await DisplayAlert("Success", "Success changing Password", "OK");
                        }

                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");

                }
                
            }
        }
    }
}
