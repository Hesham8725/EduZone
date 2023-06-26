﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EduZone.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public string Address { get; set; }
        //[Remote(action:"SearchByName",controller: "Group",HttpMethod ="Post",ErrorMessage = "No results found.")]
        public string Name { get; set; }
        public string NationalID { get; set; }
        public bool EmailActive { get; set; }
        public int Age { get; set; }
        public string Image { get; set; }
        public string Gender { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<MailOfDoctors> MailOfDoctors { get; set; }
        public virtual DbSet<Student> GetStudents { get; set; }
        public virtual DbSet<Group> GetGroups { get; set; }
        public virtual DbSet<GroupsMembers> GetGroupsMembers { get; set; }
        public virtual DbSet<Educator> GetEducators { get; set; }
        public virtual DbSet<GroupMaterial> GetMaterials { get; set; }

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostInGroup> PostInGroups { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<LikeForPostInGroup> LikeForPostInGroups { get; set; }
        public virtual DbSet<Department> GetDepartments { get; set; }
        public virtual DbSet<Course> GetCourses { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<AspNetUserClaims>().ToTable("");
        }
    }
}