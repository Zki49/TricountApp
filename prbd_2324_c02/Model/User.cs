using PRBD_Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prbd_2324_c02.Model;

public class User 
{
    [Key]
    public int UserId { get; set; }
    public List<Subscription> Subscriptions { get; internal set; }
}

