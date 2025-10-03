
using System.Security.Principal;

namespace Grocery.Core.Models
{
    public enum Role
    {
        None,
        Admin
    }
    public partial class Client : Model
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public Client(int id, string name, string emailAddress, string password) : base(id, name)
        {
            EmailAddress=emailAddress;
            Password=password;
        }

        public Role Role { get; set; } = Role.None;

        public List<GroceryList> GroceryLists { get; set; } = new();
    }
}
