using PortalTemplate.Areas.Billing.Validation.Attributes;
using PortalTemplate.Areas.Billing.Validation.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Billing.Validation.Managers
{
    /// <summary>
    /// Adds a custom model validation binder, allowing data annotation attributes to be added to models dynamically.
    /// Include the following line in Application_Start of the Global.asax.cs:
    /// ModelValidatorProviders.Providers.Add(new ExtendedDataAnnotationsModelValidatorProvider());
    /// </summary>
    public class ExtendedDataAnnotationsModelValidatorProvider : DataAnnotationsModelValidatorProvider
    {
        internal static DataAnnotationsModelValidationFactory DefaultAttributeFactory = Create;
        internal static Dictionary<Type, DataAnnotationsModelValidationFactory> AttributeFactories = new Dictionary<Type, DataAnnotationsModelValidationFactory>() 
        {
            {
                typeof(RegularExpressionAttribute),
                (metadata, context, attribute) => new RegularExpressionAttributeAdapter(metadata, context, (RegularExpressionAttribute)attribute)
            },
            {
                typeof(RequiredAttribute),
                (metadata, context, attribute) => new RequiredAttributeAdapter(metadata, context, (RequiredAttribute)attribute)
            }
        };

        internal static ModelValidator Create(ModelMetadata metadata, ControllerContext context, ValidationAttribute attribute)
        {
            return new DataAnnotationsModelValidator(metadata, context, attribute);
        }

        protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
        {
            List<ModelValidator> vals = base.GetValidators(metadata, context, attributes).ToList();
            DataAnnotationsModelValidationFactory factory;

            // Inject our new validator.
            if (metadata.ContainerType != null)
            {
                // Check if we have validation for this class name.
                if (ValidationManager.Validators.ContainsKey(metadata.ContainerType.Name))
                {
                    var validator = ValidationManager.Validators[metadata.ContainerType.Name];

                    // Check if we have validation for this property name.
                    if (validator.ContainsKey(metadata.PropertyName))
                    {
                        var property = validator[metadata.PropertyName];

                        // Required attribute.
                        if (property.Required)
                        {
                            ValidationAttribute required;

                            if (metadata.ModelType == typeof(bool))
                            {
                                // For required booleans, enforce true.
                                required = new EnforceTrueAttribute { ErrorMessage = property.ErrorMessage };
                            }
                            else if (metadata.ModelType == typeof(int) || metadata.ModelType == typeof(long) || metadata.ModelType == typeof(double) || metadata.ModelType == typeof(float))
                            {
                                // For required int, long, double, float (dropdownlists), enforce > 0.
                                required = new GreaterThanZeroAttribute() { ErrorMessage = property.ErrorMessage };
                            }
                            else
                            {
                                required = new RequiredAttribute { ErrorMessage = property.ErrorMessage };
                            }

                            if (!AttributeFactories.TryGetValue(required.GetType(), out factory))
                            {
                                factory = DefaultAttributeFactory;
                            }

                            yield return factory(metadata, context, required); 
                        }
                         //Validate File attribute.
                        if (!string.IsNullOrEmpty(property.ValidateFile))
                        {
                            ValidateFileAttribute validate = new ValidateFileAttribute(property.ValidateFile,property.ErrorMessage,property.ValidateExtMsg,property.ValidateSizeMsg);

                            if (!AttributeFactories.TryGetValue(validate.GetType(), out factory))
                            {
                                factory = DefaultAttributeFactory;
                            }

                            yield return factory(metadata, context, validate);
                        }
                        //Check Length attribute.
                        if (!string.IsNullOrEmpty(property.CheckLengthRange))
                        {
                            CheckLengthRangeAttribute validate = new CheckLengthRangeAttribute(property.ErrorMessage);

                            if (!AttributeFactories.TryGetValue(validate.GetType(), out factory))
                            {
                                factory = DefaultAttributeFactory;
                            }

                            yield return factory(metadata, context, validate);
                        }
                    }
                }
            }
        }
    }
}