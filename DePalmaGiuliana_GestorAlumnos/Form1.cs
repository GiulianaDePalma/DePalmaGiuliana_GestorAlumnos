using DePalmaGiuliana_GestorAlumnos.src;
using DePalmaGiuliana_GestorAlumnos.src.Models;

namespace DePalmaGiuliana_GestorAlumnos
{
    // Formulario principal de la aplicación
    // Contiene la interfaz gráfica y el menú superior
    public partial class Form1 : Form
    {
        // Referencia al menú lógico del sistema
        // Se encarga de delegar las acciones según la opción elegida
        private Menu menu;

        // Constructor del formulario
        // Inicializa los componentes visuales y el menú lógico
        public Form1()
        {
            InitializeComponent();
            menu = new Menu(Imprimir);
        }

        // Método utilizado como callback para mostrar mensajes
        // en la consola (TextBox) del formulario
        private void Imprimir(string texto)
        {
            txtConsola.AppendText(texto + Environment.NewLine);
        }

        // Evento del menú: Crear archivo
        private void crearArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menu.EjecutarOpcion(1);
        }

        // Evento del menú: Leer archivo
        private void leerArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menu.EjecutarOpcion(2);
        }

        // Evento del menú: Modificar archivo
        private void modificarArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menu.EjecutarOpcion(3);
        }

        // Evento del menú: Eliminar archivo
        private void eliminarArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menu.EjecutarOpcion(4);
        }

        // Evento del menú: Convertir formato de archivo
        private void convertirFormatoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menu.EjecutarOpcion(5);
        }

        // Evento del menú: Generar reporte
        private void generarReporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menu.EjecutarOpcion(6);
        }

        // Evento del menú: Salir de la aplicación
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

