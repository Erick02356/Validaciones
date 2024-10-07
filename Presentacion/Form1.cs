using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Text.RegularExpressions;

namespace Presentacion
{
    public partial class Usuario : Form
    {
        private readonly PropietarioService _PropietarioService;
      
        public Usuario()
        {
            InitializeComponent();
            _PropietarioService = new PropietarioService(); 

        }

        private void Usuario_Load(object sender, EventArgs e)
        {
            CargarPropietarios();
        }
        public bool CedulaDuplicada(string cedulaIngresada)
        {
            foreach (DataGridViewRow row in DgvPropietarios.Rows)
            {
                if (row.Cells["Cedula"].Value != null && row.Cells["Cedula"].Value.ToString() == cedulaIngresada)
                {
                    return true; // Si encuentra una cédula duplicada, devuelve true
                }
            }
            return false; // Si no encuentra ninguna cédula duplicada, devuelve false
        }
        private void BtnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                string cedulaIngresada = TxtCedula.Text;

                if (CedulaDuplicada(cedulaIngresada))
                {
                    MessageBox.Show("Ya existe un propietario con esa cédula.");
                    return; // Salir si la cédula está duplicada
                }
                if (ValidarFormulario())
                {

                    // Aquí se crea el objeto 'Propietario' usando los datos ingresados en el formulario
                    var propietario = new Propietario
                {
                    Cedula = int.Parse(TxtCedula.Text), // Convierte la cédula a número entero
                    Nombre1 = TxtNombre1.Text,          // Asigna el valor del campo de texto 'Nombre1'
                    Nombre2 = TxtSegundoNombre.Text,          // Asigna el valor del campo de texto 'Nombre2'
                    Apellido1 = TxtApellido1.Text,      // Asigna el valor del campo de texto 'Apellido1'
                    Apellido2 = TxtSegundoApellido.Text,      // Asigna el valor del campo de texto 'Apellido2'
                    Telefono = int.Parse(TxtTelefono.Text), // Convierte el teléfono a número entero
                    Email = TxtEmail.Text,              // Asigna el valor del campo de texto 'Email'
                    NombreUsuario = TxtUsuario.Text,    // Asigna el valor del campo de texto 'Usuario'
                    Clave = TxtClave.Text,              // Asigna el valor del campo de texto 'Clave'
                    Estado = TxtEstado.Text             // Asigna el valor del campo de texto 'Estado'
                };

                // Llamas al método 'Crear' que insertará el propietario en la base de datos
                Crear(propietario);

                // Muestra un mensaje indicando que la operación fue exitosa
                MessageBox.Show("Propietario creado correctamente.");
                }
            }
            catch (Exception ex)
            {
                // Muestra un mensaje de error en caso de que algo falle
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (DgvPropietarios.SelectedRows.Count > 0)
            {
                var propietario = new Propietario
                {
                    Id = Convert.ToInt32(DgvPropietarios.SelectedRows[0].Cells["Id"].Value),
                    Cedula = long.Parse(TxtCedula.Text),
                    Nombre1 = TxtNombre1.Text,
                    Nombre2 = TxtSegundoNombre.Text,
                    Apellido1 = TxtApellido1.Text,
                    Apellido2 = TxtSegundoApellido.Text,
                    Telefono = int.Parse(TxtTelefono.Text),
                    Email = TxtEmail.Text,
                    NombreUsuario = TxtUsuario.Text,
                    Clave = TxtClave.Text,
                    Estado = TxtEstado.Text
                };

                _PropietarioService.Editar(propietario);
                MessageBox.Show("Propietario editado correctamente.");
                CargarPropietarios();
            }
            else
            {
                MessageBox.Show("Seleccione un propietario para editar.");
            }
        }
   

        private void DgvPropietarios_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Obtener el índice de la fila seleccionada
            int index = e.RowIndex;

            // Verificar si hay una fila seleccionada
            if (index >= 0)
            {
                // Obtener los valores de la fila seleccionada y asignarlos a los TextBox
                TxtCedula.Text = DgvPropietarios.Rows[index].Cells["Cedula"].Value.ToString();
                TxtNombre1.Text = DgvPropietarios.Rows[index].Cells["Nombre1"].Value.ToString();
                TxtSegundoNombre.Text = DgvPropietarios.Rows[index].Cells["Nombre2"].Value.ToString();
                TxtApellido1.Text = DgvPropietarios.Rows[index].Cells["Apellido1"].Value.ToString();
                TxtSegundoApellido.Text = DgvPropietarios.Rows[index].Cells["Apellido2"].Value.ToString();
                TxtTelefono.Text = DgvPropietarios.Rows[index].Cells["Telefono"].Value.ToString();
                TxtEmail.Text = DgvPropietarios.Rows[index].Cells["Email"].Value.ToString();
                TxtUsuario.Text = DgvPropietarios.Rows[index].Cells["NombreUsuario"].Value.ToString();
                TxtClave.Text = DgvPropietarios.Rows[index].Cells["Clave"].Value.ToString();
                TxtEstado.Text = DgvPropietarios.Rows[index].Cells["Estado"].Value.ToString();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            if (DgvPropietarios.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(DgvPropietarios.SelectedRows[0].Cells["Id"].Value);

                var confirmResult = MessageBox.Show("¿Está seguro que desea eliminar este propietario?",
                                                     "Confirmar Eliminación",
                                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    _PropietarioService.Eliminar(id);
                    MessageBox.Show("Propietario eliminado correctamente.");
                    CargarPropietarios();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un propietario para eliminar.");
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            TxtCedula.Clear();
            TxtNombre1.Clear();
            TxtSegundoNombre.Clear();
            TxtApellido1.Clear();
            TxtSegundoApellido.Clear();
            TxtTelefono.Clear();
            TxtEmail.Clear();
            TxtUsuario.Clear();
            TxtClave.Clear();
            TxtEstado.Text = "(Seleccione)";
            TxtCedula.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("¿Desea salir del programa?", "salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (r == DialogResult.Yes)
            {
                this.Close();
            }
        }

        #region "Metodos Creacion"
        private void CargarPropietarios()
        {
            DgvPropietarios.DataSource = _PropietarioService.ObtenerTodos();
        }

        private void Crear(Propietario propietario)
        {
            _PropietarioService.Crear(propietario);
            CargarPropietarios();
        }

    


        #endregion
        #region Validaciones

        private bool ValidarFormulario()
        {
            // Validación de Cédula
            if (string.IsNullOrWhiteSpace(TxtCedula.Text))
            {
                MessageBox.Show("El campo cédula no puede estar vacío.");
                return false;
            }
            if (!long.TryParse(TxtCedula.Text, out _))
            {
                MessageBox.Show("La cédula debe ser un número.");
                return false;
            }
            if (TxtCedula.Text.Length != 10)
            {
                MessageBox.Show("La cédula debe tener 10 dígitos.");
                return false;
            }
            if (!Regex.IsMatch(TxtCedula.Text, @"^\d{10}$"))
            {
                throw new ArgumentOutOfRangeException("El código debe contener exactamente 10 dígitos.");
            }
   


            // Validación de Nombres
            if (string.IsNullOrWhiteSpace(TxtNombre1.Text))
            {
                MessageBox.Show("El primer nombre no puede estar vacío.");
                return false;
            }
            if (!Regex.IsMatch(TxtNombre1.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("El primer nombre solo debe contener letras.");
                return false;
            }

            if (!string.IsNullOrWhiteSpace(TxtSegundoNombre.Text) && !Regex.IsMatch(TxtSegundoNombre.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("El segundo nombre solo debe contener letras.");
                return false;
            }
            string cedulaIngresada = TxtCedula.Text;

           

            // Validación de Apellidos
            if (string.IsNullOrWhiteSpace(TxtApellido1.Text))
            {
                MessageBox.Show("El primer apellido no puede estar vacío.");
                return false;
            }
            if (!Regex.IsMatch(TxtApellido1.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("El primer apellido solo debe contener letras.");
                return false;
            }

            if (!string.IsNullOrWhiteSpace(TxtSegundoApellido.Text) && !Regex.IsMatch(TxtSegundoApellido.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("El segundo apellido solo debe contener letras.");
                return false;
            }

            // Validación de Teléfono
            if (string.IsNullOrWhiteSpace(TxtTelefono.Text))
            {
                MessageBox.Show("El campo teléfono no puede estar vacío.");
                return false;
            }
            if (!long.TryParse(TxtTelefono.Text, out _))
            {
                MessageBox.Show("El teléfono debe ser un número.");
                return false;
            }
            if (TxtTelefono.Text.Length < 7 || TxtTelefono.Text.Length > 10)
            {
                MessageBox.Show("El teléfono debe tener entre 7 y 10 dígitos.");
                return false;
            }


            // Validación de Email
            if (string.IsNullOrWhiteSpace(TxtEmail.Text))
            {
                MessageBox.Show("El campo email no puede estar vacío.");
                return false;
            }
            if (!Regex.IsMatch(TxtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("El email ingresado no tiene un formato válido.");
                return false;
            }

            // Validación de Nombre de Usuario
            if (string.IsNullOrWhiteSpace(TxtUsuario.Text))
            {
                MessageBox.Show("El nombre de usuario no puede estar vacío.");
                return false;
            }
            if (TxtUsuario.Text.Length < 5)
            {
                MessageBox.Show("El nombre de usuario debe tener al menos 5 caracteres.");
                return false;
            }

            // Validación de Clave
            if (string.IsNullOrWhiteSpace(TxtClave.Text))
            {
                MessageBox.Show("El campo clave no puede estar vacío.");
                return false;
            }
            if (TxtClave.Text.Length < 6)
            {
                MessageBox.Show("La clave debe tener al menos 6 caracteres.");
                return false;
            }
            if (!Regex.IsMatch(TxtClave.Text, @"^(?=.*[A-Za-z])(?=.*\d).{6,}$"))
            {
                MessageBox.Show("La clave debe tener al menos una letra y un número.");
                return false;
            }

            // Validación de Estado
            if (string.IsNullOrWhiteSpace(TxtEstado.Text) || TxtEstado.Text == "(Seleccione)")
            {
                MessageBox.Show("Debe seleccionar un estado válido.");
                return false;
            }

            // Si todas las validaciones pasan, retorna true
            return true;
        }


        #endregion

        private void TxtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo dígitos (números) y teclas de control (como retroceso)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Ignorar la tecla si no es válida
            }
        }

        private void TxtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo dígitos (números) y teclas de control (como retroceso)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Ignorar la tecla si no es válida
            }
        }
    }
}
