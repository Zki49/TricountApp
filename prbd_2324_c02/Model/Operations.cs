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
        public virtual List<Repartitions> repartitions { get; set; }

        public void delete() {
            Context.Remove(this);
            Context.SaveChanges();
        }
    }
}
