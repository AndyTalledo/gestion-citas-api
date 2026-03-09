using System.ComponentModel.DataAnnotations;

public class Persona
{
    [Required(ErrorMessage ="El nombre es OBLIGATORIO")]
    [StringLength(60, MinimumLength =2, ErrorMessage ="El nombre debe tener mínimo 2 caracteres, máximo 60")]
    public string Nombres { get; set; } = "";

    [Required(ErrorMessage ="El primer apellido es OBLIGATORIO")]
    [StringLength(50, MinimumLength =2, ErrorMessage ="El apellido debe tener mínimo 2 caracteres, máximo 50")]
    public string ApePaterno { get; set; } = "";

    // [Required] // Considerar que hay personas con solo 1 apellido
    [StringLength(50)]
    public string ApeMaterno { get; set; } = "";

    [Required(ErrorMessage = "El DNI es OBLIGATORIO")]
    [StringLength(8, MinimumLength = 8, ErrorMessage = "El DNI debe tener 8 dígitos")]
    public string DNI { get; set; } = "";

    [Required(ErrorMessage = "El celular es OBLIGATORIO")]
    [StringLength(9, MinimumLength = 9, ErrorMessage = "El celular debe tener 9 dígitos")]
    public string Celular { get; set; } = "";
}