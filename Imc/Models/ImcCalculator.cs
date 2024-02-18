using System.ComponentModel.DataAnnotations;
using Imc.Shared.Enums;
using Imc.Extensions;
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
    public double? Weight { get; set; }

    [Required(ErrorMessage = RequiredMessage)]
    public bool Older { get; set; }

    [Required(ErrorMessage = RequiredMessage)]
    public EGender Gender { get; set; } = EGender.NaoInformado;

    public DateTime CreatedDateTime { get; set; } = DateTime.MinValue;
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

            return diff.ToPtBR();
        }
    }

    public bool IsValid() => Height.HasValue && Weight.HasValue;
}
