namespace MiniEcommerceWebApi.Core.Interface
{
    public interface IDTO
    {
        DateTime CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
        Guid? UpdatedBy { get; set; }
        Guid CreatedBy { get; set; }
    }
}
