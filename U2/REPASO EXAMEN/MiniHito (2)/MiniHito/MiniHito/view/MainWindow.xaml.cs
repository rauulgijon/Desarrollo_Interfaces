using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MiniHito.persistence;
using MiniHito.domain;

namespace MiniHito
{
    public partial class MainWindow : Window
    {

        // Pestaña Alumnos
        ObservableCollection<Alumno> lsPersonas;
        Alumno alumno;

        // Pestaña Grupos
        ObservableCollection<Grupo> listaGruposOC;
        ObservableCollection<Alumno> alumnosSinGrupoOC;
        ObservableCollection<Alumno> alumnosEnGrupoOC;

        //Empresas
        Empresa empresa;
        ObservableCollection<Empresa> lsEmpresa;

        //Empresas
        Reto reto;
        ObservableCollection<Reto> lsReto;

        // Control de estado: ¿Estamos editando un grupo existente o creando uno nuevo?
        private Grupo grupoEnEdicion = null;

        public MainWindow()
        {
            InitializeComponent();

            // Cargar pestaña 1
            lsPersonas = new ObservableCollection<Alumno>();
            lsEmpresa = new ObservableCollection<Empresa>();
            alumno = new Alumno();
            empresa = new Empresa();
            reto = new Reto();
            lsReto = new ObservableCollection<Reto>();
            cargarPersonas();

            // Cargar pestaña 2 (Grupos) INICIALMENTE
            CargarPestanaGrupos();
        }

        // =========================================================
        // PESTAÑA 1: ALUMNADO 
        // =========================================================
        private void cargarPersonas()
        {
            lsPersonas.Clear();
            var personas = AlumnoPersistence.leerPersonas();
            foreach (var p in personas) lsPersonas.Add(p);
            if (dataGridPersonas != null) dataGridPersonas.ItemsSource = lsPersonas;
        }

        public void start()
        {
            txtNombre.Text = ""; txtApellido.Text = ""; cmbCurso.SelectedItem = null;
            btnModificar.IsEnabled = false; dataGridPersonas.SelectedItem = null;
            txtRazonSocial.Text = ""; txtDireccion.Text = ""; txtCiudad.Text = ""; txtTelefono.Text = ""; txtCorreo.Text = "";
            btnModificarEmpresa.IsEnabled = false; dataGridEmpresa.SelectedItem = null;
        }

        private void dataGridPersonas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Alumno a = dataGridPersonas.SelectedItem as Alumno;
            if (a != null)
            {
                txtNombre.Text = a.Nombre; txtApellido.Text = a.Apellidos;
                foreach (ComboBoxItem item in cmbCurso.Items)
                    if (item.Tag != null && item.Tag.ToString() == a.Especialidad.ToString()) { cmbCurso.SelectedItem = item; break; }
                btnModificar.IsEnabled = true;
            }
            else btnModificar.IsEnabled = false;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Alumno a = dataGridPersonas.SelectedItem as Alumno;
            if (a != null && MessageBox.Show("¿Eliminar alumno?", "Confirmar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try { a.eliminar(); lsPersonas.Remove(a); start(); } catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text) || cmbCurso.SelectedItem == null) return;
            try
            {
                ComboBoxItem selectedItem = cmbCurso.SelectedItem as ComboBoxItem;
                Alumno psnew = new Alumno(txtNombre.Text, txtApellido.Text, int.Parse(selectedItem.Tag.ToString()));
                psnew.insertar(); cargarPersonas(); start(); MessageBox.Show("Agregado");
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
                a.Nombre = txtNombre.Text; a.Apellidos = txtApellido.Text; a.Especialidad = int.Parse(selectedItem.Tag.ToString());
                a.actualizar(); dataGridPersonas.Items.Refresh(); start(); MessageBox.Show("Modificado");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }


        // =========================================================
        // PESTAÑA 2: GRUPOS
        // =========================================================

        private void CargarPestanaGrupos()
        {
            // 1. Cargar lista de grupos (Abajo)
            listaGruposOC = new ObservableCollection<Grupo>(GrupoPersistence.LeerGrupos());
            lstGrupos.ItemsSource = listaGruposOC;

            // 2. Resetear formulario para "Crear Nuevo"
            LimpiarModoCreacion();
        }

        // Prepara la pantalla para crear un grupo desde cero
        private void LimpiarModoCreacion()
        {
            grupoEnEdicion = null;
            lstGrupos.SelectedItem = null;
            txtNombreGrupo.Text = "";

            // Cargar TODOS los alumnos disponibles a la izquierda
            var todos = AlumnoPersistence.leerPersonas();
            alumnosSinGrupoOC = new ObservableCollection<Alumno>(todos.Where(a => a.Grupo == 0));
            lstAlumnosSinGrupo.ItemsSource = alumnosSinGrupoOC;

            // Lista derecha VACÍA (preparada para recibir gente nueva)
            alumnosEnGrupoOC = new ObservableCollection<Alumno>();
            lstAlumnosEnGrupo.ItemsSource = alumnosEnGrupoOC;
        }

        // Click en lista inferior: Cargar grupo para editar/eliminar
        private void lstGrupos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Si no hay nada seleccionado, salimos
            if (lstGrupos.SelectedItem == null) return;

            try
            {
                grupoEnEdicion = lstGrupos.SelectedItem as Grupo;
                txtNombreGrupo.Text = grupoEnEdicion.Nombre;

                // Intentamos cargar los alumnos. Si esto falla, que no rompa la selección.
                try
                {
                    var todos = AlumnoPersistence.leerPersonas();

                    // Derecha: Alumnos del grupo
                    alumnosEnGrupoOC = new ObservableCollection<Alumno>(todos.Where(a => a.Grupo == grupoEnEdicion.Id));
                    lstAlumnosEnGrupo.ItemsSource = alumnosEnGrupoOC;

                    // Izquierda: Disponibles
                    alumnosSinGrupoOC = new ObservableCollection<Alumno>(todos.Where(a => a.Grupo == 0));
                    lstAlumnosSinGrupo.ItemsSource = alumnosSinGrupoOC;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Se seleccionó el grupo, pero hubo un error cargando sus alumnos: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error general al seleccionar: " + ex.Message);
            }
        }

        // Botón >> (Mover a zona de espera derecha)
        private void btnMoverDerecha_Click(object sender, RoutedEventArgs e)
        {
            var alu = lstAlumnosSinGrupo.SelectedItem as Alumno;
            if (alu == null) return;

            // Movimiento Visual
            alumnosSinGrupoOC.Remove(alu);
            alumnosEnGrupoOC.Add(alu);

            // Si estamos EDITANDO un grupo que YA EXISTE, guardamos el cambio inmediatamente
            if (grupoEnEdicion != null)
            {
                alu.Grupo = grupoEnEdicion.Id;
                alu.actualizar();
            }
        }

        // Botón << (Sacar de la zona derecha)
        private void btnMoverIzquierda_Click(object sender, RoutedEventArgs e)
        {
            var alu = lstAlumnosEnGrupo.SelectedItem as Alumno;
            if (alu == null) return;

            // Movimiento Visual
            alumnosEnGrupoOC.Remove(alu);
            alumnosSinGrupoOC.Add(alu);

            // Si el alumno venía de un grupo real, lo liberamos en BD inmediatamente
            if (alu.Grupo != 0)
            {
                alu.Grupo = 0;
                alu.actualizar();
            }
        }

        // Botón AÑADIR / MODIFICAR
        private void btnGuardarGrupo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreGrupo.Text)) { MessageBox.Show("Pon un nombre"); return; }

            if (grupoEnEdicion == null)
            {
                // --- CREAR NUEVO ---
                Grupo nuevo = new Grupo(txtNombreGrupo.Text);
                nuevo.Insertar(); // 1. Crear grupo en BD

                // 2. Obtener el ID que le ha dado la BD
                Grupo recuperado = GrupoPersistence.ObtenerGrupoPorNombre(txtNombreGrupo.Text);

                if (recuperado != null)
                {
                    // 3. Asignar los alumnos de la lista derecha a este nuevo ID
                    foreach (var alu in alumnosEnGrupoOC)
                    {
                        alu.Grupo = recuperado.Id;
                        alu.actualizar();
                    }
                    MessageBox.Show("Grupo Creado y Alumnos Asignados");
                }
            }
            else
            {
                // --- MODIFICAR ---
                grupoEnEdicion.Nombre = txtNombreGrupo.Text;
                grupoEnEdicion.Actualizar();
                MessageBox.Show("Nombre actualizado");
            }

            CargarPestanaGrupos(); // Recargar todo
        }

        // Botón ELIMINAR (Seleccionando de la lista inferior)
        private void btnEliminarGrupo_Click(object sender, RoutedEventArgs e)
        {
            // 1. Intentamos coger el de la lista visual
            Grupo g = lstGrupos.SelectedItem as Grupo;

            // 2. Si no, intentamos coger el que está en edición
            if (g == null) g = grupoEnEdicion;

            if (g != null)
            {
                if (MessageBox.Show($"¿Eliminar el grupo '{g.Nombre}'?", "Confirmar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    g.Eliminar(); // Esto llama al método que usa 'idgrupo' (minúscula)
                    CargarPestanaGrupos(); // Refresca la lista

                    // Limpiamos la selección para evitar errores
                    LimpiarModoCreacion();
                    MessageBox.Show("Grupo Eliminado");
                }
            }
            else
            {
                MessageBox.Show("Por favor, haz clic en un grupo de la lista inferior para seleccionarlo primero.");
            }
        }

        // Botón LIMPIAR (Opcional, si tienes un botón 'Limpiar' para salir del modo edición)
        private void btnLimpiarGrupo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarModoCreacion();
        }

        // Gestión de pestañas
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                var tab = (sender as TabControl).SelectedItem as TabItem;
                if (tab != null)
                {
                    // Si el usuario hace clic en la pestaña GRUPOS, recargamos la lista desde cero
                    if (tab.Header.ToString() == "GRUPOS")
                    {
                        CargarPestanaGrupos();
                    }
                }
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { }

        // =========================================================
        // PESTAÑA 3: EMPRESAS
        // =========================================================
        private void cargarEmpresa()
        {
            lsEmpresa.Clear();
            var empresas = EmpresaPersistence.LeerEmpresas();
            foreach (var p in empresas) lsEmpresa.Add(p);
            if (dataGridEmpresa != null) dataGridEmpresa.ItemsSource = lsEmpresa;
        }

        private void btnEliminarEmpresa_Click(object sender, RoutedEventArgs e)
        {
            Empresa em = dataGridEmpresa.SelectedItem as Empresa;
            if (em != null && MessageBox.Show("¿Eliminar empresa?", "Confirmar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try { em.eliminar(); lsEmpresa.Remove(em); start(); } catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void btnAgregarEmpresa_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRazonSocial.Text) || string.IsNullOrWhiteSpace(txtDireccion.Text) || string.IsNullOrWhiteSpace(txtCiudad.Text) || string.IsNullOrWhiteSpace(txtTelefono.Text) || string.IsNullOrWhiteSpace(txtCorreo.Text)) return;
            try
            {
                Empresa psnew = new Empresa(txtRazonSocial.Text, txtDireccion.Text, txtCiudad.Text, txtTelefono.Text, txtCorreo.Text);
                psnew.insertar(); cargarEmpresa(); start(); MessageBox.Show("Agregado");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnModificarEmpresa_Click(object sender, RoutedEventArgs e)
        {
            Empresa em = dataGridEmpresa.SelectedItem as Empresa;
            if (em == null) return;
            try
            {
                em.RazonSocial = txtRazonSocial.Text; em.Ciudad = txtCiudad.Text; em.Telefono = txtTelefono.Text; em.Correo = txtCorreo.Text;
                em.actualizar(); dataGridEmpresa.Items.Refresh(); start(); MessageBox.Show("Modificado");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void dataGridEmpresa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Empresa em = dataGridEmpresa.SelectedItem as Empresa;
            if (em != null)
            {
                em.RazonSocial = txtRazonSocial.Text; em.Ciudad = txtCiudad.Text; em.Telefono = txtTelefono.Text; em.Correo = txtCorreo.Text;
                btnModificar.IsEnabled = true;
            }
            else btnModificar.IsEnabled = false;
        }

        // =========================================================
        // PESTAÑA 3: RETOS
        // =========================================================
        private void cargarReto()
        {
            lsReto.Clear();
            var retos = RetoPersistence.LeerRetos();
            foreach (var p in retos) lsReto.Add(p);
            if (dataGridReto != null) dataGridReto.ItemsSource = lsReto;
        }

        private void btnEliminarReto_Click(object sender, RoutedEventArgs e)
        {
            Reto r = dataGridReto.SelectedItem as Reto;
            if (r != null && MessageBox.Show("¿Eliminar empresa?", "Confirmar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try { r.eliminar(); lsReto.Remove(r); start(); } catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        //private void btnAgregarReto_Click(object sender, RoutedEventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(txtDescripcion.Text) || string.IsNullOrWhiteSpace(txtActivo.Text)) return;
        //    try
        //    {
        //        Reto psnew = new Reto(txtDescripcion.Text, txtActivo.bool);
        //        psnew.insertar(); cargarReto(); start(); MessageBox.Show("Agregado");
        //    }
        //    catch (Exception ex) { MessageBox.Show(ex.Message); }
        //}

        private void btnModificarReto_Click(object sender, RoutedEventArgs e)
        {
            Empresa em = dataGridEmpresa.SelectedItem as Empresa;
            if (em == null) return;
            try
            {
                em.RazonSocial = txtRazonSocial.Text; em.Ciudad = txtCiudad.Text; em.Telefono = txtTelefono.Text; em.Correo = txtCorreo.Text;
                em.actualizar(); dataGridEmpresa.Items.Refresh(); start(); MessageBox.Show("Modificado");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void dataGridReto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Empresa em = dataGridEmpresa.SelectedItem as Empresa;
            if (em != null)
            {
                em.RazonSocial = txtRazonSocial.Text; em.Ciudad = txtCiudad.Text; em.Telefono = txtTelefono.Text; em.Correo = txtCorreo.Text;
                btnModificar.IsEnabled = true;
            }
            else btnModificar.IsEnabled = false;
        }
    }
}
