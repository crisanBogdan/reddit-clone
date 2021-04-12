using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Models
{
    public partial class reddit_cloneContext : DbContext
    {
        public reddit_cloneContext()
        {
        }

        public reddit_cloneContext(DbContextOptions<reddit_cloneContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<CommentVote> CommentVote { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<PostVote> PostVote { get; set; }
        public virtual DbSet<Topic> Topic { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=B-PC;Database=reddit_clone;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CommentId).HasColumnName("comment_id");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.CommentNavigation)
                    .WithMany(p => p.InverseCommentNavigation)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK__comment__comment__4222D4EF");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__comment__post_id__4316F928");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__comment__user_id__440B1D61");
            });

            modelBuilder.Entity<CommentVote>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.CommentId })
                    .HasName("PK__commentu__D7C7606774E3E164");

                entity.ToTable("comment_vote");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.CommentId).HasColumnName("comment_id");

                entity.Property(e => e.Vote).HasColumnName("vote");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("post");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(32);

                entity.Property(e => e.TopicId).HasColumnName("topic_id");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasMaxLength(64);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__post__topic_id__21B6055D");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__post__user_id__164452B1");
            });

            modelBuilder.Entity<PostVote>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PostId })
                    .HasName("PK__postupvo__CA534F79F69D59F1");

                entity.ToTable("post_vote");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.Vote).HasColumnName("vote");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.ToTable("topic");

                entity.HasIndex(e => e.Title)
                    .HasName("TITLE_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Name)
                    .HasName("u_name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasColumnName("hash")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasColumnName("salt")
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
