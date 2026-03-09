using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/medicos")]
public class MedicoController : ControllerBase
{

    public static List<Medico> medicos = new List<Medico>()
    {
        new Medico{IdMedico=1,Nombres="Carlos",ApePaterno="Gomez",ApeMaterno="Perez",DNI="11111111",Celular="988111111",Especialidad="Cardiologia",FechaIngreso=new DateTime(2022,1,10),Estado=true},
        new Medico{IdMedico=2,Nombres="Laura",ApePaterno="Sanchez",ApeMaterno="Diaz",DNI="22222222",Celular="988222222",Especialidad="Pediatria",FechaIngreso=new DateTime(2021,5,20),Estado=true},
        new Medico{IdMedico=3,Nombres="Miguel",ApePaterno="Torres",ApeMaterno="Rojas",DNI="33333333",Celular="988333333",Especialidad="Dermatologia",FechaIngreso=new DateTime(2020,3,12),Estado=true},
        new Medico{IdMedico=4,Nombres="Rosa",ApePaterno="Lopez",ApeMaterno="Martinez",DNI="44444444",Celular="988444444",Especialidad="Neurologia",FechaIngreso=new DateTime(2019,7,15),Estado=true},
        new Medico{IdMedico=5,Nombres="Jorge",ApePaterno="Ramirez",ApeMaterno="Castro",DNI="55555555",Celular="988555555",Especialidad="Ginecologia",FechaIngreso=new DateTime(2023,2,1),Estado=false}
    };


    // GET => PARA LEER TODOS LOS MEDICOS
    // api/medicos

    /*
    [HttpGet]
    public IActionResult GetMedicos()
    {
        try
        {
            return Ok(medicos);
        }
        catch(Exception)
        {
            return StatusCode(500, "Error interno de servidor");
        }
    }
    */

    [HttpGet]
    public IActionResult GetMedicos()
    {
        try
        {
            // Almacenamos una lista de objetos anónimos tipo base (object) en "var" para
            // no tener que crear una clase específica como por ejemplo ResumenMedicoOrdenado
            var resultado = new List<object>();

            foreach (Medico medico in medicos)
            {
                resultado.Add(new
                {
                    medico.IdMedico,
                    medico.Nombres,
                    medico.ApePaterno,
                    medico.ApeMaterno,
                    medico.DNI,
                    medico.Celular,
                    medico.Especialidad,
                    medico.FechaIngreso,
                    // Si es true muestra Activo, si es false muestra Inactivo
                    Estado = medico.Estado ? "Activo" : "Inactivo" // Convierte el bool en texto
                });
            }

            return Ok(resultado);
        }
        catch(Exception)
        {
            return StatusCode(500,"Error interno del servidor");
        }
    }

    // GET => PARA OBTENER UN MEDICO POR ID
    // api/medicos/{id}
    [HttpGet("{id}")]
    public IActionResult GetMedico(int id)
    {
        try
        {
            if(id <= 0)
            {
                return BadRequest("El id debe ser mayor a 0");
            }

            foreach(Medico medico in medicos)
            {
                if(medico.IdMedico == id)
                {
                    var resultado = new
                    {
                        medico.IdMedico,
                        medico.Nombres,
                        medico.ApePaterno,
                        medico.ApeMaterno,
                        medico.DNI,
                        medico.Celular,
                        medico.Especialidad,
                        medico.FechaIngreso,
                        Estado = medico.Estado ? "Activo" : "Inactivo"
                    };

                    return Ok(resultado);
                }
            }

            return NotFound("Médico no encontrado");
        }
        catch(Exception)
        {
            return StatusCode(500,"Error interno del servidor");
        }
    }


    // POST => PARA REGISTRAR UN NUEVO MEDICO
    [HttpPost]
    public IActionResult CrearMedico([FromBody] Medico nuevoMedico)
    {
        try
        {
            if(nuevoMedico == null)
            {
                return BadRequest("Debe enviar los datos del médico");
            }

            // Obtener el siguiente ID
            if(medicos.Count == 0)
            {
                nuevoMedico.IdMedico = 1;
            }
            else
            {
                nuevoMedico.IdMedico = medicos.Max(m => m.IdMedico) + 1;
            }

            medicos.Add(nuevoMedico);

            return Created($"api/medicos/{nuevoMedico.IdMedico}",nuevoMedico);
        }
        catch(Exception)
        {
            return StatusCode(500,"Error interno del servidor");
        }
    }


    // PUT => PARA ACTUALIZAR UN MEDICO
    [HttpPut("{id}")]
    public IActionResult ActualizarMedico(int id, [FromBody] Medico medicoActualizado)
    {
        try
        {
            if(id <= 0)
            {
                return BadRequest("El id debe ser mayor a 0");
            }

            if(medicoActualizado == null)
            {
                return BadRequest("Debe enviar correctamente los datos del médico");
            }

            foreach(Medico medico in medicos)
            {
                if(medico.IdMedico == id)
                {
                    medico.Nombres = medicoActualizado.Nombres;
                    medico.ApePaterno = medicoActualizado.ApePaterno;
                    medico.ApeMaterno = medicoActualizado.ApeMaterno;
                    medico.DNI = medicoActualizado.DNI;
                    medico.Celular = medicoActualizado.Celular;
                    medico.Especialidad = medicoActualizado.Especialidad;
                    medico.FechaIngreso = medicoActualizado.FechaIngreso;
                    medico.Estado = medicoActualizado.Estado;

                    return Ok("Médico actualizado correctamente");
                }
            }

            return NotFound("Médico no encontrado");
        }
        catch(Exception)
        {
            return StatusCode(500,"Error interno del servidor");
        }
    }


    // DELETE => PARA ELIMINAR UN MEDICO
    [HttpDelete("{id}")]
    public IActionResult EliminarMedico(int id)
    {
        try
        {
            if(id <= 0)
            {
                return BadRequest("ID inválido");
            }

            foreach(Medico medico in medicos)
            {
                if(medico.IdMedico == id)
                {
                    medicos.Remove(medico);
                    return Ok("Médico eliminado correctamente");
                }
            }

            return NotFound("Médico no encontrado");
        }
        catch(Exception)
        {
            return StatusCode(500,"Error interno del servidor");
        }
    }
}