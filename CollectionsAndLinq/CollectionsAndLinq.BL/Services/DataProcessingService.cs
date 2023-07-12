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
    public DataProcessingService(IDataProvider dataProvider)
    {
        throw new NotImplementedException();
    }

    public Task<Dictionary<string, int>> GetTasksCountInProjectsByUserIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<List<TaskDto>> GetCapitalTasksByUserIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<List<(int Id, string Name)>> GetProjectsByTeamSizeAsync(int teamSize)
    {
        throw new NotImplementedException();
    }

    public Task<List<TeamWithMembersDto>> GetSortedTeamByMembersWithYearAsync(int year)
    {
        throw new NotImplementedException();
    }

    public Task<List<UserWithTasksDto>> GetSortedUsersWithSortedTasksAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UserInfoDto> GetUserInfoAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<List<ProjectInfoDto>> GetProjectsInfoAsync()
    {
        throw new NotImplementedException();
    }

    public Task<PagedList<FullProjectDto>> GetSortedFilteredPageOfProjectsAsync(PageModel pageModel, FilterModel filterModel, SortingModel sortingModel)
    {
        throw new NotImplementedException();
    }
}
