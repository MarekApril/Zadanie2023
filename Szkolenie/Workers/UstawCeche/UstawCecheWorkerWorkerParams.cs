using Soneta.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace Szkolenie.Workers.UstawCeche
{
    public class UstawCecheWorkerWorkerParams : ContextBase
    {
        public string Parametr1 { get; set; }
        public UstawCecheWorkerWorkerParams(Context context) : base(context)
        {
        }

    }
}
