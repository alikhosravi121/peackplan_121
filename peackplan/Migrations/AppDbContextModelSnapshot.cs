﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using peackplan;

#nullable disable

namespace peackplan.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OkrEntityUserEntity", b =>
                {
                    b.Property<Guid>("OkrsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("OkrsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("OkrEntityUserEntity");
                });

            modelBuilder.Entity("TeamWorkEntityUserEntity", b =>
                {
                    b.Property<Guid>("TeamWorksId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("TeamWorksId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("TeamWorkEntityUserEntity");
                });

            modelBuilder.Entity("peackplan.Entities.CompanyEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("peackplan.Entities.NoteEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OkrEntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OkrEntityId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("peackplan.Entities.NoteReceiverEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsRead")
                        .HasColumnType("boolean");

                    b.Property<Guid>("NoteId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ReadAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("NoteId");

                    b.HasIndex("ReceiverId");

                    b.ToTable("NoteReceiverEntity");
                });

            modelBuilder.Entity("peackplan.Entities.OkrEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessLevel")
                        .HasColumnType("integer");

                    b.Property<Guid?>("AvatarId")
                        .HasColumnType("uuid");

                    b.Property<int?>("CurrentValue")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("GoalValue")
                        .HasColumnType("integer");

                    b.Property<Guid>("ManagerId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ParentOkrId")
                        .HasColumnType("uuid");

                    b.Property<float>("PriorityWeight")
                        .HasColumnType("real");

                    b.Property<int?>("StartValue")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.HasIndex("ParentOkrId");

                    b.ToTable("Okrs");
                });

            modelBuilder.Entity("peackplan.Entities.PrimaryTaskEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessLevel")
                        .HasColumnType("integer");

                    b.Property<Guid?>("AvatarId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ManagerId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("OkrEntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ParentTaskId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Tags")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.HasIndex("OkrEntityId");

                    b.ToTable("PrimaryTasks");
                });

            modelBuilder.Entity("peackplan.Entities.TagEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("TagEntity");
                });

            modelBuilder.Entity("peackplan.Entities.TeamWorkEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AdminId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AvatarId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("FileSrc")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<Guid?>("OwnerTeamWrokId")
                        .HasColumnType("uuid");

                    b.Property<string>("Target")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("TeamWorks");
                });

            modelBuilder.Entity("peackplan.Entities.UploadedFileEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<Guid?>("OkrId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UploadedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OkrId");

                    b.HasIndex("UsersId");

                    b.ToTable("UploadedFiles");
                });

            modelBuilder.Entity("peackplan.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AvatarId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FileSrc")
                        .HasColumnType("text");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("IsMarried")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OkrEntityUserEntity", b =>
                {
                    b.HasOne("peackplan.Entities.OkrEntity", null)
                        .WithMany()
                        .HasForeignKey("OkrsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("peackplan.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TeamWorkEntityUserEntity", b =>
                {
                    b.HasOne("peackplan.Entities.TeamWorkEntity", null)
                        .WithMany()
                        .HasForeignKey("TeamWorksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("peackplan.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("peackplan.Entities.NoteEntity", b =>
                {
                    b.HasOne("peackplan.Entities.OkrEntity", "OkrEntity")
                        .WithMany("NoteEntities")
                        .HasForeignKey("OkrEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OkrEntity");
                });

            modelBuilder.Entity("peackplan.Entities.NoteReceiverEntity", b =>
                {
                    b.HasOne("peackplan.Entities.NoteEntity", "Note")
                        .WithMany("Receivers")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("peackplan.Entities.UserEntity", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Note");

                    b.Navigation("Receiver");
                });

            modelBuilder.Entity("peackplan.Entities.OkrEntity", b =>
                {
                    b.HasOne("peackplan.Entities.OkrEntity", "ParentOkr")
                        .WithMany()
                        .HasForeignKey("ParentOkrId");

                    b.Navigation("ParentOkr");
                });

            modelBuilder.Entity("peackplan.Entities.PrimaryTaskEntity", b =>
                {
                    b.HasOne("peackplan.Entities.UserEntity", "Manager")
                        .WithMany("PrimaryTasks")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("peackplan.Entities.OkrEntity", null)
                        .WithMany("PrimaryTasks")
                        .HasForeignKey("OkrEntityId");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("peackplan.Entities.TagEntity", b =>
                {
                    b.HasOne("peackplan.Entities.CompanyEntity", "Company")
                        .WithMany("TagEntities")
                        .HasForeignKey("CompanyId");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("peackplan.Entities.TeamWorkEntity", b =>
                {
                    b.HasOne("peackplan.Entities.CompanyEntity", "Company")
                        .WithMany("TeamWorks")
                        .HasForeignKey("CompanyId");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("peackplan.Entities.UploadedFileEntity", b =>
                {
                    b.HasOne("peackplan.Entities.OkrEntity", "Okr")
                        .WithMany("UploadedFile")
                        .HasForeignKey("OkrId");

                    b.HasOne("peackplan.Entities.UserEntity", "Users")
                        .WithMany("UploadedFile")
                        .HasForeignKey("UsersId");

                    b.Navigation("Okr");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("peackplan.Entities.CompanyEntity", b =>
                {
                    b.Navigation("TagEntities");

                    b.Navigation("TeamWorks");
                });

            modelBuilder.Entity("peackplan.Entities.NoteEntity", b =>
                {
                    b.Navigation("Receivers");
                });

            modelBuilder.Entity("peackplan.Entities.OkrEntity", b =>
                {
                    b.Navigation("NoteEntities");

                    b.Navigation("PrimaryTasks");

                    b.Navigation("UploadedFile");
                });

            modelBuilder.Entity("peackplan.Entities.UserEntity", b =>
                {
                    b.Navigation("PrimaryTasks");

                    b.Navigation("UploadedFile");
                });
#pragma warning restore 612, 618
        }
    }
}
