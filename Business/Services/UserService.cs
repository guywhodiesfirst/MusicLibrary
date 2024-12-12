using AutoMapper;
using Business.Exceptions;
using Business.Interfaces;
using Business.Models;
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
        public async Task AddAsync(UserDto model)
        {
            var userInDb = _unitOfWork.UserRepository.GetByEmailAsync(model.Email);
            if (userInDb != null)
                throw new MusicLibraryException("User already exists!");
            
            model.Id = Guid.NewGuid();
            var user = _mapper.Map<User>(model);
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid modelId)
        {
            var userInDb = _unitOfWork.UserRepository.GetByIdAsync(modelId);
            if (userInDb == null)
                throw new MusicLibraryException("User does not exist");

            await _unitOfWork.UserRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            return users == null ? Enumerable.Empty<UserDto>() : _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var userInDb = await _unitOfWork.UserRepository.GetByIdAsync(id);
            return userInDb == null ? throw new MusicLibraryException("User not found") : _mapper.Map<UserDto>(userInDb);
        }

        public async Task UpdateAsync(UserDto model)
        {
            if (model == null)
                throw new ArgumentNullException("Model can't be null");
            var userInDb = await _unitOfWork.UserRepository.GetByIdAsync(model.Id);
            if (userInDb == null)
                throw new MusicLibraryException("User not found");
            var user = _mapper.Map<User>(model);
            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}