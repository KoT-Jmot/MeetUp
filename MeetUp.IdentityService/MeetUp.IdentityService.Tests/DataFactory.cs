﻿using MeetUp.IdentityService.Application.DTOs.InputDto;
using MeetUp.IdentityService.Application.DTOs.OutputDto;
using MeetUp.IdentityService.Application.RequestFeatures;
using Microsoft.AspNetCore.Identity;

namespace MeetUp.IdentityService.Tests
{
    public static class DataFactory
    {
        public static string UserPassword = "aaa111aaa";

        public static UserForRegistrationDto GetUserForRegistrationDto()
        {
            return new UserForRegistrationDto
            {
                UserName = "ivan",
                Email = "test@gmail.com",
                PhoneNumber = "+375112345781",
                Password = "aaa111aaa"
            };
        }
        public static UserForLoginDto GetUserForLoginDto()
        {
            return new UserForLoginDto
            {
                Email = "test@gmail.com",
                Password = "aaa111aaa"
            };
        }

        public static IdentityUser GetUserEntity()
        {
            return new IdentityUser
            {
                Id = "21f85435-204d-4e1f-80bb-08db734a088b",
                UserName = "ivan",
                Email = "test@gmail.com",
                PhoneNumber = "+375112345781"
            };
        }

        public static OutputUserDto GetOutputUserDto()
        {
            return new OutputUserDto
            {
                Id = "21f85435-204d-4e1f-80bb-08db734a088b",
                UserName = "ivan",
                Email = "test@gmail.com",
                PhoneNumber = "+375112345781"
            };
        }

        public static IEnumerable<IdentityUser> GetUsers()
        {
            return new List<IdentityUser>
            {
                new IdentityUser()
                {
                    Id = "21f85435-204d-4e1f-80bb-08db734a088b",
                    UserName = "ivan",
                    Email = "test@gmail.com",
                    PhoneNumber = "+375112345781"
                },
                new IdentityUser()
                {
                    Id = "52fc493e-ed46-4df7-5544-08db734a420d",
                    UserName = "vlad",
                    Email = "vlad@gmail.com",
                    PhoneNumber = "+375295712360"
                },
                new IdentityUser()
                {
                    Id = "c7264143-e47a-42e4-b97a-29d02088282a",
                    UserName = "kirill",
                    Email = "kirill@gmail.com",
                    PhoneNumber = "+375103912871"
                }
            };
        }
        public static PagedList<OutputUserDto> GetOutputUsers()
        {
            var outputUsers = new List<OutputUserDto>()
            {
                new OutputUserDto()
                {
                    Id = "21f85435-204d-4e1f-80bb-08db734a088b",
                    UserName = "ivan",
                    Email = "test@gmail.com",
                    PhoneNumber = "+375112345781"
                },
                new OutputUserDto()
                {
                    Id = "52fc493e-ed46-4df7-5544-08db734a420d",
                    UserName = "vlad",
                    Email = "vlad@gmail.com",
                    PhoneNumber = "+375295712360"
                },
                new OutputUserDto()
                {
                    Id = "c7264143-e47a-42e4-b97a-29d02088282a",
                    UserName = "kirill",
                    Email = "kirill@gmail.com",
                    PhoneNumber = "+375103912871"
                }
            };

            return PagedList<OutputUserDto>.ToPagedList(outputUsers, 3, 1, 3);
        }
    }
}
