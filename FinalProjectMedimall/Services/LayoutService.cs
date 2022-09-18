using FinalProjectMedimall.DAL;
using FinalProjectMedimall.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace FinalProjectMedimall.Services
{
    public class LayoutService
    {

        private readonly ApplicationDbContext _context;
 

        public LayoutService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Setting> GetSettings()
        {
            List<Setting> settings = _context.Settings.ToList();
            return settings;
        }
    }
}
