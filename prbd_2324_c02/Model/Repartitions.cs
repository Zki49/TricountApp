using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_2324_c02.Model
{
    public class Repartitions
    {

        public Repartitions() { }

        [Required]
        public int weight { get; set; }

        //[ForeignKey(nameof(user))]
        public int userId { get; set; }

        //[Required]
        [ForeignKey(nameof(userId))]
        public virtual User user { get; set; }


        //[ForeignKey(nameof(operations))]
        public int operationsID { get; set; }

        //[Required]
        [ForeignKey(nameof(operationsID))]
        public virtual Operations operations { get; set; }
    }

    /*
     public class Message : EntityBase<Model> {
        public int MessageId { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string Body { get; set; }
        public bool IsPrivate { get; set; }

        [Required, ForeignKey(nameof(Author))]
        public string AuthorPseudo { get; set; }

        [Required]
        public virtual Member Author { get; set; }

        [Required, ForeignKey(nameof(Recipient))]
        public string RecipientPseudo { get; set; }

        [Required]
        public virtual Member Recipient { get; set; }
     
     */
}
