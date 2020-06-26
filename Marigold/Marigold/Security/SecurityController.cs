using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Marigold.Models;

#region Additional Namespace
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Configuration;
#endregion

namespace Marigold.Security
{
    [DataObject]
    public class SecurityController
    {
        public int? GetCurrentUserId(string userName)
        {
            int? id = null;
            var request = HttpContext.Current.Request;
            if (request.IsAuthenticated)
            {
                var manager = request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var appUser = manager.Users.SingleOrDefault(x => x.UserName == userName);
                if (appUser != null)
                    id = appUser.EmployeeId;
            }
            return id;
        }

        public string GetCurrentUserRole(string userName)
        {
            string role = "";
            var request = HttpContext.Current.Request;
            if (request.IsAuthenticated)
            {
                var manager = request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var appUser = manager.Users.SingleOrDefault(x => x.UserName == userName);
                if (appUser != null)
                    role = appUser.Role;
            }
            return role;
        }
        #region Constructors & Dependencies
        private readonly ApplicationUserManager UserManager;
        private readonly RoleManager<IdentityRole> RoleManager;
        public SecurityController()
        {
            UserManager = HttpContext.Current.Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
        }
        private void CheckResult(IdentityResult result, string item, string action)
        {
            if (!result.Succeeded)
                throw new Exception($"Failed to " + action + $" " + item +
                    $":<ul> {string.Join(string.Empty, result.Errors.Select(x => $"<li>{x}</li>"))}</ul>");
        }
        #endregion

    }
}