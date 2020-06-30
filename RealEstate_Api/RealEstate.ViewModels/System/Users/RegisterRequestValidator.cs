using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.ViewModels.System.Users
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Họ tên không được để trống")
                .MaximumLength(200).WithMessage("Họ tên không quá 200 ký tự");

            RuleFor(x => x.Address).NotEmpty().WithMessage("Địa chỉ không được để trống");

            //RuleFor(x => x.Gender).NotEmpty().WithMessage("Giới tính không được để trống");số 0:Nam sẽ bị cho là empty

            RuleFor(x => x.IdentityNumber).NotEmpty().WithMessage("CMND/CCCD không được để trống");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("Email không được để trống")
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
                .WithMessage("Email không hợp lệ");//tên đăng nhập dạng email

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Số điện thoại không được để trống");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Mật khẩu không được để trống")
                .MinimumLength(6).WithMessage("Mật khẩu ít nhất 6 ký tự");

            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.Password != request.ConfirmPassword)
                {
                    context.AddFailure("Nhập lại mật khẩu không chính xác");
                }
            });
        }
    }
}
