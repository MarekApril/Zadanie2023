using Soneta.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szkolenie.Business;
using Szkolenie.Tabele.Rows;

[assembly: NewRow(typeof(PozycjaSzkolenia))]
namespace Szkolenie.Tabele.Rows
{
    public class PozycjaSzkolenia : SzkolenieModule.PozycjaSzkoleniaRow
    {

    }
}
