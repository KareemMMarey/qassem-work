using Framework.Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace QassimPrincipality.Web.Controllers
{
    public class MockNafathController : BaseController
    {
        public MockNafathController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager = null, RoleManager<ApplicationRole> roleManager = null) : base(userManager, signInManager, roleManager)
        {
        }
        [HttpPost("authorize")]
        public IActionResult Authorize([FromBody] NafathLoginRequest request)
        {
            // Simulate waiting for mobile approval
            return Ok(new
            {
                status = "pending",
                sessionId = Guid.NewGuid().ToString()
            });
        }

        [HttpGet("token")]
        public IActionResult GetToken([FromQuery] string sessionId)
        {
            // Simulate approval and return token
            var staticPersonData = new
            {
                id = Guid.NewGuid(),
                nationalId = "1234567890",
                firstNameAr = "كريم",
                secondNameAr = "محمد",
                thirdNameAr = "حسن",
                lastNameAr = "المرى",
                fullNameAr = "كريم محمد حسن المرى",
                firstNameEn = "Kareem",
                secondNameEn = "Mohamed",
                thirdNameEn = "Hassan",
                lastNameEn = "El Marey",
                fullNameEn = "Kareem Mohamed Hassan El Marey",
                dateOfBirthHijri = "1405-01-01",
                dateOfBirthGregorian = "1985-10-15",
                gender = "Male",
                mobileNumber = "966500000000",
                issuePlace = "Riyadh",
                nationality = "Saudi",
                identityType = "NationalID",
                identityExpiryDate = "1447-01-01"
            };

            return Ok(new
            {
                access_token = "mocked-token-123",
                expires_in = 3600,
                token_type = "bearer",
                person = staticPersonData
            });
        }
    }

    

    public class NafathLoginRequest
    {
        public string NationalId { get; set; }
    }

}
