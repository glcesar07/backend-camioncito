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
    public class LabelController : ControllerBase
    {
        private IConfiguration Configuration;
        private readonly string _connectionString;        

        public LabelController(IConfiguration _configuration)
        {
            Configuration = _configuration;
            _connectionString = _configuration.GetConnectionString("MainConnection");            
        }

        [HttpGet]
        [Route("linea")]
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
                        payback = new Responses(11, ex.ToString());
                        return BadRequest(payback.Result());
                    }

                    using (SqlCommand cmd = new SqlCommand("adm.crudLinea", conn))
                    {
                        JObject data = null;

                        JArray listaObjetos = null;

                        cmd.CommandType = CommandType.StoredProcedure;                        

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

                            
                            data.Add("value", Int32.Parse(reader["value"].ToString()));
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

        [HttpGet]
        [Route("modelo")]
        //[Authorize]
        public IActionResult Profesion()
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

                    using (SqlCommand cmd = new SqlCommand("adm.crudModelo", conn))
                    {
                        JObject data = null;

                        JArray listaObjetos = null;

                        cmd.CommandType = CommandType.StoredProcedure;                        

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

                            
                            data.Add("value", Int32.Parse(reader["value"].ToString()));
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

        [HttpGet]
        [Route("tipo-vehiculo")]
        //[Authorize]
        public IActionResult Pais()
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

                    using (SqlCommand cmd = new SqlCommand("adm.crudTipoVehiculo", conn))
                    {
                        JObject data = null;

                        JArray listaObjetos = null;

                        cmd.CommandType = CommandType.StoredProcedure;                        

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

                            
                            data.Add("value", Int32.Parse(reader["value"].ToString()));
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
