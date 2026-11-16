using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ForzaTunerResults.Persistence
{
    public interface IDatabaseQuery<T>
    {
        public Task<Collection<T>> Execute();
    }
}
