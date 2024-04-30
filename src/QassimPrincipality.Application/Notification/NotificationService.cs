
using QassimPrincipality.Domain.Entities.Services.Main;
using QassimPrincipality.Domain.Interfaces;
using Framework.Core.Notifications;
using Framework.Core.SharedServices.Services;
using Framework.Identity.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace QassimPrincipality.Application
{
    public class NotificationAppService
    {
        private readonly INotificationsManager _notificationsManager;
        private readonly IUserAppService _userAppService;

        //private readonly ISmsService _smsService;
        private readonly AppSettingsService _appSettingsService;

        public NotificationAppService(INotificationsManager notificationsManager,
            IUserAppService userAppService,
            //ISmsService smsService,
            AppSettingsService appSettingsService)
        {
            _notificationsManager = notificationsManager;
            _userAppService = userAppService;
            //_smsService = smsService;
            _appSettingsService = appSettingsService;
        }

        
    }
}