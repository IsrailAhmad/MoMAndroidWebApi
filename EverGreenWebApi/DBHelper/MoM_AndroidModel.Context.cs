﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EverGreenWebApi.DBHelper
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class mom_androidEntities : DbContext
    {
        public mom_androidEntities()
            : base("name=mom_androidEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<categorymaster> categorymasters { get; set; }
        public virtual DbSet<rolemaster> rolemasters { get; set; }
        public virtual DbSet<userdatabasemaster> userdatabasemasters { get; set; }
        public virtual DbSet<userloginmaster> userloginmasters { get; set; }
        public virtual DbSet<menumaster> menumasters { get; set; }
        public virtual DbSet<locationmaster> locationmasters { get; set; }
        public virtual DbSet<userpermissionmaster> userpermissionmasters { get; set; }
    }
}
