using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using ACPA_Lab02.Helpers;


namespace ACPA_Lab02.Models
{
	public class Medicamento : IComparable, IEnumerable
	{
		[Display(Name = "Id")]
		public int id { get; set; }
		[Display(Name = "Nombre")]
		public string nombre { get; set; }
		[Display(Name = "Descripción")]
		public string descripcion { get; set; }
		[Display(Name = "Productora")]
		public string productora { get; set; }
		[Display(Name = "Precio")]
		public double precio { get; set; }
		[Display(Name = "Existencia")]
		public int existencia { get; set; }

		public int CompareTo(object obj)
		{
			var comparador = (Medicamento)obj;
			return nombre.CompareTo(comparador.id);
		}
		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}