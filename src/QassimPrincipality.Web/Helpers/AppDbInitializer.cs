using Framework.Core.SharedServices.Entities;
using QassimPrincipality.Domain.Entities.Lookups.Main;
using QassimPrincipality.Infrastructure.Data;
using System.Drawing;
using ZXing.QrCode.Internal;
using static PhoneNumbers.PhoneNumber;

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

                //ContactTypes
                if (!context.Set<EServiceCategory>().Any())
                {
                    context.Set<EServiceCategory>().AddRange(new List<EServiceCategory>()
                    {
                        new EServiceCategory()
                        {
                            NameAr = "خدمة الزواج",
                            HasSubCategory = true,
                            Icon = builder.Environment.WebRootPath+"/images/service.png",
                            Url = "",
                            Audience="",
                            CreatedBy = "E-k.marey",
                            CreatedOn = DateTime.Now,
                            ServiceFees=0,
                            DescriptionAr="خدمة الزواج",
                            ServiceRequierment="<li> First Requirement</li><li> First Requirement</li><li> First Requirement</li><li> First Requirement</li><li> First Requirement</li><li> First Requirement</li>",
                            EServiceSubCategories =new List<EServiceSubCategory>(){
                             new EServiceSubCategory()
                        {
                            NameAr = "زواج سعودي من غير سعودية",
                            Icon = builder.Environment.WebRootPath+"/images/service.png",
                            Url = "",
                            Audience="المواطنون",
                            CreatedBy = "E-k.marey",
                            CreatedOn = DateTime.Now,
                            ServiceFees=0,
                            DescriptionAr="تتيح هذه الخدمة إمكانية التقديم على طلب زواج سعودي من غير سعودية من الخارج إلكترونياً لعدد من الحالات بدون زيارة إمارة منطقة تبوك ومتابعة الطلب عن طريق الموقع الرسمي للإمارة",
                            ServiceRequierment="<li>لا يقل عمر المتقدم للزواج عن 35 سنة، ولا يزيد عن 70 سنة.</li>\r\n                    <li>تقرير طبي لطالب الزواج.</li>\r\n                    <li>تقرير طبي عن الحالة الصحية للزوجة إذا كان متزوج.</li>\r\n                    <li>تعريف عمل وإذا كان موظفا أهليا يجب تصديق التعريف من الغرفة التجارية أو مشهد مصدق من العمدة والشرطة بعدم وجود عمل.</li>\r\n                    <li>صورة من بطاقة الأحوال أو كرت العائلة إذا كان متزوج.</li>\r\n                    <li>صورة من شهادة الوفاة أو صك الطلاق لمن سبق له الزواج.</li>\r\n                    <li>صورة شخصية للزوج.</li>"

                        },
                        new EServiceSubCategory()
                        {
                            NameAr = "زواج سعودية من غير سعودي مولود بالمملكة",
                            Icon = builder.Environment.WebRootPath+"/images/service.png",
                            Url = "",
                            Audience="المقيمين",
                            CreatedBy = "E-k.marey",
                            CreatedOn = DateTime.Now,
                            ServiceFees=20,
                            DescriptionAr="تتيح هذه الخدمة إمكانية التقديم على طلب زواج سعودية من غير سعودي مولود بالمملكة إلكترونياً لعدد من الحالات بدون زيارة إمارة منطقة تبوك ومتابعة الطلب عن طريق الموقع الرسمي للإمارة\r\n\r\nملاحظة : لاستخدام الخدمات يتوجب عليك الدخول من خلال النفاذ الوطني الموحد",
                            ServiceRequierment=" <li>أن تكون الزوجة من سكان المنطقة أو المحافظات التابعة لها.</li>\r\n                    <li>لا يقل عمر الزوجة عن 21 سنة.</li>\r\n                    <li>تعريف عمل الزوج مصدق من الغرفة التجارية.</li>\r\n                    <li>تعريف عمل الزوجة مصدق من الغرفة التجارية، أو مشهد مصدق من العمدة والشرطة بعدم وجود عمل.</li>\r\n                    <li>صورة حفيظة الزوجة أو حفيظة والدها إذا كانت مضافة معه.</li>\r\n                    <li>صورة صك الطلاق أو شهادة الوفاة لمن سبق لها الزواج.</li>\r\n                    <li>صورة الإقامة والجواز للزوج سارية المفعول.</li>\r\n                    <li>صورة  شهادة ميلاد الزوج.</li>\r\n                    <li>صورة شخصية للزوج.</li>\r\n                    <li>إقرار من الزوجة بالموافقة مبصوم.</li>"
                        }
                            }
                        },
                        new EServiceCategory()
                        {
                            NameAr = " الاستعلام عن معاملة",
                            HasSubCategory = false,
                            Icon = builder.Environment.WebRootPath+"/images/service.png",
                            Url = "",
                            Audience="المواطنون",
                            CreatedBy = "E-k.marey",
                            CreatedOn = DateTime.Now,
                            ServiceFees=20,
                            DescriptionAr="طلب يتقدم المواطن للسماح له بالسفر خلال مدة المنع من السفر إلكترونياً لعدد من الحالات بدون زيارة إمارة منطقة تبوك ومتابعة الطلب عن طريق الموقع الرسمي للإمارة\r\n\r\nملاحظة : لاستخدام الخدمات يتوجب عليك الدخول من خلال النفاذ الوطني الموحد",
                            ServiceRequierment="<li>تعبئة البيانات المطلوبة</li>\r\n                    <li>صورة الهوية الوطنية لمقدم الطلب</li>\r\n                    <li>عنوان المتقدم</li>"
                        },
                        new EServiceCategory()
                        {
                            NameAr = "خدمات تنفيذ الأحكام",
                            HasSubCategory = false,
                            Icon = builder.Environment.WebRootPath+"/images/service.png",
                            Url = "",
                            Audience="المواطنون",
                            CreatedBy = "E-k.marey",
                            CreatedOn = DateTime.Now,
                            ServiceFees=20,
                            DescriptionAr="تتيح هذه الخدمة للمستفيد إمكانية التقدم بطلب تنفيذ حكم صادر ضد جهة حكومية إلكترونياً لعدد من الحالات بدون زيارة إمارة منطقة تبوك ومتابعة الطلب عن طريق الموقع الرسمي للإمارة.\r\n\r\nملاحظة : لاستخدام الخدمات يتوجب عليك الدخول من خلال النفاذ الوطني الموحد",
                            ServiceRequierment="  <li>إرفاق صورة من الهوية الوطنية</li>\r\n                    <li>إرفاق صورة لحكم ابتدائي</li>\r\n                    <li>إرفاق صورة لحكم استئناف (إن وجد)</li>\r\n                    <li>إرفاق صورة الوكالة(في حال وجود وكيل)</li>"
                        }
                    }); ;
                    context.SaveChanges();
                }


                // Contact Types
                if (!context.Set<ContactType>().Any())
                {
                    context.Set<ContactType>().AddRange(new List<ContactType>()
                    {
                        
                        new ContactType()
                        {
                            NameAr = "شكوى",
                            NameEn="Complain",
                            IsActive=true,
                            CreatedBy="admin",
                            CreatedOn= DateTime.Now,    

                        },
                        new ContactType()
                        {
                            NameAr = "اقتراح",
                            NameEn="Suggession",
                            IsActive=true,
                            CreatedBy="admin",
                            CreatedOn= DateTime.Now,
                        }
                    }); 
                    context.SaveChanges();
                }
                // Entity Types
                if (!context.Set<EntityType>().Any())
                {
                    context.Set<EntityType>().AddRange(new List<EntityType>()
                    {

                        new EntityType()
                        {
                            NameAr = "أفراد",
                            NameEn="Individuals",
                            IsActive=true,
                            CreatedBy="admin",
                            CreatedOn= DateTime.Now,

                        },
                        new EntityType()
                        {
                            NameAr = "حكومي",
                            NameEn="Government",
                            IsActive=true,
                            CreatedBy="admin",
                            CreatedOn= DateTime.Now,
                        }
                    });
                    context.SaveChanges();
                }
                // Requester Types
                if (!context.Set<RequesterType>().Any())
                {
                    context.Set<RequesterType>().AddRange(new List<RequesterType>()
                    {

                        new RequesterType()
                        {
                            NameAr = "قطاع خاص",
                            NameEn="Companies",
                            IsActive=true,
                            CreatedBy="admin",
                            CreatedOn= DateTime.Now,

                        },
                        new RequesterType()
                        {
                            NameAr = "قطاع صحي",
                            NameEn="Health",
                            IsActive=true,
                            CreatedBy="admin",
                            CreatedOn= DateTime.Now,
                        }
                    });
                    context.SaveChanges();
                }


            }

        }
    }
}
