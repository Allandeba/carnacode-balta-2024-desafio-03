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

        var imcResult = new ImcResult();
        if (imc < 18.5) {
            imcResult.Title = EImcStatus.Magreza.ToString();
            imcResult.Body = string.Format(Warning, imcResult.Title);
            imcResult.Status = EStatusImc.Ruim;
        } else if (imc >= 18.5 && imc < 25) {
            imcResult.Title = EImcStatus.Normal.ToString();
            imcResult.Body = string.Format(Congrats, imcResult.Title);
            imcResult.Status = EStatusImc.Bom;
        } else if (imc >= 25 && imc < 30) {
            imcResult.Title = EImcStatus.Sobrepeso.ToString();
            imcResult.Body = string.Format(Warning, imcResult.Title);
            imcResult.Status = EStatusImc.Ruim;
        } else if (imc >= 30 && imc < 40) {
            imcResult.Title = EImcStatus.Obesidade.ToString();
            imcResult.Body = string.Format(Warning, imcResult.Title);
            imcResult.Status = EStatusImc.Ruim;
        } else {
            imcResult.Title = EImcStatus.ObesidadeGrave.GetDisplayName();
            imcResult.Body = string.Format(Warning, imcResult.Title);
            imcResult.Status = EStatusImc.Ruim;
        }

        return imcResult;
    }
}