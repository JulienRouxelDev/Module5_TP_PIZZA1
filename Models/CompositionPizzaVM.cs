using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP_MODULE5_PIZZA.Utils;

namespace TP_MODULE5_PIZZA.Models
{
    public class CompositionPizzaVM
    {
		private Pizza pizza;
		private List<Ingredient> ingredientsDisponibles;
		private List<Pate> patesDisponibles;
		public int IdPate { get; set; }
		public List<int> IdsIngredients { get; set; }

		//public CompositionPizzaVM()
		//{
		//	if (ingredients == null)
		//	{
		//		ingredients = Pizza.IngredientsDisponibles;
		//	}

		//	if (pates==null)
		//	{
		//		pates = Pizza.PatesDisponibles;
		//	}
		//}

		public Pizza Pizza
		{
			get { return this.pizza; }
			set { this.pizza = value; }
		}

		public List<Ingredient> IngredientsDisponibles 
		{
			get { return FakeDbPizza.Instance.Ingredients; }
			//set { this.IngredientsDisponibles = value; }
		}


		public List<Pate> PatesDisponibles
		{
			get { return FakeDbPizza.Instance.Pates; }
			//set { myVar = value; }
		}


	}
}