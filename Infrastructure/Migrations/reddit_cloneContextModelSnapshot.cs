﻿// <auto-generated />
using System;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(reddit_cloneContext))]
    partial class reddit_cloneContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Infrastructure.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CommentId")
                        .HasColumnName("comment_id")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnName("content")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("PostId")
                        .HasColumnName("post_id")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("comment");
                });

            modelBuilder.Entity("Infrastructure.Models.CommentVote", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("int");

                    b.Property<int>("CommentId")
                        .HasColumnName("comment_id")
                        .HasColumnType("int");

                    b.Property<bool>("Vote")
                        .HasColumnName("vote")
                        .HasColumnType("bit");

                    b.HasKey("UserId", "CommentId")
                        .HasName("PK__commentu__D7C7606774E3E164");

                    b.ToTable("comment_vote");
                });

            modelBuilder.Entity("Infrastructure.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnName("content")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<int>("TopicId")
                        .HasColumnName("topic_id")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnName("url")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<int>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.HasIndex("UserId");

                    b.ToTable("post");
                });

            modelBuilder.Entity("Infrastructure.Models.PostVote", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnName("post_id")
                        .HasColumnType("int");

                    b.Property<bool>("Vote")
                        .HasColumnName("vote")
                        .HasColumnType("bit");

                    b.HasKey("UserId", "PostId")
                        .HasName("PK__postupvo__CA534F79F69D59F1");

                    b.ToTable("post_vote");
                });

            modelBuilder.Entity("Infrastructure.Models.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique()
                        .HasName("TITLE_UNIQUE");

                    b.ToTable("topic");
                });

            modelBuilder.Entity("Infrastructure.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnName("hash")
                        .HasColumnType("varchar(256)")
                        .HasMaxLength(256)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(24)")
                        .HasMaxLength(24)
                        .IsUnicode(false);

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnName("salt")
                        .HasColumnType("varchar(128)")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("u_name");

                    b.ToTable("user");
                });

            modelBuilder.Entity("Infrastructure.Models.Comment", b =>
                {
                    b.HasOne("Infrastructure.Models.Comment", "CommentNavigation")
                        .WithMany("InverseCommentNavigation")
                        .HasForeignKey("CommentId")
                        .HasConstraintName("FK__comment__comment__4222D4EF");

                    b.HasOne("Infrastructure.Models.Post", "Post")
                        .WithMany("Comment")
                        .HasForeignKey("PostId")
                        .HasConstraintName("FK__comment__post_id__4316F928")
                        .IsRequired();

                    b.HasOne("Infrastructure.Models.User", "User")
                        .WithMany("Comment")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__comment__user_id__440B1D61")
                        .IsRequired();
                });

            modelBuilder.Entity("Infrastructure.Models.Post", b =>
                {
                    b.HasOne("Infrastructure.Models.Topic", "Topic")
                        .WithMany("Post")
                        .HasForeignKey("TopicId")
                        .HasConstraintName("FK__post__topic_id__21B6055D")
                        .IsRequired();

                    b.HasOne("Infrastructure.Models.User", "User")
                        .WithMany("Post")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__post__user_id__164452B1")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}