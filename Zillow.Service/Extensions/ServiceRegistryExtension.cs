using Microsoft.Extensions.DependencyInjection;
using Zillow.Service.Services.AddressServices;
using Zillow.Service.Services.AuthServices;
using Zillow.Service.Services.CategoryServices;
using Zillow.Service.Services.ContractServices;
using Zillow.Service.Services.CustomerServices;
using Zillow.Service.Services.EmailServices;
using Zillow.Service.Services.FileServices;
using Zillow.Service.Services.ImageServices;
using Zillow.Service.Services.NotificationServices;
using Zillow.Service.Services.RealEstateServices;
using Zillow.Service.Services.UserServices;

namespace Zillow.Service.Extensions
{
    public static class ServiceRegistryExtension
    {
        public static IServiceCollection RegisterServices( this IServiceCollection service)
        {

            service.AddScoped<IRealEstatesService, RealEstatesService>();
            
            service.AddScoped<ICategoryService, CategoryService>();
            
            service.AddScoped<IContractService, ContractService>();
            
            service.AddScoped<ICustomerService, CustomerService>();
            
            service.AddScoped<IUserService, UserService>();
            
            service.AddScoped<IAddressService, AddressService>();

            service.AddScoped<IAuthService, AuthService>();
            
            service.AddScoped<IImageService, ImageService>();
                
            service.AddSingleton<IFileService,FileService>();
            
            service.AddSingleton<IEmailService,EmailService>();
            
            service.AddSingleton<INotificationService,NotificationService>();
            
            return service;
        }
    }
}