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