using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.BLL.DTOs
{
    public class ReviewDTO
    {
        public int Evaluation { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
