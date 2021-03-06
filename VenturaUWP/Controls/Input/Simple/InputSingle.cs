﻿using System;
using Windows.UI.Xaml;

namespace Ventura.Controls
{

    public class InputSingle : InputBase
    {
        // Statics

        public static DependencyProperty ValueProperty { private set; get; }
        public static DependencyProperty MinValueProperty { private set; get; }
        public static DependencyProperty MaxValueProperty { private set; get; }

        static InputSingle()
        {
            ValueProperty = DependencyProperty.Register(nameof(Value),
                typeof(Single), typeof(InputSingle),
                new PropertyMetadata(default(Single), OnValuePropertyChanged));

            MinValueProperty = DependencyProperty.Register(nameof(MinValue),
                typeof(string), typeof(InputSingle),
                new PropertyMetadata(""));

            MaxValueProperty = DependencyProperty.Register(nameof(MaxValue),
                typeof(string), typeof(InputSingle),
                new PropertyMetadata(""));
        }

        private static void OnValuePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            (obj as InputSingle).OnValuePropertyChanged(args);
        }

        private void OnValuePropertyChanged(DependencyPropertyChangedEventArgs args)
        {
            // Note: this.Value already contains the updated value

            ConvertValue2Text();
        }

        #region DP <-> Property

        public Single Value
        {
            set { SetValue(ValueProperty, value); }
            get { return (Single)GetValue(ValueProperty); }
        }

        public string MinValue
        {
            set { SetValue(MinValueProperty, value); }
            get { return (string)GetValue(MinValueProperty); }
        }

        public string MaxValue
        {
            set { SetValue(MaxValueProperty, value); }
            get { return (string)GetValue(MaxValueProperty); }
        }

        #endregion

        // The rest...

        protected override void ConvertText2Value()
        {
            string completedtext = AutoCompletionTools.AutoCompleteFloatingPoint(this.Text, false);

            completedtext = completedtext.Trim();

            bool valid = Single.TryParse(completedtext, out Single value_candidate);


            if (valid == false)
                return;

            if (Single.TryParse(this.MinValue, out Single minvalue))
                if (value_candidate < minvalue)
                    return;

            if (Single.TryParse(this.MaxValue, out Single maxvalue))
                if (value_candidate > maxvalue)
                    return;

            this.Value = value_candidate;
        }

        protected override void ConvertValue2Text()
        {
            this.Text = this.Value.ToString();
        }

    }

}

