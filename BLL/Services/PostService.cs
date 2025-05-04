using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsFormsApp1.DAL.Models;
using WindowsFormsApp1.DAL.Repository;
using WindowsFormsApp1.DTOs;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1.BLL.Services
{
    public class PostService
    {
        private readonly PostRepository _postRepository;
        public PostService(PostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public List<string> GetAllPostStatus()
        {
            try
            {
                var statuses = Enum.GetValues(typeof(PostStatus))
                    .Cast<PostStatus>()
                    .Select(s => s.ToString())
                    .ToList();
                return statuses;
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
        public List<Post> GetAllPosts()
        {
            try
            {
                return _postRepository.GetAllPosts().Where(p => !p.IsDeleted).ToList();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error retrieving posts", ex);
            }
        }
        /// Thêm bài viết mới
        public void CreatePost(PostDTO postDTO)
        {
            try
            {
                Post post = new Post();
                post.PostId = postDTO.PostId;
                post.Content = postDTO.Content;
                post.Title = postDTO.Title;
                post.CategoryId = postDTO.CategoryId;
                post.UserId = postDTO.UserId;
                _postRepository.AddPost(post);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error creating post", ex);
            }
        }
        // Cập nhật bài viết
        public void UpdatePost(PostDTO postDTO)
        {
            try
            {
                Post post = new Post();
                post.PostId = postDTO.PostId;
                post.Content = postDTO.Content;
                post.Title = postDTO.Title;
                post.CategoryId = postDTO.CategoryId;
                post.UserId = postDTO.UserId;
                _postRepository.UpdatePost(post);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error updating post", ex);
            }
        }
        // Xóa bài viết
        public void DeletePost(Guid postId)
        {
            try
            {
                var post = _postRepository.FindPostById(postId);
                if (post != null)
                {
                    _postRepository.DeletePost(post);
                }
                else
                {
                    throw new Exception("Post not found");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error deleting post", ex);
            }
        }
        // Tìm bài viết theo ID
        public Post GetPostById(Guid postId)
        {
            try
            {
                return _postRepository.FindPostById(postId);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error retrieving post", ex);
            }
        }
        // Tìm bài viết theo từ khóa
        public List<Post> GetPostsByKeyword(string keyword)
        {
            try
            {
                return _postRepository.GetPostsByKeyword(keyword).Where(p => !p.IsDeleted).ToList();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error retrieving posts by keyword", ex);
            }
        }
        // Tìm bài viết theo danh mục
        public List<Post> GetPostsByCategory(Guid categoryId)
        {
            try
            {
                return _postRepository.GetPostsByCategory(categoryId).Where(p => !p.IsDeleted).ToList();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error retrieving posts by category", ex);
            }
        }
        public Post AcceptPost(Guid postId, string response)
        {
            try
            {
                var post = _postRepository.FindPostById(postId);
                if (post != null && post.Status == PostStatus.Pending) // Fixed comparison to use PostStatus enum
                {
                    post.RespondedAt = DateTime.Now;
                    post.ResponseContent = response;
                    post.Status = PostStatus.Approved;
                    _postRepository.UpdatePost(post);
                    return post;
                }
                else
                {
                    throw new Exception("Post not found");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error accepting post", ex);
            }
        }
        public Post RejectPost(Guid postId, string responseContent)
        {
            try
            {
                var post = _postRepository.FindPostById(postId);
                if (post != null && post.Status == PostStatus.Pending)
                {
                    post.Status = PostStatus.Rejected;
                    post.ResponseContent = responseContent;
                    post.RespondedAt = DateTime.Now;
                    _postRepository.UpdatePost(post);
                    return post;
                }
                else
                {
                    throw new Exception("Post not found");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error rejecting post", ex);
            }
        }
        public void SoftDeletePost(Guid postId)
        {
            try
            {
                var post = _postRepository.FindPostById(postId);
                if (post != null)
                {
                    post.IsDeleted = true;
                    post.DeletedAt = DateTime.Now;
                    _postRepository.UpdatePost(post);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error soft deleting post", ex);
            }
        }
        public List<Post> GetAllActivePosts()
        {
            try
            {
                return _postRepository.GetAllPosts().Where(p => !p.IsDeleted).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public List<Post> GetPostsByStatus(PostStatus status)
        {
            try
            {
                return _postRepository.GetAllPosts()
                    .Where(p => p.Status == status && !p.IsDeleted)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving posts by status", ex);
            }
        }
        //Lọc bài viết theo quyền tac gia
        public List<Post> FilteredPostsAuthor (Guid currentUserId)
        {
            try
            {
                var allPosts = GetAllPosts();
                var filteredPosts = allPosts.Where(p => p.UserId == currentUserId || p.Status == PostStatus.Approved).ToList();
                return filteredPosts;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        //Loc bai viet theo quyen nnguoi doc
        public List<Post> FilteredPostsReader ()
        {
            try
            {
                var allPosts = GetAllPosts();
                var filteredPosts = allPosts.Where(p => p.Status == PostStatus.Approved && !p.IsDeleted).ToList();
                return filteredPosts;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<PostManagerDTO> MapPostsToPostWithCategoryDTO(List<Post> posts, List<Category> categories, List<Comment> comments, List<User> users)
        {
            try
            {
                var postData = new List<PostManagerDTO>();
                foreach (var post in posts)
                {
                    var category = categories.FirstOrDefault(c => c.CategoryId == post.CategoryId);
                    var user = users.FirstOrDefault(u => u.UserId == post.UserId);
                    var commentCount = comments.Count(c => c.PostId == post.PostId);
                    postData.Add(new PostManagerDTO
                    {
                        PostId = post.PostId,
                        PostName = post.Title,
                        AuthorName = user?.Username,
                        CategoryName = category?.Name,
                        CreatedAt = post.CreatedAt,
                        CountComment = commentCount,
                        //Status la enum nen can convert
                        Status = post.Status.ToString(),
                    });
                }
                return postData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<PostManagerDTO> SearchByAuthor(Guid authorId, string category, string status, string postTitle)
        {
            try
            {
                var posts = FilteredPostsAuthor(authorId);
                // Lấy dữ liệu danh mục, comment, và user để truyền vào hàm ánh xạ
                var categoryService = new CategoryService(new CategoryRepository(new ApplicationDbContext()));
                var categories = categoryService.GetAllCategories();
                var commentService = new CommentService(new CommentRepository(new ApplicationDbContext()));
                var comments = commentService.GetAllComments();
                var userService = new UserService(new UserRepository(new ApplicationDbContext()));
                var users = userService.GetAllUsers();

                // Lọc theo Category nếu không phải "All"
                if (!string.IsNullOrEmpty(category) && category != "All")
                {
                    var selectedCategory = categories.FirstOrDefault(c => c.Name == category);
                    if (selectedCategory != null)
                    {
                        posts = posts.Where(p => p.CategoryId == selectedCategory.CategoryId).ToList();
                    }
                    else
                    {
                        posts = new List<Post>(); // Không tìm thấy category -> trả về rỗng
                    }
                }

                // Lọc theo Status nếu không phải "All"
                if (!string.IsNullOrEmpty(status) && status != "All")
                {
                    posts = posts.Where(p => p.Status.ToString() == status).ToList();
                }

                // Lọc theo tiêu đề nếu có nhập
                if (!string.IsNullOrEmpty(postTitle))
                {
                    posts = posts.Where(p => p.Title.IndexOf(postTitle, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                }

                return MapPostsToPostWithCategoryDTO(posts, categories, comments, users);
            }
            catch (Exception ex)
            {
                throw new Exception("Error filtering posts by author", ex);
            }
        }
        public List<PostManagerDTO> SearchByAdmin(string category, string status, string postTitle)
        {
            try
            {
                var posts = GetAllActivePosts();
                var categoryService = new CategoryService(new CategoryRepository(new ApplicationDbContext()));
                var categories = categoryService.GetAllCategories();
                var commentService = new CommentService(new CommentRepository(new ApplicationDbContext()));
                var comments = commentService.GetAllComments();
                var userService = new UserService(new UserRepository(new ApplicationDbContext()));
                var users = userService.GetAllUsers();

                // Lọc theo Category nếu không phải "All"
                if (!string.IsNullOrEmpty(category) && category != "All")
                {
                    var selectedCategory = categories.FirstOrDefault(c => c.Name == category);
                    if (selectedCategory != null)
                    {
                        posts = posts.Where(p => p.CategoryId == selectedCategory.CategoryId).ToList();
                    }
                    else
                    {
                        posts = new List<Post>(); // Không tìm thấy category -> trả về rỗng
                    }
                }

                // Lọc theo Status nếu không phải "All"
                if (!string.IsNullOrEmpty(status) && status != "All")
                {
                    posts = posts.Where(p => p.Status.ToString() == status).ToList();
                }

                // Lọc theo tiêu đề nếu có nhập
                if (!string.IsNullOrEmpty(postTitle))
                {
                    posts = posts.Where(p => p.Title.IndexOf(postTitle, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                }
                return MapPostsToPostWithCategoryDTO(posts, categories, comments, users);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<PostManagerDTO> SearchByUser(string category, string postTitle)
        {
            try
            {
                var posts = FilteredPostsReader();
                var categoryService = new CategoryService(new CategoryRepository(new ApplicationDbContext()));
                var categories = categoryService.GetAllCategories();
                var commentService = new CommentService(new CommentRepository(new ApplicationDbContext()));
                var comments = commentService.GetAllComments();
                var userService = new UserService(new UserRepository(new ApplicationDbContext()));
                var users = userService.GetAllUsers();
                // Lọc theo Category nếu không phải "All"
                if (!string.IsNullOrEmpty(category) && category != "All")
                {
                    var selectedCategory = categories.FirstOrDefault(c => c.Name == category);
                    if (selectedCategory != null)
                    {
                        posts = posts.Where(p => p.CategoryId == selectedCategory.CategoryId).ToList();
                    }
                    else
                    {
                        posts = new List<Post>(); // Không tìm thấy category -> trả về rỗng
                    }
                }

                // Lọc theo tiêu đề nếu có nhập
                if (!string.IsNullOrEmpty(postTitle))
                {
                    posts = posts.Where(p => p.Title.IndexOf(postTitle, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                }
                return MapPostsToPostWithCategoryDTO(posts, categories, comments, users);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public PostViewingDTO ShowPostViewingUser(Guid postId)
        {
            try
            {
                var post = GetPostById(postId);
                var user = new UserService(new UserRepository(new ApplicationDbContext())).GetUserById(post.UserId);
                var catagory = new CategoryService(new CategoryRepository(new ApplicationDbContext())).FindCategoryById(post.CategoryId);
                var comments = new CommentService(new CommentRepository(new ApplicationDbContext())).GetCommentsByPost(postId);
                List<CommentDTO> commentDTOs = new List<CommentDTO>();
                foreach (var comment in comments)
                {
                    var commentDTO = new CommentDTO
                    {
                        CommentId = comment.CommentId,
                        Content = comment.Content,
                        PostId = postId,
                        UserId = comment.UserId,
                        UserName = new UserService(new UserRepository(new ApplicationDbContext())).GetUserById(comment.UserId).Username.ToString(),
                        CreatedAt = comment.CreatedAt,
                    };
                    commentDTOs.Add(commentDTO);
                }
                var postData = new PostViewingDTO
                {
                    PostId = postId,
                    Title = post.Title,
                    Category = catagory.Name,
                    AuthorName = user.Username,
                    AuthorId = post.UserId,
                    Status = post.Status.ToString(),
                    Content = post.Content,
                    Response = post.ResponseContent,
                    Comments = commentDTOs
                };
                System.Console.WriteLine(postData.Content + "\n" + postData.Comments);
                return postData;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }

    }
}
