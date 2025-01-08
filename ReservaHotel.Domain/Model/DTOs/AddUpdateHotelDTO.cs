namespace ReservaHotel.Domain.Model.DTOs
{
    public class AddUpdateHotelDTO
    {
        public string Nome { get; set; }
        public int Estrelas { get; set; }
        public int Andares { get; set; }
        public Guid Id { get; set; }

    }
}
