public class NotificacionCorreo : INotificacion
{
    public void Enviar(string mensaje)
    {
        Console.WriteLine("Correo enviado: " + mensaje);
    }
}