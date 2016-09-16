using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreAppEcommerce.Classes;
using Xamarin.Forms;

namespace PreAppEcommerce.Pages
{
    public partial class MainPage : ContentPage
    {

        //Propiesda para navegar entre paginas:
        private UserResponse userResponse;

        public MainPage(UserResponse userResponse)
        {
            InitializeComponent();

            this.userResponse = userResponse;

            this.Padding = Device.OnPlatform(
                    new  Thickness(10,20,10,10),
                     new Thickness(10),
                     new Thickness(10)

                );

        }


        protected override void OnAppearing()
        {
            base.OnAppearing();


            userNameLabel.Text = userResponse.FullName;
            photoImage.Source = userResponse.PhotoFullPath;

            photoImage.HeightRequest = 280;
            photoImage.WidthRequest = 280;


        }
    }
}
