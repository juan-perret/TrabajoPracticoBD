using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Crud
{
    public class ClientesBD
    {
        private string connectionString = "Server= localhost; Database= Clientes; Integrated Security=True";
        // Utilizar el connectionString dependiendo de la authetication de la base de datos, yo utilice esa
        // porque la authetication de la base de dato mia era Windows authetication
        public bool Conection()
        {
            try
            {
                SqlConnection coneccion = new SqlConnection(connectionString);
                coneccion.Open();
            }
            catch
            {
                return false;
            }
            return true;
        }
        //Verifica si esta conectado

        public List<Cliente> Get()
        {
            List<Cliente> listaClientes = new List<Cliente>();
            string querry = "select Id,Nombre,Tipo_Usuario from Clientes";
            //agregar todas las propiedades de la tabla en el querry
            using(SqlConnection conection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(querry, conection);
                try
                {
                    conection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Cliente cliente = new Cliente();
                        cliente.Id= reader.GetInt32(0);
                        cliente.Nombre = reader.GetString(1);
                        cliente.Tipo_Usuario = reader.GetInt32(2);
                        listaClientes.Add(cliente);
                        //agregar todas las propiedades de la tabla y los numeros que van despues
                        //es el orden de posicionamiento en la querry
                    }
                    reader.Close();

                    conection.Close();

                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error en la coneccion a la BD "+ ex.Message );
                    
                }
            }
            return listaClientes;
        }
        public Cliente Get(int id)
        {
            string querry = "select Id,Nombre,Tipo_Usuario from Clientes"+
                " where id=@id";

            using (SqlConnection conection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(querry, conection);
                command.Parameters.AddWithValue("@id", id);
                try
                {
                    conection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    
                    Cliente cliente = new Cliente();
                    cliente.Id = reader.GetInt32(0);
                    cliente.Nombre = reader.GetString(1);
                    cliente.Tipo_Usuario = reader.GetInt32(2);

                    reader.Close();
                    conection.Close();
                    return cliente;

                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error en la coneccion a la BD " + ex.Message);

                }
            }
        }
        //Este metodo devuelve un cliente a traves de la id, sirve para la modificacion de cliente
        public void Add(string nombre, int tipo_Usuario)
        {
            string querry = "insert into Clientes(Nombre, Tipo_usuario) values"+
                "(@name, @tipo_usuario)";
            // van los elementos a agregar a la tabla, el id no se pone porque es incremental
            using (SqlConnection conection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(querry, conection);
                command.Parameters.AddWithValue("@name", nombre);
                command.Parameters.AddWithValue("@tipo_usuario", tipo_Usuario);
                try
                {
                    conection.Open();
                    command.ExecuteNonQuery();

                    conection.Close();

                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error en la coneccion a la BD " + ex.Message);

                }
            }
        }
        //Este metodo sirve para Agregar nuevos clientes
        public void Update(string nombre, int tipo_Usuario, int id) 
        {
            string querry = "update Clientes set Nombre=@Nombre, tipo_usuario=@Tipo_Usuario" +
                " where id=@id";
            //antes del where tiene que haber un espacio sino te tira error
            using (SqlConnection conection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(querry, conection);
                command.Parameters.AddWithValue("@nombre", nombre);
                command.Parameters.AddWithValue("@tipo_usuario", tipo_Usuario);
                command.Parameters.AddWithValue("@id", id);
                try
                {
                    conection.Open();
                    command.ExecuteNonQuery();

                    conection.Close();

                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error en la coneccion a la BD " + ex.Message);

                }
            }
        }
        //Este metodo para modificar el cliente
        public void Eliminar(int id)
        {
            string querry = "delete from clientes " +
                " where id=@id";
            using (SqlConnection conection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(querry, conection);
                command.Parameters.AddWithValue("@id", id);
                try
                {
                    conection.Open();
                    command.ExecuteNonQuery();

                    conection.Close();

                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error en la coneccion a la BD " + ex.Message);

                }
            }
        }
        //Este metodo sirve para eliminar cliente
    }
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Tipo_Usuario { get; set; }
    }
    //Cree directamente la clase Cliente aca si quieren agregar las propiedades de la tabla a mostrar,
    //las deberian agregar aca
}
