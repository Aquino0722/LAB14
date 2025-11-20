namespace LabLINQ.DTOs;

public class OrderDetailsDto
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    // La lista ahora es del tipo correcto y específico
    public List<OrderDetailProductDto> Products { get; set; } = [];
}