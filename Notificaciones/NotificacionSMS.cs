public class NotificacionSMS : INotificacion
{
    public void Enviar(string mensaje)
    {
        Console.WriteLine("SMS enviado: " + mensaje);
    }
}