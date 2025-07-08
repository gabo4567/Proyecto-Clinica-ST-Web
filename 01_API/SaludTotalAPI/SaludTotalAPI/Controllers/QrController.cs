using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using SaludTotalAPI.Data;
using SaludTotalAPI.Services;

namespace SaludTotalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QrController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly ClinicaContext _context;


        public QrController(ClinicaContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }


        // GET: api/Qr/generar/5
        [HttpGet("generar/{idTurno}")]
        [Authorize(Roles = "Admin,Secretaria,Paciente")]
        public IActionResult GenerarQr(long idTurno)
        {
            // ✅ Link que estará dentro del QR
            var linkTemporal = $"http://192.168.1.35:3000/confirmar-turno/{GenerarHashQr(idTurno)}";



            // ✅ Generar QR en bytes PNG (modo moderno)
            using var qrGenerator = new QRCodeGenerator();
            using var qrData = qrGenerator.CreateQrCode(linkTemporal, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrData);
            var qrBytes = qrCode.GetGraphic(20); // tamaño 20 (puede ajustarse)

            var base64Qr = Convert.ToBase64String(qrBytes);
            return Ok(base64Qr); // 👉 La SPA puede mostrarlo con <img src={`data:image/png;base64,${base64Qr}`} />
        }
        // GET: api/Qr/confirmar/{token}
        [HttpGet("confirmar/{token}")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmarTurno(string token)
        {
            try
            {
                var datos = _jwtService.ValidateToken(token);

                if (datos == null || !datos.ContainsKey("tipo") || datos["tipo"] != "confirmar-turno")
                    return BadRequest("Token inválido o expirado.");

                long idTurno = long.Parse(datos["idTurno"]);


                var turno = await _context.Turnos.FindAsync((long)idTurno);
                if (turno == null)
                    return NotFound("Turno no encontrado.");

                turno.IdEstado = 3; // Confirmado
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Turno confirmado exitosamente." });
            }
            catch (Exception)
            {
                return BadRequest("Token no válido.");
            }
        }

        // 🔐 Genera un token temporal (10 min) codificado como hash
        private string GenerarHashQr(long idTurno)
        {
            var claims = new Dictionary<string, string>
            {
                { "tipo", "confirmar-turno" },
                { "idTurno", idTurno.ToString() }
            };

            return _jwtService.GenerateToken(claims, TimeSpan.FromMinutes(10));
        }
    }
}
