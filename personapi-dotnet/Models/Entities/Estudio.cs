using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace personapi_dotnet.Models.Entities;

public partial class Estudio
{
    public int IdProf { get; set; }

    public int CcPer { get; set; }

    public DateOnly? Fecha { get; set; }

    public string? Univer { get; set; }

    [JsonIgnore]
    public virtual Persona CcPerNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual Profesion IdProfNavigation { get; set; } = null!;
}
