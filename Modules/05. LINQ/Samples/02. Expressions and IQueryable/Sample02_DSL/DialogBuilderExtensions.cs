using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace InternalDSL
{
    public static class DialogBuilderExtensions
    {
        public static ITitledDialogBuilder<TModel> WithFieldsFromModel<TModel>(
            this ITitledDialogBuilder<TModel> dialogBuilder) where TModel : class
        {
            var type = typeof(TModel);

            foreach (var property in
                type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                dialogBuilder.WithField(GetLabel(property), property.Name);
            }

            return dialogBuilder;
        }

        public static ITitledDialogBuilder<TModel> WithField<TModel>(
            this ITitledDialogBuilder<TModel> dialogBuilder,
            Expression<Func<TModel, object>> expression,
            string label = null)
                where TModel : class
        {
            var property = expression.Body switch
            {
                UnaryExpression ue
                    when (ue.NodeType == ExpressionType.Convert)
                        => ((MemberExpression)ue.Operand).Member as PropertyInfo,
                MemberExpression me => me.Member as PropertyInfo,
                _ => null,
            };

            if (property != null)
            {
                dialogBuilder.WithField(GetLabel(property, label), property.Name);
            }

            return dialogBuilder;
        }

        private static string GetLabel(PropertyInfo property, string expliciteLabelName = null)
        {
            if (!string.IsNullOrWhiteSpace(expliciteLabelName))
                return expliciteLabelName;

            var displayName = property.GetCustomAttribute<DisplayNameAttribute>(false);

            return displayName?.DisplayName ?? property.Name;
        }
    }
}
