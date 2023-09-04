using Soneta.Business;
using Soneta.CRM;
using Soneta.Handel;
using Soneta.Kadry;
using Soneta.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szkolenie.Workers.UstawCeche
{
    class UstawCecheWorkerWorkerBL
    {
        private Context context;
        private UstawCecheWorkerWorkerParams parametry;
        public UstawCecheWorkerWorkerBL(Context cx, UstawCecheWorkerWorkerParams parametry)
        {
			this.context = cx;
            this.parametry = parametry;
        }
        public void UstawCecheAction()
        {
            Kontrahent kontrahent = CRMModule.GetInstance(this.context).Kontrahenci.WgEuVAT["8733213434"].CreateView().Cast<Kontrahent>().FirstOrDefault();

            RowCondition rc = new FieldCondition.Equal("Kategoria", KategoriaHandlowa.KorektaSprzedaży);
            rc &= new FieldCondition.GreaterEqual("Dostawa.Termin", new Date(2023, 3, 1));
            rc &= new FieldCondition.LessEqual("Dostawa.Termin", new Date(2023, 3, 31));
            rc &= new FieldCondition.In("Stan", new object[] { StanDokumentuHandlowego.Zatwierdzony, StanDokumentuHandlowego.Zablokowany });
            rc &= new FieldCondition.Equal("Features.Algorytm", "Test");

            var asdasd = HandelModule.GetInstance(this.context).DokHandlowe.WgKontrahent[kontrahent][rc].CreateView();
            asdasd.Condition &= new FieldCondition.Equal("Features.Algorytm", "Test");
        }

        //Pobiranie faktury z Context
        public void UtworzFaktureZWZ()
        {
            DokumentHandlowy dokument = this.context[typeof(DokumentHandlowy)] as DokumentHandlowy;

            using (Session sesja = this.context.Session.Login.CreateSession(false, false, "UstawCeche"))
            {
                using (ITransaction trans = sesja.Logout(true))
                {
                    DokumentHandlowy dokumentZSesji = sesja.Get(dokument);

                    dokumentZSesji.Features["OpisFaktury"] = this.parametry.Parametr1;

                    trans.CommitUI();
                }

                sesja.Save();
            }
        }
    }
}