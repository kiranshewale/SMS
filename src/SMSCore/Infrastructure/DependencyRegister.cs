using Microsoft.Extensions.DependencyInjection;
using SMSCore.Services;
using SMSCore.Services.AccountsService;
using SMSCore.Services.BranchMasterService;
using SMSCore.Services.CoDealerMasterService;
using SMSCore.Services.ColourMasterService;
using SMSCore.Services.CustomerService;
using SMSCore.Services.DesignationMasterService;
using SMSCore.Services.GatePassService;
using SMSCore.Services.ModelMasterService;
using SMSCore.Services.PDIVehiclesService;
using SMSCore.Services.PriceListMasterService;
using SMSCore.Services.QuotationManagerService;
using SMSCore.Services.RoleMasterService;
using SMSCore.Services.ShowroomVehiclesService;
using SMSCore.Services.StateListService;
using SMSCore.Services.UserMasterService;
using SMSCore.Services.VarientMasterService;
using SMSCore.Services.VehicleAllotmentService;
using SMSCore.Services.VehiclesMasterService;
using SMSCore.Services.ViewToStringService;

namespace SMSCore.Infrastructure
{
    public static class DependencyRegister
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserMasterService, UserMasterService>();
            services.AddScoped<IDesignationMasterService, DesignationMasterService>();
            services.AddScoped<IRoleMasterService, RoleMasterService>();
            services.AddScoped<IVehiclesMasterService, VehiclesMasterService>();
            services.AddScoped<IPriceListMasterService, PriceListMasterService>();
            services.AddScoped<IModelMasterService, ModelMasterService>();
            services.AddScoped<IVarientMasterService, VarientMasterService>();
            services.AddScoped<IColourMasterService, ColourMasterService>();
            services.AddScoped<IViewToStringRendererService, ViewToStringRendererService>();
            services.AddScoped<IStateListService, StateListService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IQuotationManagerService, QuotationManagerService>();
            services.AddScoped<IVehicleAllotmentService, VehicleAllotmentService>();
            services.AddScoped<IBranchMasterService, BranchMasterService>();
            services.AddScoped<IShowroomVehiclesService, ShowroomVehiclesService>();
            services.AddScoped<IPDIVehiclesService, PDIVehiclesService>();
            services.AddScoped<IGatePassService, GatePassService>();
            services.AddScoped<ICoDealerMasterService, CoDealerMasterService>();

            return services;
        }
    }
}
