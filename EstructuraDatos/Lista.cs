using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructuraDatos
{
    public class Lista<T> : ILista<T>//, IEnumerable<T> where T : IComparable
	{
		public delegate bool CompararElementosDelegate(T item1, T item2);
		
		private class NodoLista<TN>
		{
			TN _valor = default(TN);
			NodoLista<T> _siguiente = null;

			public NodoLista(TN valor, NodoLista<T> siguiente)
			{
				this.Valor = valor;
				this.Siguiente = siguiente;
			}

			public TN Valor
			{
				get { return _valor; }
				set { _valor = value; }
			}

			public NodoLista<T> Siguiente
			{
				get { return _siguiente; }
				set { _siguiente = value; }
			}

			public static bool CompararNodos(T N1, T N2)
			{
				return object.Equals(N1, N2);
			}
		}

		NodoLista<T> _head = null;

		CompararElementosDelegate _funcComparar;

		public Lista(CompararElementosDelegate comparaElementos)
		{
			_funcComparar = comparaElementos;
			_head = null;
		}

		public Lista() : this(NodoLista<T>.CompararNodos)
		{
		}

		public void Agregar(T item)
		{
			Insertar(this.Longitud, item);
		}

		public T Buscar(T item)
		{
			int index = 0;
			NodoLista<T> nodo = BuscarNodoPorValor(item, out index);
			////if (nodo == null)
			////{
			////    return null;
			////}
			////else
			////{
			////    return nodo.Valor;
			////}
			return nodo == null
				? default(T)
				: nodo.Valor;
		}

		public int BuscarIndice(T item)
		{
			int index = 0;
			BuscarNodoPorValor(item, out index);
			return index;
		}

		public bool Existe(T item)
		{
			int index = BuscarIndice(item);
			return index >= 0;
		}
		
		public void Eliminar(T item)
		{
			int index = 0;
			NodoLista<T> nodo = BuscarNodoPorValor(item, out index);
			if (nodo == null)
			{
				throw new Exception("Valor no encontrado en la lista");
			}
			else
			{
				EliminarNodo(nodo, index);
			}
		}

		public T Remover(int index)
		{
			if (index < 0 || index >= this.Longitud)
			{
				return default(T);
			}

			NodoLista<T> nodoEliminar = BuscarNodo(index);
			return EliminarNodo(nodoEliminar, index);
		}

		public void Insertar(int index, T item)
		{
			if (index < 0 || index > this.Longitud)
			{
				throw new ArgumentOutOfRangeException("index");
			}

			if (index == 0)
			{
				// Insertar en la primera posición.
				NodoLista<T> tmp = _head;

				// Crea el nuevo nodo.
				NodoLista<T> nuevoNodo = new NodoLista<T>(item, tmp);

				// Re-encadena la lista
				_head = nuevoNodo;
			}
			else
			{
				// Buscamos el nodo anterior a donde insertamos
				NodoLista<T> nodoActual = BuscarNodo(index - 1);

				// Crea el nuevo nodo
				NodoLista<T> nuevoNodo = new NodoLista<T>(item, nodoActual.Siguiente);

				// Re-encadenamos con el nuevo.
				nodoActual.Siguiente = nuevoNodo;
			}
		}
		
		public int Longitud
		{
			get
			{
				int conteo = 0;
				NodoLista<T> nodoActual = _head;
				while (nodoActual != null)
				{
					nodoActual = nodoActual.Siguiente;
					conteo++;
				}
				return conteo;
			}
		}

		public void Limpiar()
		{
			// Limpia/Dispose la información de los nodos.
			NodoLista<T> nodoActual = _head;
			NodoLista<T> tmp = null;
			while (nodoActual != null)
			{
				tmp = nodoActual.Siguiente;
				// Clear: Nodo Actual
				nodoActual = tmp;
			}
			_head = null;
		}

		public T this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Longitud)
				{
					throw new ArgumentOutOfRangeException("index");
				}

				NodoLista<T> nodoActual = BuscarNodo(index);
				return nodoActual.Valor;
			}
		}

		public T Elemento(int index)
		{
			return this[index];
		}

		public bool ListaVacia
		{
			get { return _head == null; }
		}
		
		private NodoLista<T> BuscarNodo(int index)
		{
			int posActual = 0;
			NodoLista<T> nodoActual = _head;

			// Buscamos el nodo secuencialmente
			while (posActual < index)
			{
				nodoActual = nodoActual.Siguiente;
				posActual++;
			}
			return nodoActual;
		}

		private NodoLista<T> BuscarNodoPorValor(T valor, out int index)
		{
			index = -1;
			int posActual = 0;
			NodoLista<T> nodoActual = _head;

			// Buscamos el nodo secuencialmente
			while (nodoActual != null)
			{
				if (_funcComparar(nodoActual.Valor, valor))
				{
					index = posActual;
					return nodoActual;
				}
				nodoActual = nodoActual.Siguiente;
				posActual++;
			}
			// No lo encontró
			return null;
		}

		private T EliminarNodo(NodoLista<T> nodoEliminar, int index)
		{
			if (index == 0)
			{
				_head = nodoEliminar.Siguiente;
			}
			else
			{
				NodoLista<T> nodoAnterior = BuscarNodo(index - 1);
				nodoAnterior.Siguiente = nodoEliminar.Siguiente;
			}

			T result = nodoEliminar.Valor;
			// Clear: NodoEliminar
			return result;
		}
	}
	}
