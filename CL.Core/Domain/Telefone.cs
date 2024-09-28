namespace CL.Core.Domain;

public class Telefone
{
    public int ClienteID { get; set; }
    public string Numero { get; set; }
    public Cliente Cliente { get; set; }

}


