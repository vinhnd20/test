﻿using AutoMapper;
using QuangCaoDieuHoaAPI.Common;
using QuangCaoDieuHoaAPI.Database;
using QuangCaoDieuHoaAPI.Models;
using QuangCaoDieuHoaAPI.Repositories;
using QuangCaoDieuHoaAPI.Request;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Net.Mail;
using BanVeXemPhimApi.Common;
using QuangCaoDieuHoaAPI.Utility;

namespace QuangCaoDieuHoaAPI.Services
{
    public class UserAuthenticateService
    {
        private readonly UserRepository _userRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;

        public UserAuthenticateService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper)
        {
            _userRepository = new UserRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
        }

        /// <summary>
        /// User login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public object UserLogin(LoginRequest request)
        {
            try
            {
                var user = _userRepository.UserLogin(request);
                if (user == null)
                {
                    throw new ValidateError(1000, "Incorrect email or password");
                }
                if (user.Status == 0)
                {
                    throw new ValidateError(1004, "unactivated account");
                }
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiOption.Secret));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                var claimList = new[]
                {
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.UserData, user.UserName),
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                };
                //if(request.UserName == "Test" && request.Password == "Test")
                //{
                //   var newListClaim = claimList.ToList();
                //   newListClaim.Add(new Claim(ClaimTypes.Role, "Admin"));
                //    claimList = newListClaim.ToArray();
                //}
                var token = new JwtSecurityToken(
                    issuer: _apiOption.ValidIssuer,
                    audience: _apiOption.ValidAudience,
                    expires: DateTime.Now.AddDays(1),
                    claims: claimList,
                    signingCredentials: credentials
                );
                var tokenByString = new JwtSecurityTokenHandler().WriteToken(token);
                return new
                {
                    token = tokenByString,
                    user = user
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// User register account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// 
        #region Register
        public object UserRegister(UserRegisterRequest request)
        {
            try
            {
                var user = _userRepository.FindByCondition(row => row.Email == request.Email).FirstOrDefault();
                if (user != null)
                {
                    throw new ValidateError(1001, "Email has been used");
                }
                var newUser = new User()
                {
                    UserName = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Password = UtilityFunction.CreateMD5(request.Password),
                    Email = request.Email,
                    Status = 1,
                };
                _userRepository.Create(newUser);
                _userRepository.SaveChange();
                return newUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #endregion

        /// <summary>
        /// change password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public object ChangePassword(int userId, ChangePasswordRequest request)
        {
            try
            {
                var user = _userRepository.FindOrFail(userId);
                if (user != null)
                {
                    throw new ValidateError(2000, "User doesn't exist");
                }

                if (UtilityFunction.CreateMD5(request.OldPassword) != user.Password)
                {
                    throw new ValidateError(3003, "OldPassword invalid!");
                }

                if (request.NewPassword != request.ConfirmPassword)
                {
                    throw new ValidateError(1000, "NewPassword and ConfirmPassword doesn't match!");
                }

                user.Password = UtilityFunction.CreateMD5(request.NewPassword);
                user.UpdatedDate = DateTime.UtcNow;
                _userRepository.UpdateByEntity(user);
                _userRepository.SaveChange();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// forgot password
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public object ForgotPassword(string username)
        {
            try
            {
                var user = _userRepository.FindByCondition(row => row.UserName == username).FirstOrDefault();
                if (user == null)
                {
                    throw new ValidateError(2000, "User doesn't exist");
                }

                var listNumber = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                var otp = "";
                Random rand = new Random();
                for (int i = 0; i < 4; i++)
                {
                    var randomNumber = rand.Next(0, 9);
                    otp += listNumber[randomNumber];
                }
                user.OTP = otp;
                _userRepository.UpdateByEntity(user);
                _userRepository.SaveChange();

                var tb = "<div>OTP Code là: " + user.OTP + "</div>";

                MailMessage mail = new MailMessage("pvo.dictionary.hung.dv@gmail.com", user.UserName, "Schedule management send OTP to reset password", tb);
                mail.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Host = "smtp.gmail.com";
                client.UseDefaultCredentials = false;
                client.Port = 587;
                client.Credentials = new System.Net.NetworkCredential("pvo.dictionary.hung.dv@gmail.com", "pjesgrpquyrjjzed");
                client.EnableSsl = true;
                client.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Verify OTP
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public object VerifyOTP(VerifyOTPRequest request)
        {
            try
            {
                var user = _userRepository.FindAll().Where(row => row.UserName == request.Username).FirstOrDefault();
                if (user == null)
                {
                    throw new ValidateError(2000, "UserName doesn’t exist");
                }

                if (user.OTP != request.Otp)
                {
                    throw new ValidateError(3003, "OTP invalid!");
                }

                return UtilityFunction.CreateMD5(user.UserName + user.OTP);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// reset password
        /// </summary>
        /// <param name="resetPasswordRequest"></param>
        /// <returns></returns>
        public bool ResetPassword(ResetPasswordRequest request)
        {
            try
            {
                var user = _userRepository.FindAll().Where(row => row.UserName == request.Username).FirstOrDefault();
                if (user == null)
                {
                    throw new ValidateError(2000, "UserName doesn’t exist");
                }

                if (UtilityFunction.CreateMD5(user.UserName + user.OTP) != request.KeySecret)
                {
                    throw new ValidateError(3003, "KeySecret invalid!");
                }

                user.Password = UtilityFunction.CreateMD5(request.NewPassword);
                user.OTP = "";
                user.UpdatedDate = DateTime.UtcNow;
                _userRepository.UpdateByEntity(user);
                _userRepository.SaveChange();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
