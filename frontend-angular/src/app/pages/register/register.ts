import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '../../core/services/auth.service';
import { RegisterRequest } from '../../core/models/user.model';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  nom = '';
  prenom = '';
  email = '';
  telephone = '';
  adresse = '';
  dateNaissance = '';
  password = '';
  confirmPassword = '';

  errorMessage = '';
  successMessage = '';

  onRegister(): void {
    this.errorMessage = '';
    this.successMessage = '';

    if (this.password !== this.confirmPassword) {
      this.errorMessage = 'Les mots de passe ne correspondent pas.';
      return;
    }

    const request: RegisterRequest = {
      nom: this.nom,
      prenom: this.prenom || null,
      email: this.email,
      telephone: this.telephone || null,
      adresse: this.adresse || null,
      dateNaissance: this.dateNaissance || null,
      photoProfil: null,
      password: this.password
    };

    this.authService.register(request).subscribe({
      next: () => {
        this.successMessage = 'Compte créé avec succès.';
        this.router.navigate(['/profile']);
      },
      error: (error) => {
        this.errorMessage =
          typeof error.error === 'string'
            ? error.error
            : 'Impossible de créer le compte.';
      }
    });
  }
}
