using FinalProjectMedimall.DAL;
using FinalProjectMedimall.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMedimall.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleInManager;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _http;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
               RoleManager<IdentityRole> roleInManager, ApplicationDbContext context, IHttpContextAccessor http)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleInManager = roleInManager;
            _context = context;
            _http = http;

        }
        public async  Task<IActionResult> Index()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return RedirectToAction("Login", "Contact");
            AppUser name = await _context.Users.Include(n=>n.Orders).FirstOrDefaultAsync(n => n.Id == user.Id);
            return View(name);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AppUser usernew)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return RedirectToAction("Login", "Contact");
            if (!ModelState.IsValid) return View(ModelState);
            if (usernew == null) return NotFound();
            user.FirstName = usernew.FirstName;
            user.LastName=usernew.LastName;
            user.Email = usernew.Email;
            _context.SaveChanges();
            TempData["name"] = "Succsses";
            return RedirectToAction(nameof(Index));
        }
    }
}
