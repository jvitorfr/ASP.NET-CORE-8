using App.Domain.Entities;


namespace App.Domain.Entities
{
    public  class Queue : Base
    {
        public DateTime Sended { get; set; } = DateTime.Now;
        public DateTime Requested { get; set; } = DateTime.Now;
        public string Label { get; set; } = "Empty Label";
        public string Message { get; set; } = "Empty Message";
        public string From { get; set; }
        public string ConsumedBy { get; set; }
    }
}
