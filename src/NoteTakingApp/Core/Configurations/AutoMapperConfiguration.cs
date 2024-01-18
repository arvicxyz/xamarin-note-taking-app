using AutoMapper;

namespace NoteTakingApp.Core
{
    public static class AutoMapperConfiguration
    {
        public static IMapper CreateMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.Dtos.NoteModel, Models.NoteModel>();
                cfg.CreateMap<Models.Dtos.NoteListModel, Models.NoteListModel>();
                cfg.CreateMap<Models.Entities.NoteModel, Models.NoteModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.LocalId));
                cfg.CreateMap<Models.Entities.NoteListModel, Models.NoteListModel>();
                cfg.CreateMap<Models.NoteModel, Models.Dtos.NoteModel>();
                cfg.CreateMap<Models.NoteListModel, Models.Dtos.NoteListModel>();
                cfg.CreateMap<Models.NoteModel, Models.Entities.NoteModel>();
                cfg.CreateMap<Models.NoteListModel, Models.Entities.NoteListModel>();
            });

            return mapperConfiguration.CreateMapper();
        }
    }
}
