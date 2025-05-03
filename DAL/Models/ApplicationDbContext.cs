using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using WindowsFormsApp1.DAL.Models;

namespace WindowsFormsApp1.DAL.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public ApplicationDbContext() : base("name=DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Posts)
                .WithRequired(p => p.User)
                .HasForeignKey(p => p.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Posts)
                .WithRequired(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Comments)
                .WithRequired(c => c.User)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithRequired(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Post>()
                .Property(p => p.Status)
                .HasColumnType("int");
        }

        public static void InitializeDatabase()
        {
            Database.SetInitializer(new DbInitializer());
        }

        public class DbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                try
                {
                    SeedUsers(context);
                    SeedCategories(context);
                    SeedPosts(context);
                    SeedComments(context);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Lỗi khi seed dữ liệu: {ex.Message}");
                    throw;
                }
            }

            private void SeedUsers(ApplicationDbContext context)
            {
                if (context.Users.Any()) return;

                var users = new List<User>
                {
                    new User { Username = "admin", Password = BCrypt.Net.BCrypt.HashPassword("admin123"), Email = "admin@example.com", Role = "Admin" },
                    new User { Username = "moderator1", Password = BCrypt.Net.BCrypt.HashPassword("mod123"), Email = "mod1@example.com", Role = "Author" },
                    new User { Username = "author1", Password = BCrypt.Net.BCrypt.HashPassword("author123"), Email = "author1@example.com", Role = "Author" },
                    new User { Username = "author2", Password = BCrypt.Net.BCrypt.HashPassword("author123"), Email = "author2@example.com", Role = "Author" },
                    new User { Username = "techwriter", Password = BCrypt.Net.BCrypt.HashPassword("tech123"), Email = "tech@example.com", Role = "Author" },
                    new User { Username = "healthwriter", Password = BCrypt.Net.BCrypt.HashPassword("health123"), Email = "health@example.com", Role = "Author" },
                    new User { Username = "scivisor", Password = BCrypt.Net.BCrypt.HashPassword("sci123"), Email = "sci@example.com", Role = "Author" },
                    new User { Username = "busineditor", Password = BCrypt.Net.BCrypt.HashPassword("busi123"), Email = "busi@example.com", Role = "Author" },
                    new User { Username = "reader1", Password = BCrypt.Net.BCrypt.HashPassword("reader123"), Email = "reader1@example.com", Role = "Reader" },
                    new User { Username = "reader2", Password = BCrypt.Net.BCrypt.HashPassword("reader123"), Email = "reader2@example.com", Role = "Reader" },
                    new User { Username = "premium1", Password = BCrypt.Net.BCrypt.HashPassword("prem123"), Email = "prem1@example.com", Role = "Author" },
                    new User { Username = "premium2", Password = BCrypt.Net.BCrypt.HashPassword("prem123"), Email = "prem2@example.com", Role = "Author" },
                    new User { Username = "guest1", Password = BCrypt.Net.BCrypt.HashPassword("guest123"), Email = "guest1@example.com", Role = "Reader" },
                    new User { Username = "guest2", Password = BCrypt.Net.BCrypt.HashPassword("guest123"), Email = "guest2@example.com", Role = "Reader" },
                    new User { Username = "sysadmin", Password = BCrypt.Net.BCrypt.HashPassword("sys123"), Email = "sys@example.com", Role = "Admin" },
                    new User { Username = "backadmin", Password = BCrypt.Net.BCrypt.HashPassword("back123"), Email = "back@example.com", Role = "Admin" },
                    new User { Username = "editor1", Password = BCrypt.Net.BCrypt.HashPassword("edit123"), Email = "edit1@example.com", Role = "Reader" },
                    new User { Username = "editor2", Password = BCrypt.Net.BCrypt.HashPassword("edit123"), Email = "edit2@example.com", Role = "Reader" },
                    new User { Username = "reviewer1", Password = BCrypt.Net.BCrypt.HashPassword("rev123"), Email = "rev1@example.com", Role = "Reader" },
                    new User { Username = "reviewer2", Password = BCrypt.Net.BCrypt.HashPassword("rev123"), Email = "rev2@example.com", Role = "Reader" }
                };

                context.Users.AddOrUpdate(u => u.Username, users.ToArray());
                context.SaveChanges();
            }

            private void SeedCategories(ApplicationDbContext context)
            {
                if (context.Categories.Any()) return;

                var categories = new List<Category>
                {
                    new Category { Name = "Công nghệ" },
                    new Category { Name = "Sức khỏe" },
                    new Category { Name = "Khoa học" },
                    new Category { Name = "Kinh doanh" },
                    new Category { Name = "Giải trí" },
                    new Category { Name = "Thể thao" },
                    new Category { Name = "Chính trị" },
                    new Category { Name = "Giáo dục" },
                    new Category { Name = "Du lịch" },
                    new Category { Name = "Ẩm thực" },
                    new Category { Name = "Nghệ thuật" },
                    new Category { Name = "Thời trang" },
                    new Category { Name = "Tài chính" },
                    new Category { Name = "Ô tô" },
                    new Category { Name = "Trò chơi" },
                    new Category { Name = "Làm cha mẹ" },
                    new Category { Name = "Môi trường" },
                    new Category { Name = "Lịch sử" },
                    new Category { Name = "Tâm lý học" },
                    new Category { Name = "Vũ trụ" }
                };

                context.Categories.AddOrUpdate(c => c.Name, categories.ToArray());
                context.SaveChanges();
            }

            private void SeedPosts(ApplicationDbContext context)
            {
                if (context.Posts.Any()) return;

                var users = context.Users.ToList();
                var categories = context.Categories.ToList();
                var random = new Random();

                var posts = new List<Post>();

                for (int i = 1; i <= 30; i++)
                {
                    var user = users[random.Next(users.Count)];
                    var category = categories[random.Next(categories.Count)];

                    posts.Add(new Post
                    {
                        Title = $"Bài viết mẫu số {i}",
                        Content = $"Nội dung mẫu cho bài viết số {i}",
                        CreatedAt = DateTime.Now.AddDays(-i),
                        UserId = user.UserId,
                        CategoryId = category.CategoryId,
                        Status = PostStatus.Pending,
                    });
                }

                context.Posts.AddRange(posts);
                context.SaveChanges();
            }

            private void SeedComments(ApplicationDbContext context)
            {
                if (context.Comments.Any()) return;

                var posts = context.Posts.ToList();
                var users = context.Users.ToList();
                var random = new Random();

                var comments = new List<Comment>();

                foreach (var post in posts)
                {
                    int count = random.Next(1, 5);
                    for (int i = 0; i < count; i++)
                    {
                        var user = users[random.Next(users.Count)];
                        comments.Add(new Comment
                        {
                            Content = $"Bình luận mẫu từ {user.Username}",
                            CreatedAt = DateTime.Now.AddMinutes(-random.Next(1000)),
                            PostId = post.PostId,
                            UserId = user.UserId
                        });
                    }
                }

                context.Comments.AddRange(comments);
                context.SaveChanges();
            }
        }
    }
}
