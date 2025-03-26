using CollectorsCantina.Domain.Enums;

namespace CollectorsCantina.Domain.Objects
{
    public class ApplicationResponse<T>
    {
        public T? Data { get; set; }

        public List<string> Messages { get; set; } = new List<string>();

        public ResponseStatus Status { get; set; }
    }
}
