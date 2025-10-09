namespace LabLINQ.DTOs;
public class OrderWithDetailsDto { public int OrderId { get; set; } public DateTime OrderDate { get; set; } public List<OrderDetailDto> Details { get; set; } = []; }