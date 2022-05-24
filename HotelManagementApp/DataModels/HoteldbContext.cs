using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HotelManagementApp.DataModels
{
    public partial class HoteldbContext : DbContext
    {
        public HoteldbContext()
        {
        }

        public HoteldbContext(DbContextOptions<HoteldbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Extra> Extras { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<PriceHistory> PriceHistories { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<ReservationExtra> ReservationExtras { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomImage> RoomImages { get; set; }
        public virtual DbSet<RoomOffer> RoomOffers { get; set; }
        public virtual DbSet<RoomRoomImage> RoomRoomImages { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-L3E3850;Initial Catalog=Hoteldb;Integrated Security=SSPI;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Extra>(entity =>
            {
                entity.ToTable("Extra");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Offer>(entity =>
            {
                entity.ToTable("Offer");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("End_Date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Start_Date");
            });

            modelBuilder.Entity<PriceHistory>(entity =>
            {
                entity.ToTable("Price_History");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("End_Date");

                entity.Property(e => e.RoomTypeId).HasColumnName("Room_Type_Id");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Start_Date");

                entity.HasOne(d => d.RoomType)
                    .WithMany(p => p.PriceHistories)
                    .HasForeignKey(d => d.RoomTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Price_History_Room_Type1");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("Reservation");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("End_Date");

                entity.Property(e => e.RoomId).HasColumnName("Room_Id");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Start_Date");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TotalPrice).HasColumnName("Total_Price");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_Room");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_User");
            });

            modelBuilder.Entity<ReservationExtra>(entity =>
            {
                entity.ToTable("Reservation_Extra");

                entity.Property(e => e.ExtraId).HasColumnName("Extra_Id");

                entity.Property(e => e.ReservationId).HasColumnName("Reservation_Id");

                entity.HasOne(d => d.Extra)
                    .WithMany(p => p.ReservationExtras)
                    .HasForeignKey(d => d.ExtraId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_Extra_Extra");

                entity.HasOne(d => d.Reservation)
                    .WithMany(p => p.ReservationExtras)
                    .HasForeignKey(d => d.ReservationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_Extra_Room");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.Property(e => e.RoomTypeId).HasColumnName("Room_Type_Id");

                entity.HasOne(d => d.RoomType)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.RoomTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_Room_Type");
            });

            modelBuilder.Entity<RoomImage>(entity =>
            {
                entity.ToTable("Room_Image");

                entity.Property(e => e.Picture)
                    .IsRequired()
                    .HasColumnType("image");
            });

            modelBuilder.Entity<RoomOffer>(entity =>
            {
                entity.ToTable("Room_Offer");

                entity.Property(e => e.OfferId).HasColumnName("Offer_Id");

                entity.Property(e => e.RoomTypeId).HasColumnName("Room_Type_Id");

                entity.HasOne(d => d.Offer)
                    .WithMany(p => p.RoomOffers)
                    .HasForeignKey(d => d.OfferId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_Offer_Offer");

                entity.HasOne(d => d.RoomType)
                    .WithMany(p => p.RoomOffers)
                    .HasForeignKey(d => d.RoomTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_Offer_Room_Type");
            });

            modelBuilder.Entity<RoomRoomImage>(entity =>
            {
                entity.ToTable("Room-Room_Image");

                entity.Property(e => e.RoomImageId).HasColumnName("Room_Image_Id");

                entity.Property(e => e.RoomTypeId).HasColumnName("Room_Type_Id");

                entity.HasOne(d => d.RoomImage)
                    .WithMany(p => p.RoomRoomImages)
                    .HasForeignKey(d => d.RoomImageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room-Room_Image_Room_Image");

                entity.HasOne(d => d.RoomType)
                    .WithMany(p => p.RoomRoomImages)
                    .HasForeignKey(d => d.RoomTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room-Room_Image_Room_Type");
            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.ToTable("Room_Type");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Features)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("Role_Id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_User_Role");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("User_Role");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
