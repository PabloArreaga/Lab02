using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructuraDatos
{
	class ArbolAVL<T, K> : ArbolB<T, K>, IArbolAVL<T, K>
	{
		public ArbolBinario<T> Raiz
		{
			get
			{
				return this._raiz;
			}
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
				ArbolBinario<T> siguiente = _raiz; //Empiezo a verificar desde la raiz.
				ArbolBinario<T> padre = null; //El padre de la raiz es nulo
				bool EsHijoIzquierdo = false; //La raiz no es ni izquierda ni derecha
				bool encontrado = false; //Asumo que no lo he encontrado

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
								{
									padre.HijoIzquierdo = null;
								}
								else
								{
									padre.HijoDerecho = null;
								}
								//EsHijoIzquierdo, se refiere al hijo que elimine
								Equilibrar(padre, EsHijoIzquierdo, false); //Le paso el padre, es hijo izquierdo falso o verdadero, es nuevo = false
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
									{
										padre.HijoIzquierdo = siguiente.HijoIzquierdo;
										siguiente.HijoIzquierdo.Padre = padre;
									}
									else
									{
										padre.HijoDerecho = siguiente.HijoIzquierdo;
										siguiente.HijoIzquierdo.Padre = padre;
									}

									Equilibrar(padre, EsHijoIzquierdo, false);
								}
								else
								{
									siguiente.HijoIzquierdo.Padre = null;
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
									{
										padre.HijoIzquierdo = siguiente.HijoDerecho;
										siguiente.HijoDerecho.Padre = padre;
									}
									else
									{
										padre.HijoDerecho = siguiente.HijoDerecho;
										siguiente.HijoDerecho.Padre = padre;
									}
									Equilibrar(padre, EsHijoIzquierdo, false);
								}
								else
								{
									siguiente.HijoDerecho.Padre = null;
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
										if (siguiente.HijoDerecho == null)
										{
											padre.HijoIzquierdo = null;
											Equilibrar(padre, true, false);
										}
										else
										{
											padre.HijoIzquierdo = siguiente.HijoDerecho;
											siguiente.HijoDerecho.Padre = padre;
											Equilibrar(padre, true, false);
										}
										return miDato;
									}
								}
								else
								{
									//Le estoy asignando un nuevo hijo a Siguiente
									siguiente.HijoIzquierdo = aEliminar.HijoIzquierdo;
									aEliminar.HijoIzquierdo.Padre = siguiente;
									siguiente.FactorBalance = aEliminar.FactorBalance;
									siguiente.Padre = aEliminar.Padre;

									if (padre != null)
									{
										if (EsHijoIzquierdo)
										{
											padre.HijoIzquierdo = aEliminar.HijoDerecho;
											Equilibrar(siguiente, false, false);
										}
										else
										{
											padre.HijoDerecho = aEliminar.HijoDerecho;
											Equilibrar(siguiente, false, false);
										}
									}
									else //Es la raiz
									{
										_raiz = aEliminar.HijoDerecho as ArbolBinario<T>;
										Equilibrar(_raiz, false, false);
									}
									return aEliminar.Dato;
								}
							}
						}
					}
					else //La comparación dio un valor diferente de 0
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
					}//Fin del if comparación
				} //Fin del ciclo
			}//Fin del if que verifica que no exista ningún dato.
			return default(T);
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
								siguiente.HijoDerecho.Padre = siguiente;
								Equilibrar(siguiente, false, true); //es Izquierdo = false, es nuevo = true;
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
								siguiente.HijoIzquierdo.Padre = siguiente;
								Equilibrar(siguiente, true, true); //Es izquierdo = true, es nuevo true;
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

		internal void Equilibrar(IArbolBinario<T> nodo, bool esIzquierdo, bool esNuevo)
		{
			bool salir = false; //al terminar de recorrer una rama si no es necesario rotar entonces se convierte en verdadero.
			// Recorrer camino inverso actualizando valores de FE:
			while ((nodo != null) && !salir)
			{
				bool hizoRotacion = false; //Al inicio digo que no se rotó
				if (esNuevo)
				{
					if (esIzquierdo)
						nodo.FactorBalance--; // Estamos añidiendo
					else
						nodo.FactorBalance++;
				}
				else
				{
					if (nodo.FactorBalance == 0)
						salir = true;
					if (esIzquierdo)
						nodo.FactorBalance++; // Borrando un nodo
					else
						nodo.FactorBalance--;
				}
				if (nodo.FactorBalance == 0)
					salir = true; // La altura de el arbol que empieza en nodo no ha variado, salir de Equilibrar
				//Si existe algún desbalance en esta porción de código se realizan las rotaciones
				else if (nodo.FactorBalance == -2)
				{ // Rotación a la derecha doble o simple según sea el caso y salir.
					if (nodo.HijoIzquierdo.FactorBalance == 1)
					{
						RDD(nodo); // Rotación doble
						hizoRotacion = true;
					}
					else
					{
						RSD(nodo); // Rotación simple
						hizoRotacion = true;
					}
					salir = true;
				}
				else if (nodo.FactorBalance == 2)
				{  // Rotar a la izquierda doble o simple según sea el caso y salir.
					if (nodo.HijoDerecho.FactorBalance == -1)
					{
						RDI(nodo); // Rotación doble
						hizoRotacion = true;
					}
					else
					{
						RSI(nodo); // Rotación simple
						hizoRotacion = true;
					}
					salir = true;
				}
				if ((hizoRotacion) && (nodo.Padre != null) && (!esNuevo))
					nodo = nodo.Padre;
				if (nodo.Padre != null)
				{
					if (nodo.Padre.HijoDerecho == nodo)
						esIzquierdo = false;
					else
						esIzquierdo = true;
					if ((!esNuevo) && (nodo.FactorBalance == 0))
						salir = false;
				}
				nodo = nodo.Padre; // Calcular Factor de balance, siguiente nodo del camino ossea el padre.
			}
		}

		internal void RDD(IArbolBinario<T> nodo)
		{
			//Punteros necesarios para realizar la rotación
			IArbolBinario<T> Padre = nodo.Padre;
			IArbolBinario<T> P = nodo;
			IArbolBinario<T> Q = P.HijoIzquierdo;
			IArbolBinario<T> R = Q.HijoDerecho;
			IArbolBinario<T> B = R.HijoIzquierdo;
			IArbolBinario<T> C = R.HijoDerecho;
			//Si el padre del nodo desequilibrado no es null verifico si el nodo desequilibrado es el derecho
			//o el izquierdo, si el padre es null, significa que es la raiz.
			if (Padre != null)
			{
				if (Padre.HijoDerecho == P)
					Padre.HijoDerecho = R;
				else
					Padre.HijoIzquierdo = R;
			}
			else
			{
				_raiz = R as ArbolBinario<T>;
				_raiz.Padre = null; //para que siga funcionando el padre de la raiz debe ser nulo.
			}
			// Reconstruir árbol:
			Q.HijoDerecho = B;
			P.HijoIzquierdo = C;
			R.HijoIzquierdo = Q;
			R.HijoDerecho = P;
			// Reasignar padres:
			R.Padre = Padre;
			P.Padre = Q.Padre = R;
			if (B != null)
				B.Padre = Q;
			if (C != null)
				C.Padre = P;
			// Ajustar valores de FE:
			switch (R.FactorBalance)
			{
				case -1:
					{
						Q.FactorBalance = 0;
						P.FactorBalance = 1;
					}
					break;
				case 0:
					{
						Q.FactorBalance = 0;
						P.FactorBalance = 0;
					}
					break;
				case 1:
					{
						Q.FactorBalance = -1;
						P.FactorBalance = 0;
					}
					break;
			}
			R.FactorBalance = 0;
		}

		internal void RDI(IArbolBinario<T> nodo)
		{
			//Punteros necesarios para realizar la rotación
			IArbolBinario<T> Padre = nodo.Padre;
			IArbolBinario<T> P = nodo;
			IArbolBinario<T> Q = nodo.HijoDerecho;
			IArbolBinario<T> R = Q.HijoIzquierdo;
			IArbolBinario<T> B = R.HijoIzquierdo;
			IArbolBinario<T> C = R.HijoDerecho;
			//Si el padre del nodo desequilibrado no es null verifico si el nodo desequilibrado es el derecho
			//o el izquierdo, si el padre es null, significa que es la raiz.
			if (Padre != null)
			{
				if (Padre.HijoDerecho == P)
					Padre.HijoDerecho = R;
				else
					Padre.HijoIzquierdo = R;
			}
			else
			{
				_raiz = R as ArbolBinario<T>;
				_raiz.Padre = null; //para que siga funcionando el padre de la raiz debe ser nulo.
			}
			// Recontrucción del árbol 
			P.HijoDerecho = B;
			Q.HijoIzquierdo = C;
			R.HijoIzquierdo = P;
			R.HijoDerecho = Q;
			//Actualizando a los padres
			R.Padre = Padre;
			P.Padre = Q.Padre = R;
			if (B != null)
				B.Padre = P;
			if (C != null)
				C.Padre = Q;
			// Ajustar valores de Factores de Balance
			switch (R.FactorBalance)
			{
				case -1:
					{
						P.FactorBalance = 0;
						Q.FactorBalance = 1;
					}
					break;
				case 0:
					{
						P.FactorBalance = 0;
						Q.FactorBalance = 0;
					}
					break;
				case 1:
					{
						P.FactorBalance = -1;
						Q.FactorBalance = 0;
					}
					break;
			}
			R.FactorBalance = 0;
		}

		internal void RSD(IArbolBinario<T> nodo)
		{
			//Punteros necesarios para realizar la rotación.
			IArbolBinario<T> Padre = nodo.Padre; //Parde el árbol desequilibrado.
			IArbolBinario<T> P = nodo; //Nodo desequilibrado
			IArbolBinario<T> Q = P.HijoIzquierdo;
			IArbolBinario<T> B = Q.HijoDerecho; //Nodo con altura igual al hijo derecho de P
			//Si el padre del nodo desequilibrado no es null verifico si el nodo desequilibrado es el derecho
			//o el izquierdo, si el padre es null, significa que es la raiz.
			if (Padre != null)
			{
				if (Padre.HijoDerecho == P)
					Padre.HijoDerecho = Q;
				else
					Padre.HijoIzquierdo = Q;
			}
			else
			{
				_raiz = Q as ArbolBinario<T>;
				_raiz.Padre = null; //para que siga funcionando el padre de la raiz debe ser nulo.
			}
			//Reconstruyo el arbol
			P.HijoIzquierdo = B;
			Q.HijoDerecho = P;
			//Actualizando los nuevos padres
			P.Padre = Q;
			if (B != null)
				B.Padre = P;
			Q.Padre = Padre;
			//Ajuste de valores del factor de balance
			if (Q.FactorBalance == 0)
			{
				P.FactorBalance = -1;
				Q.FactorBalance = 1;
			}
			else
			{
				P.FactorBalance = 0;
				Q.FactorBalance = 0;
			}
		}

		internal void RSI(IArbolBinario<T> nodo)
		{
			//Punteros necesarios para realizar la rotación.
			IArbolBinario<T> Padre = nodo.Padre; //Padre del arbol desbalanceado
			IArbolBinario<T> P = nodo; //Arbol desbalanceado con FE 2
			IArbolBinario<T> Q = P.HijoDerecho;
			IArbolBinario<T> B = Q.HijoIzquierdo; //Nodo con altura igual a el hijo izquierdo de P
			//Si el padre del nodo desequilibrado no es null verifico si el nodo desequilibrado es el derecho
			//o el izquierdo, si el padre es null, significa que es la raiz.
			if (Padre != null)
			{
				if (Padre.HijoDerecho == P)
					Padre.HijoDerecho = Q;
				else
					Padre.HijoIzquierdo = Q;
			}
			else
			{
				_raiz = Q as ArbolBinario<T>;
				_raiz.Padre = null; //para que siga funcionando el padre de la raiz debe ser nulo.
			}
			//Reconstruyo el padre, 
			P.HijoDerecho = B;
			Q.HijoIzquierdo = P;
			//Asignando nuevos padres
			P.Padre = Q;
			if (B != null)
				B.Padre = P;
			Q.Padre = Padre;
			//Ajusto valores del Factor de Balance
			if (Q.FactorBalance == 0)
			{
				P.FactorBalance = 1;
				Q.FactorBalance = -1;
			}
			else
			{
				P.FactorBalance = 0;
				Q.FactorBalance = 0;
			}
		}
	}
}
