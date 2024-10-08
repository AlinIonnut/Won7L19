﻿using System.ComponentModel.DataAnnotations;

namespace Won7E1.DTOs
{
    public class StudentWithoutAddressDto
    {
        public string Name { get; set; }
        public string FirstName { get; set; }

        [Range(0, int.MaxValue)]
        public int Age { get; set; }
    }
}
