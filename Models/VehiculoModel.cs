using System.ComponentModel.DataAnnotations;

namespace elcamioncito_backend.Models
{
    public class VehiculoModel
    {
        [Required(ErrorMessage = "Capacidad es requerida.")]
        public string capacidad { get; set; }

        [Required(ErrorMessage = "El consumo del vehículo es requerido.")]
        public string consumoCombustible { get; set; }

        [Required(ErrorMessage = "El numero de placa es requerido.")]
        public string placa { get; set; }

        [Required(ErrorMessage = "El anio es requerido.")]
        public string anio { get; set; }

        [Required(ErrorMessage = "La linea del es requerida.")]
        public int linea { get; set; }

        [Required(ErrorMessage = "El mode es requerido.")]
        public int modelo { get; set; }

        [Required(ErrorMessage = "El tipo de vehículo es requerido.")]
        public int tipoVehiculo { get; set; }

        [Required(ErrorMessage = "El costo de depreciación es requerido.")]
        public decimal costoDepreciacion { get; set; }

#nullable enable        
        public int? id { get; set; }
    }
}
