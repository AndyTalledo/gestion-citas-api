using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/pacientes")]
public class PacienteController : ControllerBase
{

    public static List<Paciente> pacientes = new List<Paciente>()
    {
        new Paciente{IdPaciente=1,Nombres="Andy",ApePaterno="Talledo",ApeMaterno="Ceverino",DNI="12345678",Celular="999111111",Correo="andy@gmail.com",FechaNacimiento=new DateTime(1995,5,10)},
        new Paciente{IdPaciente=2,Nombres="Jonathan",ApePaterno="Jimenez",ApeMaterno="Diaz",DNI="87654321",Celular="999222222",Correo="ana@gmail.com",FechaNacimiento=new DateTime(1998,8,20)},
        new Paciente{IdPaciente=3,Nombres="Javier",ApePaterno="Revilla",ApeMaterno="Soto",DNI="11223344",Celular="999333333",Correo="luis@gmail.com",FechaNacimiento=new DateTime(1992,2,15)},
        new Paciente{IdPaciente=4,Nombres="Maria",ApePaterno="Vargas",ApeMaterno="Rojas",DNI="55667788",Celular="999444444",Correo="maria@gmail.com",FechaNacimiento=new DateTime(1990,10,5)},
        new Paciente{IdPaciente=5,Nombres="Pedro",ApePaterno="Castro",ApeMaterno="Mendoza",DNI="99887766",Celular="999555555",Correo="pedro@gmail.com",FechaNacimiento=new DateTime(1987,12,12)}
    };


    // GET => PARA LEER TODOS LOS PACIENTES
    // api/pacientes
    /*
    [HttpGet]
    public IActionResult GetPacientes()
    {
        try
        {
            return Ok(pacientes);
        }
        catch(Exception)
        {
            return StatusCode(500,"Error interno del servidor");
        }
    }
    */

    [HttpGet]
    public IActionResult GetPacientes()
    {
        try
        {
            // Almacenamos una lista de objetos anónimos tipo base (object) en "var" para
            // no tener que crear una clase específica como por ejemplo ResumenPacienteOrdenado
            var resultado = new List<object>();

            foreach (Paciente paciente in pacientes)
            {
                resultado.Add(new
                {
                    paciente.IdPaciente,
                    paciente.Nombres,
                    paciente.ApePaterno,
                    paciente.ApeMaterno,
                    paciente.DNI,
                    paciente.Celular,
                    paciente.Correo,
                    //paciente.FechaNacimiento,
                    paciente.Edad
                });
            }

            return Ok(resultado);
        }
        catch(Exception)
        {
            return StatusCode(500,"Error interno del servidor");
        }
    }


    // GET => PARA OBTENER UN PACIENTE POR ID
    // api/pacientes/{id}
    [HttpGet("{id}")]
    public IActionResult GetPaciente(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("El id debe ser mayor a 0");
            }

            foreach(Paciente paciente in pacientes)
            {
                if(paciente.IdPaciente == id)
                {
                    var resultado = new
                    {
                        paciente.IdPaciente,
                        paciente.Nombres,
                        paciente.ApePaterno,
                        paciente.ApeMaterno,
                        paciente.DNI,
                        paciente.Celular,
                        paciente.Correo,
                        //paciente.FechaNacimiento,
                        paciente.Edad
                    };

                    return Ok(resultado);
                }
            }

            return NotFound("Paciente no encontrado");
        }
        catch(Exception)
        {
            return StatusCode(500,"Error interno del servidor");
        }
    }


    // POST => PARA REGISTRAR UN NUEVO PACIENTE
    [HttpPost]
    public IActionResult CrearPaciente([FromBody] Paciente nuevoPaciente)
    {
        try
        {
            if(nuevoPaciente == null)
            {
                return BadRequest("Debe enviar los datos del paciente");
            }

            // Obtener el siguiente ID
            if(pacientes.Count == 0)
            {
                nuevoPaciente.IdPaciente = 1;
            }
            else
            {
                nuevoPaciente.IdPaciente = pacientes.Max(p => p.IdPaciente) + 1;
            }

            pacientes.Add(nuevoPaciente);

            return Created($"api/pacientes/{nuevoPaciente.IdPaciente}",nuevoPaciente);
        }
        catch(Exception)
        {
            return StatusCode(500,"Error interno del servidor");
        }
    }


    // PUT => PARA ACTUALIZAR UN PACIENTE
    [HttpPut("{id}")]
    public IActionResult ActualizarPaciente(int id, [FromBody] Paciente pacienteActualizado)
    {
        try
        {
            if(id <= 0)
            {
                return BadRequest("El id debe ser mayor a 0");
            }

            if(pacienteActualizado == null)
            {
                return BadRequest("Debe enviar correctamente los datos del paciente");
            }

            foreach(Paciente paciente in pacientes)
            {
                if(paciente.IdPaciente == id)
                {
                    paciente.Nombres = pacienteActualizado.Nombres;
                    paciente.ApePaterno = pacienteActualizado.ApePaterno;
                    paciente.ApeMaterno = pacienteActualizado.ApeMaterno;
                    paciente.DNI = pacienteActualizado.DNI;
                    paciente.Celular = pacienteActualizado.Celular;
                    paciente.Correo = pacienteActualizado.Correo;
                    paciente.FechaNacimiento = pacienteActualizado.FechaNacimiento;

                    return Ok("Paciente actualizado correctamente");
                }
            }

            return NotFound("Paciente no encontrado");
        }
        catch(Exception)
        {
            return StatusCode(500,"Error interno del servidor");
        }
    }


    // DELETE => PARA ELIMINAR UN PACIENTE
    [HttpDelete("{id}")]
    public IActionResult EliminarPaciente(int id)
    {
        try
        {
            if(id <= 0)
            {
                return BadRequest("ID inválido");
            }

            foreach(Paciente paciente in pacientes)
            {
                if(paciente.IdPaciente == id)
                {
                    pacientes.Remove(paciente);
                    return Ok("Paciente eliminado correctamente");
                }
            }

            return NotFound("Paciente no encontrado");
        }
        catch(Exception)
        {
            return StatusCode(500,"Error interno del servidor");
        }
    }
}