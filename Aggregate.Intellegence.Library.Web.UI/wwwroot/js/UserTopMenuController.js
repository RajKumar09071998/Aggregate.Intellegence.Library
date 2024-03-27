function UserTopMenuController() {
    var self = this;
    self.ApplicationUser = {};
    self.init = function () {
        var appuser = storageService.get("ApplicationUser");
        if (appuser) {
            self.ApplicationUser = appuser;
        }
        $("#userProfileName").text(self.ApplicationUser.FirstName);
        $("#userFullName").text(self.ApplicationUser.FirstName + " " + self.ApplicationUser.LastName);
    };
}