//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tourism
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TourismEntities : DbContext
    {
        public TourismEntities()
            : base("name=TourismEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Client_type> Client_type { get; set; }
        public virtual DbSet<Feeding> Feeding { get; set; }
        public virtual DbSet<Hotel> Hotel { get; set; }
        public virtual DbSet<Manager> Manager { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Payment_type> Payment_type { get; set; }
        public virtual DbSet<PaymentStatus> PaymentStatus { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Sell> Sell { get; set; }
        public virtual DbSet<Tour> Tour { get; set; }
        public virtual DbSet<Tour_booking> Tour_booking { get; set; }
        public virtual DbSet<TourSell> TourSell { get; set; }
    }
}
