using System;
using UescColcicAPI.Core;
using UescColcicAPI.Services.InputModels;
using UescColcicAPI.Services.ViewModels;
namespace UescColcicAPI.Services.BD.Interfaces;

public interface IUserCRUD : IBaseCRUD<UserViewModel, UserInputModel>
{
    int Create(UserInputModel user);
    void Update(int id, UserInputModel user);
    void Delete(int id);
    UserViewModel ReadById(int id);
    IEnumerable<UserViewModel> ReadAll();
    User GetUserByUsername(string username);
    
}

