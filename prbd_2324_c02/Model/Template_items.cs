using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PRBD_Framework;

namespace prbd_2324_c02.Model
{
    public class Template_items : EntityBase<PridContext>
    {
        [Required]
        public int weight { get; set; }
        [Required, ForeignKey(nameof(template))]
        public int templateId { get; set; }
        [Required, ForeignKey(nameof(User))]
        public int userId { get; set; }

        
        public virtual Template template { get; set; }

        
        public virtual User User { get; set; }

        public enum Fields {
            User, Template, Weight 
        }
        
        public Template_items() {
        }

       /* public Template_items(int weight, int TemplateId , int UserId) {
            this.weight = weight;
            this.TemplateId = TemplateId;
            this.UserId = UserId;   
        }*/

        public int incrementWeight() {
            return weight++;

        }
    }
}
