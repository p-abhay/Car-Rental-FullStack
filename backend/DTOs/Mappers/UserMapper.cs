using DataAccessLayer.EFModels;
using DTOs.DTOModels;

namespace DTOs.Mapper
{
    public static class UserMapper
    {
        public static UserDTOModel ToDTO(this UserEFModel user)
        {
            //if (user == null) return null;
            return new UserDTOModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password,
                IsAdmin = user.IsAdmin
            };
        }

        public static UserEFModel ToEFModel(this UserDTOModel user)
        {
            //if (user == null) return null;
            return new UserEFModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password,
                IsAdmin = user.IsAdmin
            };
        }
    }
}
