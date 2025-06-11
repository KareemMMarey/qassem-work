using Framework.Core;
using Framework.Core.AutoMapper;
using Framework.Core.Data.Repositories;
using Framework.Core.Extensions;
using Framework.Core.Globalization;
using Framework.Core.SharedServices.Services;
using Framework.Identity.Data.Dtos;
using Framework.Identity.Data.Entities;

using Framework.Identity.Data.Repositories;
using Framework.Identity.Data.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace Framework.Identity.Data.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataProtectorTokenProvider<ApplicationUser> _dataProtectorTokenProvider;
        private readonly PhoneNumberTokenProvider<ApplicationUser> _phoneNumberTokenProvider;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly RoleRepository _roleRepository;
        private readonly UserRepository _userRepository;
        private readonly UserRolesRepository _userRolesRepository;
        private readonly AppSettingsService _appSettingsService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserRoleAppService _userRoleAppService;
        private readonly IWebHostEnvironment _environment;
        private readonly ActiveDirectoryHelperAppService _activeDirectoryAppService;
        private readonly IRoleAppService _roleAppService;
        //private readonly IRepositoryBase<AppIdentityDbContext, Skills> _skillsRepository;
        //private readonly IRepositoryBase<AppIdentityDbContext, Achievements> _achievementsRepository;
        //private readonly IRepositoryBase<AppIdentityDbContext, Education> _educationRepository;
        //private readonly IRepositoryBase<AppIdentityDbContext, Certificates> _certificatesRepository;
        public UserAppService(UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            DataProtectorTokenProvider<ApplicationUser> dataProtectorTokenProvider,
            PhoneNumberTokenProvider<ApplicationUser> phoneNumberTokenProvider,
            RoleManager<ApplicationRole> roleManager,
            RoleRepository roleRepository,
            UserRolesRepository userRolesRepository,
            AppSettingsService appSettingsService,
            UserRepository userRepository,
            SignInManager<ApplicationUser> signInManager,
            UserRoleAppService userRoleAppService,
            IWebHostEnvironment environment,
            ActiveDirectoryHelperAppService activeDirectoryAppService,
            IRoleAppService roleAppService)
            //IRepositoryBase<AppIdentityDbContext, Skills> skillsRepository,
            //IRepositoryBase<AppIdentityDbContext, Achievements> achievementsRepository,
            //IRepositoryBase<AppIdentityDbContext, Education> educationRepository,
            //IRepositoryBase<AppIdentityDbContext, Certificates> certificatesRepository)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _dataProtectorTokenProvider = dataProtectorTokenProvider;
            _phoneNumberTokenProvider = phoneNumberTokenProvider;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _signInManager = signInManager;
            _roleRepository = roleRepository;
            _userRolesRepository = userRolesRepository;
            _appSettingsService = appSettingsService;
            _userRoleAppService = userRoleAppService;
            _environment = environment;
            _activeDirectoryAppService = activeDirectoryAppService;
            _roleAppService = roleAppService;
            //_skillsRepository = skillsRepository;
            //_achievementsRepository = achievementsRepository;
            //_educationRepository = educationRepository;
            //_certificatesRepository = certificatesRepository;
        }

        public string CurrentUserName => _httpContextAccessor?.HttpContext?.User?.Claims.Where(c => c.Type == "username").Select(c => c.Value).SingleOrDefault();
        public string CurrentUserEmail => GetClaimValueByKey("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");
        public bool IsAdmin => CurrentUserRoleName == Roles.Admin.ToString();
        public async Task<UserDto> GetAsync(Guid id)
        {
            var result = await _userManager.FindByIdAsync(id.ToString());
            return result.MapTo<UserDto>();
        }
        public async Task<ApplicationUser> GetUserAsync(Guid id)
        {
            var result = await _userManager.FindByIdAsync(id.ToString());
            return result;
        }
        public async Task<List<ApplicationUser>> SearchUser(string id)
        {
            var result = await _userManager.Users.Where(q => (q.UserName.StartsWith(id) || q.FullName.StartsWith(id) || q.Email.StartsWith(id))).ToListAsync();
            return result;
        }

        //public IdentityUserSearchDto GetList(IdentityUserSearchDto model)
        //{
        //    var filters = new List<Expression<Func<ApplicationUser, bool>>>();

        //    if (model.IsActive.HasValue)
        //    {
        //        filters.Add(u => u.IsActive == model.IsActive);
        //    }

        //    if (!model.Email.IsNullOrEmpty())
        //    {
        //        filters.Add(u => u.NormalizedEmail.Contains(model.Email.ToUpper()));
        //    }

        //    if (!model.PhoneNumber.IsNullOrEmpty())
        //    {
        //        filters.Add(u => u.PhoneNumber == model.PhoneNumber);
        //    }

        //    if (!model.IdentityNo.IsNullOrEmpty())
        //    {
        //        filters.Add(u => u.UserName == model.IdentityNo);
        //    }


        //    if (!model.FullName.IsNullOrEmpty())
        //    {
        //        filters.Add(u => u.FullName.Contains(model.FullName));
        //    }
        //    //Export  ... 
        //    if (model.IsExport)
        //    {
        //        model.ExportedItems = _userRepository.SearchAndSelectWithFilters
        //        (
        //        b => b.OrderByDescending(a => a.CreatedOn),
        //        a => new ApplicationUser()
        //        {
        //            FullName = a.FullName,
        //            Email = a.Email,
        //            PhoneNumber = a.PhoneNumber,
        //            IsActive = a.IsActive
        //        },
        //        filters
        //        );
        //        return model;
        //    }
        //    model.PageSize ??= _appSettingsService.DefaultPagerPageSize;

        //    var result = _userRepository.SearchAndSelectWithFilters
        //        (
        //        model.PageNumber,
        //        model.IsExport ? _appSettingsService.ExportNoOfItems : model.PageSize.Value,
        //        b => b.OrderByDescending(a => a.CreatedOn),
        //         a => new ApplicationUser()
        //         {
        //             Id = a.Id,
        //             FullName = a.FullName,
        //             Email = a.Email,
        //             PhoneNumber = a.PhoneNumber,
        //             IsActive = a.IsActive
        //         },
        //        filters
        //        );

        //    model.Items =
        //        new StaticPagedList<ApplicationUser>(
        //            result,
        //            result.PageNumber,
        //            result.PageSize,
        //            result.TotalItemCount);

        //    return model;

        //}

        public async Task<UserGridSearchDto> GetGridList(UserGridSearchDto model)
        {
            var filters = new List<Expression<Func<ApplicationUser, bool>>>();
            if (!string.IsNullOrEmpty(model.UserTxtSearch))
            {
                filters.Add(u => u.NormalizedEmail.Contains(model.UserTxtSearch) || u.UserName.Contains(model.UserTxtSearch) || u.FullName.Contains(model.UserTxtSearch));
            }
            if (model.IsActive.HasValue)
            {
                filters.Add(u => u.IsActive == model.IsActive.Value);
            }
            if (!string.IsNullOrEmpty(model.RoleName))
            {
                var usersList = await FindAllByRoleNameAsync(model.RoleName);
                var userIDs = usersList.Select(u => u.Id).ToList();
                filters.Add(u => userIDs.Contains(u.Id));
            }
            if (model.Role != null && model.Role.Count != 0)
            {
                var usersList = await FindAllByRoleNameAsync(model.Role[0].Value);
                var userIDs = usersList.Select(u => u.Id).ToList();
                filters.Add(u => userIDs.Contains(u.Id));
            }

            model.PageSize ??= _appSettingsService.DefaultPagerPageSize;
            var result = _userRepository.SearchAndSelectWithFilters
                (
                model.PageNumber,
                model.IsExport ? _appSettingsService.ExportNoOfItems : model.PageSize.Value,
                b => b.OrderByDescending(a => a.CreatedOn),
                 a => new ApplicationUser(a.UserName, a.FullName, a.Email, a.IsActive)
                 {
                     Id = a.Id,
                     FullName = a.FullName,
                     FullNameAr = a.FullNameAr,
                     Email = a.Email,
                     UserName = a.UserName,
                     JobTitle = a.JobTitle,
                     JobTitleAr = a.JobTitleAr,
                     Department = a.Department,
                     DepartmentAr = a.DepartmentAr,
                     EmployeeNumber = a.EmployeeNumber,
                     PhoneNumber = a.PhoneNumber,
                     IsActive = a.IsActive
                 },
                filters
                );
            model.Items =
                new StaticPagedList<ApplicationUser>(
                    result,
                    result.PageNumber,
                    result.PageSize,
                    result.TotalItemCount);
            model.TotalItemsCount = model.Items.TotalItemCount;
            return await Task.FromResult(model);

        }
        public async Task<bool> ChangeStatus(Guid id)
        {
            var oldData = await _userRepository.TableNoTracking.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (oldData != null)
            {
                if (oldData.IsActive)
                {
                    oldData.IsActive = false;
                }
                else
                {
                    oldData.IsActive = true;
                }
                var updatedItem = await _userRepository.UpdateAsync(oldData, true);
                return true;
            }
            return false;
        }
        public async Task<bool> ChangeStatusAsync(Guid id)
        {
            var oldData = await _userRepository.TableNoTracking.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (oldData != null)
            {
                if (oldData.IsActive)
                {
                    oldData.IsActive = false;
                }
                else
                {
                    oldData.IsActive = true;
                }
                var updatedItem = await _userRepository.UpdateAsync(oldData, true);
                return true;
            }
            return false;
        }
        public async Task<UserManagementInsertResultDto> InsertAsync(UserDto user)
        {
            UserManagementInsertResultDto objResult = new UserManagementInsertResultDto();

            var UserObj = await FindByEmailAsync(user.UserPrincipalName);
            if (UserObj != null)
            {
                objResult.InsertedId = Guid.Empty;
                objResult.VerificationMSG = "UserAlreadyExist";
                return objResult;
            }
            user.Id = await AddToUsersTable(user, true);
            objResult.InsertedId = user.Id;

            return objResult;
        }
        public async Task<UserManagementInsertResultDto> IsUserExistAsync(string userName)
        {
            UserManagementInsertResultDto objResult = new UserManagementInsertResultDto();

            var UserObj = await FindByUsernameAsync(userName);
            if (UserObj != null)
            {
                objResult.InsertedId = Guid.Empty;
                objResult.VerificationMSG = "UserAlreadyExist";
                return objResult;
            }
            return objResult;
        }

        public async Task<Guid> AddToUsersTable(UserDto user, bool isActive)
        {
            var userToAdd = new UserCreateDto
            {
                Email = user.UserPrincipalName,
                FullName = user.DisplayName ?? user.FullName,
                UserName = user.UserName,
                IsActive = isActive,
                RoleNames = user.RoleName != null ? new string[] { user.RoleName } : null,
                Password = PasswordGenerator.Generate(8),
                JobTitle = user.JobTitle,
                Department = user.Department,
                EmployeeNumber = user.EmployeeNumber,
                PhoneNumber = user.PhoneNumber
            };
            return (await CreateAsync(userToAdd)).Id;
        }
        public async Task<bool> ChangePassword(Guid userId, string password)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return false;
            }

            var isValid = await _userManager.CheckPasswordAsync(user, password);
            //await _userManager.RemovePasswordAsync(user);

            //var result = await _userManager.AddPasswordAsync(user, password);
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }

        public async Task<UserDto> FindByIdAsync(Guid? id)
        {
            var result = await _userManager.FindByIdAsync(id.ToString());
            return result.MapTo<UserDto>();
        }

        public async Task<UserDto> FindByIdAsync(string id)
        {
            var result = await _userManager.FindByIdAsync(id.ToString());
            return result.MapTo<UserDto>();
        }
        public async Task<UserDto> GetUserInfoAsync(Guid id)
        {
            var result = await FindByIdAsync(id);
            var roles = await _userRepository.GetRolesAsync(id);
            result.RoleName = CultureHelper.IsArabic ? roles[0].DisplayNameAr : roles[0].DisplayNameEn;
            result.ExistRoles = roles.Select(a => new SelectListItem(CultureHelper.IsArabic ? a.DisplayNameAr : a.DisplayNameEn, a.Name)).ToList();
            return result;
        }
        public async Task<UserDto> GetProfileInfoAsync(Guid id)
        {
            var result = await FindByIdAsync(id);
            var roles = await _userRepository.GetRolesAsync(id);
            result.RoleName = roles[0].Name;

            return result;
        }
        public async Task<UserDto> GetByIdEditMode(Guid id)
        {
            try
            {
                var entity = await _userRepository.TableNoTracking
                    .Where(s => s.Id == id).FirstOrDefaultAsync();
                var userDto = entity.MapTo<UserDto>();
                //userDto.Skills = entity.Skills.Select(s => s.Text).ToArray();
                //userDto.Achievements = entity.Achievements.Select(s => s.Text).ToArray();
                //userDto.Education = entity.Education.Select(s => s.Text).ToArray();
                //userDto.Certificates = entity.Certificates.Select(s => s.Text).ToArray();
                return userDto;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<List<RoleDto>> GetRolesAsync(Guid id)
        {
            var roles = await _userRepository.GetRolesAsync(id);
            return roles.MapTo<List<RoleDto>>();
        }
        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles;
        }
        public List<SelectListItem> GetRoles()
        {
            var roles = _roleManager.Roles;
            return roles.MapTo<List<RoleDto>>().Select(c => new SelectListItem { Value = c.Name.ToString(), Text = CultureHelper.IsArabic ? c.DisplayNameAr : c.DisplayNameEn }).ToList();
        }

        public string GetCurrentUserRolesDisplayName()
        {
            var roles = GetRolesDisplayNameAsync(CurrentUserId.Value).GetAwaiter().GetResult();
            return roles.ToList().ToCommaSeparatedString();
        }

        public async Task<string> GetRoleDisplayName(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                return CultureHelper.IsArabic ? role.DisplayNameAr : role.DisplayNameEn;
            }
            else
            {
                await _signInManager.SignOutAsync();
            }
            return string.Empty;
        }

        public async Task<List<SelectListItem>> GetAvailableUserRoles(Guid id)
        {
            var roles = await _userRepository.GetRolesAsync(id);
            return roles.Select(r => new SelectListItem()
            {
                Value = r.Id.ToString(),
                Text = CultureHelper.IsArabic ? r.DisplayNameAr : r.DisplayNameEn
            }).ToList();
        }

        public async Task<List<string>> GetRolesDisplayNameAsync(Guid id)
        {
            var roles = await _userRepository.GetRolesAsync(id);
            var rolesDto = roles.MapTo<List<RoleDto>>();
            return rolesDto.Select(r => r.DisplayName).ToList();
        }

        public async Task<UserDto> UpdateAsync(Guid id, UserUpdateDto input)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            user.Department = input.Department;
            user.JobTitle = input.JobTitle;
            user.IsActive = input.IsActive;

            await _userManager.UpdateAsync(user);
            await _userManager.UpdateSecurityStampAsync(user);

            return user.MapTo<UserDto>();
        }
        public async Task<Guid> UpdateAsync(UserDto input)
        {
            var user = await _userManager.FindByIdAsync(input.Id.ToString());
            if (!user.Id.ToString().IsNotNullOrEmpty())
            {
                return Guid.Empty;
            }
            user.Department = input.Department;
            user.JobTitle = input.JobTitle;
            user.IsActive = input.IsActive;
            user.FullNameAr = input.FullNameAr;
            user.DepartmentAr = input.DepartmentAr;
            user.JobTitleAr = input.JobTitleAr;

            await _userManager.UpdateAsync(user);
            await UpdateUserRoles(input);

            return user.Id;
        }

        private async Task UpdateUserRoles(UserDto input)
        {
            _userRoleAppService.DeleteByUserId(input.Id);

            foreach (var userRole in input.RoleName.Split(",").ToList())
            {
                var role = await _roleAppService.FindByRoleNameAsync(userRole);
                await _userRoleAppService.InsertAsync(new UserRolesDto() { IsActive = true, RoleId = role.Id, UserId = input.Id }, true);
            }
        }

        public async Task<Guid> UpdateProfileInfoAsync(UserDto input)
        {
            var user = await _userManager.FindByIdAsync(input.Id.ToString());
            if (!user.Id.ToString().IsNotNullOrEmpty())
            {
                return Guid.Empty;
            }
            #region Fill user profile data
            user.EmployeeNumber = input.EmployeeNumber;
            user.DepartmentAr = input.DepartmentAr;
            user.FullNameAr = input.FullNameAr;
            user.JobTitleAr = input.JobTitleAr;
            user.PhoneNumber = input.PhoneNumber;
            user.Extension = input.Extension;
            user.Bio = input.Bio;
            user.ProfilePhotoId = input.ProfilePhotoId;

            

            //if (input.Achievements.Any())
            //    await SaveAchievements(input.Id, input.Achievements);

            //if (input.Education.Any())
            //    await SaveEducation(input.Id, input.Education);

            //if (input.Certificates.Any())
            //    await SaveCertificates(input.Id, input.Certificates);
            #endregion

            await _userManager.UpdateAsync(user);
            return user.Id;
        }

        //public async Task SaveSkills(Guid userId, string[] skills)
        //{
        //    if (skills != null && skills.Any())
        //    {
        //        DeleteOldSkills(userId);
        //        foreach (var skill in skills)
        //        {
        //            if (!await IsSkillExist(userId, skill))
        //            {
        //                await _skillsRepository.InsertAsync(new Skills() { Text = skill, ApplicationUserId = userId, CreatedBy = CurrentUserId.ToString() }, true);
        //            }
        //        }
        //    }
        //}
        //private void DeleteOldSkills(Guid userId)
        //{
        //    var oldSkills = _skillsRepository.TableNoTracking.Where(a => a.ApplicationUserId == userId);
        //    _skillsRepository.DeleteRange(oldSkills, true);
        //}
        //public async Task<bool> IsSkillExist(Guid userId, string skill)
        //{
        //    var entity = await _skillsRepository.TableNoTracking.FirstOrDefaultAsync(t => t.Text.ToLower() == skill.ToLower() && t.ApplicationUserId == userId);
        //    return entity != null;
        //}

        //public async Task SaveAchievements(Guid userId, string[] achievements)
        //{
        //    if (achievements != null && achievements.Any())
        //    {
        //        DeleteOldAchievements(userId);
        //        foreach (var achievement in achievements)
        //        {
        //            if (!await IsAchievementExist(userId, achievement))
        //            {
        //                await _achievementsRepository.InsertAsync(new Achievements() { Text = achievement, ApplicationUserId = userId, CreatedBy = CurrentUserId.ToString() }, true);
        //            }
        //        }
        //    }
        //}
        //private void DeleteOldAchievements(Guid userId)
        //{
        //    var oldAchievements = _achievementsRepository.TableNoTracking.Where(a => a.ApplicationUserId == userId);
        //    _achievementsRepository.DeleteRange(oldAchievements, true);
        //}
        //public async Task<bool> IsAchievementExist(Guid userId, string achievement)
        //{
        //    var entity = await _achievementsRepository.TableNoTracking.FirstOrDefaultAsync(t => t.Text.ToLower() == achievement.ToLower() && t.ApplicationUserId == userId);
        //    return entity != null;
        //}

        //public async Task SaveEducation(Guid userId, string[] educations)
        //{
        //    if (educations != null && educations.Any())
        //    {
        //        DeleteOldEducations(userId);
        //        foreach (var education in educations)
        //        {
        //            if (!await IsEducationExist(userId, education))
        //            {
        //                await _educationRepository.InsertAsync(new Education() { Text = education, ApplicationUserId = userId, CreatedBy = CurrentUserId.ToString() }, true);
        //            }
        //        }
        //    }
        //}
        //private void DeleteOldEducations(Guid userId)
        //{
        //    var oldEducations = _educationRepository.TableNoTracking.Where(a => a.ApplicationUserId == userId);
        //    _educationRepository.DeleteRange(oldEducations, true);
        //}
        //public async Task<bool> IsEducationExist(Guid userId, string education)
        //{
        //    var entity = await _educationRepository.TableNoTracking.FirstOrDefaultAsync(t => t.Text.ToLower() == education.ToLower() && t.ApplicationUserId == userId);
        //    return entity != null;
        //}

        //public async Task SaveCertificates(Guid userId, string[] certificates)
        //{
        //    if (certificates != null && certificates.Any())
        //    {
        //        DeleteOldCertificates(userId);
        //        foreach (var certificate in certificates)
        //        {
        //            if (!await IsCertificateExist(userId, certificate))
        //            {
        //                await _certificatesRepository.InsertAsync(new Certificates() { Text = certificate, ApplicationUserId = userId, CreatedBy = CurrentUserId.ToString() }, true);
        //            }
        //        }
        //    }
        //}
        //private void DeleteOldCertificates(Guid userId)
        //{
        //    var oldCertificates = _certificatesRepository.TableNoTracking.Where(a => a.ApplicationUserId == userId);
        //    _certificatesRepository.DeleteRange(oldCertificates, true);
        //}
        //public async Task<bool> IsCertificateExist(Guid userId, string certificate)
        //{
        //    var entity = await _certificatesRepository.TableNoTracking.FirstOrDefaultAsync(t => t.Text.ToLower() == certificate.ToLower() && t.ApplicationUserId == userId);
        //    return entity != null;
        //}

        public async Task UpdateBasicInfoAsync(UserDto input)
        {
            var user = await _userManager.FindByIdAsync(input.Id.ToString());

            user.FullName = input.FullName;
            await _userManager.UpdateAsync(user);
        }
        public async Task UpdateEmployeeInfoAsync(Guid userid, string phone, string fullName)
        {
            var user = await _userManager.FindByIdAsync(userid.ToString());
            user.FullName = fullName;
            user.PhoneNumber = phone;
            user.PhoneNumberConfirmed = true;
            await _userManager.UpdateAsync(user);
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
                return await _userRepository.DeleteAsync(u => u.Id == id, true);
            else
                return false;
        }

        public async Task AddRolesAsync(Guid id, string[] roleNames)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            List<UserRolesDto> userRoles = new List<UserRolesDto>();
            foreach (var roleName in roleNames)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                userRoles.Add(new UserRolesDto
                {
                    RoleId = role.Id,
                    UserId = user.Id,
                    IsActive = true
                });

            }
            await _userRoleAppService.InsertRangeAsync(userRoles, true);
            await _userManager.UpdateSecurityStampAsync(user);
        }

        public async Task AddRoleAsync(Guid id, string roleName)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var role = await _roleManager.FindByNameAsync(roleName);
            await _userRoleAppService.InsertAsync(new UserRolesDto
            {
                RoleId = role.Id,
                UserId = user.Id,
                IsActive = true
            }, true);

            await _userManager.UpdateSecurityStampAsync(user);
        }

        public async Task<UserDto> FindByUsernameAsync(string username)
        {
            if (username != null)
            {
                var user = await _userManager.FindByNameAsync(username);
                return user.MapTo<UserDto>();
            }
            return null;
        }
        public async Task<ApplicationUser> FindByUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            return user;
        }
        public async Task<List<ApplicationUser>> FindByUsernames(List<string> usernames)
        {
            var users = await _userRepository.TableNoTracking.Where(a => usernames.Contains(a.UserName)).ToListAsync();
            return users;
        }

        public async Task<UserDto> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user?.MapTo<UserDto>();
        }

        public async Task<UserDto> FindByEmailOrUsernameAsync(string email, string username)
        {
            var user = await _userRepository.TableNoTracking.FirstOrDefaultAsync(c => c.Email.ToLower().Trim() == email.ToLower().Trim() || c.UserName.ToLower().Trim() == username.ToLower().Trim());
            return user?.MapTo<UserDto>();
        }
        public async Task<UserDto> FindByPhoneAsync(string phone)
        {
            var user = await _userRepository.TableNoTracking.FirstOrDefaultAsync(a => a.PhoneNumber == phone);
            return user?.MapTo<UserDto>();
        }


        public async Task<List<UserDto>> FindAllByRoleNameAsync(string roleName, bool activeOnly = false)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            var userList = activeOnly ? users.Where(u => u.IsActive).ToList() : users.ToList();
            return userList.MapTo<List<UserDto>>();
        }

        public async Task<List<UserDto>> FindAllByCategoryAsync(int category)
        {
            var roles = await _roleAppService.GetRolesByCategoryAsync(category);
            var rolesList = roles.Select(r => r.Id).ToList();
            var userIdsList = await _userRolesRepository.TableNoTracking.Where(u => rolesList.Contains(u.RoleId)).Select(u => u.UserId).ToListAsync();
            var userList = await _userRepository.TableNoTracking.Where(u => userIdsList.Contains(u.Id)).ToListAsync();
            return userList.MapTo<List<UserDto>>();
        }
        public async Task<List<string>> FindUsersInRoleAsync(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            var userList = users.Where(u => u.IsActive).ToList();

            var userNames = userList.Select(u => u.UserName).ToList();

            return userNames;
        }

        public async Task<List<Framework.Core.Angular.Dto.SelectListItem<Guid>>> GetUserListByRoleName(string roleName, bool activeOnly = false)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            var nationalities = users.Select(p => new Framework.Core.Angular.Dto.SelectListItem<Guid>
            {
                Value = p.Id,
                NameAr = p.FullName,
                NameEn = p.FullName,
            }).ToList();
            return nationalities;
        }
        public async Task<List<Framework.Core.Angular.Dto.SelectListItem<Guid>>> GetUserList()
        {
            var users = await _userRepository.TableNoTracking.Select(p => new Framework.Core.Angular.Dto.SelectListItem<Guid>
            {
                Value = p.Id,
                NameAr = p.UserName,
                NameEn = p.UserName,
            }).ToListAsync();
            return users;
        }
        public async Task<List<UserSearchDto>> GetUserSearchList()
        {
            var users = await _userRepository.TableNoTracking.Select(p => new UserSearchDto
            {
                Id = p.Id,
                UserName = p.UserName,
                Email = p.Email,
                FullName = p.FullName,
            }).ToListAsync();
            return users;
        }
        public async Task<List<UserDto>> FindAllByRoleIdAsync(Guid roleId, bool activeOnly = false)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            var userList = activeOnly ? users.Where(u => u.IsActive).ToList() : users.ToList();
            return userList.MapTo<List<UserDto>>();
        }

        public string GenerateToken(Guid id)
        {
            var user = _userManager.FindByIdAsync(id.ToString()).Result;
            var token = _phoneNumberTokenProvider.GenerateAsync("Reset_Password", _userManager, user);
            return token.Result;
        }
        public async Task<string> GetUserNameByUserId(Guid userId)
        {
            return await _userRepository.TableNoTracking.Where(q => q.Id == userId).Select(q => q.UserName).FirstOrDefaultAsync();
        }
        public bool ValidateToken(Guid id, string token)
        {
            var user = _userManager.FindByIdAsync(id.ToString()).Result;
            var isValid = _phoneNumberTokenProvider.ValidateAsync("Reset_Password", token, _userManager, user);
            return isValid.Result;
        }
        public async Task<bool> CheckAccountAvailability(UserCreateDto input, Guid? userId = null)
        {
            var existEmail = await _userManager.FindByEmailAsync(input.Email);
            var existUserName = await _userManager.FindByNameAsync(input.UserName);
            return (existEmail == null || existUserName.Id == userId) && (existUserName == null || existUserName.Id == userId);
        }
        public async Task<UserDto> CreateAsync(UserCreateDto input)
        {
            try
            {
                var user = new ApplicationUser(input.UserName.ToLower(), input.FullName.Trim(), input.Email.Trim().ToLower(), input.IsActive);
                user.Department = input.Department;
                user.JobTitle = input.JobTitle;
                user.EmployeeNumber = input.EmployeeNumber;
                user.PhoneNumber = input.PhoneNumber;
                user.CreatedBy = CurrentUserName ?? input.UserName;
                user.EmailConfirmed = true;
                await _userManager.CreateAsync(user, input.Password);
                await _userManager.SetPhoneNumberAsync(user, input.PhoneNumber);
                await _userManager.SetTwoFactorEnabledAsync(user, input.TwoFactorEnabled);
                await _userManager.SetLockoutEnabledAsync(user, true);

                if (input.RoleNames != null && input.RoleNames.Length > 0)
                {
                    List<UserRolesDto> userRoles = new List<UserRolesDto>();
                    foreach (var roleName in input.RoleNames)
                    {
                        var role = await _roleManager.FindByNameAsync(roleName);
                        userRoles.Add(new UserRolesDto
                        {
                            RoleId = role.Id,
                            UserId = user.Id,
                            IsActive = true
                        });
                    }
                    await _userRoleAppService.InsertRangeAsync(userRoles, true);
                }
                return user.MapTo<UserDto>();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //public async Task<UserDto> CreateUserForExternalAsync(UserCreateDto input)
        //{
        //    var user = new ApplicationUser(input.UserName.ToLower(), input.FullName.Trim(), input.Email.Trim().ToLower(), input.IsExternalUser, input.IsActive);
        //    user.CreatedBy = CurrentUserName;
        //    user.EmailConfirmed = false;
        //    await _userManager.CreateAsync(user, input.Password);
        //    if (input.RoleNames != null && input.RoleNames.Length > 0)
        //    {
        //        List<UserRolesDto> userRoles = new List<UserRolesDto>();
        //        foreach (var roleName in input.RoleNames)
        //        {
        //            var role = await _roleManager.FindByNameAsync(roleName);
        //            userRoles.Add(new UserRolesDto
        //            {
        //                RoleId = role.Id,
        //                UserId = user.Id,
        //                IsActive = true
        //            });
        //        }
        //        await _userRoleAppService.InsertRangeAsync(userRoles);
        //    }
        //    return user.MapTo<UserDto>();
        //}
        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public async Task<bool> IsUserInRoleAsync(Guid userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var role = await _roleRepository.FindByNameAsync(roleName);
            return await _userRolesRepository.TableNoTracking.AnyAsync(s => s.RoleId == role.Id && s.UserId == userId && s.IsActive == true);
        }

        public bool IsCurrentUserInRole(params string[] roles) => CurrentUserRoles != null &&
            this.CurrentUserRoles.Any(r => roles.Contains(r));


        public bool IsCurrentUserRoleMatch(params string[] roles) => !string.IsNullOrEmpty(CurrentUserRoleName) &&
                    roles.Any(r => r.Contains(CurrentUserRoleName));

        public List<string> CurrentUserRoles
        {
            get
            {
                if (_httpContextAccessor.HttpContext.Items["CurrentUserRoles"] == null && CurrentUserName != null)
                {
                    _httpContextAccessor.HttpContext.Items["CurrentUserRoles"] = this.GetCurrentUserRoles().Result.ToList();
                }

                return _httpContextAccessor.HttpContext.Items["CurrentUserRoles"] as List<string>;
            }
        }

        public ApplicationUser CurrentUser
        {
            get
            {
                if (this._httpContextAccessor.HttpContext.Items["CurrentUser"] == null && CurrentUserName != null)
                {
                    var user = _userManager.FindByNameAsync(CurrentUserName).Result;
                    this._httpContextAccessor.HttpContext.Items["CurrentUser"] = user;
                }

                return this._httpContextAccessor.HttpContext.Items["CurrentUser"] as ApplicationUser;
            }
        }

        public Guid? CurrentUserId =>
            _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault()?.Value?.To<Guid?>();

        public Guid? CurrentUserRoleId =>
            _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(s => s.Type == "RoleId")?.Value?.To<Guid?>();

        public string CurrentUserRoleName =>
            _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(s => s.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value?.To<string>();

        public List<string> CurrentPortId => _httpContextAccessor?.HttpContext?.User?.Claims?.Where(q => q.Type == "port").Select(q => q.Value).ToList();
        public List<string> CurrentPortName => _httpContextAccessor?.HttpContext?.User?.Claims?.Where(q => q.Type == "portName").Select(q => q.Value).ToList();

        public async Task<List<UserDto>> GetUsersInRoles(List<string> roleNames)
        {
            var users = new List<UserDto>();
            foreach (var role in roleNames)
            {
                users.AddRange(await FindAllByRoleNameAsync(role, true));
            }

            return users;
        }

        public async Task<string> GetUserNamesInRole(string roleName)
        {
            var users = await FindAllByRoleNameAsync(roleName, true);
            var userNames = users.Select(s => s.Email).JoinAsString(",");
            return userNames;
        }

        private async Task<IList<string>> GetCurrentUserRoles()
        {
            if (CurrentUser != null)
            {
                var roles = await _userRepository.GetRolesByUsernameAsync(CurrentUserName);
                var roleNames = roles.Select(r => r.Name).ToList();
                return roleNames;
            }
            return null;
        }

        public string FindCurrentUserFullName()
        {
            var user = FindByUsernameAsync(CurrentUserName).GetAwaiter().GetResult();
            return user?.FullName;
        }

        public async Task<bool> ChangeUserStatus(Guid id)
        {

            var user = await _userManager.FindByIdAsync(id.ToString());

            user.IsActive = !user.IsActive;
            var savedUser = await _userManager.UpdateAsync(user);
            if (savedUser != null)
            {
                await _userManager.UpdateSecurityStampAsync(user);
            }
            return savedUser != null;
        }
        public async Task<bool> SetUserStatus(Guid id, bool isActive)
        {

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return false;
            user.IsActive = isActive;
            var savedUser = await _userManager.UpdateAsync(user);
            if (savedUser != null)
            {
                await _userManager.UpdateSecurityStampAsync(user);
            }
            return savedUser != null;
        }
        public async Task<bool> IsActiveUser(string UserName)
        {
            return await _userManager.Users.AnyAsync(q => q.UserName == UserName && q.IsActive == true);
        }
        public async Task<ApplicationUser> ValidLogin(LoginDto user)
        {
            var userDB = await FindByUsername(user.UserName);
            if (userDB != null)
            {
                var passHash = new PasswordHasher<string>();
                var verificationResult = passHash.VerifyHashedPassword(user.UserName, userDB.PasswordHash, user.Password);
                if (verificationResult == PasswordVerificationResult.Success)
                {
                    return userDB;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public async Task<ReturnResult> ValidateUser(UserCreateOrUpdateDtoBase input, Guid? id = null)
        {
            var result = new ReturnResult();
            //Validate Create
            if (id == null)
            {
                var userByEmail = await _userManager.FindByEmailAsync(input.Email);
                if (userByEmail != null)
                {
                    result.AddErrorItem("", "EmailAlreadyRegistered");
                    return result;
                }

                var userByPhone = _userRepository.TableNoTracking.FirstOrDefault(a => a.PhoneNumber == input.PhoneNumber);
                if (userByPhone != null)
                {
                    result.AddErrorItem("", "PhoneNumberAlreadyExist");
                    return result;
                }

            }
            else
            {
                var usersByEmail = _userRepository.TableNoTracking.Where(u => u.Email == input.Email && u.Id != id.Value).ToList();
                if (usersByEmail.Any())
                {
                    result.AddErrorItem("", "EmailAlreadyRegistered");
                    return result;
                }

                var userByPhone = _userRepository.TableNoTracking.Where(u => u.PhoneNumber == input.PhoneNumber && u.Id != id.Value).ToList();
                if (userByPhone.Any())
                {
                    result.AddErrorItem("", "PhoneNumberAlreadyExist");
                    return result;
                }

            }

            return result;
        }

        //Tokens
        public async Task<string> GenerateMobileToken(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var token = await _phoneNumberTokenProvider.GenerateAsync("Reset_Password", _userManager, user);
            return token;
        }

        public async Task<bool> ValidateMobileToken(Guid id, string token)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var isValid = await _phoneNumberTokenProvider.ValidateAsync("Reset_Password", token, _userManager, user);
            return isValid;
        }

        public async Task<string> GenerateEmailToken(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var token = await _dataProtectorTokenProvider.GenerateAsync("Reset_Password", _userManager, user);
            return token;
        }

        public async Task<bool> ValidateEmailToken(Guid id, string token)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var isValid = await _dataProtectorTokenProvider.ValidateAsync("Reset_Password", token, _userManager, user);
            return isValid;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(UserDto user)
        {
            var userObject = await _userManager.FindByIdAsync(user.Id.ToString());
            return await _userManager.GeneratePasswordResetTokenAsync(userObject);
        }

        public async Task<IdentityResult> ResetPasswordAsync(UserDto user, string token, string password)
        {
            var userObject = await _userManager.FindByIdAsync(user.Id.ToString());

            return await _userManager.ResetPasswordAsync(userObject, token, password);
        }


        public async Task<bool> ValidateADUser(string userName, string password)
        {
            bool isValid;

            using (var pc = new PrincipalContext(ContextType.Domain, _appSettingsService.ActiveDirectoryDomainName))
            {
                isValid = pc.ValidateCredentials(userName, password);
            }

            //if (isValid)
            //{

            //    isValid = await FindByUsername(userName) != null;

            //}

            return isValid;
        }
        public ADUserCreateDto FindUser(string userName)
        {
            return GetUserFromActiveDirectory(userName);
        }

        public ADUserCreateDto GetUserFromActiveDirectory(string userName)
        {
            using (var pc = new PrincipalContext(ContextType.Domain, _appSettingsService.ActiveDirectoryDomainName))
            {
                var user = UserPrincipal.FindByIdentity(pc, userName);

                return user == null
                    ? null
                    : new ADUserCreateDto
                    {
                        SurName = user.Surname,
                        Description = user.Description,
                        GivenName = user.GivenName,
                        DisplayName = user.DisplayName,
                        Name = user.Name,
                        UserPrincipalName = user.UserPrincipalName,
                        Email = user.EmailAddress ?? user.UserPrincipalName,
                        FullName = user.DisplayName,
                        NationalId = user.EmployeeId,
                        UserName = user.SamAccountName,
                        PhoneNumber = user.VoiceTelephoneNumber
                    };
            }

        }

        public IEnumerable<SelectListItem> GetApplicationRoles() => _roleRepository.TableNoTracking.Select(r => new SelectListItem(CultureHelper.IsArabic ? r.DisplayNameAr : r.DisplayNameEn, r.Id.ToString())).ToList();

        public async Task<List<Guid>> GetUserRolesAsync()
        {
            var roles = await _userRepository.GetRolesAsync(CurrentUserId.Value);
            var idList = roles.Select(r => r.Id).ToList();
            return idList;
        }
        public async Task<List<Guid>> GetUserRolesAsync(Guid UserId)
        {
            var roles = await _userRepository.GetRolesAsync(UserId);
            var idList = roles.Select(r => r.Id).ToList();
            return idList;
        }

        public async Task<List<ApplicationUser>> FindByIds(List<Guid> ids)
        {
            var users = await _userRepository.TableNoTracking.Where(a => ids.Contains(a.Id)).ToListAsync();
            return users;
        }

        public async Task<List<ApplicationUser>> FindByIds(List<string> ids)
        {
            var users = await _userRepository.TableNoTracking.Where(a => ids.Contains(a.Id.ToString())).ToListAsync();
            return users;
        }

        public async Task<bool> RemoveUserFromRole(Guid userId, Guid roleId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                _userRoleAppService.DeleteByUserIdRoleId(user.Id, roleId);
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveUserFromRole(Guid userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var role = await _roleManager.FindByNameAsync(roleName);
            if (user != null && role != null)
            {
                _userRoleAppService.DeleteByUserIdRoleId(user.Id, role.Id);
                return true;
            }
            return false;
        }

        public async Task<IdentityResult> AddRoleClaim(Guid userId, Guid roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var claims = await _userManager.GetClaimsAsync(user);
            var removeClaimsResult = await _userManager.RemoveClaimsAsync(user, claims);
            if (removeClaimsResult.Succeeded)
            {
                var result = await _userManager.AddClaimsAsync(user, new List<Claim> {
                new Claim("RoleId", roleId.ToString()),
                new Claim("RoleName", role.Name)
                });

                return result;
            }
            return null;
        }
        public string GetClaimValueByKey(string Key)
        {
            if (!string.IsNullOrEmpty(Key))
            {
                return _httpContextAccessor?.HttpContext?.User?.Claims?.Where(q => q.Type == Key).Select(q => q.Value).FirstOrDefault();
            }
            return null;
        }
        public async Task<ClaimDto> GetClaimByUsername(string username)
        {
            ClaimDto claimUser = null;
            if (!string.IsNullOrEmpty(username))
            {
                claimUser = new ClaimDto();
                var user = await _userManager.FindByNameAsync(username);
                claimUser.UserId = user.Id;
                claimUser.FullName = user.FullName;
                claimUser.FullNameAr = user.FullNameAr;
                claimUser.Departement = user.Department;
                claimUser.DepartementAr = user.DepartmentAr;
                claimUser.JobTitle = user.JobTitle;
                claimUser.Email = user.Email;
                claimUser.UserRole = _userManager.GetRolesAsync(user).Result.ToList();
            }
            return claimUser;
        }

        public async Task<RoleDto> GetFirstRoleOfCurrentuser()
        {
            RoleDto currentUserRole;
            if (CurrentUserRoleName.Contains(","))
            {
                currentUserRole = await _roleAppService.GetRoleByNameAsync(CurrentUserRoleName.Split(",")[0]);
            }
            else
            {
                currentUserRole = await _roleAppService.GetRoleByNameAsync(CurrentUserRoleName);
            }

            return currentUserRole;
        }
    }
}
