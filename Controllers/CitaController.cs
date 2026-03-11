using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/citas")]
public class CitaController : ControllerBase
{
    private static List<Cita> citas = new List<Cita>();
    /*
    private static List<Cita> citas = new List<Cita>()
    {
        new Cita{IdCita=2,IdPaciente=2,IdMedico=3,Fecha = new DateTime(2022,9,15)}
    };
    */

    /*
    [HttpGet]
    public IActionResult GetCitas()
    {
        try
        {
            return Ok(citas);
        }
        catch(Exception)
        {
            return StatusCode(500, "Error interno de servidor");
        }
    }
    */

    // GET => PARA LEER TODAS LAS CITAS
    // api/citas

    [HttpGet]
    public IActionResult GetCitas()
    {
        try
        {   // Almacenamos una lista de objetos anónimos tipo base (object) en "var" para
            // no tener que crear una clase específica como por ejemplo ResumenCita
            var resultado = new List<object>();

            foreach (Cita cita in citas)
            {
                resultado.Add(new
                {
                    cita.IdCita,
                    cita.IdPaciente,
                    cita.IdMedico,
                    cita.Fecha,
                    cita.Estado,
                    cita.FechaRegistro,
                    cita.FechaActualizacion
                });
            }

            return Ok(resultado);
        }
        catch(Exception)
        {
            return StatusCode(500, "Error interno de servidor");
        }
    }

    // GET => PARA OBTENER UNA CITA POR ID
    // api/citas/{id}
    [HttpGet("{id}")]
    public IActionResult GetCita(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("El id debe ser mayor a 0");
            }

            foreach (Cita cita in citas)
            {
                if (cita.IdCita == id)
                {
                    // Se usa "?" para indicar que el tipo permite valores null,
                    // evitando errores si aún no se ha encontrado o asignado un paciente.
                    // Declara una variable de tipo Paciente y otra tipo Medico 
                    // que puede ser nula e inicialmente no apunta a ningún objeto (paciente o medico).
                    Paciente? pacienteEncontrado = null; 
                    Medico? medicoEncontrado = null;

                    // Buscar paciente
                    foreach (Paciente paciente in PacienteController.pacientes)
                    {
                        if (paciente.IdPaciente == cita.IdPaciente)
                        {
                            pacienteEncontrado = paciente;
                            break;
                        }
                    }

                    // Buscar medico
                    foreach (Medico medico in MedicoController.medicos)
                    {
                        if (medico.IdMedico == cita.IdMedico)
                        {
                            medicoEncontrado = medico;
                            break;
                        }
                    }

                    var resultado = new
                    {
                        cita.IdCita,
                        // Creamos un nuevo objeto que devuelva los campos requeridos del paciente.
                        // Para no devolver todos los campos y para evitar error 
                        // en caso devuelva valor nulo usamos un operador ternario
                        // Si es el valor es null devuelve "null", caso contrario devuelve el objeto
                        Paciente = pacienteEncontrado is null ? null : new
                        {
                            pacienteEncontrado.IdPaciente,
                            pacienteEncontrado.Nombres,
                            pacienteEncontrado.ApePaterno,
                            pacienteEncontrado.ApeMaterno,
                            pacienteEncontrado.DNI,
                            pacienteEncontrado.Celular,
                            pacienteEncontrado.Correo,
                            // No devolvemos Fecha de nacimiento
                            pacienteEncontrado.Edad
                        },
                        // En caso de médico hacemos similar solo para ordenarlo
                        Medico = medicoEncontrado is null ? null : new
                        {
                            medicoEncontrado.IdMedico,
                            medicoEncontrado.Nombres,
                            medicoEncontrado.ApePaterno,
                            medicoEncontrado.ApeMaterno,
                            medicoEncontrado.DNI,
                            medicoEncontrado.Celular,
                            medicoEncontrado.Especialidad,
                            medicoEncontrado.FechaIngreso,
                            // Agregamos el nombre "Estado" al campo, ya que si no se coloca
                            // el bloque anónimo lo tomará como una expresión y dará un error
                            Estado = medicoEncontrado.Estado ? "Activo" : "Inactivo"
                        },
                        cita.Fecha,
                        cita.Estado,
                        cita.FechaRegistro,
                        cita.FechaActualizacion
                    };

                    return Ok(resultado);
                }
            }

            return NotFound("Cita no encontrada");
        }
        catch(Exception)
        {
            return StatusCode(500, "Error interno de servidor");
        }
    }


    // POST => PARA REGISTRAR UNA NUEVA CITA
    [HttpPost]
    public IActionResult CrearCita([FromBody] Cita nuevaCita)
    {
        try
        {
            if (nuevaCita == null)
            {
                return BadRequest("Debe enviar los datos de la cita");
            }
            
            // VALIDAR QUE EXISTAN PACIENTE Y MEDICO
            // .Any() => Evalúa la colección y retorna TRUE si encuentra al menos un elemento
            // Declaramos una variable booleana que verificará si existe un paciente en la lista y
            // verificamos con .Any() si algún paciente de la lista tiene el mismo Id que se envió en la nueva cita
            bool pacienteExiste = PacienteController.pacientes.Any(p => p.IdPaciente == nuevaCita.IdPaciente);
            // Aplicamos misma validacion para medico
            bool medicoExiste = MedicoController.medicos.Any(m => m.IdMedico == nuevaCita.IdMedico);

            if (!pacienteExiste || !medicoExiste)
            {
                return BadRequest("El paciente o el médico no existen");
            }

            // Obtener el siguiente ID
            if (citas.Count == 0)
            {
                nuevaCita.IdCita = 1;
            }
            else
            {
                nuevaCita.IdCita = citas.Max(c => c.IdCita) + 1;
            }

            citas.Add(nuevaCita);

            // Enviar notificación por correo
            INotificacion notificacion = new NotificacionCorreo();
            notificacion.Enviar("Su cita médica ha sido registrada");

            return Created($"api/citas/{nuevaCita.IdCita}", nuevaCita);
        }
        catch(Exception)
        {
            return StatusCode(500, "Error interno de servidor");
        }
    }

    // PUT => PARA ACTUALIZAR UNA CITA
    [HttpPut("{id}")]
    public IActionResult ActualizarCita(int id, [FromBody] Cita citaActualizada)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("El id debe ser mayor que 0");
            }

            if (citaActualizada == null)
            {
                return BadRequest("Debe enviar correctamente los datos de la cita");
            }

            // VALIDAR QUE EXISTAN PACIENTE Y MEDICO
            bool pacienteExiste = PacienteController.pacientes
                .Any(p => p.IdPaciente == citaActualizada.IdPaciente);

            bool medicoExiste = MedicoController.medicos
                .Any(m => m.IdMedico == citaActualizada.IdMedico);

            if (!pacienteExiste || !medicoExiste)
            {
                return BadRequest("El paciente o el médico no existen");
            }

            foreach (Cita cita in citas)
            {
                if (cita.IdCita == id)
                {
                    cita.IdPaciente = citaActualizada.IdPaciente;
                    cita.IdMedico = citaActualizada.IdMedico;
                    cita.Fecha = citaActualizada.Fecha;
                    cita.Estado = citaActualizada.Estado;
                    // Actualiza FechaActualizacion al hacer cambios
                    cita.FechaActualizacion = DateTime.Now;

                    // Enviar notificación por SMS
                    INotificacion notificacion = new NotificacionSMS();
                    notificacion.Enviar("Su cita médica fue actualizada");

                    return Ok("Cita actualizada satisfactoriamente");
                }
            }

            return NotFound("Cita no encontrada");
        }
        catch (Exception)
        {
            return StatusCode(500,"Ocurrió un error interno en el servidor");
        }
    }


    // DELETE => PARA ELIMINAR UNA CITA
    [HttpDelete("{id}")]
    public IActionResult EliminarCita(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido");
            }

            foreach (Cita cita in citas)
            {
                if (cita.IdCita == id)
                {
                    citas.Remove(cita);
                    // Enviar notificación por WhatsApp
                    INotificacion notificacion = new NotificacionWhatsApp();
                    notificacion.Enviar("Su cita médica fue cancelada");

                    return Ok("Cita eliminada correctamente");
                }
            }

            return NotFound("Cita no encontrada");
        }
        catch (Exception)
        {
            return StatusCode(500,"Ocurrió un error interno en el servidor");
        }
    }
}