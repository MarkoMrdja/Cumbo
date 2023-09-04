using AutoMapper;
using Cumbo.Server.Models.KupujemProdajemAd;
using Cumbo.Shared.Enums;

namespace Cumbo.Server.Profiles;

public class AdvertismentProfile : Profile
{
    public AdvertismentProfile()
    {
        CreateMap<AdvertismentDto, Advertisment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => "https://novi.kupujemprodajem.com" + src.Url))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => int.Parse(src.Price)))
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => GetProductTypeFromUrl(src.Url)))
                .ForMember(dest => dest.CurrentlyActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.LastActive, opt => opt.MapFrom(src => DateTime.UtcNow)
            );
    }

    private ProductType GetProductTypeFromUrl(string url)
    {
        if (url.Contains("mobilni-telefoni"))
        {
            return ProductType.Phone;
        }
        else if (url.Contains("mobilni-tel-oprema-i-delovi"))
        {
            return ProductType.AccessoriesAndParts;
        }
        else if (url.Contains("audio"))
        {
            return ProductType.Headphones;
        }
        else if (url.Contains("tv-i-video"))
        {
            return ProductType.TV;
        }
        else if (url.Contains("konzole-i-igrice"))
        {
            return ProductType.GameConsoles;
        }
        else if (url.Contains("nakit-satovi-i-dragocenosti"))
        {
            return ProductType.Watch;
        }
        else if (url.Contains("kompjuteri-laptop-i-tablet"))
        {
            return ProductType.ComputersAndTablets;
        }
        else if (url.Contains("sport-i-razonoda"))
        {
            return ProductType.SportAndLeasure;
        }
        else if (url.Contains("kompjuteri-desktop"))
        {
            return ProductType.Dektop;
        }
        else if (url.Contains("foto-aparati-i-kamere"))
        {
            return ProductType.Camera;
        }
        else
        {
            return ProductType.Unknown;
        }
    }
}

