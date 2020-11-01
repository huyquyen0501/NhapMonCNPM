using Microsoft.EntityFrameworkCore;
using NhapMonCNPM.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NhapMonCNPM.TestingWorkScope
{
    public class TestingWorkScopeAppService:NhapMonCNPMAppServiceBase
    {
        public async Task<User> TestingWorkScope()
        {
            return await WorkScope.GetAll<User>().FirstOrDefaultAsync();
        }
    }
}
