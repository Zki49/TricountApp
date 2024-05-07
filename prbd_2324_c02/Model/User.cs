using PRBD_Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace prbd_2324_c02.Model;

public class User : EntityBase<PridContext>
{
    [Key]
    public int UserId { get; set; }
    public string FullName { get; set; }
    public string mail { get; set; }
    public string hashedPassword { get; set; }
    public Boolean Role {  get; set; }
    public virtual List<Subscription> Subscriptions { get; internal set; }
    public virtual List<Repartitions> repartitions { get; internal set; }
    public virtual List<Operations> operations { get; internal set; }
    public virtual List<Tricount> tricounts { get; internal set; }


    public static User GetUserByMail(string mail) {
        var res = Context.Users
            .Where(user => user.mail == mail)
            .FirstOrDefault();
        return res;
    }

}

