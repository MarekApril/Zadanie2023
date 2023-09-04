using Soneta.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szkolenie.Handel.Workers
{
	public class MyWorker1WorkerParams : ContextBase
	{
		public MyWorker1WorkerParams(Context context) : base(context)
		{
		}

		// TODO -> Poniższy parametr dodany dla celów poglądowych. Należy usunąć.
		public string Parametr1 { get; set; }
	}
}
