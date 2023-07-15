using CollectionsAndLinq.BL.Entities;
using CollectionsAndLinq.BL.Extensions;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Models;
using CollectionsAndLinq.BL.Models.Projects;
using CollectionsAndLinq.BL.Models.Tasks;
using CollectionsAndLinq.BL.Models.Teams;
using CollectionsAndLinq.BL.Models.Users;

namespace CollectionsAndLinq.BL.Services;

// Add implementations to the methods and constructor. You can also add new members to the class.
public class DataProcessingService : IDataProcessingService
{
    private readonly IDataProvider _dataProvider;
    public DataProcessingService(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;   
    }

    public async Task<Dictionary<string, int>> GetTasksCountInProjectsByUserIdAsync(int userId)
    {
        var users = await _dataProvider.GetUsersAsync();
        var projects = await _dataProvider.GetProjectsAsync();
        var tasks = await _dataProvider.GetTasksAsync();

        var result = projects
            .Where(project => project.AuthorId == userId)
            .GroupJoin(tasks, project => project.Id, task => task.ProjectId, (project, projectTasks) => new
            {
                ProjectId = project.Id,
                ProjectName = project.Name,
                TaskCount = projectTasks.Count()
            }
        )
        .ToDictionary(
            project => $"{project.ProjectId} : {project.ProjectName}",
            project => project.TaskCount
        );
        return result;
    }

    public async Task<List<TaskDto>> GetCapitalTasksByUserIdAsync(int userId)
    {
        var users = await _dataProvider.GetUsersAsync();
        var tasks = await _dataProvider.GetTasksAsync();

        var capitalTasks = from user in users
                           join task in tasks on user.Id equals task.PerformerId into userTasks
                           where user.Id == userId
                           from userTask in userTasks.DefaultIfEmpty()
                           where userTask != null && char.IsUpper(userTask.Name[0])
                           select new TaskDto(userTask.Id, userTask.Name, userTask.Description, MapTaskStateToString(userTask.State), userTask.CreatedAt, userTask.FinishedAt);

        return capitalTasks.ToList();
    }

    private string MapTaskStateToString(TaskState state)
    {
        switch (state)
        {
            case TaskState.ToDo:
                return "To Do";
            case TaskState.InProgress:
                return "In Progress";
            case TaskState.Done:
                return "Done";
            case TaskState.Canceled:
                return "Canceled";
            default:
                return "Unknown";
        }
    }

    public async Task<List<(int Id, string Name)>> GetProjectsByTeamSizeAsync(int teamSize)
    {
        var users = await _dataProvider.GetUsersAsync();
        var teams = await _dataProvider.GetTeamsAsync();
        var projects = await _dataProvider.GetProjectsAsync();

        var results = projects
            .Join(
                teams,
                project => project.TeamId,
                team => team.Id,
                (project, team) => new { Project = project, Team = team }
            )
            .GroupJoin(
                users,
                projectTeam => projectTeam.Team.Id,
                user => user.TeamId,
                (projectTeam, usersGroup) => new { projectTeam.Project, UsersCount = usersGroup.Count() }
            )
            .Where(projectTeamUsersCount => projectTeamUsersCount.UsersCount > teamSize)
            .Select(projectTeamUsersCount => (projectTeamUsersCount.Project.Id, projectTeamUsersCount.Project.Name))
            .ToList();

        return results;
    }

    public async Task<List<TeamWithMembersDto>> GetSortedTeamByMembersWithYearAsync(int year)
    {
        var users = await _dataProvider.GetUsersAsync();
        var teams = await _dataProvider.GetTeamsAsync();
        var projects = await _dataProvider.GetProjectsAsync();
        var tasks = await _dataProvider.GetTasksAsync();

        var results = teams
            .OrderBy(team => team.Name)
            .GroupJoin(
                users,
                team => team.Id,
                user => user.TeamId,
                (team, usersGroup) => new TeamWithMembersDto(team.Id, team.Name, usersGroup
                    .Where(user => user.BirthDay.Year < year)
                    .Where(user => user.RegisteredAt.Year > 0)
                    .OrderByDescending(user => user.RegisteredAt)
                    .Select(user => user.ToUserDto())
                    .ToList())
            )
            .Where(teamWithMembers => teamWithMembers.Members.Any())
            .ToList();

        return results;
    }

    public async Task<List<UserWithTasksDto>> GetSortedUsersWithSortedTasksAsync()
    {
        var users = await _dataProvider.GetUsersAsync();
        var tasks = await _dataProvider.GetTasksAsync();

        var results = users
            .OrderBy(user => user.FirstName)
            .GroupJoin(
                tasks,
                user => user.Id,
                task => task.PerformerId,
                (user, userTasks) => new UserWithTasksDto(
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.RegisteredAt,
                    user.BirthDay,
                    userTasks
                        .Select(task => task.ToTaskDto())
                        .OrderBy(task => task.Name.Length)
                        .ToList()
                )
            )
            .ToList();

        return results;
    }

    public async Task<UserInfoDto> GetUserInfoAsync(int userId)
    {
        var users = await _dataProvider.GetUsersAsync();
        var projects = await _dataProvider.GetProjectsAsync();
        var tasks = await _dataProvider.GetTasksAsync();

        var userInfoDto = users
        .Where(user => user.Id == userId)
        .GroupJoin(
            projects,
            user => user.Id,
            project => project.AuthorId,
            (user, userProjects) => new
            {
                User = user.ToUserDto(),
                LastProject = userProjects
                    .OrderByDescending(project => project.CreatedAt)
                    .FirstOrDefault(),
                Tasks = tasks.Where(task => task.PerformerId == user.Id)
            }
        )
        .SelectMany(
            x => x.Tasks.DefaultIfEmpty(),
            (x, task) => new UserInfoDto(
                x.User,
                x.LastProject?.ToProjectDto(),
            x.LastProject != null ? tasks.Count(t => t.ProjectId == x.LastProject.Id) : 0,
                x.Tasks.Count(t => t.State == TaskState.ToDo || t.State == TaskState.InProgress || t.State == TaskState.Canceled),
                x.Tasks.OrderByDescending(t => (t.FinishedAt ?? DateTime.Now) - t.CreatedAt).FirstOrDefault()?.ToTaskDto()
            )
        )
        .FirstOrDefault();

        return userInfoDto;
    }

    public async Task<List<ProjectInfoDto>> GetProjectsInfoAsync()
    {
        var users = await _dataProvider.GetUsersAsync();
        var teams = await _dataProvider.GetTeamsAsync();
        var projects = await _dataProvider.GetProjectsAsync();
        var tasks = await _dataProvider.GetTasksAsync();

        var projectInfoDtoList = (
            from project in projects
            join task in tasks on project.Id equals task.ProjectId into projectTasks
            let longestTaskByDescription = projectTasks.OrderByDescending(t => t.Description.Length).FirstOrDefault()
            let shortestTaskByName = projectTasks.OrderBy(t => t.Name.Length).FirstOrDefault()
            let teamMembersCount = project.Description.Length > 20 || projectTasks.Count() < 3 ? users.Count(u => u.TeamId == project.TeamId) : (int?)null
            select new ProjectInfoDto(
                new ProjectDto(project.Id, project.Name, project.Description, project.CreatedAt, project.Deadline),
                longestTaskByDescription?.ToTaskDto(),
                shortestTaskByName?.ToTaskDto(),
                teamMembersCount)
        ).ToList();

        return projectInfoDtoList;
        
    }

    public async Task<PagedList<FullProjectDto>> GetSortedFilteredPageOfProjectsAsync(PageModel pageModel, FilterModel filterModel, SortingModel sortingModel)
    {
        var users = await _dataProvider.GetUsersAsync();
        var teams = await _dataProvider.GetTeamsAsync();
        var projects = await _dataProvider.GetProjectsAsync();
        var tasks = await _dataProvider.GetTasksAsync();

        // Apply filtering
        var filteredProjects = projects;
        if (filterModel != null)
        {
            if (!string.IsNullOrEmpty(filterModel.Name))
                filteredProjects = (List<Project>)filteredProjects.Where(p => p.Name.Contains(filterModel.Name));
            if (!string.IsNullOrEmpty(filterModel.Description))
                filteredProjects = (List<Project>)filteredProjects.Where(p => p.Description.Contains(filterModel.Description));
            if (!string.IsNullOrEmpty(filterModel.AuthorFirstName))
                filteredProjects = (List<Project>)filteredProjects.Where(p => users.Any(u => u.Id == p.AuthorId && u.FirstName.Contains(filterModel.AuthorFirstName)));
            if (!string.IsNullOrEmpty(filterModel.AuthorLastName))
                filteredProjects = (List<Project>)filteredProjects.Where(p => users.Any(u => u.Id == p.AuthorId && u.LastName.Contains(filterModel.AuthorLastName)));
            if (!string.IsNullOrEmpty(filterModel.TeamName))
                filteredProjects = (List<Project>)filteredProjects.Where(p => teams.Any(t => t.Id == p.TeamId && t.Name.Contains(filterModel.TeamName)));
        }

        // Apply sorting
        var sortedProjects = filteredProjects;
        if (sortingModel != null)
        {
            switch (sortingModel.Property)
            {
                case SortingProperty.Name:
                    sortedProjects = (List<Project>)(sortingModel.Order == SortingOrder.Ascending ?
                        sortedProjects.OrderBy(p => p.Name) :
                        sortedProjects.OrderByDescending(p => p.Name));
                    break;
                case SortingProperty.Description:
                    sortedProjects = (List<Project>)(sortingModel.Order == SortingOrder.Ascending ?
                        sortedProjects.OrderBy(p => p.Description) :
                        sortedProjects.OrderByDescending(p => p.Description));
                    break;

                default:
                    break;
            }
        }

        var totalCount = sortedProjects.Count();
        var pagedProjects = pageModel != null ?
            sortedProjects.Skip((pageModel.PageNumber - 1) * pageModel.PageSize).Take(pageModel.PageSize).ToList() :
            sortedProjects.ToList();

        var fullProjectDtos = pagedProjects.Select(p => new FullProjectDto(
            p.Id,
            p.Name,
            p.Description,
            p.CreatedAt,
            p.Deadline,
            tasks.Where(t => t.ProjectId == p.Id).Select(t => new TaskWithPerformerDto(
                t.Id,
                t.Name,
                t.Description,
                t.State.ToString(),
                t.CreatedAt,
                t.FinishedAt,
                users.FirstOrDefault(u => u.Id == t.PerformerId)?.ToUserDto()
            )).ToList(),
            users.FirstOrDefault(u => u.Id == p.AuthorId)?.ToUserDto(),
            teams.FirstOrDefault(t => t.Id == p.TeamId)?.ToTeamDto()
        )).ToList();

        return new PagedList<FullProjectDto>(fullProjectDtos, totalCount);
    }
}
