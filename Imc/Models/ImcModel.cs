using System.ComponentModel.DataAnnotations;
using Imc.Models.Enums;

namespace Imc.Models
{
    public class ResultImc
    {
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public EStatusImc Status { get; set; } = EStatusImc.Ruim;
        public string Icon => (Status == EStatusImc.Ruim) ? "⛔" : "✅";
    }

    public class ImcModel
    {
        private const string RequiredMessage = "Campo {0} é requerido";
        private const string RangeMessage = "{0} deve ser maior que {1} e menor que {2}";

        public Guid Guid { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = RequiredMessage)]
        [Range(0, 300, ErrorMessage = RangeMessage)]
        public int? Altura { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [Range(0, 300, ErrorMessage = RangeMessage)]
        public int? Peso { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public bool Idoso { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public EGenero Sexo { get; set; } = EGenero.NaoInformado;

        private DateTime Adicionado = DateTime.UtcNow;
        public string Inserido {
            get {
                TimeSpan diferenca = DateTime.UtcNow - Adicionado;
                if (diferenca.Minutes < 1)
                    return $"{diferenca.Seconds}s atrás";

                if (diferenca.TotalHours < 1)
                    return $"{diferenca.Minutes}m atrás";

                if (diferenca.TotalHours >= 1)
                    return $"{diferenca.Hours}h atrás";

                return diferenca.ToString("dd/MM/yyyy");
            }
        }

        public ResultImc Calcular()
        {
            if (!(Altura.HasValue && Peso.HasValue))
                throw new Exception("Passou na validação do modelo e não deveria ter passado!");

            double alturaMetros = Altura.Value / 100.0;
            double result = Peso.Value / (alturaMetros * alturaMetros);
            return GetResultImc(result);
        }

        private ResultImc GetResultImc(double imc)
        {
            const string Cuidado = "Cuidado você está {0}, dê uma atenção para sua saúde";
            const string Parabens = "Você está {0}, continue mantendo este estilo!";

            var ResultImc = new ResultImc();
            if (imc < 18.5) {
                ResultImc.Title = EImcStatus.Magreza.ToString();
                ResultImc.Body = string.Format(Cuidado, ResultImc.Title);
                ResultImc.Status = EStatusImc.Ruim;
            } else if (imc >= 18.5 && imc < 25) {
                ResultImc.Title = EImcStatus.Normal.ToString();
                ResultImc.Body = string.Format(Parabens, ResultImc.Title);
                ResultImc.Status = EStatusImc.Bom;
            } else if (imc >= 25 && imc < 30) {
                ResultImc.Title = EImcStatus.Sobrepeso.ToString();
                ResultImc.Body = string.Format(Cuidado, ResultImc.Title);
                ResultImc.Status = EStatusImc.Ruim;
            } else if (imc >= 30 && imc < 40) {
                ResultImc.Title = EImcStatus.Obesidade.ToString();
                ResultImc.Body = string.Format(Cuidado, ResultImc.Title);
                ResultImc.Status = EStatusImc.Ruim;
            } else {
                // Todo: Fazer o Display name Extension para enumeradores
                ResultImc.Title = "Obesidade Grave";
                ResultImc.Body = string.Format(Cuidado, ResultImc.Title);
                ResultImc.Status = EStatusImc.Ruim;
            }

            return ResultImc;
        }
    }
}
