using Newtonsoft.Json.Linq;

namespace elcamioncito_backend.Payback
{
    public class Responses
    {
        private int val;
        private string valException;

#nullable enable
        public Responses(int value, string? ex)
        {
            val = value;
            valException = string.IsNullOrEmpty(ex) ? value.ToString() : ex;
        }

        public JObject Result()
        {
            dynamic data = new JObject();

            switch (val)
            {
                //success code
                case 1:
                    data.value = 1;
                    data.message = "Proceso realizado con éxito.";
                    data.response = 1;
                    return data;

                case 2:
                    data.value = 2;
                    data.message = "Registro creado con éxito.";
                    data.response = 1;
                    return data;

                case 3:
                    data.value = 3;
                    data.message = "Registro actualizado con éxito.";
                    data.response = 1;
                    return data;

                case 4:
                    data.value = 4;
                    data.message = "Registro dado de baja con éxito.";
                    data.response = 1;
                    return data;

                case 5:
                    data.value = 5;
                    data.message = "Registro dado de alta con éxito.";
                    data.response = 1;
                    return data;

                // database codes 11 - 12
                case 11:
                    data.value = valException;
                    data.message = "No se ha podido conectar con la fuente de datos.";
                    data.response = 11;
                    return data;

                case 12:
                    data.value = valException;
                    data.message = "Problema interno con la fuente de datos.";
                    data.response = 12;
                    return data;

                case 13:
                    data.value = valException;
                    data.message = "No se ha podido almacenar el registro en la fuente de datos.";
                    data.response = 13;
                    return data;

                case 14:
                    data.value = 14;
                    data.message = "No se ha podido eliminar el registro en la fuente de datos.";
                    data.response = 14;
                    return data;

                // empty or null codes 20 -29
                case 21:
                    data.value = 21;
                    data.message = "El valor del identificador no puede ir vacío.";
                    data.response = 21;
                    return data;

                case 22:
                    data.value = 22;
                    data.message = "El valor inicial no puede ir vacío.";
                    data.response = 22;
                    return data;

                case 23:
                    data.value = 23;
                    data.message = "No se han encontrado datos relacionados con la búsqueda.";
                    data.response = 23;
                    return data;

                case 24:
                    data.value = 24;
                    data.message = "Uno o más parámetros requeridos no contienen valor.";
                    data.response = 24;
                    return data;

                case 25:
                    data.value = 25;
                    data.message = "Parámetro principal es requerido.";
                    data.response = 25;
                    return data;

                case 26:
                    data.value = 26;
                    data.message = "Parámetros principales son requeridos.";
                    data.response = 26;
                    return data;

                case 27:
                    data.value = 27;
                    data.message = "Parámetros principales no pueden ir vacíos.";
                    data.response = 27;
                    return data;

                //null codes 70 - 79
                case 71:
                    data.value = 71;
                    data.message = "No existen datos relacionados con la búsqueda.";
                    data.response = 71;
                    return data;

                case 72:
                    data.value = 72;
                    data.message = "Uno o mas campos requeridos no fueron enviados.";
                    data.response = 72;
                    return data;

                default:
                    data.value = 0;
                    data.message = "Proceso no realizado.";
                    data.response = 0;
                    return data;
                    //break;
            }
        }
    }
}
