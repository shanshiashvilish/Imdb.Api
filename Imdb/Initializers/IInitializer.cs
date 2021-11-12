using System.Threading.Tasks;

namespace Imdb.Api.Initializers
{
    public interface IInitializer
    {
        Task Initialize();
    }
}
