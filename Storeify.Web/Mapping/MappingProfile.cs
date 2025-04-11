namespace Storeify.Web.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Store
            CreateMap<Store, StoreFormViewModel>();
            CreateMap<StoreFormViewModel, Store>();
            CreateMap<Store, SelectListItem>()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));

            //Branches
            CreateMap<BranchFormViewModels, Branch>()
            .ReverseMap();
             CreateMap<Branch, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Location));
            //CreateMap<Branch, BranchFormViewModels>()
            //.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy)).ReverseMap();
            //CreateMap<BranchFormViewModels, Branch>()
            //   .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            //CreateMap<Branch, BranchFormViewModels>()
            //    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}

