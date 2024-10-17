using personapi_dotnet.Models.Entities;

public partial class Telefono
{
    public string Num { get; set; } = null!;
    public string? Oper { get; set; }
    public int? Dueno { get; set; }

    // Propiedad de navegación para Persona
    //public virtual Persona? DuenoNavigation { get; set; }
}
