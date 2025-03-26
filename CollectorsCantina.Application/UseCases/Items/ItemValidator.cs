using CollectorsCantina.Domain.Entities;

namespace CollectorsCantina.Application.UseCases.Items
{
    internal class ItemValidator
    {
        private readonly List<string> _errors;

        public List<string> Errors { get { return _errors; } }

        public ItemValidator()
        {
            _errors = new List<string>();
        }

        public bool ValidateItem(Item item)
        {
            var valid = true;

            if (string.IsNullOrWhiteSpace(item.Name))
            {
                valid = false;
                _errors.Add("Item name must have a value.");
            }

            //if (item.Attributes != null && item.Attributes.Any())
            //{
            //    if (item.Attributes.Any(x => string.IsNullOrWhiteSpace(x.Name)))
            //    {

            //        valid = false;
            //        _errors.Add("One or more attributes are missing the name.");
            //    }
            //}

            return valid;
        }
    }
}
