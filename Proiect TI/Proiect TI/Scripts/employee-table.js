var searchInput = document.querySelector("#search-input");

// Add an event listener to the search input field
searchInput.addEventListener("input", function (event) {
    // Get the search term from the input field
    var searchTerm = event.target.value;

    // Filter the table data based on the search term
    table.setFilter("last_name", "like", searchTerm);
});
//define data array

var table = new Tabulator("#employee-table", {
    ajaxURL: "/Database/GetEmployees",
    layout: "fitColumns",      //fit columns to width of table
    responsiveLayout: "hide",  //hide columns that dont fit on the table
    addRowPos: "top",          //when adding a new row, add it to the top of the table
    pagination: "local",       //paginate the data
    paginationSize: 20,        //allow 15 rows per page of data
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
        { title: "Nr. crt", field: "Id", sorter: "number" },
        { title: "Poza", field: "Poza", formatter: "image", width: 80, align: "right", headerSort: false,  },
        { title: "Nume", field: "Nume" },
        { title: "Prenume", field: "Prenume" },
        { title: "Functie", field: "Functie" },
        {
            title: "Actiuni",
            align: "center",
            formatter: function (cell, formatterParams, onRendered) {
                var editBtn = "<button class='btn btn-xs btn-primary' onclick='editRow(" + cell.getRow().getIndex() + ")'>Edit</button>";
                var delBtn = "<button class='btn btn-xs btn-danger' onclick='deleteRow(" + cell.getRow().getIndex() + ")'>Delete</button>";
                return editBtn + " " + delBtn;
            },
            headerSort: false
        },        
        { title: "Salar de baza", field: "SalarBaza", visible: false },
        { title: "Spor %", field: "Spor", visible: false },
        { title: "Premii brute", field: "PremiiBrute", visible: false },
        { title: "Total brut", field: "TotalBrut", visible: false },
        { title: "Brut impozabil", field: "BrutImpozabil", visible: false },
        { title: "Impozit", field: "Impozit", visible: false },
        { title: "CAS", field: "Cas", visible: false },
        { title: "CASS", field: "Cass", visible: false },
        { title: "Retineri", field: "Retineri", visible: false },
        { title: "Virat card", field: "ViratCard", visible: false },
    ]
});