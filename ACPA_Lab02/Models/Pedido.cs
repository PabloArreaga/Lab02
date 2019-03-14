using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using ACPA_Lab02.Helpers;

namespace ACPA_Lab02.Models
{
	public class Pedido : IComparable, IEnumerable
	{
		[Display(Name = "Id")]
		public int idc { get; set; }

		[Display(Name = "Nombre")]
		public string cliente { get; set; }

		[Display(Name = "Nit")]
		public string nit { get; set; }

		[Display(Name = "Descripcion")]
		public string detalle { get; set; }

		[Display(Name = "Total")]
		public double total { get; set; }

		[Display(Name = "Fecha"), DataType(DataType.Time)]
		public DateTime fecha { get; set; }
		
		public int CompareTo(object obj)
		{
			var comparador = (Pedido)obj;
			return cliente.CompareTo(comparador.idc);
		}
		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}