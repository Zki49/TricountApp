﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prbd_2324_c02.Model
{
    public class Template_items
    {
        [Required]
        public int weight { get; set; }

        [Required, ForeignKey(nameof(template))]
        public Template template { get; set; }

        [Required, ForeignKey(nameof(User))]
        public User User { get; set; }

        public enum fields {
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
