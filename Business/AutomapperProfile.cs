using AutoMapper;
using Business.Models;
using Data.Entities;

namespace Business
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile() 
        {
            CreateMap<MusicBrainzReleaseGroup, Album>()
                .ForMember(a => a.Name, mbrg => mbrg.MapFrom(x => x.Title))
                .ForMember(a => a.Genre, mbrg => mbrg.MapFrom(x => x.Tags.OrderByDescending(t => t.Count).FirstOrDefault()))
                .ForMember(a => a.Artists, mbrg => mbrg.MapFrom(x => x.ArtistCredit.Select(ac => ac.Name)))
                .ForMember(a => a.ReleaseDate, mbrg => mbrg.MapFrom(x => DateTime.Parse(x.FirstReleaseDate)));
        }
    }
}