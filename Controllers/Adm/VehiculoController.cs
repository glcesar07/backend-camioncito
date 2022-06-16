using elcamioncito_backend.Models;
using elcamioncito_backend.Payback;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Data.SqlClient;

namespace elcamioncito_backend.Controllers.Adm
{
    [Route("cam/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private IConfiguration Configuration;
        private readonly string _connectionString;
        private readonly string _nameProcedure;

        public VehiculoController(IConfiguration _configuration)
        {
            Configuration = _configuration;
            _connectionString = _configuration.GetConnectionString("MainConnection");
            _nameProcedure = "adm.crudVehiculo";
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("get-all")]
        public IActionResult All()
        {
            Responses payback;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    try
                    {
                        conn.Open();
                    }
                    catch (Exception ex)
                    {
                        payback = new Responses(11, ex.ToString());
                        return BadRequest(payback.Result());
                    }

                    using (SqlCommand cmd = new SqlCommand(_nameProcedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("opcion", 4);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataSet setter = new DataSet();

                        try
                        {
                            adapter.Fill(setter, "tabla");

                            if (setter.Tables["tabla"] == null)
                            {
                                payback = new Responses(71, null);
                                return BadRequest(payback.Result());
                            }
                        }
                        catch (Exception ex)
                        {
                            payback = new Responses(12, ex.ToString());
                            return BadRequest(payback.Result());
                        }

                        if (setter.Tables["tabla"].Rows.Count <= 0)
                        {
                            payback = new Responses(23, null);
                            return BadRequest(payback.Result());
                        }

                        return Ok(setter.Tables["tabla"]);
                    }
                }
            }
            catch (Exception ex)
            {
                payback = new Responses(12, ex.ToString());
                return BadRequest(payback.Result());
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("a-register")]
        public IActionResult ARegister(JObject request)
        {
            Responses payback;

            if (string.IsNullOrEmpty(request.GetValue("id").ToString()))
            {
                payback = new Responses(7001, null);
                return BadRequest(payback.Result());
            }

            int id = Int32.Parse(request.GetValue("id").ToString());

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    try
                    {
                        conn.Open();
                    }
                    catch (Exception ex)
                    {
                        payback = new Responses(11, ex.ToString());
                        return BadRequest(payback.Result());
                    }

                    using (SqlCommand cmd = new SqlCommand(_nameProcedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@opcion", 5);
                        cmd.Parameters.AddWithValue("@id", id);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataSet setter = new DataSet();

                        try
                        {
                            adapter.Fill(setter, "tabla");

                            if (setter.Tables["tabla"] == null)
                            {
                                payback = new Responses(71, null);
                                return BadRequest(payback.Result());
                            }
                        }
                        catch (Exception ex)
                        {
                            payback = new Responses(12, ex.ToString());
                            return BadRequest(payback.Result());
                        }

                        if (setter.Tables["tabla"].Rows.Count <= 0)
                        {
                            payback = new Responses(23, null);
                            return BadRequest(payback.Result());
                        }

                        return Ok(setter.Tables["tabla"]);
                    }
                }
            }
            catch (Exception ex)
            {
                payback = new Responses(12, ex.ToString());
                return BadRequest(payback.Result());
            }
        }

        [HttpPost]
        [Route("create")]
        //[Authorize]
        public IActionResult Create(VehiculoModel request)
        {
            Responses payback;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    payback = new Responses(1001, ex.ToString());
                    return BadRequest(payback.Result());
                }

                using (SqlCommand cmd = new SqlCommand(_nameProcedure, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@opcion", 1);
                    cmd.Parameters.AddWithValue("@capacidad", request.capacidad);
                    cmd.Parameters.AddWithValue("@consumoCombustible", request.consumoCombustible);
                    cmd.Parameters.AddWithValue("@placa", request.placa);
                    cmd.Parameters.AddWithValue("@anio", request.anio);
                    cmd.Parameters.AddWithValue("@disponibilidad", 1);
                    cmd.Parameters.AddWithValue("@linea", request.linea);
                    cmd.Parameters.AddWithValue("@modelo", request.modelo);
                    cmd.Parameters.AddWithValue("@tipoVehiculo", request.tipoVehiculo);
                    cmd.Parameters.AddWithValue("@costoDepreciacion", request.costoDepreciacion);
                    cmd.Parameters.AddWithValue("@usuario", "USUARIO001"); // Tendría que enviarse desde el localstorage

                    int result = 0;

                    try
                    {
                        result = cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        payback = new Responses(13, ex.ToString());
                        return BadRequest(payback.Result());
                    }

                    if (result <= 0)
                    {
                        payback = new Responses(23, null);
                        return BadRequest(payback.Result());
                    }

                    payback = new Responses(1, null);
                    return Ok(payback.Result());
                }
            }
        }

        [HttpPost]
        [Route("update")]
        //[Authorize]
        public IActionResult Update(VehiculoModel request)
        {
            Responses payback;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    payback = new Responses(1001, ex.ToString());
                    return BadRequest(payback.Result());
                }

                using (SqlCommand cmd = new SqlCommand(_nameProcedure, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@opcion", 2);
                    cmd.Parameters.AddWithValue("@id", request.id);
                    cmd.Parameters.AddWithValue("@capacidad", request.capacidad);
                    cmd.Parameters.AddWithValue("@consumoCombustible", request.consumoCombustible);
                    cmd.Parameters.AddWithValue("@placa", request.placa);
                    cmd.Parameters.AddWithValue("@anio", request.anio);
                    cmd.Parameters.AddWithValue("@disponibilidad", 1);
                    cmd.Parameters.AddWithValue("@linea", request.linea);
                    cmd.Parameters.AddWithValue("@modelo", request.modelo);
                    cmd.Parameters.AddWithValue("@tipoVehiculo", request.tipoVehiculo);
                    cmd.Parameters.AddWithValue("@costoDepreciacion", request.costoDepreciacion);
                    cmd.Parameters.AddWithValue("@usuario", "USUARIO001");

                    int result = 0;

                    try
                    {
                        result = cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        payback = new Responses(13, ex.ToString());
                        return BadRequest(payback.Result());
                    }

                    if (result <= 0)
                    {
                        payback = new Responses(23, null);
                        return BadRequest(payback.Result());
                    }

                    payback = new Responses(1, null);
                    return Ok(payback.Result());
                }
            }
        }

        [HttpPost]
        [Route("unsubscribe")]
        //[Authorize]
        public IActionResult Delete(JObject request)
        {
            Responses payback;

            if (string.IsNullOrEmpty(request.GetValue("id").ToString()))
            {
                payback = new Responses(7001, null);
                return BadRequest(payback.Result());
            }

            int id = Int32.Parse(request.GetValue("id").ToString());            

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    payback = new Responses(1001, ex.ToString());
                    return BadRequest(payback.Result());
                }

                using (SqlCommand cmd = new SqlCommand(_nameProcedure, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@opcion", 3);
                    cmd.Parameters.AddWithValue("@estado", 0);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@usuario", "USUARIO001");

                    int result = 0;

                    try
                    {
                        result = cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        payback = new Responses(13, ex.ToString());
                        return BadRequest(payback.Result());
                    }

                    if (result <= 0)
                    {
                        payback = new Responses(23, null);
                        return BadRequest(payback.Result());
                    }

                    payback = new Responses(4, null);
                    return Ok(payback.Result());
                }
            }
        }

        [HttpPost]
        [Route("subscribe")]
        //[Authorize]
        public IActionResult Suscribe(JObject request)
        {
            Responses payback;

            if (string.IsNullOrEmpty(request.GetValue("id").ToString()))
            {
                payback = new Responses(7001, null);
                return BadRequest(payback.Result());
            }

            int id = Int32.Parse(request.GetValue("id").ToString());            

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    payback = new Responses(1001, ex.ToString());
                    return BadRequest(payback.Result());
                }

                using (SqlCommand cmd = new SqlCommand(_nameProcedure, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@opcion", 3);
                    cmd.Parameters.AddWithValue("@estado", 1);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@usuario", "USUARIO001");

                    int result = 0;

                    try
                    {
                        result = cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        payback = new Responses(13, ex.ToString());
                        return BadRequest(payback.Result());
                    }

                    if (result <= 0)
                    {
                        payback = new Responses(23, null);
                        return BadRequest(payback.Result());
                    }

                    payback = new Responses(5, null);
                    return Ok(payback.Result());
                }
            }
        }

        [HttpGet]
        [Route("select")]
        //[Authorize]
        public IActionResult GetSelect()
        {
            Responses payback;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    try
                    {
                        conn.Open();
                    }
                    catch (Exception ex)
                    {
                        payback = new Responses(1001, ex.ToString());
                        return BadRequest(payback.Result());
                    }

                    using (SqlCommand cmd = new SqlCommand(_nameProcedure, conn))
                    {
                        JObject data = null;

                        JArray listaObjetos = null;

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@opcion", 6);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader == null)
                        {
                            payback = new Responses(71, null);
                            return BadRequest(payback.Result());
                        }

                        if (!reader.HasRows)
                        {
                            payback = new Responses(23, null);
                            return BadRequest(payback.Result());
                        }

                        listaObjetos = new JArray();

                        while (reader.Read())
                        {
                            data = new JObject();

                            //data.Add("value", Int32.Parse(reader["value"].ToString()));
                            data.Add("value", reader["value"].ToString());
                            data.Add("label", reader["label"].ToString());

                            listaObjetos.Add(data);
                        }

                        return Ok(listaObjetos);

                    }
                }
            }
            catch (Exception ex)
            {
                payback = new Responses(12, ex.ToString());
                return BadRequest(payback.Result());
            }
        }
    }
}
