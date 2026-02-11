using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MasterDetailMySqlTemplate.Models;
using MasterDetailMySqlTemplate.Persistence;

namespace MasterDetailMySqlTemplate.Controllers
{
    public class MasterDetailController : INotifyPropertyChanged
    {
        public ObservableCollection<Alumno> Alumnos { get; set; }

        private Alumno _selectedAlumno;
        public Alumno SelectedAlumno
        {
            get { return _selectedAlumno; }
            set
            {
                _selectedAlumno = value;
                OnPropertyChanged();
            }
        }

        public MasterDetailController()
        {
            var listaDesdeBD = AlumnoPersistence.leerAlumnos();


            Alumnos = new ObservableCollection<Alumno>(listaDesdeBD);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}