namespace Invoice.Domain.Entities.Base
{
    public class ObjectResponse<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public T Items { get; set; }
    }
}
