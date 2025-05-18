using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DAL.Models;
using WindowsFormsApp1.DTOs;

namespace WindowsFormsApp1.BLL.IServices
{
    public interface IPostService
    {
        List<string> GetAllPostStatus();
        List<Post> GetAllPosts();
        void CreatePost(PostDTO postDTO);
        void UpdatePost(PostDTO postDTO);
        void DeletePost(Guid postId);
        Post GetPostById(Guid postId);
        List<Post> GetPostsByKeyword(string keyword);
        List<Post> GetPostsByCategory(Guid categoryId);
        Post AcceptPost(Guid postId, string response);
        Post RejectPost(Guid postId, string responseContent);
        void SoftDeletePost(Guid postId);
        List<Post> GetAllActivePosts();
        List<Post> GetPostsByStatus(PostStatus status);
        List<Post> FilteredPostsAuthor(Guid currentUserId);
        List<Post> FilteredPostsReader();
        List<PostManagerDTO> MapPostsToPostWithCategoryDTO(List<Post> posts, List<Category> categories, List<Comment> comments, List<User> users);
        List<PostManagerDTO> SearchByAuthor(Guid authorId, string category, string status, string postTitle);
        List<PostManagerDTO> SearchByAdmin(string category, string status, string postTitle);
        List<PostManagerDTO> SearchByUser(string category, string postTitle);
        PostViewingDTO ShowPostViewingUser(Guid postId);
    }
}
