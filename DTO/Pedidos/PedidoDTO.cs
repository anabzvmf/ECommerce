namespace DTO.Pedidos;

public class PedidoDTO
{
    public string Numero { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public decimal Valor { get; set; }
    public string Status { get; set; } = string.Empty;
}