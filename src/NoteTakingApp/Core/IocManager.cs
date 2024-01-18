using DryIoc;
using NoteTakingApp.Services;
using NoteTakingApp.Services.ApiClientServices;
using NoteTakingApp.Services.Interfaces;
using NoteTakingApp.ViewModels;

namespace NoteTakingApp.Core
{
    public static class IocManager
    {
        public static IContainer Container { get; private set; }

        public static void RegisterDependencies(IContainer container)
        {
            container.RegisterInstance(AutoMapperConfiguration.CreateMapper());

            container.Register(typeof(IApiService<>), typeof(ApiService<>));
            container.Register(typeof(IDataStoreService<>), typeof(DataStoreService<>));

            // Services
            container.Register<IMainPageService, MainPageService>();

            // View Models
            container.Register<MainPageViewModel>();
            container.Register<DetailsPageViewModel>();
            container.Register<AddEditPageViewModel>();

            Container = container;
        }
    }
}
