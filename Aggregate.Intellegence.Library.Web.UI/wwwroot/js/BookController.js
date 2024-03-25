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
    }
    self.clearInputs = function () {
        $("#Name").val("");
        $("#Description").val("");
        $("#RoleCode").val("");
        $("#RoleId").val(0);
    };
}