﻿using AutoMapper;
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
                .ReverseMap();

            CreateMap<Album, AlbumDetailsDto>()
                .ForMember(dto => dto.ReviewCount, a => a.MapFrom(x => x.Reviews.Count()))
                .ForMember(dto => dto.ReviewIds, a => a.MapFrom(x => x.Reviews.Select(r => r.Id)))
                .ForMember(dto => dto.PlaylistIds, a => a.MapFrom(x => x.Playlists.Select(p => p.Id)))
                .ReverseMap();

            CreateMap<Playlist, PlaylistDto>()
                .ForMember(dto => dto.AlbumCount, p => p.MapFrom(x => x.Albums.Count()))
                .ReverseMap();

            CreateMap<Playlist, PlaylistDetailsDto>()
                .ForMember(dto => dto.AlbumCount, p => p.MapFrom(x => x.Albums.Count()))
                .ForMember(dto => dto.Albums, p => p.MapFrom(x => x.Albums))
                .ReverseMap();

            CreateMap<User, UserDto>()
                .ReverseMap();

            CreateMap<User, UserDetailsDto>()
                .ForMember(dto => dto.Reviews, u => u.MapFrom(x => x.Reviews))
                .ForMember(dto => dto.Comments, u => u.MapFrom(x => x.Comments))
                .ForMember(dto => dto.Playlists, u => u.MapFrom(x => x.Playlists))
                .ForMember(dto => dto.CommentCount, u => u.MapFrom(x => x.Comments.Count))
                .ForMember(dto => dto.PlaylistCount, u => u.MapFrom(x => x.Playlists.Count))
                .ForMember(dto => dto.ReviewCount, u => u.MapFrom(x => x.Reviews.Count))
                .ReverseMap();

            CreateMap<Review, ReviewDto>()
                .ForMember(dto => dto.AlbumName, r => r.MapFrom(x => x.Album.Name))
                .ReverseMap();

            CreateMap<Review, ReviewDetailsDto>()
                .ForMember(dto => dto.AlbumName, r => r.MapFrom(x => x.Album.Name))
                .ForMember(dto => dto.CommentIds, r => r.MapFrom(x => x.Comments.Select(c => c.Id)))
                .ForMember(dto => dto.ReactionIds, r => r.MapFrom(x => x.Reactions.Select(r => r.Id)))
                .ReverseMap();

            CreateMap<Review, ReviewReactionDto>()
                .ReverseMap();

            CreateMap<Comment, CommentDto>()
                .ForMember(dto => dto.Username, c => c.MapFrom(x => x.User.Username))
                .ReverseMap();
        }
    }
}