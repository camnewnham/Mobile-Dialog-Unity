using UnityEngine;
using System.Collections;
using System;

namespace pingak9
{
    public class NativeDialog
    {
        public NativeDialog() { }

        public static void OpenDialog(string title, string message, string ok = "Ok", Action okAction = null)
        {
#if UNITY_EDITOR
            if (UnityEditor.EditorUtility.DisplayDialog(title, message, ok))
            {
                okAction?.Invoke();
            }
#else
            MobileDialogInfo.Create(title, message, ok, okAction);
#endif
        }
        public static void OpenDialog(string title, string message, string yes, string no, Action yesAction = null, Action noAction = null)
        {
#if UNITY_EDITOR
            if (UnityEditor.EditorUtility.DisplayDialog(title, message, yes,no))
            {
                yesAction?.Invoke();
            }
            else
            {
                noAction?.Invoke();
            } 
#else
            MobileDialogConfirm.Create(title, message, yes, no, yesAction, noAction);
#endif
        }
        public static void OpenDialog(string title, string message, string accept, string neutral, string decline, Action acceptAction = null, Action neutralAction = null, Action declineAction = null)
        {
#if UNITY_EDITOR
            int res = UnityEditor.EditorUtility.DisplayDialogComplex(title, message, accept, decline, neutral);
            switch (res)
            {
                case 0:
                    acceptAction?.Invoke();
                    return;
                case 1:
                    declineAction?.Invoke();
                    return;
                case 2:
                    neutralAction?.Invoke();
                    return;
            }
#else
            MobileDialogNeutral.Create(title, message, accept, neutral, decline, acceptAction, neutralAction, declineAction);
#endif
        }
        public static void OpenDatePicker(int year , int month, int day, Action<DateTime> onChange = null, Action<DateTime> onClose = null)
        {
            MobileDateTimePicker.CreateDate(year, month, day, onChange, onClose);
        }
        public static void OpenTimePicker(Action<DateTime> onChange = null, Action<DateTime> onClose = null)
        {
            MobileDateTimePicker.CreateTime(onChange, onClose);
        }
    }
}