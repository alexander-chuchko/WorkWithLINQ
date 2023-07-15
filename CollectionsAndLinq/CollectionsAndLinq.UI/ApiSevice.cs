using CollectionsAndLinq.BL.Entities;
using CollectionsAndLinq.BL.Extensions;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Models;
using CollectionsAndLinq.BL.Models.Projects;
using CollectionsAndLinq.BL.Models.Tasks;
using CollectionsAndLinq.BL.Models.Teams;
using CollectionsAndLinq.BL.Models.Users;

namespace CollectionsAndLinq.UI
{
    public class ApiSevice
    {
        private readonly IDataProvider _dataProvider;
        private readonly IDataProcessingService _dataProcessingService;
        public ApiSevice(IDataProvider dataProvider, IDataProcessingService dataProcessingService)
        {
            _dataProvider = dataProvider;
            _dataProcessingService = dataProcessingService;
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            return await _dataProvider.GetProjectsAsync();
        }

        public async Task<Dictionary<string, int>> GetTasksCountInProjectsByUserIdAsync(int userId)
        {
            return await _dataProcessingService.GetTasksCountInProjectsByUserIdAsync(userId);
        }

        public async Task<List<TaskDto>> GetCapitalTasksByUserIdAsync(int userId)
        {
            return await _dataProcessingService.GetCapitalTasksByUserIdAsync(userId);
        }

        public async Task<List<(int Id, string Name)>> GetProjectsByTeamSizeAsync(int teamSize)
        {
            return await _dataProcessingService.GetProjectsByTeamSizeAsync(teamSize);
        }

        public async Task<List<TeamWithMembersDto>> GetSortedTeamByMembersWithYearAsync(int year)
        {
            return await _dataProcessingService.GetSortedTeamByMembersWithYearAsync(year);
        }

        public async Task<List<UserWithTasksDto>> GetSortedUsersWithSortedTasksAsync()
        {
            return await _dataProcessingService.GetSortedUsersWithSortedTasksAsync();
        }

        public async Task<UserInfoDto> GetUserInfoAsync(int userId)
        {
            return await _dataProcessingService.GetUserInfoAsync(userId);
        }

        public async Task<List<ProjectInfoDto>> GetProjectsInfoAsync()
        {
            return await _dataProcessingService.GetProjectsInfoAsync();
        }

        public async Task<PagedList<FullProjectDto>> GetSortedFilteredPageOfProjectsAsync(PageModel pageModel, FilterModel filterModel, SortingModel sortingModel)
        {
            return await _dataProcessingService.GetSortedFilteredPageOfProjectsAsync(pageModel, filterModel, sortingModel);
        }
    }
}
