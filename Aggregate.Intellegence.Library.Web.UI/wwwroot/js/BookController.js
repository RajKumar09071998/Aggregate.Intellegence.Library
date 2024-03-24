function BookController() {
    var self = this;
    self.init = function () {
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
                        icons += '<i class="fas fa-trash delete-icon  icon-padding-right" data-id="' + row.BookId + '" style="font-size: 20px;color: red;"></i>';

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
    }
}