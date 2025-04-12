namespace Storeify.Web.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Store
            CreateMap<Store, StoreViewModel>().ReverseMap();
            CreateMap<Store, SelectListItem>()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));

            //Branches
            CreateMap<BranchViewModel, Branch>()
            .ReverseMap();
             CreateMap<Branch, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));

            //Category
            CreateMap<CategoryViewModel, Category>().ReverseMap();
            CreateMap<Category, SelectListItem>()
             .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));


            //Inventory
            CreateMap<InventoryViewModel, Inventory>()
            .ReverseMap();
            CreateMap<Inventory, SelectListItem>()
               .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));

            //Category
            CreateMap<ProductViewModel, Product>()
            .ReverseMap();
            CreateMap<Product, SelectListItem>()
               .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));
        }

    }
}

