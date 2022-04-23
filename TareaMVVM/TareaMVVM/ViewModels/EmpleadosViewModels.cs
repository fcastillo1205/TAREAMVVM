using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TareaMVVM.Views;
using Xamarin.Forms;

namespace TareaMVVM.ViewModels
{
    public class EmpleadosViewModels : BaseViewModel
    {

        private int _id;
        private string _nombre;
        private string _apellido;
        private string _edad;
        private string _direccion;
        private string _puesto;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; OnPropertyChanged(); }
        }

        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; OnPropertyChanged(); }
        }

        public string Edad
        {
            get { return _edad; }
            set { _edad = value; OnPropertyChanged(); }
        }

        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; OnPropertyChanged(); }
        }

        public string Puesto
        {
            get { return _puesto; }
            set { _puesto = value; OnPropertyChanged(); }
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        //lista de comandos a utilizar en pantalla
        public ICommand CommandListEmple { get; set; }
        public ICommand CommandSaveEmple { get; set; }
        public ICommand CommandUpdateEmple { get; set; }
        public ICommand CommandDeleteEmple { get; set; }

        
        //acciones ICommand 
        public async void listEmple()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ListaEmpleados());
        }

        public async void updateEmple()
        {
            if (String.IsNullOrEmpty(Nombre))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe Completar el campo nombre", "Salir");
            }else if (String.IsNullOrEmpty(Apellido))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe Completar el campo apellido", "Salir");
            }
            else if (String.IsNullOrEmpty(Edad))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe Completar el campo edad", "Salir");
            }
            else if (String.IsNullOrEmpty(Direccion))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe Completar el campo direccion", "Salir");
            }
            else if (String.IsNullOrEmpty(Puesto))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe Completar el campo puesto", "Salir");
            }
            else
            {
                var emp = new Models.Empleados
                {
                    id = ID,
                    nombre = Nombre,
                    apellido = Apellido,
                    edad = Edad,
                    direccion = Direccion,
                    puesto = Puesto
                };

                var resultado = await App.BaseDatos.GrabarEmpleado(emp);

                if (resultado == 1)
                {
                    await Application.Current.MainPage.DisplayAlert("Advertencia", "Empleado Modificado", "Salir");

                    await Application.Current.MainPage.Navigation.PushAsync(new ListaEmpleados());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Advertencia", "No se pudo Modificar el Empleado", "Salir");
                }
            }           
        }

        public async void deleteEmple()
        {
            var emp = new Models.Empleados
            {
                id = ID,
                nombre = Nombre,
                apellido = Apellido,
                edad = Edad,
                direccion = Direccion,
                puesto = Puesto
            };

            var resultado = await App.BaseDatos.EliminarEmpleado(emp);

            if (resultado == 1)
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "Empleado Eliminado", "Salir");

                await Application.Current.MainPage.Navigation.PushAsync(new ListaEmpleados());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "No se pudo eliminar el empleado", "Salir");
            }


        }

        public async void saveEmple()
        {
            if (String.IsNullOrEmpty(Nombre))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe Completar el campo nombre", "Salir");
            }
            else if (String.IsNullOrEmpty(Apellido))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe Completar el campo apellido", "Salir");
            }
            else if (String.IsNullOrEmpty(Edad))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe Completar el campo edad", "Salir");
            }
            else if (String.IsNullOrEmpty(Direccion))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe Completar el campo direccion", "Salir");
            }
            else if (String.IsNullOrEmpty(Puesto))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe Completar el campo puesto", "Salir");
            }
            else
            {
                var emp = new Models.Empleados
                {
                    nombre = Nombre,
                    apellido = Apellido,
                    edad = Edad,
                    direccion = Direccion,
                    puesto = Puesto
                };

                var resultado = await App.BaseDatos.GrabarEmpleado(emp);

                if (resultado == 1)
                {
                    await Application.Current.MainPage.DisplayAlert("Mensaje", "Empleado guardado", "Cerrar");
                    Nombre = String.Empty;
                    Edad = String.Empty;
                    Apellido = String.Empty;
                    Direccion = String.Empty;
                    Puesto = String.Empty;

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No se Pudo Guardar el Empleado", "Cerrar");

                }
            }
        }

        public EmpleadosViewModels()
        {
            CommandListEmple = new Command(() => listEmple());
            CommandSaveEmple = new Command(() => saveEmple());
            CommandUpdateEmple = new Command(() => updateEmple());
            CommandDeleteEmple = new Command(() => deleteEmple());
        }

    }
}
