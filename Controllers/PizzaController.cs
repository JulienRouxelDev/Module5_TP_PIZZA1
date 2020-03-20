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
                if (ModelState.IsValid)        
                {
                    if (FakeDbPizza.Instance.Pizzas.Any(p=>p.Nom.ToUpper()==compositionPizzaVM.Pizza.Nom.ToUpper()))
                    {
                        ModelState.AddModelError("", $"La Pizza {compositionPizzaVM.Pizza.Nom} existe déjà.");
                        return View(compositionPizzaVM);
                    }

                    if (compositionPizzaVM.IdsIngredients.Count < 2 || compositionPizzaVM.IdsIngredients.Count > 5)
                    {
                        ModelState.AddModelError("", $"La Pizza doit contenir entre 2 et 5 ingrédients.");
                        return View(compositionPizzaVM);
                    }

                    //Comparaison des ingrédients

                    //On sélectionne les pizzas de la bdd ayant le même nombre d'ingrédients
                    var pizzasAvecxIngredients = FakeDbPizza.Instance.Pizzas.Where(p => p.Ingredients.Count() == compositionPizzaVM.IdsIngredients.Count());
                    bool pizzaExiste = false;
                    //Pour chacune de ces pizzas on regarde si tous les ingrédients correspondent
                    foreach (var p in pizzasAvecxIngredients)
                    {
                        var listIdIngredients = p.Ingredients.Select(x => x.Id);
                        if (compositionPizzaVM.IdsIngredients.All(x => listIdIngredients.Contains(x)))
                        {
                            pizzaExiste = true;
                        }
                        
                    }

                    if (pizzaExiste)
                    {
                        ModelState.AddModelError("", "Une pizza contenant exactement les mêmes ingrédients existe déjà");
                        return View(compositionPizzaVM);
                    }

                    Pizza pizza = compositionPizzaVM.Pizza;
                    Pate pate = FakeDbPizza.Instance.Pates.FirstOrDefault(p => p.Id == compositionPizzaVM.IdPate);
                    List<Ingredient> ingredients = FakeDbPizza.Instance.Ingredients.Where(
                        x => compositionPizzaVM.IdsIngredients.Contains(x.Id)).ToList();

                    pizza.Pate = pate;
                    pizza.Ingredients = ingredients;
                    if (FakeDbPizza.Instance.Pizzas.Count == 0)
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
                return View(compositionPizzaVM);
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
                //Nom de la pizza
                if (FakeDbPizza.Instance.Pizzas.Any(p => p.Nom.ToUpper() == vm.Pizza.Nom.ToUpper()
                    && p.Id!=vm.Pizza.Id))
                {
                    ModelState.AddModelError("", $"La Pizza {vm.Pizza.Nom} existe déjà.");
                    return View(vm);
                }

                //Nombre d'ingredients
                if (vm.IdsIngredients.Count < 2 || vm.IdsIngredients.Count > 5)
                {
                    ModelState.AddModelError("", $"La Pizza doit contenir entre 2 et 5 ingrédients.");
                    return View(vm);
                }

                //Comparaison des ingrédients

                //On sélectionne les pizzas de la bdd ayant le même nombre d'ingrédients
                var pizzasAvecxIngredients = FakeDbPizza.Instance.Pizzas.Where(p => p.Ingredients.Count() == vm.IdsIngredients.Count() && p.Id != vm.Pizza.Id);
                bool pizzaExiste = false;
                foreach (var p in pizzasAvecxIngredients)
                {
                    var listIdIngredients = p.Ingredients.Select(x => x.Id);
                    if (vm.IdsIngredients.All(x => listIdIngredients.Contains(x)))
                    {
                        pizzaExiste = true;
                    }

                }

                if (pizzaExiste)
                {
                    ModelState.AddModelError("", "Une pizza contenant exactement les mêmes ingrédients existe déjà");
                    return View(vm);
                }

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
