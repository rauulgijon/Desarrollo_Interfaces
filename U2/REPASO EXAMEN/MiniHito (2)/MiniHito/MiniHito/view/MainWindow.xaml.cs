using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; // Necesario para las listas
using System.Linq; // Necesario para los filtros (Where)
using System.Windows;
using System.Windows.Controls;
using MiniHito.persistence;
using MiniHito.domain;

namespace MiniHito
{
    public partial class MainWindow : Window
    {
        // ---------------------------------------------------------
        // VARIABLES GLOBALES
        // ---------------------------------------------------------

        // Variables Pestaña 1 (Alumnado)
        ObservableCollection<Alumno> lsPersonas;
        Alumno alumno;

        // Variables Pestaña 2 (Grupos)
        ObservableCollection<Grupo> listaGruposOC;
        ObservableCollection<Alumno> alumnosSinGrupoOC;
        ObservableCollection<Alumno> alumnosEnGrupoOC;

        // ---------------------------------------------------------
        // INICIO
        // ---------------------------------------------------------
        public MainWindow()
        {
            InitializeComponent();

            // Inicializar pestaña 1
            lsPersonas = new ObservableCollection<Alumno>();
            alumno = new Alumno();
            cargarPersonas();
        }

        // ============================================================================
        // SECCIÓN 1: LÓGICA DE ALUMNADO (Tu código original restaurado)
        // ============================================================================

        private void cargarPersonas()
        {
            lsPersonas.Clear();
            var personas = AlumnoPersistence.leerPersonas();
            foreach (var p in personas)
            {
                lsPersonas.Add(p);
            }
            // Vinculamos el DataGrid de la pestaña 1
            if (dataGridPersonas != null)
                dataGridPersonas.ItemsSource = lsPersonas;
        }

        public void start()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            cmbCurso.SelectedItem = null;
            btnModificar.IsEnabled = false;
            dataGridPersonas.SelectedItem = null;
        }

        private void dataGridPersonas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Alumno a = dataGridPersonas.SelectedItem as Alumno;
            if (a != null)
            {
                txtNombre.Text = a.Nombre;
                txtApellido.Text = a.Apellidos;
                foreach (ComboBoxItem item in cmbCurso.Items)
                {
                    if (item.Tag != null && item.Tag.ToString() == a.Especialidad.ToString())
                    {
                        cmbCurso.SelectedItem = item;
                        break;
                    }
                }
                btnModificar.IsEnabled = true;
            }
            else
            {
                btnModificar.IsEnabled = false;
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Alumno a = dataGridPersonas.SelectedItem as Alumno;
            if (a != null)
            {
                if (MessageBox.Show("¿Estás seguro de eliminar este alumno?", "Confirmar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        a.eliminar();
                        lsPersonas.Remove(a);
                        start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione una persona para eliminar");
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text) || cmbCurso.SelectedItem == null)
            {
                MessageBox.Show("Complete todos los campos");
                return;
            }

            ComboBoxItem selectedItem = cmbCurso.SelectedItem as ComboBoxItem;
            int especialidad = int.Parse(selectedItem.Tag.ToString());

            Alumno existente = lsPersonas.FirstOrDefault(a => a.Nombre == txtNombre.Text && a.Apellidos == txtApellido.Text);

            if (existente != null)
            {
                MessageBox.Show("La persona ya existe");
            }
            else
            {
                try
                {
                    Alumno psnew = new Alumno(txtNombre.Text, txtApellido.Text, especialidad);
                    psnew.insertar();
                    cargarPersonas(); // Recargar para obtener ID
                    start();
                    MessageBox.Show("Agregado correctamente");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Alumno a = dataGridPersonas.SelectedItem as Alumno;
            if (a == null) return;

            if (string.IsNullOrWhiteSpace(txtNombre.Text) || cmbCurso.SelectedItem == null)
            {
                MessageBox.Show("Datos incompletos");
                return;
            }

            ComboBoxItem selectedItem = cmbCurso.SelectedItem as ComboBoxItem;
            int especialidad = int.Parse(selectedItem.Tag.ToString());

            try
            {
                a.Nombre = txtNombre.Text;
                a.Apellidos = txtApellido.Text;
                a.Especialidad = especialidad;
                a.actualizar();

                dataGridPersonas.Items.Refresh();
                MessageBox.Show("Modificado correctamente");
                start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        // ============================================================================
        // SECCIÓN 2: LÓGICA DE GRUPOS (El código nuevo)
        // ============================================================================

        private void CargarPestanaGrupos()
        {
            // 1. Cargar lista de Grupos
            listaGruposOC = new ObservableCollection<Grupo>(GrupoPersistence.LeerGrupos());
            lstGrupos.ItemsSource = listaGruposOC;

            // 2. Cargar Alumnos
            var todos = AlumnoPersistence.leerPersonas();

            // Izquierda: Alumnos SIN grupo (Grupo == 0)
            alumnosSinGrupoOC = new ObservableCollection<Alumno>(todos.Where(a => a.Grupo == 0));
            lstAlumnosSinGrupo.ItemsSource = alumnosSinGrupoOC;

            // Derecha: Vacía al principio
            alumnosEnGrupoOC = new ObservableCollection<Alumno>();
            lstAlumnosEnGrupo.ItemsSource = alumnosEnGrupoOC;
        }

        private void lstGrupos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Grupo g = lstGrupos.SelectedItem as Grupo;
            if (g != null)
            {
                txtNombreGrupo.Text = g.Nombre;

                // Cargar alumnos de ESTE grupo
                var todos = AlumnoPersistence.leerPersonas();
                var delGrupo = todos.Where(a => a.Grupo == g.Id);

                alumnosEnGrupoOC.Clear();
                foreach (var alu in delGrupo) alumnosEnGrupoOC.Add(alu);

                // Refrescar izquierda por si acaso
                var sinGrupo = todos.Where(a => a.Grupo == 0);
                alumnosSinGrupoOC.Clear();
                foreach (var alu in sinGrupo) alumnosSinGrupoOC.Add(alu);
            }
        }

        private void btnGuardarGrupo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreGrupo.Text)) return;

            Grupo g = lstGrupos.SelectedItem as Grupo;
            if (g == null)
            {
                // NUEVO
                Grupo nuevo = new Grupo(txtNombreGrupo.Text);
                nuevo.Insertar();
                MessageBox.Show("Grupo creado");
            }
            else
            {
                // EDITAR
                g.Nombre = txtNombreGrupo.Text;
                g.Actualizar();
                MessageBox.Show("Grupo modificado");
            }
            CargarPestanaGrupos();
            LimpiarSeleccionGrupo();
        }

        private void btnEliminarGrupo_Click(object sender, RoutedEventArgs e)
        {
            Grupo g = lstGrupos.SelectedItem as Grupo;
            if (g != null)
            {
                if (MessageBox.Show("¿Eliminar grupo? Los alumnos quedarán libres.", "Confirmar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    g.Eliminar();
                    CargarPestanaGrupos();
                    LimpiarSeleccionGrupo();
                }
            }
        }

        private void btnLimpiarGrupo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarSeleccionGrupo();
        }

        private void LimpiarSeleccionGrupo()
        {
            lstGrupos.SelectedItem = null;
            txtNombreGrupo.Text = "";
            if (alumnosEnGrupoOC != null) alumnosEnGrupoOC.Clear();
        }

        private void btnMoverDerecha_Click(object sender, RoutedEventArgs e)
        {
            var alu = lstAlumnosSinGrupo.SelectedItem as Alumno;
            var grup = lstGrupos.SelectedItem as Grupo;

            if (alu != null && grup != null)
            {
                alu.Grupo = grup.Id;
                alu.actualizar(); // Guardar en BD
                alumnosSinGrupoOC.Remove(alu);
                alumnosEnGrupoOC.Add(alu);
            }
            else
            {
                MessageBox.Show("Selecciona alumno y grupo");
            }
        }

        private void btnMoverIzquierda_Click(object sender, RoutedEventArgs e)
        {
            var alu = lstAlumnosEnGrupo.SelectedItem as Alumno;
            if (alu != null)
            {
                alu.Grupo = 0; // Sin grupo
                alu.actualizar(); // Guardar en BD
                alumnosEnGrupoOC.Remove(alu);
                alumnosSinGrupoOC.Add(alu);
            }
        }

        // ============================================================================
        // SECCIÓN 3: EVENTOS COMPARTIDOS (Cambio de Pestaña)
        // ============================================================================

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verificamos que el evento venga del TabControl y no de un control interno
            if (e.Source is TabControl)
            {
                var tab = (sender as TabControl).SelectedItem as TabItem;
                if (tab != null)
                {
                    // Si vamos a la pestaña ALUMNADO
                    if (tab.Header.ToString() == "ALUMNADO")
                    {
                        cargarPersonas();
                    }
                    // Si vamos a la pestaña GRUPOS
                    else if (tab.Header.ToString() == "GRUPOS")
                    {
                        CargarPestanaGrupos();
                    }
                }
            }
        }

        // Evento vacío requerido si lo tienes en el XAML antiguo
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}