using System.Threading;
using System.Threading.Tasks;

namespace SMSCore
{
    public interface IWorker
    {
        Task DoWork(CancellationToken cancellationToken);
    }
}