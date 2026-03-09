using System.ComponentModel.DataAnnotations;

public class Cita
{
    public int IdCita { get; set; }

    [Required]
    public int IdPaciente { get; set; }

    [Required]
    public int IdMedico { get; set; }

    [Required(ErrorMessage ="La fecha de la cita es OBLIGATORIA")]
    // Agregamos ? para ver correctamente mensaje de error en POSTMAN 
    // y no solo error Bad Request 400, además de que evita
    // que si no se le manda valor a la fecha no guarde por defecto 01/01/0001 00:00:00
    public DateTime? Fecha { get; set; }

    [StringLength(20, MinimumLength = 7, ErrorMessage ="El estado debe tener mínimo 7 caracteres, máximo 20")]
    public string Estado { get; set; } = "Programada";
}