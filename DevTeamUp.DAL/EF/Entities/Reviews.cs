using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.DAL.EF.Entities
{
    // Отзывы о пользователе 
    public class Review : EntityBase
    {
        public int Evaluation { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public int AuthorId { get; set; }
        [InverseProperty(nameof(User.UserComments))]
        public User Author { get; set; }


        public int RecipientId { get; set; }
        [InverseProperty(nameof(User.CommentsForUser))]
        public User Recipient { get; set; }


    }
}
