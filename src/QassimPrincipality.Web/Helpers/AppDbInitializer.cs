
using Microsoft.EntityFrameworkCore;
using QassimPrincipality.Domain.Entities.Lookups.NewSchema;
using QassimPrincipality.Domain.Entities.Lookups.NewSchema.Content;
using QassimPrincipality.Infrastructure.Data;

namespace QassimPrincipality.Web.Helpers
{
	public class AppDbInitializer
	{
		public static void Seed(IServiceCollection services, WebApplicationBuilder builder)
		{
			using (ServiceProvider serviceProvider = services.BuildServiceProvider())
			{
				var context = serviceProvider.GetRequiredService<AppDbContext>();

				context.Database.EnsureCreated();
				SeedForms(context);
				SeedCategories(context);
				SeedCountries(context);
				SeedAboutPageSections(context);
				SeedGovernrates(context);
				SeedNews(context);
				SeedStatistics(context);
				SeedEServices(context);
				SeedEServiceDetails(context);
				SeedEServiceRequirement(context);
				SeedEServiceFlow(context);
				SeedFAQs(context);
				SeedServiceSteps(context);

            }
		}



		private static void SeedForms(AppDbContext context)
		{
			// EService Form
			if (!context.Set<EServiceForm>().Any())
			{
				context
					.Set<EServiceForm>()
					.AddRange(
						new List<EServiceForm>()
						{
								new EServiceForm()
								{
									NameAr = "نموذج خدمات الزواج",
									NameEn = "Mirrage Services",
									StepCount = 4,
									IsActive = true,
									CreatedBy = "admin",
									CreatedOn = DateTime.Now,
								},
								new EServiceForm()
								{
									NameAr = "نموذج خدمات السجناء",
									NameEn = "Prisoners Services",
									StepCount = 4,
									IsActive = true,
									CreatedBy = "admin",
									CreatedOn = DateTime.Now,
								},
								new EServiceForm()
								{
									NameAr = "نموذج الخدمات الأساسية",
									NameEn = "Main Services",
									StepCount = 4,
									IsActive = true,
									CreatedBy = "admin",
									CreatedOn = DateTime.Now,
								}
						}
					);
				context.SaveChanges();
			}
		}
		private static void SeedCategories(AppDbContext context)
		{
			// Services Category
			if (!context.Set<ServicesCategory>().Any())
			{
				context
					.Set<ServicesCategory>()
					.AddRange(
						new List<ServicesCategory>()
						{
								new ServicesCategory()
								{
									NameAr = "خدمات الزواج",
									NameEn = "Mirrage Services",
									IsActive = true,
									CreatedBy = "admin",
									CreatedOn = DateTime.Now,
								},
								new ServicesCategory()
								{
									NameAr = "خدمات السجناء",
									NameEn = "Prisoners Services",
									IsActive = true,
									CreatedBy = "admin",
									CreatedOn = DateTime.Now,
								},
								new ServicesCategory()
								{
									NameAr = "خدمات أساسية",
									NameEn = "Main Services",
									IsActive = true,
									CreatedBy = "admin",
									CreatedOn = DateTime.Now
								}
						}
					);
				context.SaveChanges();
			}
		}
		private static void SeedCountries(AppDbContext context)
		{
			if (!context.Set<Country>().Any())
			{
				var countries = new List<Country>
		{
			new Country
			{
				NameAr = "مصر",
				NameEn = "Egypt",
				IsActive = true,
				CreatedBy = "admin",
				CreatedOn = DateTime.Now
			},
			new Country
			{
				NameAr = "السعودية",
				NameEn = "Saudi Arabia",
				IsActive = true,
				CreatedBy = "admin",
				CreatedOn = DateTime.Now
			},
			new Country
			{
				NameAr = "الهند",
				NameEn = "India",
				IsActive = true,
				CreatedBy = "admin",
				CreatedOn = DateTime.Now
			},
			new Country
			{
				NameAr = "الأردن",
				NameEn = "Jordan",
				IsActive = true,
				CreatedBy = "admin",
				CreatedOn = DateTime.Now
			},
			new Country
			{
				NameAr = "باكستان",
				NameEn = "Pakistan",
				IsActive = true,
				CreatedBy = "admin",
				CreatedOn = DateTime.Now
			}
		};

				context.Set<Country>().AddRange(countries);
				context.SaveChanges();
			}
		}
		private static void SeedAboutPageSections(AppDbContext context)
		{
			if (!context.Set<AboutPageSection>().Any())
			{
				var sections = new List<AboutPageSection>
		{
			new AboutPageSection
			{
				AboutSectionType = Domain.Enums.AboutSectionType.About,
				NameAr = "عن الإمارة",
				NameEn = "About Emirate",
				Description = "أنشئت إمارة منطقة القصيم عام 1326هـ...",
				Order = 1,
				IsActive = true,
				CreatedBy = "admin",
				CreatedOn = DateTime.Now
			},
			new AboutPageSection
			{
				AboutSectionType = Domain.Enums.AboutSectionType.Tasks,
				NameAr = "مهام الإمارة",
				NameEn = "Duties of Emirate",
				Description = "تمثل الإمارة السلطة الإدارية في المنطقة...",
				Order = 2,
				IsActive = true,
				CreatedBy = "admin",
				CreatedOn = DateTime.Now
			},
			new AboutPageSection
			{

				AboutSectionType = Domain.Enums.AboutSectionType.Goals,
				NameAr = "أهداف الإمارة",
				NameEn = "Objectives",
				Description = "1. ضبط الأمن والنظام العام...",
				Order = 3,
				IsActive = true,
				CreatedBy = "admin",
				CreatedOn = DateTime.Now
			},
			new AboutPageSection
			{
				AboutSectionType = Domain.Enums.AboutSectionType.Policies,
				NameAr = "الأنظمة والسياسات",
				NameEn = "Systems and Policies",
				Description = "تخضع إمارة منطقة القصيم للأنظمة والسياسات العامة...",
				Order = 4,
				IsActive = true,
				CreatedBy = "admin",
				CreatedOn = DateTime.Now
			},
			new AboutPageSection
			{

				AboutSectionType = Domain.Enums.AboutSectionType.General,
				NameAr = "المسمى",
				NameEn = "Name",
				Description = "أُطلق لفظ الإمارة على المنطقة...",
				Order = 5,
				IsActive = true,
				CreatedBy = "admin",
				CreatedOn = DateTime.Now
			},
			new AboutPageSection
			{

				AboutSectionType = Domain.Enums.AboutSectionType.Population,
				NameAr = "المساحة والسكان",
				NameEn = "Area & Population",
				Description = "تبلغ مساحة القصيم 73,000 كيلومتر مربع...",
				Order = 6,
				IsActive = true,
				CreatedBy = "admin",
				CreatedOn = DateTime.Now
			}
		};

				context.Set<AboutPageSection>().AddRange(sections);
				context.SaveChanges();
			}
		}
		private static void SeedGovernrates(AppDbContext context)
		{
			if (!context.Set<Governorate>().Any())
			{
				context.Set<Governorate>().AddRange(new List<Governorate>
		{
			new Governorate {  NameAr = "محافظة الأسياح", NameEn = "Al-Asyah Governorate", ImageUrl = "/images/governorates/asiyah.jpg", Order = 1, IsActive = true, CreatedBy = "admin", CreatedOn = DateTime.Now },
			new Governorate {  NameAr = "محافظة البكيرية", NameEn = "Al-Bukayriyah Governorate", ImageUrl = "/images/governorates/bukairiyah.jpg", Order = 2, IsActive = true, CreatedBy = "admin", CreatedOn = DateTime.Now },
			new Governorate {  NameAr = "محافظة البدائع", NameEn = "Al-Badaea Governorate", ImageUrl = "/images/governorates/badaea.jpg", Order = 3, IsActive = true, CreatedBy = "admin", CreatedOn = DateTime.Now },
			new Governorate {  NameAr = "محافظة رياض الخبراء", NameEn = "Riyadh Al-Khubara Governorate", ImageUrl = "/images/governorates/riyadh.jpg", Order = 4, IsActive = true, CreatedBy = "admin", CreatedOn = DateTime.Now },
			new Governorate {  NameAr = "محافظة الرس", NameEn = "Ar-Rass Governorate", ImageUrl = "/images/governorates/arass.jpg", Order = 5, IsActive = true, CreatedBy = "admin", CreatedOn = DateTime.Now },
			new Governorate {  NameAr = "محافظة النبهانية", NameEn = "Al-Nabhaniyah Governorate", ImageUrl = "/images/governorates/nbhaniah.jpg", Order = 6, IsActive = true, CreatedBy = "admin", CreatedOn = DateTime.Now },
			new Governorate {  NameAr = "محافظة المذنب", NameEn = "Al-Mithnab Governorate", ImageUrl = "/images/governorates/muthnib.jpg", Order = 7, IsActive = true, CreatedBy = "admin", CreatedOn = DateTime.Now },
			new Governorate {  NameAr = "محافظة ضرية", NameEn = "Dhuriyah Governorate", ImageUrl = "/images/governorates/dhariah.jpg", Order = 8, IsActive = true, CreatedBy = "admin", CreatedOn = DateTime.Now },
			new Governorate {  NameAr = "محافظة عنيزة", NameEn = "Unaizah Governorate", ImageUrl = "/images/governorates/onaizah.jpg", Order = 9, IsActive = true, CreatedBy = "admin", CreatedOn = DateTime.Now },
			new Governorate {  NameAr = "محافظة الشماسية", NameEn = "Al-Shamasiyah Governorate", ImageUrl = "/images/governorates/shamasiah.jpg", Order = 10, IsActive = true, CreatedBy = "admin", CreatedOn = DateTime.Now },
			new Governorate {  NameAr = "محافظة عيون الجواء", NameEn = "Uyun Al-Jiwa Governorate", ImageUrl = "/images/governorates/uyon.jpg", Order = 11, IsActive = true, CreatedBy = "admin", CreatedOn = DateTime.Now },
			new Governorate {  NameAr = "محافظة عقلة الصقور", NameEn = "Uqlat As-Suqur Governorate", ImageUrl = "/images/governorates/uqla.jpg", Order = 12, IsActive = true, CreatedBy = "admin", CreatedOn = DateTime.Now },
			new Governorate {  NameAr = "محافظة بريدة", NameEn = "Buraidah Governorate", ImageUrl = "/images/governorates/buraydah.jpg", Order = 13, IsActive = true, CreatedBy = "admin", CreatedOn = DateTime.Now }
		});

				context.SaveChanges();
			}
		}
		private static void SeedNews(AppDbContext context)
		{
			if (!context.Set<News>().Any())
			{
				context.Set<News>().AddRange(new List<News>
		{
			new News
			{
				NameAr = "هذا نص افتراضي هنا هذا نص افتراضي هنا",
				NameEn = "Sample headline goes here",
				ShortDescriptionAr = "هذا نص افتراضي هنا هذا نص افتراضي هنا هذا نص افتراضي هنا",
				ShortDescriptionEn = "This is sample news description in English.",
				ImageUrl = "/images/news/news1.jpg",
				PublishDate = DateTime.Parse("2023-03-19"),
				IsPublished = true,
				CreatedBy = "admin",
				CreatedOn = DateTime.Now
			},
			new News
			{
				NameAr = "هذا نص افتراضي هنا هذا نص افتراضي هنا",
				NameEn = "Sample headline goes here",
				ShortDescriptionAr = "هذا نص افتراضي هنا هذا نص افتراضي هنا هذا نص افتراضي هنا",
				ShortDescriptionEn = "Another sample news description in English.",
				ImageUrl = "/images/news/news2.jpg",
				PublishDate = DateTime.Parse("2023-03-19"),
				IsPublished = true,
				CreatedBy = "admin",
				CreatedOn = DateTime.Now
			},
			new News
			{
				NameAr = "هذا نص افتراضي هنا هذا نص افتراضي هنا",
				NameEn = "Sample headline goes here",
				ShortDescriptionAr = "هذا نص افتراضي هنا هذا نص افتراضي هنا هذا نص افتراضي هنا",
				ShortDescriptionEn = "News details with English description.",
				ImageUrl = "/images/news/news3.jpg",
				PublishDate = DateTime.Parse("2023-03-19"),
				IsPublished = true,
				CreatedBy = "admin",
				CreatedOn = DateTime.Now
			}
		});

				context.SaveChanges();
			}
		}
		private static void SeedStatistics(AppDbContext context)
		{
			if (!context.Set<Statistic>().Any())
			{
				context.Set<Statistic>().AddRange(new List<Statistic>
		{
			new Statistic { NameAr = "المنشآت التعليمية", NameEn = "Educational Institutions", Value = "4200", IconClass = "fa fa-school", OrderIndex = 1, IsActive = true, CreatedBy = "admin", CreatedOn = DateTime.Now },
			new Statistic { NameAr = "إجمالي عدد السكان", NameEn = "Total Population", Value = "1,336,179", IconClass = "fa fa-users", OrderIndex = 2, IsActive = true, CreatedBy = "admin", CreatedOn = DateTime.Now },
			new Statistic { NameAr = "إجمالي عدد الأسر", NameEn = "Total Families", Value = "313,247", IconClass = "fa fa-home", OrderIndex = 3, IsActive = true, CreatedBy = "admin", CreatedOn = DateTime.Now },
			new Statistic { NameAr = "المنشآت الصحية", NameEn = "Healthcare Facilities", Value = "300", IconClass = "fa fa-hospital", OrderIndex = 4, IsActive = true, CreatedBy = "admin", CreatedOn = DateTime.Now },
			new Statistic { NameAr = "المنشآت الرياضية", NameEn = "Sports Facilities", Value = "172", IconClass = "fa fa-futbol", OrderIndex = 5, IsActive = true, CreatedBy = "admin", CreatedOn = DateTime.Now }
		});

				context.SaveChanges();
			}
		}
		private static void SeedEServices(AppDbContext context)
		{
			if (!context.Set<EService>().Any())
			{
				var services = new List<EService>
		{
			new EService
			{
				NameAr = "خدمة زواج سعودي من غير سعودية بالخارج",
				NameEn = "Marriage Service for a Saudi Male with a Non-Saudi Female Abroad",
				DescriptionAr = "تتيح هذه الخدمة إمكانية التقديم على طلب زواج سعودي من غير سعودية خارج المملكة.",
				DescriptionEn = "This service allows Saudi men to apply for marriage to a non-Saudi woman outside the Kingdom.",
				ServiceCode = "MS-NA",
				EServiceFormId = 1,
				CategoryId = 1,
				IconUrl = "icons/archive.svg",
				CreatedBy = "admin",
				CreatedOn = DateTime.UtcNow,
				IsActive = true
			},
			new EService
			{
				NameAr = "زواج سعودي من غير سعودية مقيمة",
				NameEn = "Marriage of a Saudi Male with a Resident Non-Saudi Female",
				DescriptionAr = "تتيح هذه الخدمة إمكانية التقديم على طلب زواج سعودي من غير سعودية مقيمة داخل المملكة.",
				DescriptionEn = "This service allows Saudi men to apply for marriage to a non-Saudi woman residing in the Kingdom.",
				ServiceCode = "MS-NR",
				EServiceFormId = 1,
				CategoryId = 1,
				IconUrl = "icons/archive.svg",
				CreatedBy = "admin",
				CreatedOn = DateTime.UtcNow,
				IsActive = true
			},
			new EService
			{
				NameAr = "زواج سعودي من غير سعودية مولودة بالسعودية",
				NameEn = "Marriage of a Saudi Male with a Non-Saudi Female Born in KSA",
				DescriptionAr = "تتيح هذه الخدمة إمكانية التقديم على زواج سعودي من غير سعودية مولودة بالمملكة.",
				DescriptionEn = "This service allows Saudi men to apply for marriage to a non-Saudi woman born in Saudi Arabia.",
				ServiceCode = "MS-NB",
				EServiceFormId = 1,
				CategoryId = 1,
				IconUrl = "icons/archive.svg",
				CreatedBy = "admin",
				CreatedOn = DateTime.UtcNow,
				IsActive = true
			},
			new EService
			{
				NameAr = "زواج سعودية من غير سعودي",
				NameEn = "Marriage of a Saudi Female with a Non-Saudi Male",
				DescriptionAr = "تتيح هذه الخدمة إمكانية التقديم على زواج سعودية من غير سعودي.",
				DescriptionEn = "N/A",
				ServiceCode = "MF-NS",
				EServiceFormId = 1,
				CategoryId = 1,
				IconUrl = "icons/archive.svg",
				CreatedBy = "admin",
				CreatedOn = DateTime.UtcNow,
				IsActive = true
			},
			new EService
			{
				NameAr = "طلب محو سابقة - رد اعتبار",
				NameEn = "Request to Clear Criminal Record - Reputation Restoration",
				DescriptionAr = "طلب يتقدم به السجين السابق لمحو سابقة ورد اعتبار.",
				DescriptionEn = "A request submitted by a former prisoner to have his record cleared and reputation restored.",
				ServiceCode = "RR-RI",
				EServiceFormId = 2,
				CategoryId = 2,
				IconUrl = "icons/archive.svg",
				CreatedBy = "admin",
				CreatedOn = DateTime.UtcNow,
				IsActive = true
			},
			new EService
			{
				NameAr = "طلب الخروج المؤقت للسجين",
				NameEn = "Request for Temporary Release of Prisoner",
				DescriptionAr = "طلب من ذوي السجين لخروجه المؤقت.",
				DescriptionEn = "A request from the prisoner's family for temporary release.",
				ServiceCode = "PR-TR",
				EServiceFormId = 2,
				CategoryId = 2,
				IconUrl = "icons/archive.svg",
				CreatedBy = "admin",
				CreatedOn = DateTime.UtcNow,
				IsActive = true
			},
			new EService
			{
				NameAr = "طلب نقل سجين من سجن لآخر",
				NameEn = "Request to Transfer Prisoner Between Prisons",
				DescriptionAr = "طلب من ذوي السجين لنقله لسجن آخر.",
				DescriptionEn = "A request from the prisoner's family to transfer him to another prison.",
				ServiceCode = "PR-TP",
				EServiceFormId = 2,
				CategoryId = 2,
				IconUrl = "icons/archive.svg",
				CreatedBy = "admin",
				CreatedOn = DateTime.UtcNow,
				IsActive = true
			},
			new EService
			{
				NameAr = "طلب عدم الإبعاد من المملكة العربية السعودية (لغير السعودي)",
				NameEn = "Request to Prevent Deportation from Saudi Arabia (for Non-Saudis)",
				DescriptionAr = "طلب من غير السعودي بعدم الإبعاد من المملكة.",
				DescriptionEn = "A request from a non-Saudi to prevent deportation.",
				ServiceCode = "PR-ND",
				EServiceFormId = 2,
				CategoryId = 2,
				IconUrl = "icons/archive.svg",
				CreatedBy = "admin",
				CreatedOn = DateTime.UtcNow,
				IsActive = true
			},
			new EService
			{
				NameAr = "طلب السماح له بالسفر ورفع اسمه من القائمة (للسعودي)",
				NameEn = "Request to Allow Travel and Remove Name from Watchlist (for Saudis)",
				DescriptionAr = "طلب من السعودي لرفع اسمه من القائمة والسماح له بالسفر.",
				DescriptionEn = "Request to remove a Saudi citizen’s name from the travel ban list.",
				ServiceCode = "PR-TV",
				EServiceFormId = 2,
				CategoryId = 2,
				IconUrl = "icons/archive.svg",
				CreatedBy = "admin",
				CreatedOn = DateTime.UtcNow,
				IsActive = true
			},
			new EService
			{
				NameAr = "الاستدعاء الإلكتروني",
				NameEn = "Electronic Summon",
				DescriptionAr = "تتيح هذه الخدمة إمكانية إرسال استدعاء إلكتروني.",
				DescriptionEn = "This service allows electronic summons to be sent.",
				ServiceCode = "EL-SU",
				EServiceFormId = 3,
				CategoryId = 3,
				IconUrl = "icons/archive.svg",
				CreatedBy = "admin",
				CreatedOn = DateTime.UtcNow,
				IsActive = true
			},
			new EService
			{
				NameAr = "الاستعلام عن معاملة",
				NameEn = "Inquiry about a Transaction",
				DescriptionAr = "تمكن المستخدم من الاستعلام عن حالة المعاملة.",
				DescriptionEn = "Allows users to inquire about the status of a transaction.",
				ServiceCode = "IN-TR",
				EServiceFormId = 3,
				CategoryId = 3,
				IconUrl = "icons/archive.svg",
				CreatedBy = "admin",
				CreatedOn = DateTime.UtcNow,
				IsActive = true
			}
		};

				context.Set<EService>().AddRange(services);
				context.SaveChanges();
			}
		}
		private static void SeedEServiceDetails(AppDbContext context)
		{
			if (!context.Set<EServiceDetails>().Any())
			{
				var details = new List<EServiceDetails>
		{
			new EServiceDetails
			{
				ServiceId = 1,
				AudienceTypeAr = "المواطنون",
				AudienceTypeEn = "Citizens",
				ExecutionTimeAr = "فوري",
				ExecutionTimeEn = "Immediate",
				CostAr = "N/A",
				CostEn = "N/A",
				CreatedBy = "admin",
				CreatedOn = DateTime.UtcNow
			},
			new EServiceDetails
			{
				ServiceId = 2,
				AudienceTypeAr = "المواطنون",
				AudienceTypeEn = "Citizens",
				ExecutionTimeAr = "فوري",
				ExecutionTimeEn = "Immediate",
				CostAr = "N/A",
				CostEn = "N/A",
				CreatedBy = "admin",
				CreatedOn = DateTime.UtcNow
			},
			new EServiceDetails
			{
				ServiceId = 3,
				AudienceTypeAr = "المواطنون",
				AudienceTypeEn = "Citizens",
				ExecutionTimeAr = "فوري",
				ExecutionTimeEn = "Immediate",
				CostAr = "N/A",
				CostEn = "N/A",
				CreatedBy = "admin",
				CreatedOn = DateTime.UtcNow
			},
			new EServiceDetails
			{
				ServiceId = 4,
				AudienceTypeAr = "المواطنون",
				AudienceTypeEn = "Citizens",
				ExecutionTimeAr = "فوري",
				ExecutionTimeEn = "N/A",
				CostAr = "N/A",
				CostEn = "N/A",
				CreatedBy = "admin",
				CreatedOn = DateTime.UtcNow
			},
			new EServiceDetails
			{
				ServiceId = 5,
				AudienceTypeAr = "المواطنون والمقيمون",
				AudienceTypeEn = "Citizens and Residents",
				ExecutionTimeAr = "فوري",
				ExecutionTimeEn = "Immediate",
				CostAr = "N/A",
				CostEn = "N/A",
				CreatedBy = "admin",
				CreatedOn = DateTime.UtcNow
			},
			new EServiceDetails
			{
				ServiceId = 6,
				AudienceTypeAr = "ذوي السجين",
				AudienceTypeEn = "Prisoner's family",
				ExecutionTimeAr = "فوري",
				ExecutionTimeEn = "Immediate",
				CostAr = "N/A",
				CostEn = "N/A",
				CreatedBy = "admin",
				CreatedOn = DateTime.UtcNow
			},
			new EServiceDetails
			{
				ServiceId = 7,
				AudienceTypeAr = "ذوي السجين",
				AudienceTypeEn = "Prisoner's family",
				ExecutionTimeAr = "فوري",
				ExecutionTimeEn = "Immediate",
				CostAr = "N/A",
				CostEn = "N/A",
				CreatedBy = "admin",
				CreatedOn = DateTime.UtcNow
			},
			new EServiceDetails
			{
				ServiceId = 8,
				AudienceTypeAr = "غير السعوديين",
				AudienceTypeEn = "Non-Saudis",
				ExecutionTimeAr = "فوري",
				ExecutionTimeEn = "Immediate",
				CostAr = "N/A",
				CostEn = "N/A",
				CreatedBy = "admin",
				CreatedOn = DateTime.UtcNow
			},
			new EServiceDetails
			{
				ServiceId = 9,
				AudienceTypeAr = "السعوديين",
				AudienceTypeEn = "Saudis",
				ExecutionTimeAr = "فوري",
				ExecutionTimeEn = "Immediate",
				CostAr = "N/A",
				CostEn = "N/A",
				CreatedBy = "admin",
				CreatedOn = DateTime.UtcNow
			},
			new EServiceDetails
			{
				ServiceId = 10,
				AudienceTypeAr = "الأفراد",
				AudienceTypeEn = "Individuals",
				ExecutionTimeAr = "فوري",
				ExecutionTimeEn = "Immediate",
				CostAr = "N/A",
				CostEn = "N/A",
				CreatedBy = "admin",
				CreatedOn = DateTime.UtcNow
			},
			new EServiceDetails
			{
				ServiceId = 11,
				AudienceTypeAr = "الجهات والأفراد",
				AudienceTypeEn = "Entities and Individuals",
				ExecutionTimeAr = "فوري",
				ExecutionTimeEn = "Immediate",
				CostAr = "N/A",
				CostEn = "N/A",
				CreatedBy = "admin",
				CreatedOn = DateTime.UtcNow
			}
		};

				context.Set<EServiceDetails>().AddRange(details);
				context.SaveChanges();
			}
		}
		private static void SeedEServiceRequirement(AppDbContext context)
		{
			if (!context.Set<EServiceRequirement>().Any())
			{
				var details = new List<EServiceRequirement>
				{
								new EServiceRequirement
					{
						ServiceId = 1,
						DescriptionAr = "1. لا يقل عمر المتقدم عن٣٠عام ولا يزيد عن ٧٠ عام",
						DescriptionEn = "1. The applicant's age must not be less than 30 and not more than 70 years old",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 1,
						DescriptionAr = "2. من كان مطلقاً أو زوجته متوفاه أو به عاهة أو زوجته بها عاهة يرفق ما يثبت ذلك",
						DescriptionEn = "2. If divorced, widowed, or with disability, must attach proof",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 1,
						DescriptionAr = "3. ألا يكون المتقدم من شاغلي الوظائف المشمولين باللائحة الأمنية أو العسكرية",
						DescriptionEn = "3. The applicant must not be in military or security roles",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 1,
						DescriptionAr = "4. أن يكون من سكان منطقة القصيم ويثبت ذلك بالعنوان الوطني",
						DescriptionEn = "4. Must be resident of Qassim and prove via national address",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 1,
						DescriptionAr = "5. إن كان بعصمة المتقدم زوجة فعليه إرفاق تقرير طبي معتمد يثبت وجود مانع طبي",
						DescriptionEn = "5. If applicant has a wife, must attach official medical report stating valid reason",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 1,
						DescriptionAr = "6.من سبق له الطلاق فلا يقبل طلبه إلا بعد مضي ستة أشهر على تاريخ صك الطلاق.",
						DescriptionEn = "4. The applicant must be a resident of the Qassim region, which must be proven through his national address.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 2,
						DescriptionAr = "1. لا يقل عمر المتقدم عن٣٠عام ولا يزيد عن ٧٠ عام إلا إذا كانت المخطوبة ابنة عم الخاطب أو ابنة خالة من الدرجة الأولى فيجب ألا يقل عمره عن ٢٥ عام.",
						DescriptionEn = "1. The applicant's age must not be less than 30 years and not more than 70 years, unless the woman to be married is the man’s first cousin, in which case his age must not be less than 25 years.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 2,
						DescriptionAr = "2. من كان مطلقاً أو زوجته متوفاه أو به عاهة أو مرض ثابت بتقرير طبي أو به عاهة أو مرض ثابت بتقرير طبي من مستشفى حكومي فيستثنى من الحد الأدنى للعمر.",
						DescriptionEn = "2. If the man is divorced, his wife has passed away, or he has a permanent disability or illness confirmed by a medical report from a government hospital, he is exempt from the minimum age requirement.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 2,
						DescriptionAr = "3. أن تكون المرأة المخطوبة مقيمة بالمملكة وتحمل إقامة سارية المفعول.",
						DescriptionEn = "3. The woman must be residing in the Kingdom and have a valid residence permit.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 2,
						DescriptionAr = "4. ألا يكون المتقدم من شاغلي الوظائف المشمولين بالمنع.",
						DescriptionEn = "4. The applicant must not hold a position that falls under the restrictions on marrying non-Saudi women.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 2,
						DescriptionAr = "5. أن يكون من سكان منطقة القصيم ويثبت ذلك بالعنوان الوطني.",
						DescriptionEn = "5. The applicant must be a resident of the Qassim region, which must be proven through his national address.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 2,
						DescriptionAr = "6. إن كان بعصمة المتقدم زوجة فعليه إرفاق تقرير طبي من مستشفى حكومي يثبت عجزها عن الحمل أو عدم قدرتها على على القيام بالواجبات الزوجية أو الأعباء المنزلية.",
						DescriptionEn = "6. If the applicant currently has a wife, he must provide a medical report from a government hospital proving that his wife is unable to bear children or perform marital or household duties.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 2,
						DescriptionAr = "7. من سبق له الطلاق فلا يقبل طلبه إلا بعد مضي ستة أشهر على تاريخ صك الطلاق.",
						DescriptionEn = "7. If the applicant has previously been divorced, his application will only be accepted six months after the issuance date of the divorce certificate.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					}
					,
					new EServiceRequirement
					{
						ServiceId = 3,
						DescriptionAr = "1. لا يقل عمر المتقدم عن٣٠عام ولا يزيد عن ٧٠ عام إلا إذا كانت المخطوبة ابنة عم الخاطب أو ابنة خالة من الدرجة الأولى فيجب ألا يقل عمره عن ٢٥ عام.",
						DescriptionEn = "1. The applicant's age must not be less than 30 years and not more than 70 years, unless the woman to be married is the man’s first cousin, in which case his age must not be less than 25 years.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 3,
						DescriptionAr = "2. أن تحمل المرأة المخطوبة إقامة سارية المفعول.",
						DescriptionEn = "2. The woman must be residing in the Kingdom and have a valid residence permit.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 3,
						DescriptionAr = "3. تكون المرأة المخطوبة من مواليد المملكة ويثبت ذلك بشهادة ميلاد صادرة من الأحوال المدنية.",
						DescriptionEn = "3. The woman must be born in the Kingdom, which must be proven by a birth certificate issued by the Ahwal.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 3,
						DescriptionAr = "4. ألا يكون المتقدم من شاغلي الوظائف المشمولين بالمنع.",
						DescriptionEn = "4. The applicant must not hold a position that falls under the restrictions on marrying non-Saudi women.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 3,
						DescriptionAr = "5. أن يكون من سكان منطقة القصيم ويثبت ذلك بالعنوان الوطني.",
						DescriptionEn = "5. The applicant must be a resident of the Qassim region, which must be proven through his national address.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 3,
						DescriptionAr = "6. إن كان بعصمة المتقدم زوجة فعليه إرفاق تقرير طبي من مستشفى حكومي يثبت عجزها عن الحمل أو عدم قدرتها على على القيام بالواجبات الزوجية أو الأعباء المنزلية.",
						DescriptionEn = "6. If the applicant currently has a wife, he must provide a medical report from a government hospital proving that his wife is unable to bear children or perform marital or household duties.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 3,
						DescriptionAr = "7. من سبق له الطلاق فلا يقبل طلبه إلا بعد مضي ستة أشهر على تاريخ صك الطلاق.",
						DescriptionEn = "7. If the applicant has previously been divorced, his application will only be accepted six months after the issuance date of the divorce certificate.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 4,
						DescriptionAr = "1. نص الطلب",
						DescriptionEn = "1. Request text",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 4,
						DescriptionAr = "2. إرفاق صورة الهوية",
						DescriptionEn = "2. A copy of the applicant’s national ID",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 4,
						DescriptionAr = "3. أن لا يقل عمرها عن (25) عاماً إلا في حالة كون الخاطب الأجنبي من مواليد المملكة أو يوجد قرابة مع المخطوبة كأن تكون ابنة عمه أو خاله فيكون العمر الأدنى هو (21) عاماً.",
						DescriptionEn = "3. The applicant’s age must not be less than 25 years unless the foreign suitor was born in Saudi Arabia or is a relative (such as a cousin); in such cases, the minimum age is 21 years",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 4,
						DescriptionAr = "4. صورة من صك الطلاق إذا كانت المخطوبة مطلقة.",
						DescriptionEn = "4. A copy of the divorce certificate if the applicant is divorced",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 4,
						DescriptionAr = "5. صورة مصدقة من إقامة طالب الزواج الأجنبي سارية المفعول وإثبات عمله من الجهة التي يعمل على كفالتها.",
						DescriptionEn = "5. A certified copy of the foreign suitor’s valid residency permit and a proof of employment from the sponsoring employer",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 5,
						DescriptionAr = "1. نص الطلب",
						DescriptionEn = "1. A written request document from the individual.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 5,
						DescriptionAr = "2. إرفاق صورة الهوية",
						DescriptionEn = "2. A copy of the ID.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 5,
						DescriptionAr = "3. نسخة من صك الحكم",
						DescriptionEn = "3. A copy of the court ruling document.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 5,
						DescriptionAr = "4. العنوان الوطني",
						DescriptionEn = "4. National address.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 5,
						DescriptionAr = "5. رقم الهاتف",
						DescriptionEn = "5. Phone number.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 5,
						DescriptionAr = "6. صفة مقدم الطلب (أصيل / وكيل / محامي)",
						DescriptionEn = "6. The applicant's relationship to the case.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 6,
						DescriptionAr = "1. نص الطلب يحتوي على (اسم السجين، صلة القرابة للسجين، اسم السجن)",
						DescriptionEn = "1. The request text must include (the inmate's name, the relationship to the inmate, the name of the prison).",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 6,
						DescriptionAr = "2. صفة مقدم الطلب (يجب أن يكون من الدرجة الأولى أو وكيل شرعي): (أصيل / وكيل / محامي/ قريب من الدرجة الأولى)",
						DescriptionEn = "2. The applicant's relationship must be a first-degree relative or a legal representative.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 6,
						DescriptionAr = "3. إرفاق صورة الهوية الوطنية لمقدم الطلب",
						DescriptionEn = "3. A copy of the applicant's national ID must be attached.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 6,
						DescriptionAr = "4. مكان السجين",
						DescriptionEn = "4. The location of the inmate.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 6,
						DescriptionAr = "5. سبب طلب خروج السجين اضطرارياَ:\n إذا كان الخروج لحضور دفن أو عزاء إرفاق ما يلي:\n -شهادة الوفاة (اشتراط أن يكون من الدرجة الأولى وهم الوالدين والأبناء)\n -تصريح الدفن\n إذا كان الخروج لحضور زواج إرفاق ما يلي:\n -كرت الدعوة\n -المكان والزمان\n أو إذا كان الخروج لزيارة الوالدين والأقارب، زيارة المرضى في المستشفيات، خروج للتسجيل في الجامعة، أو غيره، يجب إرفاق المستندات المثبتة لصحة ذلك",
						DescriptionEn = "5. The reason for the emergency release request:\n     *If the release is to attend a funeral or condolence, attach the following:\n        ** Death certificate (must be from a first-degree relative, such as parents or children).\n        ** Burial permit.\n     * If the release is to attend a wedding, attach the following:\n        ** Invitation card.\n        ** Date and location.\n     * If the release is for visiting parents or relatives, visiting patients in hospitals, registering at a university, or other reasons, attach documents verifying the validity of the request.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 7,
						DescriptionAr = "1. نص الطلب",
						DescriptionEn = "1. The text of the request.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 7,
						DescriptionAr = "2. إرفاق صورة الهوية",
						DescriptionEn = "2. A copy of the ID must be attached.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 7,
						DescriptionAr = "3. صك الحكم النهائي",
						DescriptionEn = "3. A copy of the final court ruling.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 7,
						DescriptionAr = "4. العنوان الوطني للسجين أو محل الإقامة.",
						DescriptionEn = "4. The inmate’s national address or place of residence.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 7,
						DescriptionAr = "5. تحديد مكان السجن الحالي (حسب المناطق الإدارية ال13)",
						DescriptionEn = "5. Specify the current prison location (based on the 13 administrative regions).",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 7,
						DescriptionAr = "6. تحديد مكان السجن المطلوب النقل له (حسب المناطق الإدارية ال13).",
						DescriptionEn = "6. Specify the desired prison location for the transfer (based on the 13 administrative regions).",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 7,
						DescriptionAr = "7. صفة مقدم الطلب (أصيل / وكيل / محامي/ قريب من الدرجة الأولى)",
						DescriptionEn = "7. The applicant's status (principal, legal representative, lawyer, or first-degree relative).",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 8,
						DescriptionAr = "1. نص الطلب",
						DescriptionEn = "1. The text of the request.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 8,
						DescriptionAr = "2. إرفاق صورة الهوية",
						DescriptionEn = "2. A copy of the ID must be attached.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 8,
						DescriptionAr = "3. صفة المتقدم :(أصيل / وكيل / محامي/ قريب من الدرجة الأولى)",
						DescriptionEn = "3. The applicant's relationship or status.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 8,
						DescriptionAr = "4. نسخة من صك الحكم",
						DescriptionEn = "4. A copy of the court ruling issued by the court.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 8,
						DescriptionAr = "5. صورة جواز السفر لمقدم الطلب.",
						DescriptionEn = "5. A copy of the applicant's passport.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 8,
						DescriptionAr = "6. عدم ثبوت الإدانة في أحد جرائم المخدارت وإرفاق مايثبت ذلك.",
						DescriptionEn = "6. Proof of no conviction for any drug-related offenses, along with documentation to support this.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 8,
						DescriptionAr = "7. موافقة خطية من الكفيل بعدم إبعاده.",
						DescriptionEn = "7. A written approval from the sponsor indicating no objection to the deportation.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 9,
						DescriptionAr = "1. نص الطلب",
						DescriptionEn = "1. The text of the request.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 9,
						DescriptionAr = "2. إرفاق صورة الهوية",
						DescriptionEn = "2. A copy of the ID must be attached.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 9,
						DescriptionAr = "3. إرفاق نسخة من صك الحكم",
						DescriptionEn = "3. A copy of the court ruling issued by the court.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 9,
						DescriptionAr = "4. العنوان الوطني",
						DescriptionEn = "4. National address.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 9,
						DescriptionAr = "5. إرفاق المستند الرسمي لسبب الطلب:\n -إذا كان من أسباب السفر طلب العلاج إرفاق تقرير طبي من مستشفى حكومي.\n -إذا كان من أسباب السفر للدراسة إرفاق الوثائق التي تفيد بذلك.\n -إذا كان من أسباب السفر السماح له بالزواج من الخارج إرفاق مايثبت ذلك.\n -في حال كانت أسباب أخرى إرفاق الوثائق التي تفيد بذلك.",
						DescriptionEn = "5. Attach the official document supporting the reason for the request:\n     *If the reason for travel is medical treatment, attach a medical report from a government hospital.\n     *If the reason for travel is for education, attach relevant documents supporting this.\n     *If the reason for travel is to allow for marriage abroad, attach documentation to prove this.\n     *If there are other reasons, attach the relevant supporting documents.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 9,
						DescriptionAr = "6. صفة مقدم الطلب (أصيل / وكيل / محامي/ قريب من الدرجة الأولى)",
						DescriptionEn = "6. The applicant's status (principal, legal representative, lawyer, or first-degree relative).",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 10,
						DescriptionAr = "1. نص الطلب",
						DescriptionEn = "1. The text of the request.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 10,
						DescriptionAr = "2. إرفاق صورة الهوية",
						DescriptionEn = "2. A copy of the ID must be attached.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 10,
						DescriptionAr = "3. إرفاق مرفقات تدعم الطلب ( إن وجدت )",
						DescriptionEn = "3. Attach any supporting documents (if available).",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 11,
						DescriptionAr = "1. رقم المعاملة المرسل لكم من قبل الإمارة",
						DescriptionEn = "1. The transaction number sent to you by the Qassim Province Emirate.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 11,
						DescriptionAr = "2. السنة الهجرية للمعاملة",
						DescriptionEn = "2. The Hijri year of the transaction.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 11,
						DescriptionAr = "3. رقم الهوية الوطنية لصاحب المعاملة",
						DescriptionEn = "3. The national ID number of the transaction owner.",
						IsPaper = false,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 1,
						DescriptionAr = "1. صورة من الهوية الوطنية سارية المفعول وسجل الأسرة لمن لديه مضافين سواءً زوجة أو أبناء.",
						DescriptionEn = "1. A copy of the valid national ID and family register for those with dependents, whether a wife or children.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 1,
						DescriptionAr = "2. برنت من الأحوال المدنية (عن طريق موقع أبشر) بالإضافة إلى برنت التابعين أو إذا كان له زوجة أو أبناء.",
						DescriptionEn = "2. A printout from Civil Affairs (via Absher) along with a printout for dependents if he has a wife or children.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 1,
						DescriptionAr = "3. تعريف من جهة العمل يشمل على مقر عمله ومسمى وظيفته ومرتبته وراتبه وأن يكون مطابق للمهنة المدونة في برنت الأحوال.",
						DescriptionEn = "3. An employment verification letter detailing the workplace, job title, rank, and salary, which must match the profession listed in the Civil Affairs printout.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 1,
						DescriptionAr = "4. للموظف الأهلي يجب أن يحضر مع التعريف برنت من التأمينات الاجتماعية.",
						DescriptionEn = "4. Private sector employees must provide the verification letter along with a printout from social insurance.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 1,
						DescriptionAr = "5. من كان طالباً فيحضر تعريف من الجهة التي يدرس بها.",
						DescriptionEn = "5. Students must submit a verification letter from their educational institution.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 1,
						DescriptionAr = "6. من كان يعمل متسبباً فيحضر مشهد مصدق من العمدة والشرطة ويكون يتوافق مع المهنة المدونة في برنت الأحوال.",
						DescriptionEn = "6. Self-employed individuals must present a certified statement from the local authority and police that matches the profession listed in the Civil Affairs printout.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 1,
						DescriptionAr = "7. لمن لديه زوجة على ذمته فيرفق تقرير طبي من مستشفى حكومي يثبت عدم قدرتها على الحمل أو القيام بالواجبات الزوجية أو الأعباء المنزلية.",
						DescriptionEn = "7. If the applicant has a wife, he must submit a medical report from a government hospital proving her inability to bear children or perform marital or household duties.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 1,
						DescriptionAr = "8. صورة من صك الطلاق لمن سبق له الطلاق.",
						DescriptionEn = "8. A copy of the divorce certificate for those previously divorced.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 1,
						DescriptionAr = "9. إن كانت المرأة المخطوبة سبق لها الطلاق فيرفق نسخة من صك الطلاق.",
						DescriptionEn = "9. If the woman has been previously divorced, a copy of her divorce certificate must be attached.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 1,
						DescriptionAr = "10. فحص ما قبل الزواج للطرف السعودي.",
						DescriptionEn = "10. Pre-marital health screening for the man.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 2,
						DescriptionAr = "1. صورة من الهوية الوطنية سارية المفعول وسجل الأسرة لمن لديه مضافين سواءً زوجة أو أبناء.",
						DescriptionEn = "1. A copy of the valid national ID and family register for those with dependents, whether a wife or children.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 2,
						DescriptionAr = "2. برنت من الأحوال المدنية (عن طريق موقع أبشر) بالإضافة إلى برنت التابعين أو إذا كان له زوجة أو أبناء.",
						DescriptionEn = "2. A printout from Ahwal (via Absher) along with a printout for dependents if he has a wife or children.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 2,
						DescriptionAr = "3. تعريف من جهة العمل يشمل على مقر عمله ومسمى وظيفته ومرتبته وراتبه وأن يكون مطابق للمهنة المدونة في برنت الأحوال.",
						DescriptionEn = "3. An employment verification letter detailing the workplace, job title, rank, and salary, which must match the profession listed in the Civil Affairs printout.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 2,
						DescriptionAr = "4. للموظف الأهلي يجب أن يحضر مع التعريف برنت من التأمينات الاجتماعية.",
						DescriptionEn = "4. Private sector employees must provide the verification letter along with a printout from social insurance.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 2,
						DescriptionAr = "5. من كان طالباً فيحضر تعريف من الجهة التي يدرس بها.",
						DescriptionEn = "5. Students must submit a verification letter from their educational institution.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 2,
						DescriptionAr = "6. من كان يعمل متسبباً فيحضر مشهد مصدق من العمده والشرطه ويكون يتوافق مع المهنة المدونة في برنت الأحوال.",
						DescriptionEn = "6. Self-employed individuals must present a certified statement from the local authority and police that matches the profession listed in the Civil Affairs printout.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 2,
						DescriptionAr = "7. لمن لديه زوجة على ذمته فيرفق تقرير طبي من مستشفى حكومي يثبت عدم قدرتها على الحمل أو القيام بالواجبات الزوجية أو الأعباء المنزلية.",
						DescriptionEn = "7. If the applicant has a wife, he must submit a medical report from a government hospital proving her inability to bear children or perform marital or household duties.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 2,
						DescriptionAr = "8. صورة من صك الطلاق لمن سبق له الطلاق.",
						DescriptionEn = "8. A copy of the divorce certificate for those previously divorced.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 2,
						DescriptionAr = "9. برنت من الجوازات للمرأة المخطوبة.",
						DescriptionEn = "9. A document from the passport office regarding the woman's status.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 2,
						DescriptionAr = "10. صورة من إقامة المرأة المخطوبة وجواز سفرها سارية المفعول.",
						DescriptionEn = "10. A copy of the woman's valid residence permit and passport.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 2,
						DescriptionAr = "11. إن كانت المرأة المخطوبة سبق لها الطلاق فيرفق نسخة من صك الطلاق.",
						DescriptionEn = "11. If the woman has been previously divorced, a copy of her divorce certificate must be attached.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 2,
						DescriptionAr = "12. فحص ما قبل الزواج للطرفين.",
						DescriptionEn = "12. Pre-marital health examination for both parties.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 2,
						DescriptionAr = "13. صورة من هوية ولي أمر المرأة المخطوبة ، وإن كان وليها غير والدها فيرفق صورة من صك الولاية أو الوكالة.",
						DescriptionEn = "13. A copy of the guardian's ID for the woman, and if the guardian is not her father, a copy of the guardianship or power of attorney document must be attached.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 3,
						DescriptionAr = "1. صورة من الهوية الوطنية سارية المفعول وسجل الأسرة لمن لديه مضافين سواءً زوجة أو أبناء.",
						DescriptionEn = "1. A copy of the valid national ID and family register for those with dependents, whether a wife or children.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 3,
						DescriptionAr = "2. برنت من الأحوال المدنية (عن طريق موقع أبشر) بالإضافة إلى برنت التابعين أو إذا كان له زوجة أو أبناء.",
						DescriptionEn = "2. A printout from Ahwal (via Absher) along with a printout for dependents if he has a wife or children.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 3,
						DescriptionAr = "3. تعريف من جهة العمل يشمل على مقر عمله ومسمى وظيفته ومرتبته وراتبه وأن يكون مطابق للمهنة المدونة في برنت الأحوال.",
						DescriptionEn = "3. An employment verification letter detailing the workplace, job title, rank, and salary, which must match the profession listed in the Ahwal printout.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 3,
						DescriptionAr = "4. للموظف الأهلي يجب أن يحضر مع التعريف برنت من التأمينات الاجتماعية.",
						DescriptionEn = "4. Private sector employees must provide the verification letter along with a printout from social insurance.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 3,
						DescriptionAr = "5. من كان طالباً فيحضر تعريف من الجهة التي يدرس بها.",
						DescriptionEn = "5. Students must submit a verification letter from their educational institution.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 3,
						DescriptionAr = "6. من كان يعمل متسبباً فيحضر مشهد مصدق من العمدة والشرطة ويكون يتوافق مع المهنة المدونة في برنت الأحوال.",
						DescriptionEn = "6. Self-employed individuals must present a certified statement from the local authority and police that matches the profession listed in the Civil Affairs printout.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 3,
						DescriptionAr = "7. لمن لديه زوجة على ذمته فيرفق تقرير طبي من مستشفى حكومي يثبت عدم قدرتها على الحمل أو القيام بالواجبات الزوجية أو الأعباء المنزلية.",
						DescriptionEn = "7. If the applicant has a wife, he must submit a medical report from a government hospital proving her inability to bear children or perform marital or household duties.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 3,
						DescriptionAr = "8. صورة من صك الطلاق لمن سبق له الطلاق.",
						DescriptionEn = "8. A copy of the divorce certificate for those previously divorced.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 3,
						DescriptionAr = "9. صورة من شهادة ميلاد المرأة المخطوبة صادر من أحوال المدنية.",
						DescriptionEn = "9. A copy of the woman's birth certificate issued by the Ahwal.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 3,
						DescriptionAr = "10. برنت من الجوازات للمرأة المخطوبة.",
						DescriptionEn = "10. A document from the passport office regarding the woman's status.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 3,
						DescriptionAr = "11. صورة من إقامة المرأة المخطوبة وجواز سفرها سارية المفعول.",
						DescriptionEn = "11. A copy of the woman's valid residence permit and passport.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					},
					new EServiceRequirement
					{
						ServiceId = 3,
						DescriptionAr = "12. إن كانت المرأة المخطوبة سبق لها الطلاق فيرفق نسخة من صك الطلاق.",
						DescriptionEn = "12. If the woman has been previously divorced, a copy of her divorce certificate must be attached.",
						IsPaper = true,
						CreatedBy = "admin",
						CreatedOn = DateTime.UtcNow
					}











		};

				context.Set<EServiceRequirement>().AddRange(details);
				context.SaveChanges();
			}
		}
		private static void SeedEServiceFlow(AppDbContext context)
		{
			if (!context.Set<EServiceFlow>().Any())
			{
				var serviceFlow = new List<EServiceFlow>();
				var serviceNames = new List<(string ArabicName, string EnglishName)>
				{
					("خدمة زواج سعودي من غير سعودية بالخارج", "Marriage Service for a Saudi Male with a Non-Saudi Female Abroad"),
					("زواج سعودي من غير سعودية مقيمة", "Marriage of a Saudi Male with a Resident Non-Saudi Female"),
					("زواج سعودي من غير سعودية مولودة بالسعودية", "Marriage of a Saudi Male with a Non-Saudi Female Born in Saudi Arabia"),
					("زواج سعودية من غير سعودي", "Marriage of a Saudi Female with a Non-Saudi Male"),
					("طلب محو سابقة - رد اعتبار", "Request to Clear Criminal Record - Reputation Rehabilitation"),
					("طلب الخروج المؤقت للسجين", "Request for Temporary Release of Prisoner"),
					("طلب نقل سجين من سجن لآخر", "Request to Transfer Prisoner Between Prisons"),
					("طلب عدم الإبعاد من المملكة العربية السعودية (لغير السعودي)", "Request to Prevent Deportation from Saudi Arabia (for Non-Saudis)"),
					("طلب السماح له بالسفر ورفع اسمه من القائمة (للسعودي)", "Request to Allow Travel and Remove Name from Watchlist (for Saudis)"),
					("الاستدعاء الإلكتروني", "Electronic Summon"),
					("الاستعلام عن معاملة", "Inquiry about a Transaction")
				};
				for (int serviceId = 1; serviceId <= 11; serviceId++)
				{
					int steps = 7;

					for (int step = 1; step <= steps; step++)
					{
						serviceFlow.Add(new EServiceFlow
						{
							ServiceId = serviceId,
							NameAr = $"الخطوة {step} من الخدمة {serviceId}",
							NameEn = $"Step {step} of Service {serviceId}",
							DescriptionAr = step switch
							{
								1 => "قم بتسجيل الدخول إلى خدمات إمارة منطقة القصيم عبر البوابة الوطنية (نفاذ).",
								2 => "اختر خدمة \""+ serviceNames[serviceId-1].ArabicName + "\" من قائمة الخدمات الإلكترونية.",
								3 => "اضغط على \"بدء الخدمة\" وقم بتعبئة البيانات المطلوبة وأرفق المستندات اللازمة.",
								4 => "بعد التأكد من صحة المعلومات والمرفقات، اضغط على \"إرسال الطلب\".",
								5 => "سيتم إشعارك برقم الطلب بعد إرساله.",
								6 => "يمكنك متابعة حالة طلبك من خلال صفحة \"طلباتي\".",
								7 => "عند اعتماد الطلب، سيتم تحويله إلى معاملة وستتلقى إشعاراً برقم المعاملة.",
								_ => "خطوة غير معروفة"
							},
							DescriptionEn = step switch
							{
								1 => "Log in to the services of the Qassim Region Emirate via the National Portal (Nafath).",
								2 => "Select the '"+ serviceNames[serviceId-1].EnglishName + "' service from the electronic services list.",
								3 => "Click on 'Start Service' and fill in the required information and attach the necessary documents.",
								4 => "After verifying the information and attachments, click on 'Submit Request'.",
								5 => "You will be notified with the request number after submission.",
								6 => "You can track your request status through the 'My Requests' page.",
								7 => "Upon approval, the request will be converted into a transaction and you will receive a transaction number notification.",
								_ => "Unknown step"
							},
							CreatedBy = "admin",
							CreatedOn = DateTime.UtcNow
						});
					}
				}

				context.Set<EServiceFlow>().AddRange(serviceFlow);
				context.SaveChanges();
			}
		
		}

		private static void SeedFAQs(AppDbContext context) {
			if (!context.Set<ServiceFAQ>().Any())
			{
				var faqs = new List<ServiceFAQ>();

				// Arabic and English questions and answers
				string questionAr = "هل تقديم العنوان الوطني إجباري؟";
				string questionEn = "Is providing the national address mandatory?";
				string answerAr = "نعم، تقديم العنوان الوطني إلزامي كجزء من متطلبات الطلب.";
				string answerEn = "Yes, providing the national address is mandatory as part of the application requirements.";

				// Generate FAQs for each service (1 to 11)
				for (int serviceId = 1; serviceId <= 11; serviceId++)
				{
					for (int orderIndex = 1; orderIndex <= 4; orderIndex++)
					{
						faqs.Add(new ServiceFAQ
						{
							ServiceId = serviceId,
							NameAr = questionAr,
							NameEn = questionEn,
							AnswerAr = answerAr,
							AnswerEn = answerEn,
							OrderIndex = orderIndex,
							CreatedBy = "admin",
							CreatedOn = DateTime.UtcNow
						});
					}
				}

				context.Set<ServiceFAQ>().AddRange(faqs);
				context.SaveChanges();
			}

		}

        private static void SeedServiceSteps(AppDbContext context)
        {
            // Service Steps
            if (!context.Set<ServiceStep>().Any())
            {
                var steps = new List<ServiceStep>();

                // Services 1 to 11, each with 4 steps
                for (int serviceId = 1; serviceId <= 11; serviceId++)
                {
					if (serviceId <= 3) {
						steps.AddRange(new List<ServiceStep>
					{
							new ServiceStep()
							{
								ServiceId = serviceId,
								StepNumber = 1,
								StepNameAr = "بيانات الطلب",
								StepNameEn = "Request Data",
								DescriptionAr = "إدخال   بيانات الطلب",
								DescriptionEn = "Enter the applicant information",
								NameAr = "الخطوة الأولى",
                                NameEn = "Request Data",
                                IsRequired = true,
								Order = 1,
								IsActive = true,
								CreatedBy = "admin",
								CreatedOn = DateTime.Now,
							},
							new ServiceStep()
							{
								ServiceId = serviceId,
								StepNumber = 2,
								StepNameAr = "إرفاق المستندات",
								StepNameEn = "Attach Documents",
								DescriptionAr = "إرفاق المستندات المطلوبة",
								DescriptionEn = "Attachment the required files",
                                NameAr = "الخطوة الثانية",
                                NameEn = "Attachments",
                                IsRequired = true,
								Order = 2,
								IsActive = true,
								CreatedBy = "admin",
								CreatedOn = DateTime.Now,
							},
							new ServiceStep()
							{
								ServiceId = serviceId,
								StepNumber = 3,
								StepNameAr = "مراجعة الطلب",
								StepNameEn = "Review Request",
								DescriptionAr = "مراجعة وتأكيد البيانات",
								DescriptionEn = "Review and confirm the information",
                                NameAr = "الخطوة الثالثة",
                                NameEn = "Review",
                                IsRequired = true,
								Order = 3,
								IsActive = true,
								CreatedBy = "admin",
								CreatedOn = DateTime.Now,
							}
					});
					}
					else if (serviceId <= 9) {
                        steps.AddRange(new List<ServiceStep>
                    {
                            new ServiceStep()
                            {
                                ServiceId = serviceId,
                                StepNumber = 1,
                                StepNameAr = "بيانات مقدم الطلب",
                                StepNameEn = "Applicant Information",
                                DescriptionAr = "إدخال بيانات مقدم الطلب",
                                DescriptionEn = "Enter the applicant information",
                                NameAr = "الخطوة الأولى",
                                NameEn = "Request Data",
                                IsRequired = true,
                                Order = 1,
                                IsActive = true,
                                CreatedBy = "admin",
                                CreatedOn = DateTime.Now,
                            },
                            new ServiceStep()
                            {
                                ServiceId = serviceId,
                                StepNumber = 2,
                                StepNameAr = "بيانات المعني بالطلب",
                                StepNameEn = "Request Subject Information",
                                DescriptionAr = "إدخال بيانات المعني بالطلب",
                                DescriptionEn = "Enter the request subject information",
                                NameAr = "الخطوة الثانية",
                                NameEn = "Request Subject Info",
                                IsRequired = true,
                                Order = 2,
                                IsActive = true,
                                CreatedBy = "admin",
                                CreatedOn = DateTime.Now,
                            },
                            new ServiceStep()
                            {
                                ServiceId = serviceId,
                                StepNumber = 3,
                                StepNameAr = "إرفاق المستندات",
                                StepNameEn = "Attach Documents",
                                DescriptionAr = "إرفاق المستندات المطلوبة",
                                DescriptionEn = "Attach the required documents",
                                NameAr = "الخطوة الثالثة",
                                NameEn = "Attachments",
                                IsRequired = true,
                                Order = 3,
                                IsActive = true,
                                CreatedBy = "admin",
                                CreatedOn = DateTime.Now,
                            },
                            new ServiceStep()
                            {
                                ServiceId = serviceId,
                                StepNumber = 4,
                                StepNameAr = "مراجعة الطلب",
                                StepNameEn = "Review Request",
                                DescriptionAr = "مراجعة وتأكيد البيانات",
                                DescriptionEn = "Review and confirm the information",
                                NameAr = "الخطوة الرابعة",
                                NameEn = "Review",
                                IsRequired = true,
                                Order = 4,
                                IsActive = true,
                                CreatedBy = "admin",
                                CreatedOn = DateTime.Now,
                            }
                    });
                    }
                    else 
                    {
                        steps.AddRange(new List<ServiceStep>
                    {
                          
                            new ServiceStep()
                            {
                                ServiceId = serviceId,
                                StepNumber = 1 ,
                                StepNameAr = "بيانات بالطلب",
                                StepNameEn = "Request Data",
                                DescriptionAr = "إدخال بيانات بالطلب",
                                DescriptionEn = "Request Data",
                                NameAr = "الخطوة الثانية",
                                NameEn = "Request Data",
                                
                                IsRequired = true,
                                Order = 1,
                                IsActive = true,
                                CreatedBy = "admin",
                                CreatedOn = DateTime.Now,
                            },
                            new ServiceStep()
                            {
                                ServiceId = serviceId,
                                StepNumber = 2,
                                StepNameAr = "إرفاق المستندات",
                                StepNameEn = "Attach Documents",
                                DescriptionAr = "إرفاق المستندات المطلوبة",
                                DescriptionEn = "Attach the required documents",
                                NameAr = "الخطوة الثالثة",
                                NameEn = "Attachments",
                                IsRequired = true,
                                Order = 2,
                                IsActive = true,
                                CreatedBy = "admin",
                                CreatedOn = DateTime.Now,
                            },
                            new ServiceStep()
                            {
                                ServiceId = serviceId,
                                StepNumber = 3,
                                StepNameAr = "مراجعة الطلب",
                                StepNameEn = "Review Request",
                                DescriptionAr = "مراجعة وتأكيد البيانات",
                                DescriptionEn = "Review and confirm the information",
                                NameAr = "الخطوة الرابعة",
                                NameEn = "Review",
                                IsRequired = true,
                                Order = 3,
                                IsActive = true,
                                CreatedBy = "admin",
                                CreatedOn = DateTime.Now,
                            }
                    });
                    }

                }

                context.Set<ServiceStep>().AddRange(steps);
                context.SaveChanges();
            }
        }





    }
}
