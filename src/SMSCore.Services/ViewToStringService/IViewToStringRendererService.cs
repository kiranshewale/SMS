using System.Threading.Tasks;

namespace SMSCore.Services.ViewToStringService
{
    public interface IViewToStringRendererService
    {
        Task<string> RenderToStringAsync(string viewName, object model);
    }
}