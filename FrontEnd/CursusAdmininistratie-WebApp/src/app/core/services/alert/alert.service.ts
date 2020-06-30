import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';


@Injectable({
  providedIn: 'root'
})
export class AlertService {
  durationInSeconds = 5 * 1000;

  constructor(private snackbar: MatSnackBar) { }

    successMessage(message) {
      this.snackbar.open(message, 'X', {
        duration: this.durationInSeconds,
        panelClass: ['green-snackbar']
      });
    }

    errorMessage(message) {
      this.snackbar.open(message, 'X' ,{
        duration: this.durationInSeconds,
        panelClass: ['red-snackbar']
      });
    }

    infoMessage(message) {
      this.snackbar.open(message, 'X' ,{
        duration: this.durationInSeconds,
        panelClass: ['blue-snackbar']
      });
    }

    warningMessage(message) {
      this.snackbar.open(message, 'X' ,{
        duration: this.durationInSeconds,
        panelClass: ['orange-snackbar']
      });
    }

}
