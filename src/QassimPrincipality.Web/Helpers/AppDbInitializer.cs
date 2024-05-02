using QassimPrincipality.Domain.Entities.Lookups.Main;
using QassimPrincipality.Infrastructure.Data;
using ZXing.QrCode.Internal;

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

                //EServiceCategory
                if (!context.Set<EServiceCategory>().Any())
                {
                    context.Set<EServiceCategory>().AddRange(new List<EServiceCategory>()
                    {
                        new EServiceCategory()
                        {
                            NameAr = "خدمة الزواج",
                            HasSubCategory = true,
                            Icon = builder.Environment.WebRootPath,
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
                            Icon = builder.Environment.WebRootPath,
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
                            Icon = builder.Environment.WebRootPath,
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
                            Icon = builder.Environment.WebRootPath,
                            Url = "",
                            Audience="المواطنون",
                            CreatedBy = "E-k.marey",
                            CreatedOn = DateTime.Now,
                            ServiceFees=20,
                            DescriptionAr="طلب يتقدم المواطن للسماح له بالسفر خلال مدة المنع من السفر إلكترونياً لعدد من الحالات بدون زيارة إمارة منطقة تبوك ومتابعة الطلب عن طريق الموقع الرسمي للإمارة\r\n\r\nملاحظة : لاستخدام الخدمات يتوجب عليك الدخول من خلال النفاذ الوطني الموحد",
                            ServiceRequierment="<li>تعبئة البيانات المطلوبة</li>\r\n                    <li>صورة الهوية الوطنية لمقدم الطلب</li>\r\n                    <li>عنوان المتقدم</li>"
                        },

                    }); ;
                    context.SaveChanges();
                }
                //if (!context.Set<EServiceSubCategory>().Any())
                //{
                //    context.Set<EServiceSubCategory>().AddRange(new List<EServiceSubCategory>()
                //    {
                //        new EServiceSubCategory()
                //        {
                //            NameAr = "زواج سعودي من غير سعودية",
                //            Icon = builder.Environment.WebRootPath,
                //            Url = "",
                //            Audience="المواطنون",
                //            CategoryId=1,
                //            CreatedBy = "E-k.marey",
                //            CreatedOn = DateTime.Now,
                //            ServiceFees=0,
                //            DescriptionAr="تتيح هذه الخدمة إمكانية التقديم على طلب زواج سعودي من غير سعودية من الخارج إلكترونياً لعدد من الحالات بدون زيارة إمارة منطقة تبوك ومتابعة الطلب عن طريق الموقع الرسمي للإمارة",
                //            ServiceRequierment="<li>لا يقل عمر المتقدم للزواج عن 35 سنة، ولا يزيد عن 70 سنة.</li>\r\n                    <li>تقرير طبي لطالب الزواج.</li>\r\n                    <li>تقرير طبي عن الحالة الصحية للزوجة إذا كان متزوج.</li>\r\n                    <li>تعريف عمل وإذا كان موظفا أهليا يجب تصديق التعريف من الغرفة التجارية أو مشهد مصدق من العمدة والشرطة بعدم وجود عمل.</li>\r\n                    <li>صورة من بطاقة الأحوال أو كرت العائلة إذا كان متزوج.</li>\r\n                    <li>صورة من شهادة الوفاة أو صك الطلاق لمن سبق له الزواج.</li>\r\n                    <li>صورة شخصية للزوج.</li>"

                //        },
                //        new EServiceSubCategory()
                //        {
                //            NameAr = "زواج سعودية من غير سعودي مولود بالمملكة",
                //            Icon = builder.Environment.WebRootPath,
                //            Url = "",
                //            Audience="المقيمين",
                //            Id=2,
                //            CreatedBy = "E-k.marey",
                //            CreatedOn = DateTime.Now,
                //            ServiceFees=20,
                //            DescriptionAr="تتيح هذه الخدمة إمكانية التقديم على طلب زواج سعودية من غير سعودي مولود بالمملكة إلكترونياً لعدد من الحالات بدون زيارة إمارة منطقة تبوك ومتابعة الطلب عن طريق الموقع الرسمي للإمارة\r\n\r\nملاحظة : لاستخدام الخدمات يتوجب عليك الدخول من خلال النفاذ الوطني الموحد",
                //            ServiceRequierment=" <li>أن تكون الزوجة من سكان المنطقة أو المحافظات التابعة لها.</li>\r\n                    <li>لا يقل عمر الزوجة عن 21 سنة.</li>\r\n                    <li>تعريف عمل الزوج مصدق من الغرفة التجارية.</li>\r\n                    <li>تعريف عمل الزوجة مصدق من الغرفة التجارية، أو مشهد مصدق من العمدة والشرطة بعدم وجود عمل.</li>\r\n                    <li>صورة حفيظة الزوجة أو حفيظة والدها إذا كانت مضافة معه.</li>\r\n                    <li>صورة صك الطلاق أو شهادة الوفاة لمن سبق لها الزواج.</li>\r\n                    <li>صورة الإقامة والجواز للزوج سارية المفعول.</li>\r\n                    <li>صورة  شهادة ميلاد الزوج.</li>\r\n                    <li>صورة شخصية للزوج.</li>\r\n                    <li>إقرار من الزوجة بالموافقة مبصوم.</li>"
                //        },

                //    }); ;
                //    context.SaveChanges();
                //}
            }

        }
    }
}
