using PreAppEcommerce.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace PreAppEcommerce.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();


            this.Padding = Device.OnPlatform(
                new Thickness(10, 20, 10, 10),
                new Thickness(10),
                new Thickness(10));


            loginButton.Clicked += LoginButton_Clicked;

        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(userEntry.Text))
            {
                await DisplayAlert("Error", "You must enter an Email.", "Acept");
                userEntry.Focus();
                return;

            }

            if (string.IsNullOrEmpty(passwordEntry.Text))
            {
                await DisplayAlert("Error", "You must enter an Password.", "Acept");
                passwordEntry.Focus();
                return;
            }


            Login();


        }

        private  async void Login()
        {
            waitActivityIndicator.IsRunning = true;


            var loginReques = new LoginRequest
            {

                Email = userEntry.Text,
                Password = passwordEntry.Text
            };

            var result = string.Empty;

            try
            {

                var jsonRequest = JsonConvert.SerializeObject(loginReques);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var client = new HttpClient();

                client.BaseAddress = new Uri("http://zulu-software.com");
                var url = "/ECommerce/api/Users/Login";


                var response = await client.PostAsync(url, httpContent);

                if (!response.IsSuccessStatusCode)
                {
                    waitActivityIndicator.IsRunning = false;
                    await DisplayAlert("Error","Wrong User or Password. " ,"Acept");
                    passwordEntry.Text = string.Empty;
                    passwordEntry.Focus();

                    return;
                }

                result = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {

                waitActivityIndicator.IsRunning = false;
                await DisplayAlert("Error", ex.Message, "Acept");

                return;
            }

            var userResponse = JsonConvert.DeserializeObject<UserResponse>(result);

            userResponse.Password = passwordEntry.Text;

            waitActivityIndicator.IsRunning = false;

            //await DisplayAlert("Message","Fuck Yeah.!!!","Acept");

            //navegacion  en xamarin:
            //forma de pasar un objeto entre paginas:
            await Navigation.PushAsync(new MainPage(userResponse));




        }
    }
}
