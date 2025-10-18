import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';
import { MatSnackBar } from '@angular/material/snack-bar';
@Injectable({
  providedIn: 'root',
})
export class NotificationService {
constructor(private snackBar: MatSnackBar){

}
  showError(message: string, title: string = 'something went wrong!') {{
    Swal.fire({
      title:title,
      text:message,
      icon: 'error',
      customClass: {
        popup: 'showError',
        confirmButton: 'errorButton',
        title: 'errorTitle'
      }
    })
  }}
  showNotification(message:string, title:string) {
    Swal.fire({
      title: title,
      text: message,
      icon: 'success',
    });
  }
  showSuccess(message: string, action?: string){
    this.snackBar.open(message, action, {
        duration: 3000, // Optional: duration in milliseconds
        panelClass:"snackbar-success"
    });
  }
}
