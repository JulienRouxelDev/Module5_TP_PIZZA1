using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP_MODULE5_PIZZA.Models;

namespace TP_MODULE5_PIZZA.Utils
{
    public class FakeDbPizza
    {
        private static FakeDbPizza _instance;
        static readonly object instanceLock = new object();

        private FakeDbPizza()
        {
            this.ingredients = this.GetIngredients();
            this.pates = this.GetPates();
            this.pizzas = new List<Pizza>();
        }

        public static FakeDbPizza Instance
        {
            get
            {
                if (_instance == null) //Les locks prennent du temps, il est préférable de vérifier d'abord la nullité de l'instance.
                {
                    lock (instanceLock)
                    {
                        if (_instance == null) //on vérifie encore, au cas où l'instance aurait été créée entretemps.
                            _instance = new FakeDbPizza();
                    }
                }
                return _instance;
            }
        }

        private List<Ingredient> ingredients;
        private List<Pate> pates;
        private List<Pizza> pizzas;

        public List<Ingredient> Ingredients
        {
            get { return ingredients; }
        }

        public List<Pate> Pates
        {
            get { return pates; }
        }

        public List<Pizza> Pizzas
        {
            get { return pizzas; }
        }

        private List<Ingredient> GetIngredients()
        {
            return new List<Ingredient>
            {
                new Ingredient{Id=1,Nom="Mozzarella"},
                new Ingredient{Id=2,Nom="Jambon"},
                new Ingredient{Id=3,Nom="Tomate"},
                new Ingredient{Id=4,Nom="Oignon"},
                new Ingredient{Id=5,Nom="Cheddar"},
                new Ingredient{Id=6,Nom="Saumon"},
                new Ingredient{Id=7,Nom="Champignon"},
                new Ingredient{Id=8,Nom="Poulet"}
            };
        }

        private List<Pate> GetPates()
        {
            return new List<Pate>
            {
                new Pate{ Id=1,Nom="Pate fine, base crême"},
                new Pate{ Id=2,Nom="Pate fine, base tomate"},
                new Pate{ Id=3,Nom="Pate épaisse, base crême"},
                new Pate{ Id=4,Nom="Pate épaisse, base tomate"}
            };
        }

    }
}