using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace aumanager_geo_core.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SortName { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int PhoneCode { get; set; }

        public ICollection<State> States { get; set; }
    }
}
