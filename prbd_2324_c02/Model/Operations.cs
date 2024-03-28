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
        public Operations OperationsId {  get; set; }

        public string title {  get; set; }

        public int tricountId { get; set; }

        [ForeignKey(nameof(tricountId))]
        public virtual Tricount Tricount { get; set; }

        public double Amount {  get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;

        public int userId { get; set; }

        [ForeignKey(nameof(userId))]
        public virtual User user { get; set; }
        public List<Repartitions> repartitions { get; set; }
    }
}
