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
                .ForMember(a => a.Genre, mbrg => mbrg.MapFrom(x => x.Tags.OrderByDescending(t => t.Count).Select(t => t.Name).FirstOrDefault()))
                .ForMember(a => a.Artists, mbrg => mbrg.MapFrom(x => x.ArtistCredit.Select(ac => ac.Name)))
                .ForMember(a => a.ReleaseDate, mbrg => mbrg.MapFrom(x => DateTime.Parse(x.FirstReleaseDate)));

            CreateMap<Album, AlbumDto>()
                .ForMember(dto => dto.ReviewCount, a => a.MapFrom(x => x.Reviews.Count()))
                .ReverseMap();

            CreateMap<Album, AlbumDetailsDto>()
                .ForMember(dto => dto.ReviewCount, a => a.MapFrom(x => x.Reviews.Count()))
                .ForMember(dto => dto.ReviewIds, a => a.MapFrom(a => a.Reviews.Select(r => r.Id)))
                .ForMember(dto => dto.PlaylistIds, a => a.MapFrom(a => a.Playlists.Select(p => p.Id)))
                .ReverseMap();
        }
    }
}