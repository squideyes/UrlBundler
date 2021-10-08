using System.Threading.Tasks;

namespace BackEnd.Infrastructure
{
    public interface IBlackListChecker
    {
        Task<bool> Check(string value);
    }
}