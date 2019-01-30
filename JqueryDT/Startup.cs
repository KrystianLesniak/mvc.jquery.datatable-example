using System.Web.Mvc;
using JqueryDT;
using Microsoft.Owin;
using Mvc.JQuery.DataTables;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace JqueryDT
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ModelBinders.Binders.Add(typeof(DataTablesParam), new DataTablesModelBinder());
        }
    }
}