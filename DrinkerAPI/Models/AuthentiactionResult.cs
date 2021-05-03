using System.Collections.Generic;

namespace DrinkerAPI.Models
{
    public class AuthentiactionResult
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
