using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class RemoveViewModel
    {
        [Required]
        public int NewElement1 { get; set; }
        public List<Dictelement> TheDictionary { get; set; }
    }
}
