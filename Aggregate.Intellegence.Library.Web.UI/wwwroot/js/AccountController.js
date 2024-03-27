function AccountController() {
    var self = this;
    var requests = [];
    self.init = function () {
        requests.push($.ajax({
            url: '/Role/FetchAllRoles',
            method: 'GET'
        }));
        $.when.apply($, requests)
            .done(function () {
                var responses = arguments;
                self.LoadRoleDropdown(responses[0]);
                console.log(responses)
            }).fail(function () {
                console.log('One or more requests failed.');
            });
        $(document).on("click", "#saveUser", function () {
            $(".se-pre-con").show();
            var id = $("#Id").val();
            var fName = $("#fName").val();
            var lName = $("#lName").val();
            var phoneNo = $("#phoneNo").val();
            var role = $("#dropdownRole").val();
            var email = $("#email").val();
            var password = $("#password").val();

            var userInfo = {
                Id: id ? parseInt(Id) : 0,
                FirstName: fName,
                LastName: lName,
                RoleId: parseInt(role),
                Phone: phoneNo,
                Email: email,
                Password: password,
            };
            $.ajax({
                url: '/Account/RegisterUser',
                data: JSON.stringify(userInfo),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processData: true,
                cache: false,
                success: function (response) {
                    self.clearInputs();
                    var redirectUrl = "/Account/Login";
                    window.location.href = redirectUrl;
                    $(".se-pre-con").hide();
                },
                error: function (xhr, status, error) {
                    console.error(error);
                }
            })
        })
        $(document).on("click", "#btnLogin", function () {

            var username = $("#username").val();
            var password = $("#password").val();
            var remember = $("#rememberMe").val();

            var userlogin = {
                username: username,
                password: password,
                rememberMe: remember
            };

            $.ajax({
                url: '/Account/Login',
                data: JSON.stringify(userlogin),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processData: true,
                cache: false,
                success: function (response) {

                    if (response.status) {

                        var appUserInfo = storageService.get('ApplicationUser');
                        if (appUserInfo) {
                            storageService.remove('ApplicationUser');
                        }

                        var applicationUser = response.appUser;

                        storageService.set('ApplicationUser', applicationUser);

                        //var redirectUrl = "";

                        //if (applicationUser.RoleId === 1000)
                        //    redirectUrl = "/Employee/Index"
                        //else
                        //    redirectUrl = "/UserDashBoard/Index";

                        redirectUrl = "/Book/Index";

                        $(".se-pre-con").hide();

                        window.location.href = redirectUrl;
                    }
                },
                error: function (xhr, status, error) {
                    console.error(error);
                }
            });
        });
        self.LoadRoleDropdown = function (data) {

            var $dropdown = $('#dropdownRole');

            $dropdown.empty();

            var $defaultOption = $('<option>', {
                value: '',
                text: 'Select a role'
            });
            $dropdown.append($defaultOption);

            data.data.forEach(function (item) {
                var $option = $('<option>', {
                    value: item.Id,
                    text: item.Name
                });
                $dropdown.append($option);
            });
            $dropdown.dropdown();
        }
    }
    self.clearInputs = function () {
        $("#fName").val("");
        $("#lName").val("");
        $("#dropdownRole").prop('selectedIndex', 0);
        $("#email").val("");
        $("#password").val("");
        $("#RoleId").val(0);
    };
}