using Swashbuckle.Application;
using System.Web.Http;

namespace InsuranceCompany
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableSwagger(sw =>
                    {
                        sw.SingleApiVersion("v1", "InsuranceCompany")
                            .Contact(contact => contact
                                .Name("Facundo Sa")
                                .Email("sajuanfacundo@gmail.com")
                            );
                    })
                .EnableSwaggerUi(c => { });
        }
    }
}
