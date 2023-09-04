using Soneta.Business;
using Soneta.Business.Licence;
using Soneta.Business.UI;
using Soneta.Types;
using System;
using System.ComponentModel;
using System.Linq;
using Szkolenie.Business;
using Szkolenie.UI.ViewInfos;

[assembly: FolderView("Handel/Pozycje Szkolenia",
    Priority = 10000,
    Description = "PozcjeSzkolenia",
    TableName = "PozycjeSzkolenie",
    ViewType = typeof(PozycjeSzkolenieViewViewInfo)
)]

namespace Szkolenie.UI.ViewInfos
{
    public class PozycjeSzkolenieViewViewInfo : ViewInfo
    {
        public PozycjeSzkolenieViewViewInfo()
        {
            ResourceName = "PozycjeSzkolenieView";
            InitContext += PozycjeSzkolenieViewViewInfo_InitContext;
            CreateView += PozycjeSzkolenieViewViewInfo_CreateView;
        }

        void PozycjeSzkolenieViewViewInfo_InitContext(object sender, ContextEventArgs args)
        {
            //Pakujeme do context co chcemy
            args.Context.Set(new WParams(args.Context));
        }

        void PozycjeSzkolenieViewViewInfo_CreateView(object sender, CreateViewEventArgs args)
        {
            PozycjeSzkolenieViewViewInfo.WParams parameters;
            if (!args.Context.Get(out parameters))
                return;
            args.View = ViewCreate(parameters);
            args.View.AllowEdit = true;
            args.View.AllowNew = true;
            args.View.AllowRemove = true;
        }

        public class WParams : ContextBase
        {
            private string nazwisko;
            public string Nazwisko 
            {
                get
                {
                    return this.nazwisko;
                }
                set
                {
                    this.nazwisko = value;
                    OnChanged();
                }
            }

            public WParams(Context context) : base(context)
            {
            }
        }

        protected View ViewCreate(WParams pars)
        {
            View view = null;
            if (string.IsNullOrEmpty(pars.Nazwisko))
            {
                view = SzkolenieModule.GetInstance(pars).PozycjeSzkolenie.CreateView();
            }
            else
            {
                view = SzkolenieModule.GetInstance(pars).PozycjeSzkolenie.WgNazwisko[pars.Nazwisko].CreateView();
            }

            view.AllowEdit = true;
            view.AllowNew = true;
            view.AllowRemove = true;
            view.AllowUpdate = true;
            return view;
        }
    }
}
