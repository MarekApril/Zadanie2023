using ICSharpCode.NRefactory.CSharp;
using Soneta.Business;
using Soneta.CRM;
using Soneta.Handel;
using Soneta.Kadry;
using Soneta.Magazyny;
using Soneta.Types;
using Soneta.Zadania;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szkolenie.Initializer;
using Szkolenie.Workers.UstawCeche;

[assembly: Worker(typeof(UstawCecheWorkerWorker), typeof(DokHandlowe))]
namespace Szkolenie.Workers.UstawCeche
{
    class UstawCecheWorkerWorker
    {
        [Context]
        public UstawCecheWorkerWorkerParams Parametry { get; set; }

        [Context]
        public Context Context { get; set; }

        [Action("Ustaw cechę",
         Mode = ActionMode.ReadOnlySession,
         Target = ActionTarget.ToolbarWithText,
         Icon = ActionIcon.Book)]
        // Po kliknięciu w worker zawsze jest wywoływana PIERWSZA METODA POD ATRYBUTEM ACTION w tym wyapdku to UstawCecheNaKorektach
        public void UstawCecheNaKorektach()
        {

			DokumentHandlowy[] dokumenty = this.Context[typeof(DokumentHandlowy[]), false] as DokumentHandlowy[];
            using (Session nowaSesja = this.Context.Session.Login.CreateSession(false, false, "Zmiana Dokumentu"))
            {
                DokumentHandlowy dokument = nowaSesja.Get(dokumenty.FirstOrDefault());
				using (ITransaction trans = nowaSesja.Logout(true))
                {
					dokument.Stan = StanDokumentuHandlowego.Bufor;

                    trans.Commit();
                }

                nowaSesja.Saving += new Delegat(dokument).Session_Saved;
				nowaSesja.Save();
            }

			UstawCecheWorkerWorkerBL ustawCecheWorker = new UstawCecheWorkerWorkerBL(this.Context, this.Parametry);
            ustawCecheWorker.UstawCecheAction();
        }

		private void NowaSesja_Savinga(object sender, EventArgs e)
		{
			Session sesja = sender as Session;
			sesja.Saved -= this.NowaSesja_Saving;
		}
		private void NowaSesja_Saving(object sender, EventArgs e)
        {
            Session sesja = sender as Session;
            sesja.Saving -= this.NowaSesja_Saving;

        }

        public static bool IsVisibleUstawCecheNaKorektach(Context context)
        {
            var asddsd = context.Session.Login.Operator;
            return true;
        }
    }
}