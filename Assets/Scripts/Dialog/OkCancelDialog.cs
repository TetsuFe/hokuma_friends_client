// OkCancelDialog.cs

using System;
using UnityEngine;

namespace Dialog
{
    public class OkCancelDialog : MonoBehaviour
    {
        public enum DialogResult
        {
            Ok,
            Cancel,
        }

        // ダイアログが操作されたときに発生するイベント
        public Action<DialogResult> FixDialog { get; set; }

        // OKボタンが押されたとき
        public void OnOk()
        {
            this.FixDialog?.Invoke(DialogResult.Ok);
            Destroy(this.gameObject);
        }

        // Cancelボタンが押されたとき
        public void OnCancel()
        {
            // イベント通知先があれば通知してダイアログを破棄してしまう
            this.FixDialog?.Invoke(DialogResult.Cancel);
            Destroy(this.gameObject);
        }
    }
}