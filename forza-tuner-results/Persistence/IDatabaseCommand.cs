using System.Threading.Tasks;

namespace ForzaTunerResults.Persistence
{
    public interface IDatabaseCommand<T>
    {
        public Task Execute(T parameter);
    }
}
