using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDALibrary;

namespace EstructuraDatos
{
	class ArbolB<T, K> : IArbolBusquedaBinario<T, K>
	{
		protected ArbolBinario<T> _raiz;
		CompararLlavesDelegate<K> _fnCompararLave;
		ObtenerLlaveDelegate<T, K> _fnObtenerLlave;
		Lista<T> miLista;

		public ArbolB(CompararLlavesDelegate<K> p_FuncionCompararLlaves, ObtenerLlaveDelegate<T, K> p_FuncionObtenerLlaves)
		{
			miLista = new Lista<T>();
			_raiz = null;
			_raiz.Padre = null;
			this.FuncionCompararLlave = p_FuncionCompararLlaves;
			this.FuncionObtenerLlave = p_FuncionObtenerLlaves;
		}

		public ArbolB()
		{
			_raiz = null;
			_fnCompararLave = null;
			_fnObtenerLlave = null;
			miLista = new Lista<T>();
		}
		
		public T Buscar(K llave)
		{
			if ((this.FuncionCompararLlave == null) || (this.FuncionObtenerLlave == null))
				throw new Exception("No se han inicializado las funciones para operar la estructura");

			if (Equals(llave, default(K)))
				throw new ArgumentNullException("La llave enviada no es valida");

			if (_raiz == null)
				return default(T);
			else
			{
				ArbolBinario<T> siguiente = _raiz;
				K llaveSiguiente = this.FuncionObtenerLlave(siguiente.Dato);
				bool encontrado = false;

				while (!encontrado)
				{
					llaveSiguiente = this.FuncionObtenerLlave(siguiente.Dato);

					// > 0 si el primero es mayor < 0 si el primero es menor y 0 si son iguales
					int comparacion = this.FuncionCompararLlave(llave, llaveSiguiente);

					if (comparacion == 0)
					{
						return siguiente.Dato;
					}
					else
					{
						if (comparacion > 0)
						{
							if (siguiente.HijoDerecho == null)
							{
								return default(T);
							}
							else
							{
								siguiente = siguiente.HijoDerecho as ArbolBinario<T>;
							}
						}
						else
						{
							if (siguiente.HijoIzquierdo == null)
							{
								return default(T);
							}
							else
							{
								siguiente = siguiente.HijoIzquierdo as ArbolBinario<T>;
							}
						}
					}//Fin del if comparaci{on
				} //Fin del ciclo
			}//Fin del if que verifica que no exista ningún dato.
			return default(T);
		}
		
		public T Eliminar(K llave)
		{
			if ((this.FuncionCompararLlave == null) || (this.FuncionObtenerLlave == null))
				throw new Exception("No se han inicializado las funciones para operar la estructura");

			if (Equals(llave, default(K)))
				throw new ArgumentNullException("La llave enviada no es valida");

			if (_raiz == null)
				throw new Exception("El arbol se encuentra vacio");
			else //Si el árbol no está vacio
			{
				ArbolBinario<T> siguiente = _raiz;
				ArbolBinario<T> padre = null;
				bool EsHijoIzquierdo = false;
				bool encontrado = false;

				while (!encontrado)
				{
					K llaveSiguiente = this.FuncionObtenerLlave(siguiente.Dato);

					// > 0 si el primero es mayor < 0 si el primero es menor y 0 si son iguales
					int comparacion = this.FuncionCompararLlave(llave, llaveSiguiente);

					if (comparacion == 0)
					{
						if ((siguiente.HijoDerecho == null) && (siguiente.HijoIzquierdo == null)) //Si es una hoja
						{
							T miDato = siguiente.Dato;
							if ((padre != null))
							{
								if (EsHijoIzquierdo)
									padre.HijoIzquierdo = null;
								else
									padre.HijoDerecho = null;
							}
							else //Si padre es null entonces es la raiz
							{
								_raiz = null;
							}
							return miDato;
						}
						else
						{
							if (siguiente.HijoDerecho == null) //Si solo tiene rama izquierda
							{
								T miDato = siguiente.Dato;
								if ((padre != null))
								{
									if (EsHijoIzquierdo)
										padre.HijoIzquierdo = siguiente.HijoIzquierdo;
									else
										padre.HijoDerecho = siguiente.HijoDerecho;
								}
								else
								{
									_raiz = siguiente.HijoIzquierdo as ArbolBinario<T>;
								}

								return miDato;
							}
							else if (siguiente.HijoIzquierdo == null)  //Si solo tiene rama derecha
							{
								T miDato = siguiente.Dato;
								if ((padre != null))
								{
									if (EsHijoIzquierdo)
										padre.HijoIzquierdo = siguiente.HijoDerecho;
									else
										padre.HijoDerecho = siguiente.HijoDerecho;
								}
								else
								{
									_raiz = siguiente.HijoDerecho as ArbolBinario<T>;
								}
								return miDato;
							}
							else  //Tiene ambas ramas el que lo sustituirá será el mas izquierdo de los derechos
							{
								ArbolBinario<T> aEliminar = siguiente;
								siguiente = siguiente.HijoDerecho as ArbolBinario<T>;
								int cont = 0;
								while (siguiente.HijoIzquierdo != null)
								{
									padre = siguiente;
									siguiente = siguiente.HijoIzquierdo as ArbolBinario<T>;
									cont++;
								}

								if (cont > 0)
								{
									if (padre != null)
									{
										T miDato = aEliminar.Dato;
										aEliminar.Dato = siguiente.Dato;
										padre.HijoIzquierdo = null;
										return miDato;
									}
								}
								else
								{
									siguiente.HijoIzquierdo = aEliminar.HijoIzquierdo;

									if (padre != null)
									{
										if (EsHijoIzquierdo)
											padre.HijoIzquierdo = aEliminar.HijoDerecho;
										else
											padre.HijoDerecho = aEliminar.HijoDerecho;
									}
									else //Es la raiz
									{
										if (EsHijoIzquierdo)
											_raiz = aEliminar.HijoDerecho as ArbolBinario<T>;
										else
											_raiz = aEliminar.HijoDerecho as ArbolBinario<T>;
									}
									return aEliminar.Dato;
								}
							}
						}
					}
					else
					{
						if (comparacion > 0)
						{
							if (siguiente.HijoDerecho == null)
							{
								return default(T);
							}
							else
							{
								padre = siguiente;
								EsHijoIzquierdo = false;
								siguiente = siguiente.HijoDerecho as ArbolBinario<T>;
							}
						}
						else //menor que 0
						{
							if (siguiente.HijoIzquierdo == null)
							{
								return default(T);
							}
							else
							{
								padre = siguiente;
								EsHijoIzquierdo = true;
								siguiente = siguiente.HijoIzquierdo as ArbolBinario<T>;
							}
						}
					}//Fin del if comparaci{on
				} //Fin del ciclo
			}//Fin del if que verifica que no exista ningún dato.
			return default(T);
		}

		public bool ExisteElemento(T dato)
		{
			if ((this.FuncionCompararLlave == null) || (this.FuncionObtenerLlave == null))
				throw new Exception("No se han inicializado las funciones para operar la estructura");

			if (Equals(dato, default(T)))
				throw new ArgumentNullException("La llave enviada no es valida");

			if (_raiz == null)
				return false;
			else
			{
				ArbolBinario<T> siguiente = _raiz;
				K llaveBuscar = this.FuncionObtenerLlave(dato);
				bool encontrado = false;

				while (!encontrado)
				{
					K llaveSiguiente = this.FuncionObtenerLlave(siguiente.Dato);
					// > 0 si el primero es mayor < 0 si el primero es menor y 0 si son iguales
					int comparacion = this.FuncionCompararLlave(llaveBuscar, llaveSiguiente);

					if (comparacion == 0)
					{
						return true;
					}
					else
					{
						if (comparacion > 0)
						{
							if (siguiente.HijoDerecho == null)
							{
								return false;
							}
							else
							{
								siguiente = siguiente.HijoDerecho as ArbolBinario<T>;
							}
						}
						else
						{
							if (siguiente.HijoIzquierdo == null)
							{
								return false;
							}
							else
							{
								siguiente = siguiente.HijoIzquierdo as ArbolBinario<T>;
							}
						}
					}//Fin del if comparaci{on
				} //Fin del ciclo
			}//Fin del if que verifica que no exista ningún dato.
			return false;
		}
		
		public CompararLlavesDelegate<K> FuncionCompararLlave
		{
			get
			{
				return _fnCompararLave;
			}
			set
			{
				_fnCompararLave = value;
			}
		}

		public ObtenerLlaveDelegate<T, K> FuncionObtenerLlave
		{
			get
			{
				return _fnObtenerLlave;
			}
			set
			{
				_fnObtenerLlave = value;
			}
		}

		public void Insertar(T dato)
		{
			if ((this.FuncionCompararLlave == null) || (this.FuncionObtenerLlave == null))
				throw new Exception("No se han inicializado las funciones para operar la estructura");

			if (dato == null)
				throw new ArgumentNullException("El dato ingresado está vacio");

			if (_raiz == null)
				_raiz = new ArbolBinario<T>(dato);
			else
			{
				ArbolBinario<T> siguiente = _raiz;
				K llaveInsertar = this.FuncionObtenerLlave(dato);
				bool yaInsertado = false;

				while (!yaInsertado)
				{
					K llaveSiguiente = this.FuncionObtenerLlave(siguiente.Dato);

					// > 0 si el primero es mayor < 0 si el primero es menor y 0 si son iguales
					int comparacion = this.FuncionCompararLlave(llaveInsertar, llaveSiguiente);

					if (comparacion == 0)
					{
						throw new Exception("El dato ingresado posee una llave que ya existe en la estructura");
					}
					else
					{
						if (comparacion > 0)
						{
							if (siguiente.HijoDerecho == null)
							{
								siguiente.HijoDerecho = new ArbolBinario<T>(dato);
								yaInsertado = true;
							}
							else
							{
								siguiente = siguiente.HijoDerecho as ArbolBinario<T>;
							}

						}
						else
						{
							if (siguiente.HijoIzquierdo == null)
							{
								siguiente.HijoIzquierdo = new ArbolBinario<T>(dato);
								yaInsertado = true;
							}
							else
							{
								siguiente = siguiente.HijoIzquierdo as ArbolBinario<T>;
							}
						}
					}//Fin del if comparaci{on
				} //Fin del ciclo
			}
		}

		public bool ExisteElementoPorLlave(K llave)
		{
			if ((this.FuncionCompararLlave == null) || (this.FuncionObtenerLlave == null))
				throw new Exception("No se han inicializado las funciones para operar la estructura");

			if (Equals(llave, default(K)))
				throw new ArgumentNullException("La llave enviada no es valida");

			if (_raiz == null)
				return false;
			else
			{
				ArbolBinario<T> siguiente = _raiz;
				bool encontrado = false;

				while (!encontrado)
				{
					K llaveSiguiente = this.FuncionObtenerLlave(siguiente.Dato);

					// > 0 si el primero es mayor < 0 si el primero es menor y 0 si son iguales
					int comparacion = this.FuncionCompararLlave(llave, llaveSiguiente);

					if (comparacion == 0)
					{
						return true;
					}
					else
					{
						if (comparacion > 0)
						{
							if (siguiente.HijoDerecho == null)
							{
								return false;
							}
							else
							{
								siguiente = siguiente.HijoDerecho as ArbolBinario<T>;
							}
						}
						else
						{
							if (siguiente.HijoIzquierdo == null)
							{
								return false;
							}
							else
							{
								siguiente = siguiente.HijoIzquierdo as ArbolBinario<T>;
							}
						}
					}//Fin del if comparaci{on
				} //Fin del ciclo
			}//Fin del if que verifica que no exista ningún dato.
			return false;
		}

		public void RecorrerInOrder(VisitarNodoDelegate<T> fnVisitar)
		{
			miLista.Limpiar();
			_raiz.RecorrerInfijo(VisitarArbol);
			for (int i = 0; i < miLista.Longitud; i++)
			{
				fnVisitar(miLista[i]);
			}
		}

		public void RecorrerPostOrder(VisitarNodoDelegate<T> fnVisitar)
		{
			miLista.Limpiar();
			_raiz.RecorrerPosfijo(VisitarArbol);
			for (int i = 0; i < miLista.Longitud; i++)
			{
				fnVisitar(miLista[i]);
			}
		}

		public void RecorrerPreOrder(VisitarNodoDelegate<T> fnVisitar)
		{
			miLista.Limpiar();
			_raiz.RecorrerPrefijo(VisitarArbol);
			for (int i = 0; i < miLista.Longitud; i++)
			{
				fnVisitar(miLista[i]);
			}
		}
		
		internal void VisitarArbol(IArbolBinario<T> arbol)
		{
			miLista.Agregar(arbol.Dato);
		}
	}
}

