

using Apollo.Domain.entities;
using MySql.Data.Entity;

namespace Apollo.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
   [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public partial class JeeModel : DbContext
    {
        public JeeModel()
            : base("name=JeeModel")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            // Database.SetInitializer<JeeModel>(new jeeModelContextInitialize());
        }

        public virtual DbSet<artwork> artwork { get; set; }
        public virtual DbSet<collection> collection { get; set; }
        public virtual DbSet<_event> _event { get; set; }
        public virtual DbSet<follow> follow { get; set; }
        public virtual DbSet<gallery> gallery { get; set; }
        public virtual DbSet<media> media { get; set; }
        public virtual DbSet<notification> notification { get; set; }
        public virtual DbSet<orders> orders { get; set; }
        public virtual DbSet<rating> rating { get; set; }
        public virtual DbSet<schedule> schedule { get; set; }
        public virtual DbSet<showroom> showroom { get; set; }
        public virtual DbSet<ticket> ticket { get; set; }
        public virtual DbSet<user> user { get; set; }
        public virtual DbSet<whishlist> whishlist { get; set; }
        public virtual DbSet<NewsLetter> newsletter { get; set; }
        public virtual DbSet<NewsLettersOpens> newsletteropens { get; set; }
        public virtual DbSet<transportJob> TransportJobs { get; set; }
        public virtual DbSet<Conversation> conversations { get; set; }
        public virtual DbSet<Message> messages { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewsLettersOpens>()
                .Property(e => e.ID);


            modelBuilder.Entity<artwork>()
                            .Property(e => e.descreption)
                            .IsUnicode(false);

            modelBuilder.Entity<artwork>()
                .Property(e => e.mediaPath)
                .IsUnicode(false);

            modelBuilder.Entity<artwork>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<artwork>()
                .HasMany(e => e.media)
                .WithOptional(e => e.artwork)
                .HasForeignKey(e => e.artWork_id);

            modelBuilder.Entity<artwork>()
                .HasMany(e => e.rating)
                .WithRequired(e => e.artwork)
                .HasForeignKey(e => e.idArt)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<artwork>()
                .HasMany(e => e.showroom)
                .WithMany(e => e.artwork)
                .Map(m => m.ToTable("showroom_artwork").MapLeftKey("artWorks_id").MapRightKey("ShowRoom_id"));

            modelBuilder.Entity<artwork>()
                .HasMany(e => e.whishlist)
                .WithMany(e => e.artwork)
                .Map(m => m.ToTable("whishlist_artwork").MapLeftKey("artWorks_id").MapRightKey("WhishList_id"));

            modelBuilder.Entity<collection>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<collection>()
                .HasMany(e => e.user)
                .WithOptional(e => e.collection)
                .HasForeignKey(e => e.collection_id);

            modelBuilder.Entity<collection>()
                .HasMany(e => e.artwork)
                .WithMany(e => e.collection)
                .Map(m => m.ToTable("collection_artwork", "apollo").MapLeftKey("Collection_id").MapRightKey("artworks_id"));

            modelBuilder.Entity<_event>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<_event>()
                .Property(e => e.imagePath)
                .IsUnicode(false);

            modelBuilder.Entity<_event>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<_event>()
                .HasMany(e => e.ticket)
                .WithOptional(e => e._event)
                .HasForeignKey(e => e.event_id);

            modelBuilder.Entity<_event>()
                .HasMany(e => e.gallery1)
                .WithMany(e => e.event1)
                .Map(m => m.ToTable("gallery_event", "apollo").MapLeftKey("events_id").MapRightKey("Gallery_id"));

            modelBuilder.Entity<gallery>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<gallery>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<gallery>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<gallery>()
                .HasMany(e => e._event)
                .WithOptional(e => e.gallery)
                .HasForeignKey(e => e.gallery_id);

            modelBuilder.Entity<gallery>()
                .HasMany(e => e.media)
                .WithMany(e => e.gallery)
                .Map(m => m.ToTable("gallery_media", "apollo").MapLeftKey("Gallery_id").MapRightKey("album_id"));

            modelBuilder.Entity<gallery>()
                .HasMany(e => e.schedule)
                .WithMany(e => e.gallery)
                .Map(m => m.ToTable("gallery_schedule", "apollo").MapLeftKey("Gallery_id").MapRightKey("calendar_id"));

            modelBuilder.Entity<media>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<media>()
                .Property(e => e.path)
                .IsUnicode(false);

            modelBuilder.Entity<notification>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<notification>()
                .Property(e => e.message)
                .IsUnicode(false);

            modelBuilder.Entity<orders>()
                .HasMany(e => e.artwork)
                .WithMany(e => e.orders)
                .Map(m => m.ToTable("orders_artwork").MapLeftKey("Orders_Id").MapRightKey("ArtWorks_id"));

            modelBuilder.Entity<schedule>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<schedule>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<showroom>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<showroom>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<ticket>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<ticket>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.role)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.country)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.firstname)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.gender)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.imagePath)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.lastname)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.state)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.street)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.userName)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.zipCode)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.artwork)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.artist_id);

            modelBuilder.Entity<user>()
                .HasMany(e => e.follow)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.userId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.follow1)
                .WithRequired(e => e.user1)
                .HasForeignKey(e => e.artistId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.gallery)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.galleryOwner_id);

            modelBuilder.Entity<user>()
                .HasMany(e => e.media)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<user>()
                .HasMany(e => e.notification)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<user>()
                .HasMany(e => e.rating)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.idUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.showroom)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.artist_id);

            modelBuilder.Entity<user>()
                .HasMany(e => e.gallery1)
                .WithMany(e => e.user1)
                .Map(m => m.ToTable("renting", "apollo").MapLeftKey("artistId").MapRightKey("galleryId"));

            modelBuilder.Entity<user>()
                .HasMany(e => e.showroom1)
                .WithMany(e => e.user1)
                .Map(m => m.ToTable("user_showroom").MapLeftKey("User_id").MapRightKey("showrooms_id"));

            modelBuilder.Entity<whishlist>()
                .HasMany(e => e.user)
                .WithOptional(e => e.whishlist)
                .HasForeignKey(e => e.whishList_id);
        }
    }
    public class jeeModelContextInitialize : DropCreateDatabaseIfModelChanges<JeeModel>
    {
        protected override void Seed(JeeModel context)
        {

            // before exec we Detach Db (Right Click -> Detach | On DB in SQl Server Object Explorer)
            // else it will tell DB Occupied
        }

    }
}
