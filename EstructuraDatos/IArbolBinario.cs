using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructuraDatos
{
	public delegate void VisitarArbolDelegate<T>(IArbolBinario<T> item);

	public interface IArbolBinario<T>
	{
		T Dato { get; set; }

		IArbolBinario<T> HijoIzquierdo { get; set; }

		IArbolBinario<T> HijoDerecho { get; set; }

		IArbolBinario<T> Padre { get; set; }

		int FactorBalance { get; set; }

		void RecorrerPrefijo(VisitarArbolDelegate<T> visitar);

		void RecorrerInfijo(VisitarArbolDelegate<T> visitar);

		void RecorrerPosfijo(VisitarArbolDelegate<T> visitar);

	}
}
