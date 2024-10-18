namespace personapi_dotnet.Models.Entities;

public partial class Estudio
{
    public int IdProf { get; set; }

    public int CcPer { get; set; }

    public DateOnly? Fecha { get; set; }

    public string? Univer { get; set; }

    public virtual Persona? CcPerNavigation { get; set; }
    public virtual Profesion? IdProfNavigation { get; set; }

}
