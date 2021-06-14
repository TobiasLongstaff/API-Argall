using API_Argall.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API_Argall.Context
{
    public class AppDbContext
    {

        private readonly string _connectionString;

        public AppDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");
        }

        public async Task<List<Argall_Bd>> GetAll()
        {
            using (SqlConnection sql = new SqlConnection("data source=DESKTOP-GOJT95K;initial catalog=Cuser_SA; user id=Tobias; password=2231; MultipleActiveResultSets=true"))
            {
                using (SqlCommand cmd = new SqlCommand("pallets_getactivos", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    var response = new List<Argall_Bd>();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue(reader));
                        }
                    }

                    return response;
                }
            }
        }

        public async Task<Argall_Bd> Login(Argall_Bd value)
        {
            using (SqlConnection sql = new SqlConnection("data source=DESKTOP-GOJT95K;initial catalog=Cuser_SA; user id=Tobias; password=2231; MultipleActiveResultSets=true"))
            {
                using (SqlCommand cmd2 = new SqlCommand("usuarios_getbyone", sql))
                {
                    string password = EncriptaLinea(value.password);
                    await sql.OpenAsync();
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.Add(new SqlParameter("@idusuario", value.idusuario));
                    cmd2.Parameters.Add(new SqlParameter("@password", password));
                    Argall_Bd response = null;

                    using (var reader = await cmd2.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValueLogin(reader);
                        }
                    }

                    return response;
                }
            }
        }

        public async Task <Argall_Bd> InsertAgregar(Argall_Bd value)
        {
            using (SqlConnection sql = new SqlConnection("data source=DESKTOP-GOJT95K;initial catalog=Cuser_SA; user id=Tobias; password=2231; MultipleActiveResultSets=true"))
            {
                using (SqlCommand cmd = new SqlCommand("pallets_save", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idbulto", value.idbulto));
                    cmd.Parameters.Add(new SqlParameter("@idpuesto", value.idpuesto));
                    cmd.Parameters.Add(new SqlParameter("@movimiento", value.movimiento));
                    Argall_Bd response = null;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValuePOST(reader);
                        }
                    }

                    return response;
                }
            }
        }

        public async Task <Argall_Bd> CerrarPallets(Argall_Bd value)
        {
            using (SqlConnection sql = new SqlConnection("data source=DESKTOP-GOJT95K;initial catalog=Cuser_SA; user id=Tobias; password=2231; MultipleActiveResultSets=true"))
            {
                using (SqlCommand cmd = new SqlCommand("pallets_close", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idpallet", value.idpallet));
                    cmd.Parameters.Add(new SqlParameter("@idpuesto", value.idpuesto));
                    cmd.Parameters.Add(new SqlParameter("@movimiento", value.movimiento));
                    Argall_Bd response = null;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValuePOST(reader);
                        }
                    }

                    return response;
                }
            }
        }

        public static string EncriptaLinea(string renglon)
        {
            string Salida = "";
            string Car;

            Salida = "";
            for (int i = 0; i < renglon.Length; i++)
            {

                Car = renglon.Substring(i, 1);
                if (!(char.ConvertToUtf32(Car, 0) == 9 || char.ConvertToUtf32(Car, 0) == 32))
                {
                    int numero = (char)255 - char.ConvertToUtf32(Car, 0);
                    Car = char.ConvertFromUtf32(numero).ToString();
                }
                Salida = Salida + Car;
            }
            return (Salida);
        }

        private Argall_Bd MapToValue(SqlDataReader reader)
        {
            return new Argall_Bd()
            {
                Pallet = reader["# Pallet"].ToString(),
                PLU = reader["PLU"].ToString(),
                Articulo = reader["Articulo"].ToString(),
                Puesto = reader["# Puesto"].ToString(),
                Puesto2 = reader["Puesto"].ToString(),
                Caja = reader["Cajas"].ToString(),
                PesoBruto = reader["Peso Bruto"].ToString(),
                Tara = reader["Tara"].ToString(),
                PesoNeto = reader["Peso Neto"].ToString(),
            };
        }

        private Argall_Bd MapToValuePOST(SqlDataReader reader)
        {
            return new Argall_Bd()
            {
                Pallet = reader["# Pallet"].ToString(),
                Bulto = reader["# Bulto"].ToString(),
                Error = reader["Error"].ToString(),
                Observacion = reader["Observaciones"].ToString()
            };
        }

        private Argall_Bd MapToValueLogin(SqlDataReader reader)
        {
            return new Argall_Bd()
            {
                usuario = reader["# usuario"].ToString(),
                nombre = reader["nombre"].ToString(),
                cambiar_clave = reader["cambiar clave"].ToString(),
                idrol = reader["# rol"].ToString(),
                rol = reader["rol"].ToString()
            };
        }

        /*public async Task<Argall_Bd> GetById(int Idbulto)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetValueById", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idbulto", Idbulto));
                    Argall_Bd response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValue(reader);
                        }
                    }

                    return response;
                }
            }
        }*/

    }
}
