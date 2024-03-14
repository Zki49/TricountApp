﻿using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_2324_c02.Model
{
    [Table("templates")]
    public class Template
    {
        public enum Fields
        {
            Id, Title, Repartition, Tricount
        }

        public static readonly Template DUMMY = new Template("No, I'll use custom repartition", 0);

        public Template() {
        }

        public Template(string title, int tricountId) {
            Title = title;
            TricountId = tricountId;
        }

        [Key]
        public int Id { get; set; }

        public List<TemplateItem> TemplateItems { get; set; }

        public string Title { get; set; }

        [Column("tricount")]
        public int TricountId { get; set; }

        [ForeignKey(nameof(TricountId))]
        public Tricount Tricount { get; set; }

        public override bool Equals(object obj) {
            if (obj is not Template template)
                return false;

            return Id == template.Id;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Id);
        }

        public override string ToString() {
            return Title;
        }

        public static void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Template>()
                .HasMany(t => t.TemplateItems)
                .WithOne(ti => ti.Template)
                .HasForeignKey(ti => ti.TemplateId);

            modelBuilder.Entity<Template>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Template>()
                .HasOne(t => t.Tricount)
                .WithMany(tc => tc.Templates)
                .HasForeignKey(t => t.TricountId);
        }
        //dgdgdgd
        public static Template GetByKey(int id) {
            using var dbContext = 
            return dbContext.Templates
                .FirstOrDefault(t => t.Id == id);
        }

        public static List<Template> GetByTricount(int id) {
            using var dbContext =
            return dbContext.Templates
                .Where(t => t.TricountId == id)
                .ToList();
        }

        public static Template GetByTitle(int id, string title) {
            using var dbContext = 
            return dbContext.Templates
                .FirstOrDefault(t => t.TricountId == id && t.Title == title);
        }

        public static List<Template> GetAll() {
            using var dbContext = 
            return dbContext.Templates.ToList();
        }

        public void Save() {
            using var dbContext = new PridContext();
            var existingTemplate = dbContext.Templates.Find(Id);

            if (existingTemplate == null) {
                dbContext.Templates.Add(this);
            } else {
               
            }

            dbContext.SaveChanges();
        }

        public void Delete() {
            using var dbContext = 
            var templateToDelete = dbContext.Templates.Find(Id);

            if (templateToDelete != null) {
                dbContext.Templates.Remove(templateToDelete);
                dbContext.SaveChanges();
            }
        }

        public static Template GetByTitleBis(string title) {
            using var dbContext = 
            return dbContext.Templates
                .FirstOrDefault(t => t.Title == title);
        }
    }
}
}
