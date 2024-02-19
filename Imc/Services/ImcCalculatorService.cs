using Imc.Models;
using Imc.Shared.Enums;

namespace Imc.Services;

public interface IImcCalculator
{
    ImcResult Calculate(ImcCalculator imcCalculator);
}

public interface IImcCalculatorService : IImcCalculator;

public class ImcCalculatorService : IImcCalculatorService
{
    public ImcResult Calculate(ImcCalculator imcCalculator)
    {
        if (!imcCalculator.IsValid())
            throw new Exception("Passou na validação do modelo e não deveria ter passado!");

        double heightInMeters = imcCalculator.Height!.Value / 100.0;
        double result = imcCalculator.Weight!.Value / (heightInMeters * heightInMeters);
        return GetImcResult(result);
    }

    private ImcResult GetImcResult(double imc)
    {
        const string Warning = "Cuidado você está {0}, dê uma atenção para sua saúde";
        const string Congrats = "Você está {0}, continue mantendo este estilo!";

        var ImcResult = new ImcResult();
        if (imc < 18.5) {
            ImcResult.Title = EImcStatus.Magreza.ToString();
            ImcResult.Body = string.Format(Warning, ImcResult.Title);
            ImcResult.Status = EStatusImc.Ruim;
        } else if (imc >= 18.5 && imc < 25) {
            ImcResult.Title = EImcStatus.Normal.ToString();
            ImcResult.Body = string.Format(Congrats, ImcResult.Title);
            ImcResult.Status = EStatusImc.Bom;
        } else if (imc >= 25 && imc < 30) {
            ImcResult.Title = EImcStatus.Sobrepeso.ToString();
            ImcResult.Body = string.Format(Warning, ImcResult.Title);
            ImcResult.Status = EStatusImc.Ruim;
        } else if (imc >= 30 && imc < 40) {
            ImcResult.Title = EImcStatus.Obesidade.ToString();
            ImcResult.Body = string.Format(Warning, ImcResult.Title);
            ImcResult.Status = EStatusImc.Ruim;
        } else {
            // Todo: Fazer o Display name Extension para enumeradores
            ImcResult.Title = "Obesidade Grave";
            ImcResult.Body = string.Format(Warning, ImcResult.Title);
            ImcResult.Status = EStatusImc.Ruim;
        }

        return ImcResult;
    }
}