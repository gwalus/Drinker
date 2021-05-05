﻿using System.Collections.Generic;

namespace DrinkerAPI.Dtos
{
    public class CoctailDto
    {
        public int Id { get; set; }      
        public string Name { get; set; }
        public string Category { get; set; }
        public string Alcoholic { get; set; }
        public string Glass { get; set; }
        public string Instructions { get; set; }
        public string PhotoUrl { get; set; }
        public string DateModified { get; set; }
        public IList<IngredientDto> Ingradients { get; set; }
    }
}
