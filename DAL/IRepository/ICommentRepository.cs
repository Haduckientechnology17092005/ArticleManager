using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DAL.Models;

namespace WindowsFormsApp1.DAL.IRepository
{
    public interface ICommentRepository
    {
        void AddComment(Comment comment);
        Comment FindCommentById(Guid id);
        List<Comment> GetAllComments();
    }
}
