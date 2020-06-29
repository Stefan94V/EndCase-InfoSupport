﻿using CursusAdministratie.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CursusAdministratie.Data.Services.Interfaces
{
    public interface ICursusService
    {
        Task<Cursus> GetAsync(int id);
        Task<List<Cursus>> GetAllAsync();
        Task<Cursus> CreateAsync(Cursus cursus);
        Task<Cursus> UpdateAsync(Cursus cursus);
        Task<bool> RemoveAsync(int id);
    }
}
