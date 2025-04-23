using MiniEcommerceWebApi.Core.Interface;

namespace MiniEcommerceWebApi.Core.Model
{
    public class EntityIdentity : IEntityIdentity
    {
        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as IEntityIdentity;

            if (item == null)
                return false;

            return this.Id.Equals(item.Id);
        }
    }
}
