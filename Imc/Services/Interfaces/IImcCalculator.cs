using Imc.Models;

namespace Imc.Services.Interfaces;

public interface IImcCalculator
{
    ImcResult Calculate(ImcCalculator imcCalculator);
}

public interface IImcCalculatorService : IImcCalculator;