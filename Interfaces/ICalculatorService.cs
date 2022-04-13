using System.Threading.Tasks;
using api.Dtos.Responses;

namespace api.Interfaces
{
    public interface ICalculatorService
    {
        Task<CalculateNetworthResponse> CalculateNetWorth(int userId, string currency);
    }
}