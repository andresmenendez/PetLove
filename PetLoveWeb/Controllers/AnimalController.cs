using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetLoveWeb.Banco;
using PetLoveWeb.Gerenciadores;
using System.Web.Security;
using PetLoveWeb.Models;

namespace PetLoveWeb.Controllers
{ 
    public class AnimalController : Controller
    {
        //
        // GET: /Turma/

        public ViewResult Index()
        {
            string userId = Membership.GetUser().ProviderUserKey.ToString();
            return View(GerenciadorAnimal.GetInstance().ObterPorUsuario(Convert.ToInt32(userId)));
        }

        //
        // GET: /Turma/Details/5

        public ViewResult Details(int id)
        {
            AnimalModel animal = GerenciadorAnimal.GetInstance().Obter(id);

            return View(animal);
        }

        //
        // GET: /Turma/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Turma/Create

        [HttpPost]
        public ActionResult Create(AnimalModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = Membership.GetUser().ProviderUserKey.ToString();
                model.Id_Usuario = Convert.ToInt32(userId);
                GerenciadorAnimal.GetInstance().Inserir(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        //
        // GET: /Turma/Edit/5

        public ActionResult Edit(int id)
        {
            AnimalModel animal = GerenciadorAnimal.GetInstance().Obter(id);
            return View(animal);
        }

        //
        // POST: /Turma/Edit/5

        [HttpPost]
        public ActionResult Edit(AnimalModel model)
        {
            if (ModelState.IsValid)
            {
                //GerenciadorTurma.GetInstance().Editar(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //
        // GET: /Turma/Delete/5

        public ActionResult Delete(int id)
        {
            AnimalModel animal = GerenciadorAnimal.GetInstance().Obter(id);
            ViewBag.Error = "";
            return View(animal);
        }

        //
        // POST: /Turma/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            AnimalModel animal = GerenciadorAnimal.GetInstance().Obter(id);
            try
            {
                ViewBag.Error = "Não foi possível remover esta animal, pois devem existir atividades associadas. Se não existirem animals associadas contacte o ADM do sistema.";
                return View(animal);
            }
            catch (Exception)
            {
                ViewBag.Error = "Não foi possível remover esta animal, pois devem existir atividades associadas. Se não existirem animals associadas contacte o ADM do sistema.";
                throw;
            }
            return RedirectToAction("Index");
        }

    }
}