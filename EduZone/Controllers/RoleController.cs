using EduZone.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EduZone.Controllers
{
  //  [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult NewRole()
        {
            
            return View();
        }
        [HttpPost]
        
        public async Task<ActionResult> NewRole(RoleViewModel roleModel)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            RoleStore<IdentityRole> store = new RoleStore<IdentityRole>(context);
            RoleManager<IdentityRole> manger = new RoleManager<IdentityRole>(store);
            IdentityRole role = new IdentityRole();
            role.Name = roleModel.RoleName;
            IdentityResult result =  await manger.CreateAsync(role);
            if (result.Succeeded)
            {
                return Content("Add");
            }
            else
            {
                return View(roleModel);
            }
        }


    }
}