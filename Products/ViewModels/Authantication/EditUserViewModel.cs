using System.ComponentModel.DataAnnotations;

namespace Products.ViewModels.Authantication
{
    public class EditProfileViewModel
    {
        //public string Id { get; set; }

       
        public string UserName { get; set; }

        public IFormFile? ProfileImage { get; set; }

        public string? ExistingImagePath { get; set; }
    }

}
