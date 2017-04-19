using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AndroidApp
{
    class TimePickerFragment : DialogFragment
    {
        private readonly Context context;
        private int hour;
        private int minute;
        private readonly TimePickerDialog.IOnTimeSetListener listener;

        public TimePickerFragment(Context context, int hour, int minute, TimePickerDialog.IOnTimeSetListener listener)
        {
            this.context = context;
            this.hour = hour;
            this.minute = minute;
            this.listener = listener;
        }

        public override Dialog OnCreateDialog(Bundle savedState)
        {
            var dialog = new TimePickerDialog(context, listener, hour, minute, false);
            return dialog;
        }
    }
}