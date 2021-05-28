﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkerAPI.Models
{
    public class CoctailToAdd
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Alcoholic { get; set; }
        public string Glass { get; set; }
        public string Instructions { get; set; }
        public IFormFile PhotoUrl { get; set; }
        public string DateModified { get; set; }
        public virtual IList<Ingredient> Ingradients { get; set; }
    }
}