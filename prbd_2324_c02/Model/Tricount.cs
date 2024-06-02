using Azure;
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
using PRBD_Framework;

namespace prbd_2324_c02.Model
{
    public class Tricount: EntityBase<PridContext>
    {
       
        public Tricount() {
        }
         
        private String _description;
       

        [Key]
        public int Id { get; set; }
        public string Title { get => _description==null?"no description ": _description; set=>_description=value ; }
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
                      .Sum(operation => operation.Amount *((double)(operation.repartitions.First(repartition => repartition.userId == user.UserId).weight /(double) operation.repartitions.Sum(repartition => repartition.weight))));

          /*  Console.WriteLine(Operations
                          .Where(operation => operation.repartitions.Any(repartition => repartition.userId == user.UserId))
                          .Sum(operation => operation.Amount));
            Console.WriteLine(Operations
                          .Where(operation => operation.repartitions.Any(repartition => repartition.userId == user.UserId))
                          .Sum(operation => (double)operation.repartitions
                                .First(repartition => repartition.userId == user.UserId).weight / (double)operation.repartitions.Sum(repartition => repartition.weight)));
          */
            var myExpense = Operations
                .Where(operation => operation.userId == user.UserId)
                .Sum(operation => operation.Amount);

          /*  Console.WriteLine(myExpense);
            Console.WriteLine(res);
          */
           // Console.WriteLine( myExpense - res);

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



               return -(res-myExpense);
        }
        
        public static Tricount AddTricount( string Title , string description ,DateTime date ,List<User> participants , User Creator) {
            Tricount tricount = new Tricount();
            tricount.Title= Title ;  
            tricount.Description= description ;
            tricount.CreatedAt= date ;
            tricount.Creator= Creator ;
             foreach(User user in participants ) {
                Subscription sub = new Subscription();
                sub.UserId = user.UserId ;
                sub.TricountId=tricount.Id ;
                tricount.Subscriptions.Add(sub) ;

            }
            
           Context.Add(tricount);
            Context.SaveChanges();
            return tricount;
            
             

        }

        public void delete() {
            Context.Remove(this);
            Context.SaveChanges();
        }
       

       /* public static void OnModelCreating(ModelBuilder modelBuilder) {
          
        }*/
    }

}

