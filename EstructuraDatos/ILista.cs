<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructuraDatos
{
	interface ILista<T>
	{
		void Agregar(T item);

		T Buscar(T item);

		int BuscarIndice(T item);

		bool Existe(T item);

		void Eliminar(T item);

		T Remover(int index);

		void Insertar(int index, T item);

		int Longitud { get; }

		void Limpiar();

		T this[int index] { get; }

		T Elemento(int index);

		bool ListaVacia { get; }
	}
}
//comentario
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructuraDatos
{
	interface ILista<T>
	{
		void Agregar(T item);

		T Buscar(T item);

		int BuscarIndice(T item);

		bool Existe(T item);

		void Eliminar(T item);

		T Remover(int index);

		void Insertar(int index, T item);

		int Longitud { get; }

		void Limpiar();

		T this[int index] { get; }

		T Elemento(int index);

		bool ListaVacia { get; }
	}
}

>>>>>>> ebbe336cd8265ef346fc6f93b7d4436e3d862170
