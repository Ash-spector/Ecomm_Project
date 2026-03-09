using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecomm_Project.Models
{
    public class Category
    {
        public int id {  get; set; }
        [Required]
        public string Name { get; set; }
    }
}
