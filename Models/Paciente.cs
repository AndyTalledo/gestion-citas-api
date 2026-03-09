using System.ComponentModel.DataAnnotations;

public class Paciente : Persona
{
    public int IdPaciente { get; set; }

    [Required(ErrorMessage ="La fecha de nacimiento es OBLIGATORIO")]
    public DateTime FechaNacimiento { get; set; }

    [Required(ErrorMessage ="El correo electrónico es OBLIGATORIO")]
    [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
    public string Correo { get; set; } = "";

    public int Edad
    {
        get
        {
            int edad = DateTime.Now.Year - FechaNacimiento.Year;
            // Condicionamos a que si aún no llega su fecha de nacimiento
            // del año en curso, le reste 1 año a su edad
            if (DateTime.Now.DayOfYear < FechaNacimiento.DayOfYear)
            {
                edad--;
            }

            return edad;
        }
    }
}