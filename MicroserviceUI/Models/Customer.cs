﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MicroserviceUI.Models
{
    public class Customer
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }        

    }
}
