using Soneta.Business;
using Soneta.Business.UI;
using Soneta.Handel;
using System;
using Szkolenie.Handel.Workers;

[assembly: Worker(typeof(MyWorker1Worker), typeof(DokumentHandlowy))]

namespace Szkolenie.Handel.Workers
{
	public class MyWorker1Worker
	{

		[Context]
		public MyWorker1WorkerParams @params
		{
			get;
			set;
		}


		[Action("MyWorker1Worker/ToDo", Mode = ActionMode.SingleSession)]
		public MessageBoxInformation ToDo()
		{
			DokumentHandlowy asd;

			return new MessageBoxInformation("Potwierdzasz wykonanie operacji ?")
			{
				Text = "Opis operacji",
				YesHandler = () =>
				{
					using (var t = @params.Session.Logout(true))
					{
						t.Commit();
					}
					return "Operacja została zakończona";
				},
				NoHandler = () => "Operacja przerwana"
			};

		}
	}
}
