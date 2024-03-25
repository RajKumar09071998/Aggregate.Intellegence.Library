function RoleController() {
    var self = this;
    
    self.init = function () {
        $('#addEditRoleModal').modal({ backdrop: 'static', keyboard: false });
        var rolesGrid = $('#RolesGrid').DataTable({
            responsive: false,
            serverSide: false,
            ajax: {
                url: '/Role/FetchAllRoles',
                type: 'GET',
                dataSrc: 'data'
            },
            columns: [
                { data: 'Name' },
                { data: 'Code' },
                {
                    data: null,
                    render: function (data, type, row) {
                        var icons = '';
                        icons += '<i class="fas fa-edit edit-icon icon-padding-right" data-id="' + row.Id + '" style="padding: 5px; font-size: 20px; color: red;"></i>'
                            +
                            '<i class="fas fa-trash delete-icon icon-padding-right" data-id="' + row.Id + '" style="padding: 5px; font-size: 20px; color: red;"></i>';

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
        $(document).on("click", "#addRole", function () {
            self.clearInputs();
            $("#addEditRoleModal").modal("show");
        });
        $(document).on("click", ".edit-icon", function () {
            $(".se-pre-con").show();
            var data = $(this);
            var row = data.closest('tr');
            var dataItem = rolesGrid.row(row).data();
            $("#Id").val(dataItem.Id);
            $("#Role").val(dataItem.Name);
            $("#Code").val(dataItem.Code);
            $("#addEditRoleModalLabel").text("Edit Role");
            $("#addEditRoleModal").modal("show");
            $(".se-pre-con").hide();
        });
        $(document).on("click", "#SaveRole", function () {
            $(".se-pre-con").show();
            var id = $("#Id").val();
            var name = $("#Role").val();
            var code = $("#Code").val();
            

            var roles = {
                Id: id ? parseInt(id) : 0,
                Name: name,
                Code: code,
                CreatedOn: new Date(),
                CreatedBy: 0,
                ModifiedOn: new Date(),
                ModifiedBy: 0,
                IsActive: true
            };
            $.ajax({
                url: '/Role/InsertOrUpdateRole',
                data: JSON.stringify(roles),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processData: true,
                cache: false,
                success: function (response) {
                    $('#addEditRoleModal').modal('hide');
                    self.clearInputs();
                    rolesGrid.ajax.reload();
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
            var dataItem = rolesGrid.row(row).data();
            var id = dataItem.Id;
            var name = dataItem.Name;
            var code = dataItem.Code;

            var deleteRole = {
                Id: id ? parseInt(id) : 0,
                ModifiedOn: new Date(),
                ModifiedBy: 0,
                IsActive: false,
                Name: name,
                Code: code,
                ModifiedOn: new Date(),
                ModifiedBy: 0,
            };
            $.ajax({
                url: '/Role/InsertOrUpdateRole',
                data: JSON.stringify(deleteRole),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processData: true,
                cache: false,
                success: function (response) {
                    rolesGrid.ajax.reload();
                    $(".se-pre-con").hide();
                },
                error: function (xhr, status, error) {
                    console.error(error);
                }
            })
        })
    }
    self.clearInputs = function () {
        $("#Id").val(0);
        $("#Role").val("");
        $("#Code").val("");
    };
}