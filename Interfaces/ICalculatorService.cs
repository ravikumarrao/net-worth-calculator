using api.Dtos.Responses;

namespace api.Interfaces
{
    public interface ICalculatorService
    {
        CalculateNetworthResponse CalculateNetWorth(int userId);
    }
}