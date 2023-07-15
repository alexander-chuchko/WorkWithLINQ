
using CollectionsAndLinq.BL.Models.Projects;
using CollectionsAndLinq.BL.Models.Tasks;
using CollectionsAndLinq.BL.Models.Teams;
using CollectionsAndLinq.BL.Models.Users;

namespace CollectionsAndLinq.BL.Extensions
{
    public static class DtoModel
    {
        public static TaskDto ToTaskDto(this Entities.Task task)
        {
            TaskDto taskDto = null;
            if (task != null) 
            {
                taskDto = new TaskDto(task.Id, char.ToUpper(task.Name[0]) + task.Name.Substring(1), task.Description, task.State.ToString(), task.CreatedAt, task.FinishedAt);
            }

            return taskDto;
        }

        public static UserDto ToUserDto(this Entities.User user)
        {
            UserDto userDto = null;
            if (user != null)
            {
                userDto = new UserDto(user.Id, user.FirstName, user.LastName, user.Email, user.RegisteredAt, user.BirthDay);
            }

            return userDto;
        }

        public static ProjectDto ToProjectDto (this Entities.Project project)
        {
            ProjectDto projectDto = null;
            if (project != null)
            {
                projectDto = new ProjectDto(project.Id, project.Name, project.Description, project.CreatedAt, project.Deadline);
            }

            return projectDto;
        }

        public static TeamDto ToTeamDto(this Entities.Team team)
        {
            TeamDto teamDto = null;
            if (team != null)
            {
                teamDto = new TeamDto(team.Id, team.Name, team.CreatedAt);
            }

            return teamDto;
        }
    }
}
