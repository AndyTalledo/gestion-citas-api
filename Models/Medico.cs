using System.ComponentModel.DataAnnotations;

public class Medico : Persona
{
    public int IdMedico { get; set; }

    [Required(ErrorMessage ="La especialidad es OBLIGATORIA")]
    [StringLength(50, MinimumLength = 7, ErrorMessage ="Especialidad debe tener mínimo 7 caracteres, máximo 50")]
    public string Especialidad { get; set; } = "";

    [Required(ErrorMessage ="La fecha de ingreso es OBLIGATORIA")]
    public DateTime FechaIngreso { get; set; }

    public bool Estado { get; set; }
}