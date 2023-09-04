using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szkolenie.Business;
using Szkolenie.Tabele.Rows;

namespace Szkolenie.Tabele
{
    public class PozycjeSzkolenie : SzkolenieModule.PozycjaSzkoleniaTable
    {
        protected override string[] GetDefaultLocatorFields()
        {
            string locator = nameof(PozycjaSzkolenia.Nazwisko);
            var locators = base.GetDefaultLocatorFields().ToList();
            if (!locators.Contains(locator))
            {
                locators.Add(locator);
            }
            return locators.ToArray();
        }
    }
}
