using CollectionsAndLinq.BL.Entities;
using CollectionsAndLinq.BL.Interfaces;

namespace CollectionsAndLinq.BL.Services
{
    public class DataProvider : IDataProvider
    {
        public Task<List<Project>> GetProjectsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Entities.Task>> GetTasksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Team>> GetTeamsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
