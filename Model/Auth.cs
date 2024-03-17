using System.Runtime.Serialization;

namespace PaymentsApi.Model
{
    public class Auth
    {

        public decimal Monto { get; set; }
        public string TipoCliente { get; set; }

        public string  Estado {  get; set; }

        public string TipoAuth { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Auth(decimal monto, string tipoCliente, string tipoAuth)
        {
            this.Monto = monto;
            this.TipoCliente = tipoCliente;
            this.TipoAuth = tipoAuth;
            this.Estado = EstadoAuth.Rechazado.ToString();
            this.FechaRegistro = DateTime.Now;
        }
    }

    public enum TipoAuth
    {
        [EnumMember(Value = "Cobro")]
        Cobro = 0,

        [EnumMember(Value = "Devolucion")]
        Devolucion = 1
    }

    public enum TipoCliente
    {
        [EnumMember(Value = "Primero")]
        Primero =  1,

        [EnumMember(Value = "Segundo")]
        Segundo = 2
    }

    public enum EstadoAuth
    {
        [EnumMember(Value = "Rechazado")]
        Rechazado = 1,

        [EnumMember(Value = "Aprobado")]
        Aprobado = 2,

        [EnumMember(Value = "Pendiente de reversa")]
        AprobadoPendienteReversa = 3,

        [EnumMember(Value = "Reversa aprobada")]
        AprobadoReversa = 4
    }
}
