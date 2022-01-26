using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace Fietsbaas.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private string email;
        private int points;
        private ImageSource profilePic = null; 

        public string Email
        {
            get => email;
            set => SetProperty( ref email, value );
        }

        public int Points
        {
            get => points;
            set => SetProperty( ref points, value );
        }
        public ImageSource ProfilePic
        {
            get => profilePic; 
            set => SetProperty( ref profilePic, value );
        }

        public Command ResetPasswordCommand { get; set; }
        public Command ResetEmailCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Command TakeProfilePicture { get; set; }

        public ProfileViewModel()
        {
            Email = App.User.Email;
            Points = App.User.Points;
            TakeProfilePicture = new Command( OnTakePictureClicked );
        }

        private async void OnTakePictureClicked(object obj)
        {
            var result = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo"
            });
            var stream = await result.OpenReadAsync();
            App.User.ProfilePicture = ProfilePic;
            ProfilePic = App.User.ProfilePicture;
        }
    }     
}
