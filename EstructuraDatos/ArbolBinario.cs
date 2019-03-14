using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructuraDatos
{
	class ArbolBinario<T> : IArbolBinario<T>
	{
		T _dato;
		IArbolBinario<T> _hijoDerecho = null;
		IArbolBinario<T> _hijoIzquierdo = null;
		IArbolBinario<T> _padre = null;
		int _factor;

		public ArbolBinario(T dato) : this(dato, null, null)
		{
		}

		public ArbolBinario(T dato, IArbolBinario<T> hijoIzquierdo,
			IArbolBinario<T> hijoDerecho)
		{
			this.Dato = dato;
			this.HijoIzquierdo = hijoIzquierdo;
			this.HijoDerecho = hijoDerecho;
			this.Padre = null;
			this.FactorBalance = 0;
		}

		public int FactorBalance
		{
			get
			{
				return _factor;
			}
			set
			{
				_factor = value;
			}
		}

		public T Dato
		{
			get
			{
				return _dato;
			}
			set
			{
				_dato = value;
			}
		}

		public IArbolBinario<T> HijoIzquierdo
		{
			get
			{
				return _hijoIzquierdo;
			}
			set
			{
				_hijoIzquierdo = value;
			}
		}

		public IArbolBinario<T> HijoDerecho
		{
			get
			{
				return _hijoDerecho;
			}
			set
			{
				_hijoDerecho = value;
			}
		}

		public IArbolBinario<T> Padre
		{
			get
			{
				return _padre;
			}
			set
			{
				_padre = value;
			}
		}

		public void RecorrerPrefijo(VisitarArbolDelegate<T> visitar)
		{
			visitar(this);

			if (this.HijoIzquierdo != null)
			{
				this.HijoIzquierdo.RecorrerPrefijo(visitar);
			}

			if (this.HijoDerecho != null)
			{
				this.HijoDerecho.RecorrerPrefijo(visitar);
			}
		}

		public void RecorrerInfijo(VisitarArbolDelegate<T> visitar)
		{
			if (this.HijoIzquierdo != null)
			{
				this.HijoIzquierdo.RecorrerInfijo(visitar);
			}

			visitar(this);

			if (this.HijoDerecho != null)
			{
				this.HijoDerecho.RecorrerInfijo(visitar);
			}
		}

		public void RecorrerPosfijo(VisitarArbolDelegate<T> visitar)
		{
			if (this.HijoIzquierdo != null)
			{
				this.HijoIzquierdo.RecorrerPosfijo(visitar);
			}

			if (this.HijoDerecho != null)
			{
				this.HijoDerecho.RecorrerPosfijo(visitar);
			}

			visitar(this);
		}
	}
}
