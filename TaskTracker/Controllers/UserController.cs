﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using TaskTracker.DB;
using TaskTracker.Models.ViewModels;

namespace TaskTracker.Controllers
{
    public class UserController : Controller
    {
        public IMembershipService MembershipService
        {
            get;
            private set;
        }

        public IFormsAuthentication FormsAuth
        {
            get;
            private set;
        }

        public UserController() : this(null, null) { }

        public UserController(IFormsAuthentication formsAuth, IMembershipService membershipService)
        {
            FormsAuth = formsAuth ?? new FormsAuthenticationService();
            MembershipService = membershipService ?? new UserMembershipService();
        }

        //
        // GET: /User/LogOn

        public ActionResult LogOn()
        {
            return View("LogOn");
        }

        //
        // POST: /User/LogOn

        [HttpPost]
        public ActionResult LogOn(UserLogOnViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("LogOn", model);
            }

            if (String.IsNullOrEmpty(model.UserName))
            {
                ModelState.AddModelError("UserName", "UserName is required.");
            }

            if (String.IsNullOrEmpty(model.Password))
            {
                ModelState.AddModelError("Password", "Password is required.");
            }

            if (MembershipService.ValidateUser(model.UserName, model.Password))
            {
                FormsAuth.SetAuthCookie(model.UserName, true);

                return Redirect("~/");
            }

            ModelState.AddModelError("_FORM", "Invalid username or password.");
            return View("LogOn", model);
        }

        //
        // GET: /User/LogOff

        public ActionResult LogOff()
        {
            FormsAuth.SignOut();

            return Redirect("~/");
        }

    }

    public interface IMembershipService
    {
        bool ValidateUser(string userName, string password);
    }

    public class UserMembershipService : IMembershipService
    {
        private MembershipProvider provider;

        public UserMembershipService() : this(null) { }

        public UserMembershipService(MembershipProvider provider)
        {
            this.provider = provider ?? Membership.Provider;
        }

        public bool ValidateUser(string userName, string password)
        {
            return this.provider.ValidateUser(userName, password);
        }
    }

    public interface IFormsAuthentication
    {
        void SetAuthCookie(string userName, bool createPersistentCookie);

        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthentication
    {
        public void SetAuthCookie(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
