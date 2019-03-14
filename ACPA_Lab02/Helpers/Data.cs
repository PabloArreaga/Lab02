using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACPA_Lab02.Models;
using EstructuraDatos;

namespace ACPA_Lab02.Helpers
{
	public class Data
	{
		private static Data _instance = null;
		public static Data Instance
		{
			get
			{
				if (_instance == null) _instance = new Data();
				{
					return _instance;
				}
			}
		}
		// Estructuras
		public Lista<Medicamento> listaEmpleados = new Lista<Medicamento>();
	}
}