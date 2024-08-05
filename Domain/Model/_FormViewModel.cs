using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class _FormViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage ="This field is required.")]
        [EmailAddress(ErrorMessage = "Invalid email  format")]
        public string Email {  get; set; }
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string PhoneNo {  get; set; }
		public string Address {  get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string State {  get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string City { get; set; }

    }
}
