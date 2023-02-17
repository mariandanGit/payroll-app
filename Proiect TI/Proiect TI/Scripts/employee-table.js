function deleteRow(row) {
    if (row) {
        var id = row.getData().Id;
        if (confirm("Are you sure you want to delete this employee?")) {
            $.ajax({
                type: "POST",
                url: "/Database/DeleteEmployee?id=" + id,
                contentType: 'application/json; charset=utf-8',
                success: function () {
                    table.setData("/Database/GetEmployees");
                },
                error: function (xhr, status, error) {
                    console.log(error);
                }
            });
        }
    }
}

function openModal(rowData) {

    $('#employee-modal-id').val(rowData.Id);
    $('#employee-modal-nume').val(rowData.Nume);
    $('#employee-modal-prenume').val(rowData.Prenume);
    $('#employee-modal-functie').val(rowData.Functie);
    $('#employee-modal-salar').val(rowData.SalarBaza);
    $('#employee-modal-spor').val(rowData.Spor);
    $('#employee-modal-premii-brute').val(rowData.PremiiBrute);
    $('#employee-modal-total-brut').val(rowData.TotalBrut).prop('disabled', true);
    $('#employee-modal-brut-impozabil').val(rowData.BrutImpozabil).prop('disabled', true);
    $('#employee-modal-impozit').val(rowData.Impozit).prop('disabled', true);
    $('#employee-modal-cas').val(rowData.Cas).prop('disabled', true);
    $('#employee-modal-cass').val(rowData.Cass).prop('disabled', true);
    $('#employee-modal-retineri').val(rowData.Retineri);
    $('#employee-modal-virat-card').val(rowData.ViratCard).prop('disabled', true);

    $('#employee-modal').modal('show');
}
var filterField = document.getElementById("filter-field");
var searchInput = document.getElementById("search-input");
var clearButton = document.getElementById("filter-clear");

searchInput.addEventListener("input", function (event) {
    var filterVal = filterField.options[filterField.selectedIndex].value;
    var searchTerm = event.target.value;
    if (filterVal) {
        table.setFilter([
            { field: filterVal, type: "like", value: searchTerm }
        ]);
    }
});

clearButton.addEventListener("click", function () {
    searchInput.value = "";
    filterField.selectedIndex = 0;
    table.clearFilter();
});

var table = new Tabulator("#employee-table", {
    ajaxURL: "/Database/GetEmployees",
    layout: "fitDataStretch",      //fit columns to width of table
    addRowPos: "top",          //when adding a new row, add it to the top of the table
    pagination: "local",       //paginate the data
    paginationSize: 20,        //allow 15 rows per page of data
    placeholder: "Lipsa date",
    paginationCounter: "rows", //display count of paginated rows in footer
    initialSort: [             //set the initial sort order of the data
        { column: "Nume", dir: "asc" },
    ],
    columnDefaults: {
        tooltip: true,         //show tool tips on cells
    },
    height: "100%",
    columns:[
        { title: "Nr. crt", field: "Id", sorter: "number", vertAlign: "middle", width: 100 },
        {
            title: "Poza",
            field: "Poza",
            width: 100,
            formatter: function (cell, formatterParams, onRendered) {

                var img = document.createElement("img");
                img.src = "/Database/GetEmployeeImage?id=" + cell.getRow().getData().Id;
                img.style.width = "70px";
                img.style.height = "70px";
                img.style.objectFit = "cover";
                img.style.borderRadius = "50%";

                img.onerror = function () {
                    img.src = "/Images/default-user.png";
                };

                return img;
            },
            vertAlign: "middle",
            headerSort: false,
        },
        { title: "Nume", field: "Nume", vertAlign: "middle", width: 200 },
        { title: "Prenume", field: "Prenume", vertAlign: "middle", width: 200},
        { title: "Functie", field: "Functie", vertAlign: "middle", width: 200},
        {
            title: "Actiuni",
            vertAlign: "middle",
            formatter: function (cell, formatterParams, onRendered) {

                var editButton = document.createElement("button");
                editButton.classList.add("btn", "btn-primary");
                editButton.style.marginRight = "5px";
                editButton.innerHTML = "Editare";
                editButton.addEventListener("click", function (e) {
                    var rowData = cell.getRow().getData();

                    var img = document.getElementById("employee-image");
                    img.src = "/Database/GetEmployeeImage?id=" + cell.getRow().getData().Id;

                    img.onerror = function () {
                        img.src = "/Images/default-user.png";
                    };

                    openModal(rowData);
                });

                var deleteButton = document.createElement("button");
                deleteButton.classList.add("btn", "btn-danger");
                deleteButton.style.marginRight
                deleteButton.innerHTML = "Stergere";
                deleteButton.addEventListener("click", function (e) {
                    var row = cell.getRow();
                    if (row) {
                        var id = row.getData().Id;
                        if (confirm("Sigur doriti sa stergeti acest angajat?")) {
                            $.ajax({
                                type: "POST",
                                url: "/Database/DeleteEmployee?id=" + id,
                                contentType: 'application/json; charset=utf-8',
                                success: function () {
                                    table.setData("/Database/GetEmployees"); 
                                },
                                error: function (xhr, status, error) {
                                    console.log(error); 
                                }
                            });
                        }
                    }
                });
                var container = document.createElement("div");
                container.appendChild(editButton);
                container.appendChild(deleteButton);

                return container;
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
