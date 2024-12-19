using AutoMapper;
using Business.Exceptions;
using Business.Interfaces;
using Business.Models.Users;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task DeleteAsync(Guid modelId)
        {
            var userInDb = await _unitOfWork.UserRepository.GetByIdAsync(modelId);
            if (userInDb == null)
                throw new MusicLibraryException("User does not exist");

            await _unitOfWork.UserRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DoesUserExistByIdAsync(Guid id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            return user != null;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            return users == null ? Enumerable.Empty<UserDto>() 
                : _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var userInDb = await _unitOfWork.UserRepository.GetByIdAsync(id);
            return userInDb == null ? null 
                : _mapper.Map<UserDto>(userInDb);
        }

        public async Task<UserDetailsDto> GetByIdWithDetailsAsync(Guid id)
        {
            var userInDb = await _unitOfWork.UserRepository.GetByIdWithDetailsAsync(id);
            return userInDb == null ? null 
                : _mapper.Map<UserDetailsDto>(userInDb);
        }

        public async Task UpdateAsync(UserUpdateDto model)
        {
            if (model == null)
                throw new ArgumentNullException("Model can't be null");
            var userInDb = await _unitOfWork.UserRepository.GetByIdAsync(model.Id);
            if (userInDb == null)
                throw new MusicLibraryException("User not found");
            userInDb.About = model.About;
            await _unitOfWork.UserRepository.UpdateAsync(userInDb);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}