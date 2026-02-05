using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MiniHito.domain;
using MiniHito.persistence;
using MiniHito.services; // Asegúrate de haber creado el archivo CalendarificService.cs en la carpeta services

namespace MiniHito
{
    public partial class MainWindow : Window
    {
        // ================= COLECCIONES DE DATOS =================
        ObservableCollection<Alumno> lsPersonas;

        ObservableCollection<Grupo> listaGruposOC;
        ObservableCollection<Alumno> alumnosSinGrupoOC;
        ObservableCollection<Alumno> alumnosEnGrupoOC;

        ObservableCollection<Empresa> lsEmpresa;

        ObservableCollection<Reto> lsReto;

        // Variable para controlar qué grupo estamos editando
        Grupo grupoEnEdicion = null;

        public MainWindow()
        {
            InitializeComponent();

            // Inicializar las colecciones
            lsPersonas = new ObservableCollection<Alumno>();
            lsEmpresa = new ObservableCollection<Empresa>();
            lsReto = new ObservableCollection<Reto>();

            // Cargar datos iniciales de todas las pestañas
            CargarPersonas();
            CargarPestanaGrupos();
            cargarEmpresa();
            cargarReto();
        }

        // Evento genérico para refrescar datos al cambiar de pestaña
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                var tab = (sender as TabControl).SelectedItem as TabItem;
                if (tab != null)
                {
                    if (tab.Header.ToString() == "ALUMNADO") CargarPersonas();
                    else if (tab.Header.ToString() == "GRUPOS") CargarPestanaGrupos();
                    else if (tab.Header.ToString() == "EMPRESAS") cargarEmpresa();
                    else if (tab.Header.ToString() == "RETOS") cargarReto();
                }
            }
        }

        // =======================================================
        // LÓGICA DE ALUMNADO (Pestaña 1)
        // =======================================================
        private void CargarPersonas()
        {
            lsPersonas.Clear();
            var personas = AlumnoPersistence.leerPersonas();
            foreach (var p in personas) lsPersonas.Add(p);
            if (dataGridPersonas != null) dataGridPersonas.ItemsSource = lsPersonas;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || cmbCurso.SelectedItem == null) return;
            try
            {
                ComboBoxItem selectedItem = cmbCurso.SelectedItem as ComboBoxItem;
                Alumno a = new Alumno(txtNombre.Text, txtApellido.Text, int.Parse(selectedItem.Tag.ToString()));
                a.insertar();
                CargarPersonas();
                LimpiarCamposAlumno();
                MessageBox.Show("Alumno agregado");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Alumno a = dataGridPersonas.SelectedItem as Alumno;
            if (a == null) return;
            try
            {
                ComboBoxItem selectedItem = cmbCurso.SelectedItem as ComboBoxItem;
                a.Nombre = txtNombre.Text;
                a.Apellidos = txtApellido.Text;
                a.Especialidad = int.Parse(selectedItem.Tag.ToString());
                a.actualizar();
                dataGridPersonas.Items.Refresh();
                MessageBox.Show("Alumno modificado");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Alumno a = dataGridPersonas.SelectedItem as Alumno;
            if (a != null && MessageBox.Show("¿Eliminar?", "Confirmar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                a.eliminar();
                lsPersonas.Remove(a);
                LimpiarCamposAlumno();
            }
        }

        private void dataGridPersonas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Alumno a = dataGridPersonas.SelectedItem as Alumno;
            if (a != null)
            {
                txtNombre.Text = a.Nombre;
                txtApellido.Text = a.Apellidos;
                foreach (ComboBoxItem item in cmbCurso.Items)
                    if (item.Tag != null && item.Tag.ToString() == a.Especialidad.ToString()) { cmbCurso.SelectedItem = item; break; }
                btnModificar.IsEnabled = true;
            }
            else
            {
                LimpiarCamposAlumno();
            }
        }

        private void LimpiarCamposAlumno()
        {
            txtNombre.Text = ""; txtApellido.Text = ""; cmbCurso.SelectedItem = null;
            btnModificar.IsEnabled = false; dataGridPersonas.SelectedItem = null;
        }

        // =======================================================
        // LÓGICA DE GRUPOS (Pestaña 2)
        // =======================================================
        private void CargarPestanaGrupos()
        {
            listaGruposOC = new ObservableCollection<Grupo>(GrupoPersistence.LeerGrupos());
            if (lstGrupos != null) lstGrupos.ItemsSource = listaGruposOC;
            LimpiarModoCreacionGrupo();
        }

        private void LimpiarModoCreacionGrupo()
        {
            grupoEnEdicion = null;
            if (lstGrupos != null) lstGrupos.SelectedItem = null;
            if (txtNombreGrupo != null) txtNombreGrupo.Text = "";

            var todos = AlumnoPersistence.leerPersonas();
            // Izquierda: Alumnos sin grupo (Grupo == 0)
            alumnosSinGrupoOC = new ObservableCollection<Alumno>(todos.Where(a => a.Grupo == 0));
            if (lstAlumnosSinGrupo != null) lstAlumnosSinGrupo.ItemsSource = alumnosSinGrupoOC;

            // Derecha: Alumnos del grupo seleccionado (ahora ninguno)
            alumnosEnGrupoOC = new ObservableCollection<Alumno>();
            if (lstAlumnosEnGrupo != null) lstAlumnosEnGrupo.ItemsSource = alumnosEnGrupoOC;
        }

        private void lstGrupos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstGrupos.SelectedItem == null) return;
            grupoEnEdicion = lstGrupos.SelectedItem as Grupo;
            txtNombreGrupo.Text = grupoEnEdicion.Nombre;

            var todos = AlumnoPersistence.leerPersonas();
            // Derecha: Cargamos los alumnos de ESTE grupo
            alumnosEnGrupoOC = new ObservableCollection<Alumno>(todos.Where(a => a.Grupo == grupoEnEdicion.Id));
            lstAlumnosEnGrupo.ItemsSource = alumnosEnGrupoOC;

            // Izquierda: Siempre mostramos los que no tienen grupo
            alumnosSinGrupoOC = new ObservableCollection<Alumno>(todos.Where(a => a.Grupo == 0));
            lstAlumnosSinGrupo.ItemsSource = alumnosSinGrupoOC;
        }

        private void btnGuardarGrupo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreGrupo.Text)) return;

            if (grupoEnEdicion == null) // CREAR
            {
                Grupo nuevo = new Grupo(txtNombreGrupo.Text);
                nuevo.Insertar();
                // Recuperar ID para asignar alumnos
                Grupo recuperado = GrupoPersistence.ObtenerGrupoPorNombre(txtNombreGrupo.Text);
                if (recuperado != null)
                {
                    foreach (var alu in alumnosEnGrupoOC) { alu.Grupo = recuperado.Id; alu.actualizar(); }
                }
                MessageBox.Show("Grupo Creado");
            }
            else // MODIFICAR
            {
                grupoEnEdicion.Nombre = txtNombreGrupo.Text;
                grupoEnEdicion.Actualizar();
                MessageBox.Show("Grupo Actualizado");
            }
            CargarPestanaGrupos();
        }

        private void btnEliminarGrupo_Click(object sender, RoutedEventArgs e)
        {
            if (grupoEnEdicion != null)
            {
                if (MessageBox.Show("Se eliminará el grupo y los alumnos quedarán libres. ¿Seguro?", "Borrar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    grupoEnEdicion.Eliminar();
                    CargarPestanaGrupos();
                }
            }
        }

        private void btnLimpiarGrupo_Click(object sender, RoutedEventArgs e) => LimpiarModoCreacionGrupo();

        private void btnMoverDerecha_Click(object sender, RoutedEventArgs e)
        {
            var alu = lstAlumnosSinGrupo.SelectedItem as Alumno;
            if (alu != null)
            {
                alumnosSinGrupoOC.Remove(alu);
                alumnosEnGrupoOC.Add(alu);
                // Si estamos editando un grupo real, guardamos el cambio en BBDD ya
                if (grupoEnEdicion != null) { alu.Grupo = grupoEnEdicion.Id; alu.actualizar(); }
            }
        }

        private void btnMoverIzquierda_Click(object sender, RoutedEventArgs e)
        {
            var alu = lstAlumnosEnGrupo.SelectedItem as Alumno;
            if (alu != null)
            {
                alumnosEnGrupoOC.Remove(alu);
                alumnosSinGrupoOC.Add(alu);
                // Liberar alumno
                alu.Grupo = 0; alu.actualizar();
            }
        }

        // =======================================================
        // LÓGICA DE EMPRESAS (Pestaña 3 - Maestro/Detalle)
        // =======================================================
        private void cargarEmpresa()
        {
            lsEmpresa.Clear();
            var empresas = EmpresaPersistence.LeerEmpresas();
            foreach (var p in empresas) lsEmpresa.Add(p);
            if (dataGridEmpresa != null) dataGridEmpresa.ItemsSource = lsEmpresa;
        }

        private void dataGridEmpresa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // El Binding en XAML hace todo el trabajo de mostrar los datos en los TextBox
        }

        private void btnAgregarEmpresa_Click(object sender, RoutedEventArgs e)
        {
            dataGridEmpresa.SelectedItem = null; // Limpiar selección
            // Crear nueva empresa con los datos escritos
            Empresa nueva = new Empresa(
                txtRazonSocial.Text, txtDireccion.Text, txtCiudad.Text, txtTelefono.Text, txtCorreo.Text
            );
            nueva.insertar();
            cargarEmpresa();
            MessageBox.Show("Empresa creada");
        }

        private void btnModificarEmpresa_Click(object sender, RoutedEventArgs e)
        {
            Empresa em = dataGridEmpresa.SelectedItem as Empresa;
            if (em != null)
            {
                // Al usar Binding TwoWay, el objeto 'em' ya tiene los cambios de los TextBox
                em.actualizar();
                dataGridEmpresa.Items.Refresh();
                MessageBox.Show("Empresa actualizada");
            }
        }

        private void btnEliminarEmpresa_Click(object sender, RoutedEventArgs e)
        {
            Empresa em = dataGridEmpresa.SelectedItem as Empresa;
            if (em != null)
            {
                em.eliminar();
                lsEmpresa.Remove(em);
            }
        }

        // =======================================================
        // LÓGICA DE RETOS (Pestaña 4 - API y Validación)
        // =======================================================
        private void cargarReto()
        {
            lsReto.Clear();
            var retos = RetoPersistence.LeerRetos();
            foreach (var r in retos) lsReto.Add(r);
            if (dataGridReto != null) dataGridReto.ItemsSource = lsReto;
        }

        // Evento clave del examen: Validar API
        private async void dpFechaInicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpFechaInicio.SelectedDate.HasValue)
            {
                lblInfoFestivo.Text = "Comprobando festivos...";
                lblInfoFestivo.Foreground = System.Windows.Media.Brushes.Orange;

                // Llamada a tu servicio API
                bool esFestivo = await CalendarificService.EsFestivo(dpFechaInicio.SelectedDate.Value);

                if (esFestivo)
                {
                    lblInfoFestivo.Text = "¡ERROR! La fecha seleccionada es un día FESTIVO NACIONAL.";
                    lblInfoFestivo.Foreground = System.Windows.Media.Brushes.Red;
                    btnAgregarReto.IsEnabled = false; // Bloquear botón
                }
                else
                {
                    lblInfoFestivo.Text = "Fecha válida (Día laborable).";
                    lblInfoFestivo.Foreground = System.Windows.Media.Brushes.Green;
                    btnAgregarReto.IsEnabled = true; // Permitir guardar
                }
            }
        }

        private void btnAgregarReto_Click(object sender, RoutedEventArgs e)
        {
            if (dpFechaInicio.SelectedDate == null || string.IsNullOrWhiteSpace(txtRetoNombre.Text))
            {
                MessageBox.Show("Faltan datos (Nombre o Fecha)"); return;
            }
            Reto r = new Reto(
                txtRetoNombre.Text,
                txtRetoDescripcion.Text,
                dpFechaInicio.SelectedDate.Value,
                chkRetoActivo.IsChecked == true
            );
            r.insertar();
            cargarReto();
            MessageBox.Show("Reto agregado");
        }

        private void btnModificarReto_Click(object sender, RoutedEventArgs e)
        {
            Reto r = dataGridReto.SelectedItem as Reto;
            if (r != null)
            {
                r.Nombre = txtRetoNombre.Text;
                r.Descripcion = txtRetoDescripcion.Text;
                r.FechaInicio = dpFechaInicio.SelectedDate.Value;
                r.Activo = chkRetoActivo.IsChecked == true;
                r.actualizar();
                dataGridReto.Items.Refresh();
                MessageBox.Show("Reto modificado");
            }
        }

        private void btnEliminarReto_Click(object sender, RoutedEventArgs e)
        {
            Reto r = dataGridReto.SelectedItem as Reto;
            if (r != null)
            {
                r.eliminar();
                lsReto.Remove(r);
            }
        }

        private void dataGridReto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Reto r = dataGridReto.SelectedItem as Reto;
            if (r != null)
            {
                txtRetoNombre.Text = r.Nombre;
                txtRetoDescripcion.Text = r.Descripcion;
                dpFechaInicio.SelectedDate = r.FechaInicio;
                chkRetoActivo.IsChecked = r.Activo;
            }
        }
    }
}