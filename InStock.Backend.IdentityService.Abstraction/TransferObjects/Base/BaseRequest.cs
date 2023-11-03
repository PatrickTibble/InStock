using System.ComponentModel.DataAnnotations;

namespace InStock.Backend.IdentityService.Abstraction.TransferObjects.Base
{
    public abstract class BaseRequest : IValidatableObject
    {
        private IList<ValidationResult>? _validationResults;
        public virtual bool IsValid => Validate();

        public virtual IList<string?>? ValidationErrors
        {
            get
            {
                if (_validationResults == null || _validationResults.Count == 0)
                {
                    return null;
                }

                return _validationResults.Select(v => v.ErrorMessage).ToList();
            }
        }

        public virtual bool Validate()
        {
            var context = new ValidationContext(this, null, null);
            return Validator.TryValidateObject(this, context, _validationResults, true);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            Validator.TryValidateObject(this, validationContext, _validationResults, true);
            return _validationResults!;
        }
    }
}