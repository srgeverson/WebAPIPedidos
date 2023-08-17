using System.Text;

namespace WebAPIPedidos.Core;
public class WebAPIPedido
{
    public static readonly byte[] SECRET_KEY = Encoding.ASCII.GetBytes("261d901b2208e2306405d03a95541b1cb5047266");
    public static readonly double TEMPO_EM_SEGUNDOS_TOKEN = 1500.0;
}
