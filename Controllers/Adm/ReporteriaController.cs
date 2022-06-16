using elcamioncito_backend.Models;
using elcamioncito_backend.Payback;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace elcamioncito_backend.Controllers.Adm
{
    [Route("cam/[controller]")]
    [ApiController]
    public class ReporteriaController : ControllerBase
    {
        private IConfiguration Configuration;
        private readonly string _connectionString;
        private readonly string _nameProcedure;

        public ReporteriaController(IConfiguration _configuration)
        {
            Configuration = _configuration;
            _connectionString = _configuration.GetConnectionString("MainConnection");
            _nameProcedure = "adm.Reporteria";
        }

        [HttpPost]
        [Route("available-vehicle")]
        public IActionResult AvailableVehicle(ReporteriaModel request)
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
                        cmd.Parameters.AddWithValue("@opcion", 1);
                        cmd.Parameters.AddWithValue("@fechaInicial", request.fechaInicial);
                        cmd.Parameters.AddWithValue("@fechaFinal", request.fechaFinal);

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

        [HttpGet]
        [Route("all-available-vehicle")]
        public IActionResult AllAvailableVehicle()
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
                        cmd.Parameters.AddWithValue("@opcion", 2);                        

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
    }
}
