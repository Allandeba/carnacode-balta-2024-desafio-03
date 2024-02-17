using System.ComponentModel.DataAnnotations;
using Imc.Shared.Enums;
using static Imc.Shared.Constants.MessageConstants;
using static Imc.Shared.Constants.TimeConstants;

namespace Imc.Models;

public class ImcCalculator
{
    public Guid Guid { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = RequiredMessage)]
    [Range(0, 300, ErrorMessage = RangeMessage)]
    public int? Height { get; set; }

    [Required(ErrorMessage = RequiredMessage)]
    [Range(0, 300, ErrorMessage = RangeMessage)]
    public int? Weight { get; set; }

    [Required(ErrorMessage = RequiredMessage)]
    public bool Older { get; set; }

    [Required(ErrorMessage = RequiredMessage)]
    public EGender Gender { get; set; } = EGender.NaoInformado;

    private DateTime CreatedDateTime = DateTime.UtcNow;
    public string AddedTime {
        get {
            const string sameMessage = "atrás";
            TimeSpan diff = DateTime.UtcNow - CreatedDateTime;

            if (diff.TotalMinutes < Minute)
                return $"{diff.Seconds}s {sameMessage}";

            if (diff.TotalHours < Hour)
                return $"{diff.Minutes}m {sameMessage}";

            if (diff.TotalDays < 1)
                return $"{diff.Hours}h {sameMessage}";

            if (diff.TotalDays == 1)
                return $"{diff.TotalDays} dia {sameMessage}";

            if (diff.TotalDays > 1)
                return $"{diff.TotalDays} dias {sameMessage}";

            return diff.ToString("dd/MM/yyyy");
        }
    }

    public ImcResult Calculate()
    {
        if (!(Height.HasValue && Weight.HasValue))
            throw new Exception("Passou na validação do modelo e não deveria ter passado!");

        double HeightInMeters = Height.Value / 100.0;
        double result = Weight.Value / (HeightInMeters * HeightInMeters);
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
