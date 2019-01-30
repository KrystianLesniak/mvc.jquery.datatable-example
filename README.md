# mvc.jquery.datatable-example

It's example of DataTables builded in ASP.NET MVC using library by [mcintyre321](https://github.com/mcintyre321). Web app is based on .Net Framework ver. 4.5.2

This site is based on [mvc.jquery.datatables](https://github.com/mcintyre321/mvc.jquery.datatables) by [mcintyre321](https://github.com/mcintyre321).

### Some code is explained by comments in solutions

Firstly, ensure that you have jquery version 3+ and jquery ui installed in your project.
Install packages for this library
```
Install-Package Mvc.JQuery.DataTables
Install-Package Mvc.JQuery.DataTables.Templates
```

Now we need to bind model during startup of project
```csharp
ModelBinders.Binders.Add(typeof(DataTablesParam), new DataTablesModelBinder());
```

Let's create ViewModel of our table
*/Models/CustomerTableRowViewModel.cs*
```csharp
using System;
using Mvc.JQuery.DataTables;
using Mvc.JQuery.DataTables.Models;

namespace JqueryDT.Models
{
    public class CustomerTableRowViewModel
    {
        [DataTables(DisplayName = "Customer name")] // We can set custom title of heading
        public string Name { get; set; }

        [DataTables(SortDirection = SortDirection.Ascending, Order = 0)]
        public int Id { get; set; }

        [DataTables(DisplayName = "E-Mail",
            Searchable = true)] // searchable declare if column can be searchable in filtering
        public string Email { get; set; }

        [DataTables(Visible = false)] // Visible decalres column to (not) be visible in DataTable
        public bool IsAdmin { get; set; }

        [DataTables(Sortable = false, Visible = false)]
        public bool IsLoyal { get; set; }

        [DataTables(Visible = false)] public decimal Price { get; set; }

        [DataTables(Width = "100px")] // Width sets custom width of column
        public string Position { get; set; }

        [DataTablesFilter(DataTablesFilterType.DateRange)]
        public DateTime? Hired { get; set; }

        [DataTablesExclude]
        public string ThisColumnIsExcluded // We can totally exclude property from DataTable
            => "Hello World!";

        [DataTables(Sortable = false, Searchable = false)] // Sortable declare that column won't be sortable with arrows
        [DataTablesFilter(DataTablesFilterType.None)]
        public string Thumb { get; set; }

        public string Actions { get; set; }
    }
}
```
Now let's head to our controller
*/Controllers/HomeController.cs*
```csharp
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
```
I think that all comments are self-explanationary

Include some bundles to our View
```csharp
            bundles.Add(new ScriptBundle("~/bundles/jquery-datatables-js").Include(
                "~/Content/jquery.dataTables.js", // Standard DataTable
                "~/Content/jquery-datatables-column-filter/media/js/jquery.dataTables.columnFilter.js", // These ones if you want to use the column filters
                "~/Content/jquery-datatables-column-filter/jquery-ui-timepicker-addon.js", // And these if you want date time pickers in the filters
                "~/Content/dataTables.colVis.min.js")); // And these if you want to use column visibility

            bundles.Add(new StyleBundle("~/bundles/jquery-datatables-css").Include(
                "~/Content/jquery.dataTables.css", // Standard DataTable
                "~/Content/jquery-datatables-column-filter/media/js/jquery.dataTables.columnFilter.css", // These ones if you want to use the column filters
                "~/Content/jquery-datatables-column-filter/jquery-ui-timepicker-addon.css", // And these if you want date time pickers in the filters
                "~/Content/dataTables.colVis.css")); // And these if you want to use column visibility
```

And finally our View:
*Views/Home/Index.cshtml*
```csharp
@{
    ViewBag.Title = "Home Page";
}
@model Mvc.JQuery.DataTables.DataTableConfigVm
@Styles.Render("~/bundles/jquery-datatables-css")

@Scripts.Render("~/bundles/jquery")
@Scripts.Render("~/bundles/jquery-ui")
@Scripts.Render("~/bundles/jquery-datatables-js")

<br/>
@Html.Partial("DataTable", Model)
```
It renders ready-to-use partial view provided from library

![img](https://i.imgur.com/GbDTKLy.png)
