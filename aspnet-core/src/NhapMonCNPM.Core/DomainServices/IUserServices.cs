using Abp.Domain.Services;
using NhapMonCNPM.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NhapMonCNPM.DomainServices
{
    public interface IUserServices : IDomainService
    {
        IQueryable<User> GetUserByRole(string roleName);
        bool UserHasRole(long userId, string roleName);
    }
}
