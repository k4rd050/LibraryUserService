﻿using System.ComponentModel.DataAnnotations;

namespace UserManagerQuery.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        public double Reputation { get; set; }

        [Required]
        public StatusEnum Status { get; set; }

    }
}
