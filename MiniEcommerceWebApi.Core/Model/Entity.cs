using MiniEcommerceWebApi.Core.Interface;

using System.ComponentModel.DataAnnotations;

namespace MiniEcommerceWebApi.Core.Model
{
    public class Entity : EntityIdentity, IEntity
    {

        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        //   public string? CreatedFrom { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }


        public override bool Equals(object obj)
        {
            var item = obj as IEntityIdentity;

            if (item == null)
                return false;

            return this.Id.Equals(item.Id);
        }
    }
}
