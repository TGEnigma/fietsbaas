using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace Fietsbaas.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private string email;
        private int points;
        private string profilePic = null; 

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
        public string ProfilePic
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
            using (var fileStream = File.Open(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ProfilePic"), FileMode.CreateNew))
                stream.CopyTo(fileStream);
                App.User.ProfilePicture = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ProfilePic");
            ProfilePic = App.User.ProfilePicture;
            stream.Dispose(); 
        }
    }     
}
