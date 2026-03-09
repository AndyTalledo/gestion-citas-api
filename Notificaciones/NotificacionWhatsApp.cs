public class NotificacionWhatsApp : INotificacion
{
    public void Enviar(string mensaje)
    {
        Console.WriteLine("WhatsApp enviado: " + mensaje);
    }
}