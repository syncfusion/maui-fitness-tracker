using Syncfusion.Maui.Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker
{
    class EditorTextChangedBehavior : Behavior<CustomStyleEditor>
    {
        protected override void OnAttachedTo(CustomStyleEditor bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEditorTextChanged;
        }

        protected override void OnDetachingFrom(CustomStyleEditor bindable)
        {
            bindable.TextChanged -= OnEditorTextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void OnEditorTextChanged(object? sender, TextChangedEventArgs e)
        {
            var viewModel = (sender as CustomStyleEditor)!.BindingContext as AiAssistViewModel;

            // Disable send icon when Stop responding is loading.
            viewModel!.EnableSendIcon = viewModel.enableNewChatIcon && !string.IsNullOrEmpty(e.NewTextValue);
        }
    }
}
