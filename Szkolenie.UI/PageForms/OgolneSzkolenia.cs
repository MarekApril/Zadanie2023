using Soneta.Business;
using Soneta.Business.App;
using Soneta.Handel;
using Szkolenie.Business;
using Szkolenie.UI.PageForms;

[assembly: Worker(typeof(OgolneSzkolenia))]
namespace Szkolenie.UI.PageForms
{
    public class OgolneSzkolenia
    {
        [Context]
        public Session Session { get; set; }

        [Context]
        public Login Login { get; set; }

        [Context]
        public DokumentHandlowy Dokument { get; set; }

        public bool WidocznoscZakladki
        {
            get
            {
                return Dokument.Kategoria == KategoriaHandlowa.KorektaSprzedaży;
            }
        }

        public View PozycjeSzkolenia
        {
            get
            {
                return SzkolenieModule.GetInstance(Session).PozycjeSzkolenie.CreateView();
            }
        }
    }
}
