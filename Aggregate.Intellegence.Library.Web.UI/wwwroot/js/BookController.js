function BookController() {
    var self = this;
    self.init = function () {
        $('#addEditBookModal').modal({ backdrop: 'static', keyboard: false });
        var booksGrid = $('#BooksGrid').DataTable({
            responsive: false,
            serverSide: false,
            ajax: {
                url: '/Book/FetchAllBooks',
                type: 'GET',
                dataSrc: 'data'
            },
            columns: [
                { data: 'Title' },
                { data: 'Author' },
                { data: 'Publisher' },
                { data: 'PublishYear' },
                { data: 'Genre' },
                { data: 'Language' },
                { data: 'PageCount' },
                {
                    data: null,
                    render: function (data, type, row) {
                        var icons = '';
                        icons += '<i class="fas fa-edit edit-icon icon-padding-right" data-id="' + row.BookId + '" style="padding: 5px; font-size: 20px; color: red;"></i>'
                            +
                            '<i class="fas fa-trash delete-icon icon-padding-right" data-id="' + row.BookId + '" style="padding: 5px; font-size: 20px; color: red;"></i>'
                            +
                            '<i class="fas fa-file-download download-icon icon-padding-right" data-id="' + row.BookId + '" style="padding: 5px; font-size: 20px; color: red;"></i>';
                                                  

                        return icons;
                    }
                }
            ],
            responsive: false,
            serverSide: false,
            "order": [[0, "asc"]],
            "pageLength": 20,
            "scrollX": true,
            "scrollCollapse": true
        });
        $(document).on("click", "#addBook", function () {
            self.clearInputs();
            $("#addEditBookModalLabel").text("Add Book");
            $("#addEditBookModal").modal("show");
        });
        $(document).on("click", ".edit-icon", function () {
            $(".se-pre-con").show();
            var data = $(this);
            var row = data.closest('tr');
            var dataItem = booksGrid.row(row).data();
            $("#BookId").val(dataItem.BookId);
            $("#Title").val(dataItem.Title);
            $("#Author").val(dataItem.Author);
            $("#Publisher").val(dataItem.Publisher);
            $("#PublishYear").val(dataItem.PublishYear);
            $("#Genre").val(dataItem.Genre);
            $("#Language").val(dataItem.Language);
            $("#PageCount").val(dataItem.PageCount);
            $("#Description").val(dataItem.Description);
            $("#addEditBookModalLabel").text("Edit Book");
            $("#addEditBookModal").modal("show");
            $(".se-pre-con").hide();
        });
        $(document).on("click", "#SaveBook", function () {
            $(".se-pre-con").show();
            var bookId = $("#BookId").val();
            var title = $("#Title").val();
            var author = $("#Author").val();
            var publisher = $("#Publisher").val();
            var publishYear = $("#PublishYear").val();
            var genre = $("#Genre").val();
            var language = $("#Language").val();
            var pageCount = $("#PageCount").val();
            var description = $("#Description").val();

            var books = {
                BookId: bookId ? parseInt(bookId):0,
                Title: title,
                Author: author,
                Publisher: publisher,
                PublishYear: publishYear,
                Genre: genre,
                Language: language,
                PageCount: pageCount,
                Description: description,
                CreatedOn: new Date(),
                CreatedBy: 0,
                ModifiedOn: new Date(),
                ModifiedBy: 0,
                IsActive: true
            };
            $.ajax({
                url: '/Book/InsertOrUpdateBook',
                data: JSON.stringify(books),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processData: true,
                cache: false,
                success: function (response) {
                    $('#addEditBookModal').modal('hide');
                    self.clearInputs();
                    booksGrid.ajax.reload();
                    $(".se-pre-con").hide();
                },
                error: function (xhr, status, error) {
                    console.error(error);
                }
            })
        })
        $(document).on("click", ".delete-icon", function () {

            $(".se-pre-con").show();
            var data = $(this);
            var row = data.closest('tr');
            var dataItem = booksGrid.row(row).data();
            var bookId = dataItem.BookId;
            var title = dataItem.Title;
            var author = dataItem.Author;
            var publisher = dataItem.Publisher;
            var publishYear = dataItem.PublishYear;
            var genre = dataItem.Genre;
            var language = dataItem.Language;
            var pageCount = dataItem.PageCount;
            var description = dataItem.Description;

            var deleteBook = {
                BookId: bookId ? parseInt(bookId) : 0,
                Title: title,
                Author: author,
                Publisher: publisher,
                PublishYear: publishYear,
                Genre: genre,
                Language: language,
                PageCount: pageCount,
                Description: description,
                CreatedOn: new Date(),
                CreatedBy: 0,
                ModifiedOn: new Date(),
                ModifiedBy: 0,
                IsActive: false

            };
            $.ajax({
                url: '/Book/InsertOrUpdateBook',
                data: JSON.stringify(deleteBook),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processData: true,
                cache: false,
                success: function (response) {
                    booksGrid.ajax.reload();
                    $(".se-pre-con").hide();
                },
                error: function (xhr, status, error) {
                    console.error(error);
                }
            })
        })
        $(document).on("click", ".download-icon", function () {
            $(".se-pre-con").show();
            var data = $(this);
            var row = data.closest('tr');
            var dataItem = booksGrid.row(row).data();

            var pdf = new jspdf.jsPDF();

            var logoImg = new Image();
            logoImg.src = '../../assets/img/Aggregate_Inttelegence.png';
            pdf.addImage(logoImg, 'PNG', 0, 0, 60, 50); // Adjust coordinates and dimensions
            pdf.text('Date:' + new Date().toLocaleDateString(), 160, 15);
            pdf.text('Place:Hyderabad', 160, 25)
            pdf.text('Title: ' + dataItem.Title, 90, 60)
            pdf.text('Author : ' + dataItem.Author, 80, 70);
            pdf.text('Published By : ' + dataItem.Publisher, 65, 80);
            pdf.text('Published In : ' + dataItem.PublishYear, 80, 90);
            pdf.text(dataItem.Description, 15, 120).setFontSize(10);
            pdf.text('aggregateintelligence.com', 8, 295);
            pdf.text('info@aggregateintelligence.com', 153, 295);
            pdf.rect(7, 5, pdf.internal.pageSize.width - 13, pdf.internal.pageSize.height - 10, 'S');
            pdf.save('AI_Library'+new Date() + '.pdf');
        })
    }
    self.clearInputs = function () {
        $("#Name").val("");
        $("#Description").val("");
        $("#RoleCode").val("");
        $("#RoleId").val(0);
    };
}