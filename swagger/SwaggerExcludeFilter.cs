using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace shop_admin_api.swagger
{
	public class SwaggerExcludeFilter : ISchemaFilter
	{
		public void Apply(OpenApiSchema schema, SchemaFilterContext context)
		{
			if (schema?.Properties == null)
			{
				return;
			}

			var excludedProperties = context.Type.GetProperties().Where(t => t.GetCustomAttribute<SwaggerIgnorePropertyAttribute>() != null);

			foreach (var excludedProperty in excludedProperties)
			{
				var propertyToRemove = schema.Properties.Keys.SingleOrDefault(x => string.Equals(x, excludedProperty.Name, StringComparison.OrdinalIgnoreCase));

				if (propertyToRemove != null)
				{
					schema.Properties.Remove(propertyToRemove);
				}
			}
		}
	}

	[AttributeUsage(AttributeTargets.Property)]
	public class SwaggerIgnorePropertyAttribute : Attribute
	{
	}
}
