<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructuraDatos
{
	class Nodo<T>
	{
		public T Value { get; set; }
		public Nodo<T> siguiente { get; set; }
		public Nodo<T> anterior { get; set; }

		public Nodo(T value)
		{
			Value = value;
			siguiente = null;
			anterior = null;
		}
	}
}
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructuraDatos
{
	class Nodo<T>
	{
		public T Value { get; set; }
		public Nodo<T> siguiente { get; set; }
		public Nodo<T> anterior { get; set; }

		public Nodo(T value)
		{
			Value = value;
			siguiente = null;
			anterior = null;
		}
	}
}
>>>>>>> ebbe336cd8265ef346fc6f93b7d4436e3d862170
