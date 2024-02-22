using System.ComponentModel.DataAnnotations;

namespace Imc.Shared.Enums;

public enum EImcStatus
{
    Magreza,
    Normal,
    Sobrepeso,
    Obesidade,
    [Display(Name = "Obesidade grave")]
    ObesidadeGrave
}
