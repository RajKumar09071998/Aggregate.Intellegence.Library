﻿namespace Aggregate.Intellegence.Library.Web.UI.Model
{
    public class MVCApplicationUser
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public long? RoleId { get; set; }
    }
}
