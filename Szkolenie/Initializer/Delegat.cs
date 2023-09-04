using Soneta.Business;
using Soneta.Handel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szkolenie.Initializer
{
    class Delegat
    {
        private DokumentHandlowy dokument;
        public Delegat(DokumentHandlowy dokument)
        {
            this.dokument = dokument;
        }
        public void Session_Saved(object sender, EventArgs e)
        {
            Session sesja = sender as Session;
            if (sesja == null)
                return;

            sesja.Saved -= Session_Saved;

            using (Session nowaSesja = sesja.Login.CreateSession(false, false, "USstawOpis"))
            {
                using (ITransaction trans = nowaSesja.Logout(true))
                {
                    DokumentHandlowy dokSesja = nowaSesja.Get(dokument);
                    dokSesja.Features["OpisFaktury"] = "Zmiana kontrahenta";

                    trans.CommitUI();
                }

                nowaSesja.Save();
            }
        }
    }
}
