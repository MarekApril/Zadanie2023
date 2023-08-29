using Soneta.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Soneta.Kadry;
using Soneta.KadryPlace;
using Soneta.Types;
using Rekrutacja.Workers.Template;
using Soneta.Tools;
using Syncfusion.XlsIO.Parser.Biff_Records;

//Rejetracja Workera - Pierwszy TypeOf określa jakiego typu ma być wyświetlany Worker, Drugi parametr wskazuje na jakim Typie obiektów będzie wyświetlany Worker
[assembly: Worker(typeof(TemplateWorker), typeof(Pracownicy))]
namespace Rekrutacja.Workers.Template
{
    public class TemplateWorker
    {
        //Aby parametry działały prawidłowo dziedziczymy po klasie ContextBase
        public class TemplateWorkerParametry : ContextBase
        {
            [Caption("A")]
            [DefaultWidth(300)]
            [Priority(1)]
            public string ZmiennaA { get; set; }
            [Priority(2)]
            [Caption("B")]
            [DefaultWidth(300)]
            public string ZmiennaB { get; set; }
            [Priority(3)]
            [Caption("Data obliczeń")]
            public Date DataObliczen { get; set; }
            [DefaultWidth(100)]
            [Priority(4)]
            [Caption("Operacja")]
            public string MathSign { get; set; }
            public TemplateWorkerParametry(Context context) : base(context)
            {
                this.DataObliczen = DateTime.Now;
            }
        }
        //Obiekt Context jest to pudełko które przechowuje Typy danych, aktualnie załadowane w aplikacji
        //Atrybut Context pobiera z "Contextu" obiekty które aktualnie widzimy na ekranie
        [Context]
        public Context Cx { get; set; }
        //Pobieramy z Contextu parametry, jeżeli nie ma w Context Parametrów mechanizm sam utworzy nowy obiekt oraz wyświetli jego formatkę
        [Context]
        public TemplateWorkerParametry Parametry { get; set; }
        //Atrybut Action - Wywołuje nam metodę która znajduje się poniżej
        [Action("Kalkulator",
           Description = "Prosty kalkulator ",
           Priority = 10,
           Mode = ActionMode.ReadOnlySession,
           Icon = ActionIcon.Accept,
           Target = ActionTarget.ToolbarWithText)]
        public void WykonajAkcje()
        {
            //Włączenie Debug, aby działał należy wygenerować DLL w trybie DEBUG
            DebuggerSession.MarkLineAsBreakPoint();

            //Implementacja lini od 60-65 zawsze zwraca null, dlatego napisałem inny kod.
            ////Pobieranie danych z Contextu
            //Pracownik pracownik = null;
            //if (this.Cx.Contains(typeof(Pracownik))) 
            //{
            //    pracownik = (Pracownik)this.Cx[typeof(Pracownik)];
            //}

            //List of the workers selected.
            var selectedWorkers = (Pracownik[])Cx.Accessor.CurrentContext["Soneta.Kadry.Pracownik[]"];

            //Modyfikacja danych
            //Aby modyfikować dane musimy mieć otwartą sesję, któa nie jest read only
            using (Session nowaSesja = this.Cx.Login.CreateSession(false, false, "ModyfikacjaPracownika"))
            {
                //Otwieramy Transaction aby można było edytować obiekt z sesji
                using (ITransaction trans = nowaSesja.Logout(true))
                {
                    var result = Parametry.ZmiennaA.DoubleParser() + Parametry.ZmiennaB.DoubleParser();
                    foreach (var selectedWorker in selectedWorkers)
                    {
                        //Pobieramy obiekt z Nowo utworzonej sesji
                        var pracownikZSesja = nowaSesja.Get(selectedWorker);
                        //Features - są to pola rozszerzające obiekty w bazie danych, dzięki czemu nie jestesmy ogarniczeni to kolumn jakie zostały utworzone przez producenta
                        pracownikZSesja.Features["DataObliczen"] = this.Parametry.DataObliczen;
                        pracownikZSesja.Features["Wynik"] = result;

                        //Zatwierdzamy zmiany wykonane w sesji
                        trans.CommitUI();
                    }
                }
                //Zapisujemy zmiany
                nowaSesja.Save();
            }
        }
    }

    public static class Extentions
    {
        public static double DoubleParser(this string signs)
        {
            var lista = new List<double>();
            foreach (var sign in signs)
            {
                if (sign == 48)
                {
                    lista.Add(0);
                }
                if (sign == 49)
                {
                    lista.Add(1);
                }
                if (sign == 50)
                {
                    lista.Add(2);
                }
                if (sign == 51)
                {
                    lista.Add(3);
                }
                if (sign == 52)
                {
                    lista.Add(4);
                }
                if (sign == 53)
                {
                    lista.Add(5);
                }
                if (sign == 54)
                {
                    lista.Add(6);
                }
                if (sign == 55)
                {
                    lista.Add(7);
                }
                if (sign == 56)
                {
                    lista.Add(8);
                }
                if (sign == 57)
                {
                    lista.Add(9);
                }
            }

            var listtt = lista.ToArray();

            double combine = 0;

            for (int i = 0; i < listtt.Length; i++)
            {
                combine += listtt[i] * Convert.ToDouble(System.Math.Pow(10, listtt.Length - i - 1));
            }

            return combine;
        }
    }
}