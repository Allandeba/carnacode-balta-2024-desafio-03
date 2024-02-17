using System.ComponentModel.DataAnnotations;

namespace Imc.Shared.Enums;

public enum EGender
{
    [Display(Name = "Não informado")]
    NaoInformado,
    Masculino,
    Feminino
}
