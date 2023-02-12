var searchInput = document.querySelector("#search-input");

// Add an event listener to the search input field
searchInput.addEventListener("input", function (event) {
    // Get the search term from the input field
    var searchTerm = event.target.value;

    // Filter the table data based on the search term
    table.setFilter("last_name", "like", searchTerm);
});
//define data array
var tabledata = [
    {
        id: 1,
        image: "https://via.placeholder.com/80",
        last_name: "Smith",
        first_name: "John",
        job_title: "Software Engineer",
        base_salary: 80000,
        bonus_percentage: 10,
        bonus_amount: 8000,
        total_gross: 88000,
        taxable_gross: 75000,
        tax_amount: 15000,
        cas_amount: 6000,
        cass_amount: 8000,
        deductions: 5000,
        net_pay: 64000
    }];

var table = new Tabulator("#employee-table", {
    data: tabledata,           //load row data from array
    layout: "fitColumns",      //fit columns to width of table
    responsiveLayout: "hide",  //hide columns that dont fit on the table
    addRowPos: "top",          //when adding a new row, add it to the top of the table
    history: true,             //allow undo and redo actions on the table
    pagination: "local",       //paginate the data
    paginationSize: 7,         //allow 7 rows per page of data
    paginationCounter: "rows", //display count of paginated rows in footer
    movableColumns: true,      //allow column order to be changed
    initialSort: [             //set the initial sort order of the data
        { column: "name", dir: "asc" },
    ],
    columnDefaults: {
        tooltip: true,         //show tool tips on cells
    },
    height: "100%",
    columns:[
        { title: "Nr. crt", field: "id", sorter: "number" },
        { title: "Poza", field: "image", formatter: "image", width: 80, align: "center", headerSort: false },
        { title: "Nume", field: "last_name" },
        { title: "Prenume", field: "first_name" },
        { title: "Functie", field: "job_title" },
        { title: "Actiuni", field: "actions", headerSort: false },
        { title: "Salar de baza", field: "base_salary", visible: false },
        { title: "Spor %", field: "bonus_percentage", visible: false },
        { title: "Premii brute", field: "bonus_amount", visible: false },
        { title: "Total brut", field: "total_gross", visible: false },
        { title: "Brut impozabil", field: "taxable_gross", visible: false },
        { title: "Impozit", field: "tax_amount", visible: false },
        { title: "CAS", field: "cas_amount", visible: false },
        { title: "CASS", field: "cass_amount", visible: false },
        { title: "Retineri", field: "deductions", visible: false },
        { title: "Virat card", field: "net_pay", visible: false },
    ],
});