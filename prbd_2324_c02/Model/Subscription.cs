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
   

        [Table("subscriptions")]
        public class Subscription
        {
            public enum Fields
            {
                Tricount, User
            }

            public Subscription() {
            }

            public Subscription(int tricountId, int userId) {
                TricountId = tricountId;
                UserId = userId;
            }

            [Key, Column("tricount")]
            public int TricountId { get; set; }

            [Key, Column("user")]
            public int UserId { get; set; }

            [ForeignKey(nameof(TricountId))]
            public Tricount Tricount { get; set; }

            [ForeignKey(nameof(UserId))]
            public User User { get; set; }

            public override bool Equals(object obj) {
                if (obj is not Subscription subscription)
                    return false;

                return TricountId == subscription.TricountId && UserId == subscription.UserId;
            }

            public override int GetHashCode() {
                return HashCode.Combine(TricountId, UserId);
            }

            public override string ToString() {
                return $"Subscription[tricountId={TricountId}, userId={UserId}]";
            }

            public static void OnModelCreating(ModelBuilder modelBuilder) {
                modelBuilder.Entity<Subscription>()
                    .HasKey(s => new { s.TricountId, s.UserId });

                modelBuilder.Entity<Subscription>()
                    .HasOne(s => s.Tricount)
                    .WithMany(t => t.Subscriptions)
                    .HasForeignKey(s => s.TricountId);

                modelBuilder.Entity<Subscription>()
                    .HasOne(s => s.User)
                    .WithMany(u => u.Subscriptions)
                    .HasForeignKey(s => s.UserId);
            }

            public static Subscription GetByKey(int tricountId, int userId) {
                using var dbContext = 
                return dbContext.Subscriptions
                    .FirstOrDefault(s => s.TricountId == tricountId && s.UserId == userId);
            }

            public static List<Subscription> GetAll() {
                using var dbContext = 
                return dbContext.Subscriptions.ToList();
            }

            public void Save() {
                using var dbContext = new YourDbContext(); // Replace YourDbContext with your actual DbContext class
                var existingSubscription = dbContext.Subscriptions.Find(TricountId, UserId);

                if (existingSubscription == null) {
                    dbContext.Subscriptions.Add(this);
                } else {
                   
                }

                dbContext.SaveChanges();
            }

            public void Delete() {
                using var dbContext = 
                var subscriptionToDelete = dbContext.Subscriptions.Find(TricountId, UserId);

                if (subscriptionToDelete != null) {
                    dbContext.Subscriptions.Remove(subscriptionToDelete);
                    dbContext.SaveChanges();
                }
            }

            public void AddNew() {
                using var dbContext = 
                dbContext.Subscriptions.Add(this);
                dbContext.SaveChanges();
            }
        }

    }

