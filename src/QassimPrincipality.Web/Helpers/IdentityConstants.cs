namespace QassimPrincipality.Web.Helpers
{
    public static class IdentityConstants
    {
        public static class Role
        {
            public const string ADMIN = "Admin";
            public const string MANAGER = "Manager";
            public const string EDITOR = "Editor";
            public const string MEMBER = "Member";
        }

        public static class Namer
        {
            public const string RETURN_URL = "ReturnUrl";
            public const string INDEX = "Index";
            public const string SECURITY = "Security";
            public const string RECOVERY_CODES = "RecoveryCodes";
            public const string VERIFICATION_CODE = "VerificationCode";
            public const string CURRENT_TIME = "CurrentTime";
        }

        public static class Cookie
        {
            public const string NAME = "IdentityCookie";
            public const string LOGIN_PATH = "/Security/SignIn";
            public const string LOGOUT_PATH = "/Base/SignOut";
            public const string ACCESS_DENIED_PATH = "/Security/AccessDenied";
        }

        public static class Identity
        {
            public const string ALLOWED_USERNAME_CHARACTERS = "abcdefghijklmnoöpqrsştuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._()";
            public const int FAILED_ENTRY_RIGHT = 3;
            public const int RECOVERY_CODE_COUNT = 5;
        }

        public static class ClaimName
        {
            public const string AGE = "Age";
            public const string CITY = "City";
            public const string ISSUER = "Internal"; //Değıtıcı kim? Kim yarattı?
            public const string EXPIRE_DATE_EXCHANGE = "ExpireDateExchange";
        }

        public static class Policy
        {
            public const string CITY = "CityPolicy";
            public const string AGE = "AgePolicy";
            public const string EXCHANGE = "ExchangePolicy";
        }

        public static class ErrorMessage
        {
            public const string USER_NOT_FOUND = "المستخدم غير موجود";
            public const string USER_LOCKOUT = "المستخدم موقوف.";
            public const string FAILED_LOGIN = "خطأ في تسجيل الدخول.";
            public const string INCORRECT_LOGIN = "بيانات المستخدم غير صحيحة.";
            public const string PASSWORD_WRONG = "بيانات المستخدم غير صحيحة.";
            public const string EMAIL_NOT_VERIFIED = "البريد غير صحيح لم يتم التحقق";
            public const string LOGIN_ERROR = "بيانات المستخدم غير صحيحة.";
            public const string PHONE_NUMBER_USE = "الجوال مستخدم مسبقا";
            public const string VERIFICATION_CODE_NOT_VALID = "كود التحقق غير صحيح";
            public const string VERIFICATION_CODE_NOT_MATCHED = "كود التحقق غير مطابق";
            public const string FAILED_LOGIN_RIGHT_TRY = "عدد مرات التحقق: {0}";
            public const string VERIFICATION_TIME_OVER = "انتهى وقت التحقق";
            public const string PHONE_NUMBER_REQUIRED = "رقم الجوال مطلوب";
        }

        public static class QrCode
        {
            public const string Path = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digit=6";
        }
    }
}
