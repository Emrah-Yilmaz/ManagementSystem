namespace ManagementSystem.Domain.Models.Args.User
{
    public class UpdateAddressArgs : CreateAddressArgs
    {
        public int Id { get; set; }

        internal Entities.Address Modify(Entities.Address entity)
        {
            entity.UserId = UserId;
            entity.CityId = CityId;
            entity.DistrictId = DistrictId;
            entity.QuerterId = QuarterId;
            entity.Description = Description;
            return entity;
        }
    }
}
