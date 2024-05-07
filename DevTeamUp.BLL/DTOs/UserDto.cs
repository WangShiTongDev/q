namespace DevTeamUp.BLL.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public IList<ProjectDTO> ProjectsMember { get; set; }
        public IList<SkillDTO> Skill { get; set; }

    }
}