using FluentValidation;
using MultilayerFrameworkSample.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MultilayerFrameworkSample.Model.Validations
{
    /// <summary>
    /// 用户字段验证
    /// </summary>
    public class UserInfoValidation : Base.BaseValidate<UserInfo>
    {
        /// <summary>
        /// 验证id
        /// </summary>
        protected void ValidateId()
            => RuleFor(c => c.Id)
            .NotEmpty().WithMessage("id不可为空")
            .NotNull().WithMessage("id不可为null");

        /// <summary>
        /// 验证邮箱
        /// </summary>
        protected void ValidateEMail()
            => RuleFor(c => c.EMail)
            .NotEmpty().WithMessage("邮箱不可为空")
            .NotNull().WithMessage("邮箱不可为null")
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage("邮箱格式不正确");

        /// <summary>
        /// 验证密码
        /// </summary>
        protected void ValidatePassword()
            => RuleFor(c => c.Password)
            .NotEmpty().WithMessage("密码不可为空")
            .NotNull().WithMessage("密码不可为null")
            .Length(6, 10).WithMessage("密码长度需要在6到10位之间");
    }
}
