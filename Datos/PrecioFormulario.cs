using System.Globalization;

namespace PracticasLDP3.Datos;

public static class PrecioFormulario
{
    // Acepta coma o punto decimal, sin depender del idioma de Windows.
    // No acepta separadores de miles ni notación científica.
    public static bool IntentarLeer(string? texto, out double precio)
    {
        string valor = (texto ?? "").Trim().Replace(',', '.');
        return double.TryParse(valor, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign,
            CultureInfo.InvariantCulture, out precio) && double.IsFinite(precio) && precio >= 0;
    }
}
