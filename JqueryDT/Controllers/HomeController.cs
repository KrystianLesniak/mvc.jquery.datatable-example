using System;
using System.Linq;
using System.Web.Mvc;
using JqueryDT.Enums;
using JqueryDT.Models;
using JqueryDT.Repositories;
using Mvc.JQuery.DataTables;

namespace JqueryDT.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var getDataUrl =
                Url.Action(nameof(GetUsers)); // We are getting here url of action which returns DataTableResult

            var model = DataTablesHelper.DataTableVm<CustomerTableRowViewModel>("idForTableElement", getDataUrl);
            model.Filter = true; // This enables functionality to filter columns

            model.UseColumnFilterPlugin = true; // Thanks this filter is displayed for every column seperately

            model.FilterOn("Position", new {sSelector = "#custom-filter-placeholder-position"})
                .Select(Enum.GetNames(typeof(PositionType))); // PositionType is enum so we won't be displaying text for filtering. Instead we are creating custom filter which gets array of string as argument so we have to transform it to string.

            model.FilterOn("Actions").None(); // Besides disabling filtering by attribute in viewmodel we can also do it like that

            return View(model);
        }

        public DataTablesResult<CustomerTableRowViewModel> GetUsers(DataTablesParam dataTableParam)
        {
            var _customerRepository = new CustomerRepository();

            IQueryable<Customer> customers =
                _customerRepository
                    .GetCustomers(); //  You are taking IQueryable from your database, here I used fake mocked database that returns
            var userViews = customers.Select(c =>
                new CustomerTableRowViewModel //  We need to transform Iqueryable into RowViewModel we created earlier
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    Position = c.Position.ToString(),
                    Hired = c.Hired,
                    IsAdmin = c.IsAdmin,
                    Price = c.Salary / 2,
                    IsLoyal = c.Position == PositionType.Premium ? true : false,
                    Thumb = "https://randomuser.me/api/portraits/thumb/men/" + c.Id + ".jpg"
                });
            return DataTablesResult.Create(userViews, dataTableParam, rowViewModel => new
            {
                Thumb = "<img src='" + rowViewModel.Thumb + "' />",
                Actions = "<button id='Delete' value='" + rowViewModel.Id + "'>Delete</button>" //Here we can transform rowViewModel to our likes. I presented how we can create action for every column
            });
        }
    }
}