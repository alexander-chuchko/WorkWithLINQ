using CollectionsAndLinq.BL.Entities;
using CollectionsAndLinq.BL.Interfaces;

namespace CollectionsAndLinq.UI
{
    public class ApiSevice
    {
        private readonly IDataProvider _dataProvider;
        public ApiSevice(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;   
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            return await _dataProvider.GetProjectsAsync();
        }
    }
}
