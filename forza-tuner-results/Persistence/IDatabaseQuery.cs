using System.Threading.Tasks;

namespace ForzaTunerResults.Persistence
{
    public interface IDatabaseQuery<T>
    {
        public Task<T> Execute();
    }
}
