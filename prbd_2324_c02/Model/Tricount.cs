using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace prbd_2324_c02.Model
{
    public class Tricount
    {
       
        public Tricount() {
        }

       

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatorId { get; set; }
       

        
        public virtual List<Operations> Operations { get; set; } = new List<Operations>();

        public virtual List<Subscription> Subscriptions { get; set; }=new List<Subscription>();

        //public virtual List<User> Participants { get; set; }

        public virtual List<Template> Templates { get; set; } = new List<Template>();

        public virtual User Creator { get; set; } 

        public override bool Equals(object obj) {
            if (obj is not Tricount tricount)
                return false;

            return Id == tricount.Id;
        }

        public override int GetHashCode() {
            return Id.GetHashCode();
        }

        public override string ToString() {
            return $"Tricount[id={Id}, title={Title}, description={Description}, createdAt={CreatedAt}, creatorId={CreatorId}]";
        }

       
        public double balance(User user) {
           
            var res = Operations
                      .Where(operation => operation.repartitions.Any(repartition => repartition.userId == user.UserId))
                    .Sum(operation => operation.Amount / operation.repartitions.First(repartition => repartition.userId == user.UserId).weight);
            
            var myExpense = Operations
                .Where(operation => operation.userId == user.UserId)
                .Sum(operation => operation.Amount);
           
            Console.WriteLine(res);

/*
 * 
 * public double GetUserBalance(User user) {
    double userBalance = Tricounts
        .Where(tricount => tricount.CreatorId == user.Id) // Assuming CreatorId represents the user who created the tricount
        .SelectMany(tricount => tricount.Operations) // Get all operations related to tricounts created by the user
        .Sum(operation => operation.Amount); // Sum all operation amounts

    return userBalance;
}

 * */



               return res-myExpense;
        }
        

       

       /* public static void OnModelCreating(ModelBuilder modelBuilder) {
          
        }*/
    }

}

