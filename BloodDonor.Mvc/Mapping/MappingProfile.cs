using AutoMapper;
using BloodDonor.Mvc.Models.Entities;
using BloodDonor.Mvc.Models.ViewModel;
using BloodDonor.Mvc.Services.Implementations;
using BloodDonor.Mvc.Utilities;

namespace BloodDonor.Mvc.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Donation, DonationListViewModel>()
                .ForMember(dest => dest.Campaign, opt => opt.MapFrom(src => src.Campaign.Title))
                .ForMember(dest => dest.DonorName, opt => opt.MapFrom(src => src.BloodDonor.FullName));
            CreateMap<CampaignEntity, CampaignListViewModel>()
                .ForMember(dest => dest.DonorCount, opt => opt.Ignore());
            CreateMap<BloodDonorEntity, BloodDonorListViewModel>()
                .ForMember(dest => dest.BloodGroup, opt => opt.MapFrom(src => src.BloodGroup.ToString()))
                .ForMember(dest => dest.Donations, opt => opt.MapFrom(src => src.Donations))
                .ForMember(dest => dest.Campaigns, opt => opt.MapFrom(src => src.DonorCampaigns.Select(dc => dc.Campaign)))
                .ForMember(dest => dest.LastDonationDate, opt => opt.MapFrom(src => DateHelper.GetLastDonationDateString(src.LastDonationDate)))
                .ForMember(dest => dest.IsEligible, opt => opt.MapFrom(src => BloodDonorService.IsEligible(src)))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateHelper.CalculateAge(src.DateOfBirth)));

            CreateMap<BloodDonorCreateViewModel, BloodDonorEntity>();
            CreateMap<BloodDonorEditViewModel, BloodDonorEntity>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore())
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ExistingProfilePicture));
            CreateMap<BloodDonorEntity, BloodDonorEditViewModel>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore())
                .ForMember(dest => dest.ExistingProfilePicture, opt => opt.MapFrom(src => src.ProfilePicture));
        }
    }
}
