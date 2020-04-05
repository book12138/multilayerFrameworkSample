using MultilayerFrameworkSample.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalTestProject.Fixture
{
    public class UserInfoFixture
    {
        private readonly IUserInfoDal userInfoDal;
        public UserInfoFixture(IUserInfoDal userInfoDal)
            => this.userInfoDal = userInfoDal;
    }
}
