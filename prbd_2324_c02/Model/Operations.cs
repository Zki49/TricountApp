using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_2324_c02.Model
{
    public class Operations : EntityBase<PridContext>
    {
        //copie profonde
        public Operations(Operations other) {
            //on ne changera pas cela donc c'est pas grave on va quand meme pas travailler pour rien ;)
            user = other.user; 
            title = other.title;
            //on ne changera pas cela donc c'est pas grave on va quand meme pas travailler pour rien ;)
            Tricount = other.Tricount;
            Amount = other.Amount;
            string dateString = other.CreatAt.ToString();
            CreatAt= DateTime.Parse(dateString);
            copieList(other);

        }
        public Operations() { }
        [Key]
        public virtual int OperationsId {  get; set; }

        public string title {  get; set; }

        
        public int tricountId { get; set; }

        
        public virtual Tricount Tricount { get; set; }

        public double Amount {  get; set; }
        public DateTime CreatAt { get; set; } = DateTime.Now;

        public int userId { get; set; }

        public virtual User user { get; set; }
        public virtual List<Repartitions> repartitions { get; set; } = new();
        

        public void delete() {
            Context.Remove(this);
            Context.SaveChanges();
        }
        public void save() {
            Context.Add(this);
            Context.SaveChanges();
            //repartitions.Clear();
        }
        public String toString() {
            return Amount.ToString();
        }

        public void copieList(Operations other) {
            foreach(var rep in other.repartitions) {
                Repartitions repa = new Repartitions(rep);
                repa.operations = this;
                repa.operations.Amount = Amount;
                repartitions.Add(repa);
            }
        }

    }
}
