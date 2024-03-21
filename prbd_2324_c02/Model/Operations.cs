using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_2324_c02.Model
{
    public class Operations
    {
        public Operations() { }
        public int OperationsId {  get; set; }

        public string title {  get; set; }

        public virtual Tricount Tricount { get; set; }

        public double Amount {  get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;

        [ForeignKey(nameof(initiator))]
        public virtual User initiator { get; set; }
    }
}
