using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using PetLoveWeb.Models;
using PetLoveWeb.Gerenciadores;

namespace PetLoveWeb.Controllers
{
    public class AccountController : Controller
    {

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Nome de usuário ou senha incorreto.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, "123", "123", true, null, out createStatus);
                
                if (createStatus == MembershipCreateStatus.Success)
                {
                    UsuarioModel usuario = new UsuarioModel();
                    usuario.Email = model.Email;
                    usuario.Nome = model.NomeCompleto;
                    usuario.Telefone = model.Telefone;
                    usuario.Usuario = model.UserName;
                    usuario.Senha = "banco";
                    GerenciadorUsuario.GetInstance().Inserir(usuario);

                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            ChangePasswordModel model = new ChangePasswordModel();
            string userId = Membership.GetUser().ProviderUserKey.ToString();
            UsuarioModel user = GerenciadorUsuario.GetInstance().Obter(Convert.ToInt32(userId));
            model.NomeCompleto = user.Nome;
            model.UserName = user.Usuario;
            model.Telefone = user.Telefone;
            model.Email = user.Email;
            return View(model);
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    string userId = Membership.GetUser().ProviderUserKey.ToString();
                    UsuarioModel user = GerenciadorUsuario.GetInstance().Obter(Convert.ToInt32(userId));
                    user.Nome = model.NomeCompleto;
                    user.Telefone = model.Telefone;
                    user.Email = model.Email;
                    user.Senha = "banco";
                    GerenciadorUsuario gu = new GerenciadorUsuario();
                    gu.Editar(user);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "A senha anterior está incorreta, ou a senha atual está inválida.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Nome do usuário já existe. Favor utilizar um nome diferente.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Um nome de usuário para este e-mail já doi usado. Favor digitar outro e-mail válido.";

                case MembershipCreateStatus.InvalidPassword:
                    return "A senha está inválida. A senha deve conter pelo menos 6 caracteres e um símbolo especial como '@', '!' etc.";

                case MembershipCreateStatus.InvalidEmail:
                    return "E-mail inválido. Favor digitar outro e-mail válido.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "Nome do usuário inválido. . Favor digitar outro nome de usuário válido.";

                case MembershipCreateStatus.ProviderError:
                    return "Não foi possível realizar seu cadastro no sistema. Tente novamente mais tarde. Se o problema persistir, contacte o Administrador do sistema.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "Erro desconhecido. Favor contactar o administrador so sistema.";
            }
        }
        #endregion
    }
}
