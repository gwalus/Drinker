﻿using DrinkerAPI.Models;
using System.Threading.Tasks;

namespace DrinkerAPI.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthentiactionResult> RegisterAsync(string email, string password);
        Task<AuthentiactionResult> LoginAsync(string email, string password);
    }
}
