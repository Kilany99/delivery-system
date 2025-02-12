
using Microsoft.AspNet.Identity.EntityFramework;

namespace OrderService.Domain.Entities;

public class Customer : IdentityUser
{
    public string FullName { get; set; }
}