using Microsoft.AspNetCore.Mvc;
using PaymentsApi.Model;
using System.IO;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace PaymentsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private static Auth CreateAuth(decimal monto, string tipoCliente, string tipoAuth)
        {
            Auth auth = new Auth(monto, tipoCliente, tipoAuth);

            return auth;
        }

        private static void Loguer(Auth auth, bool endsAuth)
        {
            string logFile = ObtainFile(endsAuth);
            WriteLine(logFile, CreateLine(auth));
        }

        private static string ObtainFile(bool endsAuth)
        {
            var pathFile = ObtainPath(endsAuth);
            if (!System.IO.File.Exists(pathFile))
            {
                var createFile = new FileStream(ObtainPath(endsAuth), FileMode.Create);
                createFile.Close();
                WriteLine(pathFile, CreateHead());
            }
            return pathFile;
        }

        private static string ReadFile(bool onlyAprovedAuth)
        {
            return System.IO.File.ReadAllText(ObtainPath(onlyAprovedAuth));
        }

        private static string ObtainPath(bool  onlyAprovedAuth)
        {
            return Path.GetTempPath() + (onlyAprovedAuth ? @"finalLog.txt" : @"log.txt");
        }

        private static string CreateLine(Auth auth)
        {
            string line = CreateCol(auth.TipoAuth.ToString(), 21);
            line += CreateCol(auth.Monto.ToString(), 15, true);
            line += CreateCol(auth.Estado.ToString(), 24);
            line += CreateCol(auth.FechaRegistro.ToString(), 19, true);
            line += CreateCol(auth.TipoCliente.ToString(), 8);
            line += CreateCol("", 0);
            return line.Trim();
        }

        private static string CreateHead()
        {
            string line = CreateCol("Tipo de autorizacion:", 21);
            line += CreateCol("Monto:", 15);
            line += CreateCol("Estado:", 24);
            line += CreateCol("Fecha:", 19);
            line += CreateCol("Cliente:", 8);
            line += CreateCol("", 0);
            return line.Trim();
        }

        private static void WriteLine(string logFile, string line)
        {
            using var sw = new StreamWriter(logFile, append: true);
            sw.WriteLine(line);
            sw.Close();
        }

        private static string CreateCol(string colName, int colLength, bool isNumber = false)
        {
            string space = " | ";
            colName = isNumber ? colName.PadLeft(colLength): colName.PadRight(colLength);
            return space + colName;
        }

        [HttpPost("Validate")]
        public Auth Validate(decimal monto, TipoCliente tipoCliente, TipoAuth tipoAuth)
        {
            Auth auth = CreateAuth(monto, tipoCliente.ToString(), tipoAuth.ToString());
            if (int.TryParse(monto.ToString(), out _))
            {
                if(auth.TipoCliente == TipoCliente.Primero.ToString())
                {
                    auth.Estado = EstadoAuth.Aprobado.ToString();
                    Loguer(auth, true);
                }
                else
                {
                    auth.Estado = EstadoAuth.AprobadoPendienteReversa.ToString();
                }
            }
            Loguer(auth, false);

            return auth;
        }

        [HttpPost("ReverseAuth")]
        public Auth ReverseAuth(Auth auth, bool authReverse)
        {
            if (auth.TipoCliente == TipoCliente.Segundo.ToString() && auth.Estado == EstadoAuth.AprobadoPendienteReversa.ToString() && authReverse)
            {
                auth.Estado = EstadoAuth.AprobadoReversa.ToString();
                Loguer(auth, true);
            }
            else
            {
                auth.Estado = EstadoAuth.Rechazado.ToString();
            }
            Loguer(auth, false);
            return auth;
        }


        [HttpGet("GetAproveReverse")]
        public string GetAproveReverse(bool onlyAprovedAuth) {
            return ReadFile(onlyAprovedAuth);
        }

    }
}
