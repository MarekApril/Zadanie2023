using Soneta.Business;
using Soneta.CRM;
using Soneta.Handel;
using Soneta.Kadry;
using Soneta.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szkolenie.Initializer;

[assembly: ProgramInitializer(typeof(SzkolenieInitializer))]
namespace Szkolenie.Initializer
{
    class SzkolenieInitializer : IProgramInitializer
    {
        public void Initialize()
        {
            HandelModule.DokumentHandlowySchema.AddKontrahentAfterEdit(WykonajZmianaKontrahent);
		}

        protected void WykonajZmianaKontrahent(HandelModule.DokumentHandlowyRow row)
        {
            if (row is DokumentHandlowy dokument)
            {
                if (string.IsNullOrEmpty(dokument?.Kontrahent?.NIP))
                    throw new Exception("Jak bez NIP Kontrahent.");

                dokument.Session.Saved += new Delegat(dokument).Session_Saved;
            }
        }
    }
}
