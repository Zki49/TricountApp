using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_2324_c02.Model
{
    public class Tricount
    {
       
        public Tricount() {
        }

        public Tricount(string title, int creatorId) {
            Title = title;
            CreatedAt = DateTime.Now;
            CreatorId = creatorId;
        }

        public Tricount(string title, string description, int creatorId) {
            Title = title;
            Description = description;
            CreatedAt = DateTime.Now;
            CreatorId = creatorId;
        }

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatorId { get; set; }

        
        public virtual List<Operations> Operations { get; set; }

        public virtual List<Subscription> Subscriptions { get; set; }

        //public virtual List<User> Participants { get; set; }

        public virtual List<Template> Templates { get; set; }

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

      
        

       

       /* public static void OnModelCreating(ModelBuilder modelBuilder) {
          
        }*/
    }

}

