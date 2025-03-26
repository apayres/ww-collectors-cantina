using CollectorsCantina.Domain.Entities;

namespace CollectorsCantina.Application.UseCases.Collections
{
    internal class CollectionValidator
    {
        private readonly List<string> _errors;

        public List<string> Errors { get { return _errors; } }

        public CollectionValidator()
        {
            _errors = new List<string>();
        }

        public bool ValidateCollection(Collection collection)
        {
            var valid = true;

            if (string.IsNullOrWhiteSpace(collection.Name))
            {
                valid = false;
                _errors.Add("Collection name must have a value.");
            }

            return valid;
        }
    }
}
