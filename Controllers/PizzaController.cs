using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP_MODULE5_PIZZA.Models;
using TP_MODULE5_PIZZA.Utils;

namespace TP_MODULE5_PIZZA.Controllers
{
    public class PizzaController : Controller
    {

   
        // GET: Pizza
        public ActionResult Index()
        {
            return View(FakeDbPizza.Instance.Pizzas);
        }

        // GET: Pizza/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {
            CompositionPizzaVM vm = new CompositionPizzaVM();
            return View(vm);
        }

        // POST: Pizza/Create
        [HttpPost]
        public ActionResult Create(CompositionPizzaVM compositionPizzaVM)
        {
            try
            {
                Pizza pizza = compositionPizzaVM.Pizza;
                Pate pate = FakeDbPizza.Instance.Pates.FirstOrDefault(p => p.Id==compositionPizzaVM.IdPate);
                List<Ingredient> ingredients = FakeDbPizza.Instance.Ingredients.Where(
                    x => compositionPizzaVM.IdsIngredients.Contains(x.Id)).ToList();
                    

                
                pizza.Pate = pate;
                pizza.Ingredients = ingredients;
                if (FakeDbPizza.Instance.Pizzas.Count==0)
                {

                    //pizza.Id = FakeDbPizza.Instance.Pizzas.Count + 1
                    pizza.Id = 1;

                }
                else
                {
                    pizza.Id = FakeDbPizza.Instance.Pizzas.Max(X => X.Id) + 1;
                }



                //Ajout de la pizza
                FakeDbPizza.Instance.Pizzas.Add(pizza);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pizza/Edit/5
        public ActionResult Edit(int id)
        {
            CompositionPizzaVM vm = new CompositionPizzaVM();
            
            vm.Pizza = FakeDbPizza.Instance.Pizzas.FirstOrDefault(x => x.Id == id);

            if (vm.Pizza.Pate != null)
            {
                vm.IdPate = vm.Pizza.Pate.Id;
            }

            if (vm.Pizza.Ingredients.Any())
            {
                vm.IdsIngredients = vm.Pizza.Ingredients.Select(x => x.Id).ToList();
            }

            return View(vm);
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        public ActionResult Edit(CompositionPizzaVM vm)
        {
            try
            {
                Pizza pizza = FakeDbPizza.Instance.Pizzas.FirstOrDefault(x => x.Id == vm.Pizza.Id);
                pizza.Nom = vm.Pizza.Nom;
                pizza.Pate = FakeDbPizza.Instance.Pates.FirstOrDefault(x => x.Id == vm.IdPate);
                pizza.Ingredients = FakeDbPizza.Instance.Ingredients.Where(x => vm.IdsIngredients.Contains(x.Id)).ToList();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pizza/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
