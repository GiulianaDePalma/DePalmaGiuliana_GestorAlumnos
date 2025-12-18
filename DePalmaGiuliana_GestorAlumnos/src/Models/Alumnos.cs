using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DePalmaGiuliana_GestorAlumnos.src.Models
{
    // Clase que representa a un alumno del sistema
    public class Alumno
    {
        // Identificador único del alumno
        public string Legajo { get; set; }

        // Apellido del alumno
        public string Apellido { get; set; }

        // Nombres del alumno
        public string Nombres { get; set; }

        // Número de documento del alumno
        public string NumeroDocumento { get; set; }

        // Dirección de correo electrónico del alumno
        public string Email { get; set; }

        // Número de teléfono del alumno
        public string Telefono { get; set; }

        // Sobrescribe el método ToString para representar al alumno
        // como una cadena de texto con los campos separados por '|'
        public override string ToString()
        {
            return $"{Legajo}|{Apellido}|{Nombres}|{NumeroDocumento}|{Email}|{Telefono}";
        }
    }
}
