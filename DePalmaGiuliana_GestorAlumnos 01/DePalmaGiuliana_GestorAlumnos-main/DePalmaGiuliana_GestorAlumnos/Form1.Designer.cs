namespace DePalmaGiuliana_GestorAlumnos
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtConsola = new TextBox();
            menuStrip1 = new MenuStrip();
            archivoToolStripMenuItem = new ToolStripMenuItem();
            crearArchivoToolStripMenuItem = new ToolStripMenuItem();
            leerArchivoToolStripMenuItem = new ToolStripMenuItem();
            modificarArchivoToolStripMenuItem = new ToolStripMenuItem();
            eliminarArchivoToolStripMenuItem = new ToolStripMenuItem();
            convertirFormatoToolStripMenuItem = new ToolStripMenuItem();
            generarReporteToolStripMenuItem = new ToolStripMenuItem();
            salirToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // txtConsola
            // 
            txtConsola.Dock = DockStyle.Fill;
            txtConsola.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtConsola.Location = new Point(0, 24);
            txtConsola.Multiline = true;
            txtConsola.Name = "txtConsola";
            txtConsola.ReadOnly = true;
            txtConsola.ScrollBars = ScrollBars.Vertical;
            txtConsola.Size = new Size(884, 537);
            txtConsola.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { archivoToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(884, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            archivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { crearArchivoToolStripMenuItem, leerArchivoToolStripMenuItem, modificarArchivoToolStripMenuItem, eliminarArchivoToolStripMenuItem, convertirFormatoToolStripMenuItem, generarReporteToolStripMenuItem, salirToolStripMenuItem });
            archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            archivoToolStripMenuItem.Size = new Size(60, 20);
            archivoToolStripMenuItem.Text = "Archivo";
            // 
            // crearArchivoToolStripMenuItem
            // 
            crearArchivoToolStripMenuItem.Name = "crearArchivoToolStripMenuItem";
            crearArchivoToolStripMenuItem.Size = new Size(180, 22);
            crearArchivoToolStripMenuItem.Text = "Crear Archivo";
            crearArchivoToolStripMenuItem.Click += crearArchivoToolStripMenuItem_Click;
            // 
            // leerArchivoToolStripMenuItem
            // 
            leerArchivoToolStripMenuItem.Name = "leerArchivoToolStripMenuItem";
            leerArchivoToolStripMenuItem.Size = new Size(180, 22);
            leerArchivoToolStripMenuItem.Text = "Leer Archivo";
            leerArchivoToolStripMenuItem.Click += leerArchivoToolStripMenuItem_Click;
            // 
            // modificarArchivoToolStripMenuItem
            // 
            modificarArchivoToolStripMenuItem.Name = "modificarArchivoToolStripMenuItem";
            modificarArchivoToolStripMenuItem.Size = new Size(180, 22);
            modificarArchivoToolStripMenuItem.Text = "Modificar Archivo";
            modificarArchivoToolStripMenuItem.Click += modificarArchivoToolStripMenuItem_Click;
            // 
            // eliminarArchivoToolStripMenuItem
            // 
            eliminarArchivoToolStripMenuItem.Name = "eliminarArchivoToolStripMenuItem";
            eliminarArchivoToolStripMenuItem.Size = new Size(180, 22);
            eliminarArchivoToolStripMenuItem.Text = "Eliminar Archivo";
            eliminarArchivoToolStripMenuItem.Click += eliminarArchivoToolStripMenuItem_Click;
            // 
            // convertirFormatoToolStripMenuItem
            // 
            convertirFormatoToolStripMenuItem.Name = "convertirFormatoToolStripMenuItem";
            convertirFormatoToolStripMenuItem.Size = new Size(180, 22);
            convertirFormatoToolStripMenuItem.Text = "Convertir Formato";
            convertirFormatoToolStripMenuItem.Click += convertirFormatoToolStripMenuItem_Click;
            // 
            // generarReporteToolStripMenuItem
            // 
            generarReporteToolStripMenuItem.Name = "generarReporteToolStripMenuItem";
            generarReporteToolStripMenuItem.Size = new Size(180, 22);
            generarReporteToolStripMenuItem.Text = "Generar Reporte";
            generarReporteToolStripMenuItem.Click += generarReporteToolStripMenuItem_Click;
            // 
            // salirToolStripMenuItem
            // 
            salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            salirToolStripMenuItem.Size = new Size(180, 22);
            salirToolStripMenuItem.Text = "Salir";
            salirToolStripMenuItem.Click += salirToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 561);
            Controls.Add(txtConsola);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Gestor de Alumnos";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtConsola;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem archivoToolStripMenuItem;
        private ToolStripMenuItem crearArchivoToolStripMenuItem;
        private ToolStripMenuItem leerArchivoToolStripMenuItem;
        private ToolStripMenuItem modificarArchivoToolStripMenuItem;
        private ToolStripMenuItem eliminarArchivoToolStripMenuItem;
        private ToolStripMenuItem convertirFormatoToolStripMenuItem;
        private ToolStripMenuItem generarReporteToolStripMenuItem;
        private ToolStripMenuItem salirToolStripMenuItem;
    }
}
